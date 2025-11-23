using System.Buffers.Binary;
using System.Drawing.Drawing2D;

namespace VectorCubeAnimationEditor
{
    internal class RoundRect : Primitive
    {
        private Int16 x0;
        private Int16 y0;
        private Int16 w;
        private Int16 h;
        private Int16 radius;
        private UInt16 color;

        public Int16 X0
        {
            get { return x0; }
            set { x0 = value; }
        }

        public Int16 Y0
        {
            get { return y0; }
            set { y0 = value; }
        }

        public Int16 W
        {
            get { return w; }
            set { w = value; }
        }

        public Int16 H
        {
            get { return h; }
            set { h = value; }
        }

        public Int16 Radius
        {
            get { return radius; }
            set { radius = (value < 0) ? (Int16)0 : (value > Math.Min(W / 2, H / 2)) ? (Int16)Math.Min(W / 2, H / 2) : value; }
        }

        public override UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public RoundRect()
        {
            X0 = AnimationConstants.DEFAULT_PRIMITIVE_LEFT;
            Y0 = AnimationConstants.DEFAULT_PRIMITIVE_TOP;
            W = AnimationConstants.DEFAULT_PRIMITIVE_SIZE;
            H = AnimationConstants.DEFAULT_PRIMITIVE_SIZE;
            Radius = 0;
            Color = 0;
        }

        public RoundRect(RoundRect rectangle)
        {
            X0 = rectangle.X0;
            Y0 = rectangle.Y0;
            W = rectangle.W;
            H = rectangle.H;
            Radius = rectangle.Radius;
            Color = rectangle.Color;
        }

        public override Primitive Clone()
        {
            return new RoundRect(this);
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {
            GraphicsPath path = GetScreenRectanglePath();
            Color drawColor = Utility.GetColorFromUIint16(Color);
            Brush brush = new SolidBrush(drawColor);
            Pen pen = new Pen(drawColor.ColorToInverse());
            pen.DashStyle = DashStyle.Dash;
            e.FillPath(brush, path);
            if (isHighlighted) e.DrawPath(pen, path);
        }

        public override void Move(Point offset)
        {
            X0 += (Int16)offset.X;
            Y0 += (Int16)offset.Y;
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AnimationConstants._RoundRect);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), X0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), W);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), H);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Radius);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Color);
            bytePosition += 4;
        }

        public override void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            X0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            Y0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            W = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            H = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            Radius = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            Color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 4;
        }

        #region Screen mapped methods

        public Point ScreenCen
        {
            get { return new Point(ScreenX0 + (ScreenW / 2), ScreenY0 + (ScreenH / 2)); }
        }

        public Int16 ScreenX0
        {
            get { return (Int16)(X0 * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenY0
        {
            get { return (Int16)(Y0 * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenW
        {
            get { return (Int16)(W * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenH
        {
            get { return (Int16)(H * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenRadius
        {
            get { return (Int16)(Radius * AnimationConstants._ScaleFactor); }
        }

        public Point ScreenRectangleCenter
        {
            get { return new Point((W / 2) * AnimationConstants._ScaleFactor, (H / 2) * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenHalfDiagonal
        {
            get { return (Int16)(Math.Sqrt(Math.Pow(ScreenRectangleCenter.X + 1, 2) + Math.Pow(ScreenRectangleCenter.Y + 1, 2))); }
        }

        #region Mouse handling

        private Point MouseLocation = new Point(0, 0);
        private bool isMouseUp = true;
        private bool isMoving = false;
        private int SelectedSide = -1;
        private int SelectedVertex = -1;

        public override void MouseDown(Point point)
        {
            MouseLocation = point;
            isMouseUp = false;
            SelectedVertex = GetSelectedVertex(point, 2);

            if (SelectedVertex < 0)
            {
                SelectedSide = GetSelectedSide(MouseLocation);
                if (GetSelectedSide(MouseLocation) < 0)
                {
                    if (IsPointNearCenter(MouseLocation))
                    {
                        isMoving = true;
                    }
                }
            }

        }

        public override bool MouseMove(Point point, PictureBox pctbxCanvas)
        {
            Point mouseDelta = new Point(point.X - MouseLocation.X, point.Y - MouseLocation.Y);
            Point unscaledMouseDelta = new Point((int)Math.Floor((double)mouseDelta.X / AnimationConstants._ScaleFactor),
                                                (int)Math.Floor((double)mouseDelta.Y / AnimationConstants._ScaleFactor));

            if (isMouseUp)
            {
                if (IsPointNearCenter(point)) pctbxCanvas.Cursor = Cursors.Hand;
                else if (GetSelectedSide(point) > -1) pctbxCanvas.Cursor = Cursors.Hand;
                else if (GetSelectedVertex(point, 2) > -1) pctbxCanvas.Cursor = Cursors.Hand;
                else pctbxCanvas.Cursor = Cursors.Arrow;
                return false;
            }

            if (SelectedVertex > -1)
            {
                Point screenCenter = ScreenCen;
                int offsetX = point.X - screenCenter.X;
                int offsetY = point.Y - screenCenter.Y;
                int offsetR = (Int16)Math.Sqrt(Math.Pow(offsetX, 2) + Math.Pow(offsetY, 2));
                Radius = (Int16)(ScreenHalfDiagonal - offsetR);
            }
            if (SelectedSide > -1)
            {
                switch (SelectedSide)
                {
                    case 0:
                        Y0 += (Int16)unscaledMouseDelta.Y;
                        H -= (Int16)unscaledMouseDelta.Y;
                        break;
                    case 1:
                        W += (Int16)unscaledMouseDelta.X;
                        break;
                    case 2:
                        H += (Int16)unscaledMouseDelta.Y;
                        break;
                    case 3:
                        X0 += (Int16)unscaledMouseDelta.X;
                        W -= (Int16)unscaledMouseDelta.X;
                        break;
                }
            }
            if (isMoving) Move(unscaledMouseDelta);

            MouseLocation.X += unscaledMouseDelta.X * AnimationConstants._ScaleFactor;
            MouseLocation.Y += unscaledMouseDelta.Y * AnimationConstants._ScaleFactor;

            return true;
        }

        public override void MouseUp()
        {
            isMouseUp = true;
            isMoving = false;
            SelectedSide = -1;
            SelectedVertex = -1;
        }

        #endregion

        public bool IsPointNearCenter(Point point)
        {
            int margin = 4;
            return Utility.ArePointsWithinMargin(ScreenCen, point, margin);
        }

        public Point[] GetVertices()
        {
            Point bottomRight = new Point(X0 + W, Y0 + H);
            Point bottomLeft = new Point(X0, Y0 + H);
            Point topLeft = new Point(X0, Y0);
            Point topRight = new Point(X0 + W, Y0);

            Point[] vertices = new Point[] { bottomRight, bottomLeft, topLeft, topRight };

            return vertices;
        }

        public Point[] GetScreenVertices()
        {
            Point[] vertices = GetVertices();

            for (int index = 0; index < vertices.Length; index++)
            {
                vertices[index].X *= AnimationConstants._ScaleFactor;
                vertices[index].Y *= AnimationConstants._ScaleFactor;
            }

            return vertices;
        }

        public Point[] GetVerticesMinusRadius()
        {
            Point[] vertices = GetVertices();
            vertices[0].X -= Radius;
            vertices[0].Y -= Radius;
            vertices[1].X += Radius;
            vertices[1].Y -= Radius;
            vertices[2].X += Radius;
            vertices[2].Y += Radius;
            vertices[3].X -= Radius;
            vertices[3].Y += Radius;

            return vertices;
        }

        public Point[] GetScreenVerticesMinusRadius()
        {
            Point[] vertices = GetVerticesMinusRadius();

            for (int index = 0; index < vertices.Length; index++)
            {
                vertices[index].X *= AnimationConstants._ScaleFactor;
                vertices[index].Y *= AnimationConstants._ScaleFactor;
            }

            return vertices;
        }

        public Point[] GetSides()
        {
            Point[] vertices = GetVertices();
            Point l00 = new Point(X0 + Radius, Y0);
            Point l01 = new Point(X0 + W - Radius, Y0);
            Point l10 = new Point(X0 + W, Y0 + Radius);
            Point l11 = new Point(X0 + W, Y0 + H - Radius);
            Point l20 = new Point(X0 + W - Radius, Y0 + H);
            Point l21 = new Point(X0 + Radius, Y0 + H);
            Point l30 = new Point(X0, Y0 + H - Radius);
            Point l31 = new Point(X0, Y0 + Radius);

            Point[] lines = { l00, l01, l10, l11, l20, l21, l30, l31 };
            return lines;
        }

        public Point[] GetScreenSides()
        {
            Point[] lines = GetSides();

            for (int index = 0; index < lines.Length; index++)
            {
                lines[index].X *= AnimationConstants._ScaleFactor;
                lines[index].Y *= AnimationConstants._ScaleFactor;
            }

            return lines;
        }

        public int GetSelectedVertex(Point point, int margin)
        {
            Point[] vertices = GetScreenVerticesMinusRadius();
            int selectedVertex = -1;
            for (int index = 0; index < vertices.Length; index++)
            {
                if (Utility.IsPointOnRadiusClamped(point, vertices[index], ScreenRadius, margin * AnimationConstants._ScaleFactor, index * 90, index * 90 + 90))
                {
                    selectedVertex = index;
                }

            }
            return selectedVertex;
        }

        public int GetSelectedSide(Point point)
        {
            Point[] lines = GetScreenSides();
            int margin = 4;
            int selectedLine = -1;
            for (int index = 0; index < lines.Length; index += 2)
            {
                if (Utility.DistanceFromLine(lines[index], lines[index + 1], point) < margin)
                {
                    selectedLine = index / 2;
                }

            }
            return selectedLine;
        }

        public GraphicsPath GetScreenRectanglePath()
        {
            System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(ScreenX0, ScreenY0, ScreenW, ScreenH);
            int diameter = 2 * ScreenRadius;
            Size size = new Size(diameter, diameter);
            System.Drawing.Rectangle arc = new System.Drawing.Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (Radius == 0)
            {
                path.AddRectangle(bounds);
            }
            else
            {
                path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
                path.AddArc(bounds.X + (bounds.Width - diameter) - 1, bounds.Y, diameter + 1, diameter + 8, 270, 90);
                path.AddArc(bounds.X + (bounds.Width - diameter) - 7, bounds.Y + (bounds.Height - diameter) - 7, diameter + 7, diameter + 7, 0, 90);
                path.AddArc(bounds.X, bounds.Y + (bounds.Height - diameter) - 1, diameter + 8, diameter + 1, 90, 90);
                path.CloseFigure();
            }
            return path;
        }

        #endregion

    }
}
