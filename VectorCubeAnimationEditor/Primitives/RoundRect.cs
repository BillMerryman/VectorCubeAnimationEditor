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
            GraphicsPath path = GetRectanglePath();
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

        public Point[] GetVertices()
        {
            Point topLeft = new Point(X0, Y0);
            Point topRight = new Point(X0 + W, Y0);
            Point bottomRight = new Point(X0 + W, Y0 + H);
            Point bottomLeft = new Point(X0, Y0 + H);

            Point[] vertices = new Point[] { bottomRight, bottomLeft, topLeft, topRight };

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
        private bool isResizing = false;
        private int isVertexMoving = -1;

        public override void MouseDown(Point point)
        {
            MouseLocation = point;
            isMouseUp = false;
            isVertexMoving = SelectedVertex(point, 2);

            if (isVertexMoving < 0)
            {
                if (IsPointNearBottomRight(MouseLocation, 2))
                {
                    isResizing = true;
                }
                else if (IsPointInside(MouseLocation))
                {
                    isMoving = true;
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
                if (SelectedVertex(point, 2) > -1) pctbxCanvas.Cursor = Cursors.Hand;
                else pctbxCanvas.Cursor = Cursors.Arrow;
            }

            bool result = false;
            if (isVertexMoving > -1)
            {
                Point screenCenter = ScreenCen;
                int offsetX = point.X - screenCenter.X;
                int offsetY = point.Y - screenCenter.Y;
                int offsetR = (Int16)Math.Sqrt(Math.Pow(offsetX, 2) + Math.Pow(offsetY, 2));
                Radius = (Int16)(ScreenHalfDiagonal - offsetR);
                result = true;
            }
            if (isResizing)
            {
                if (W + unscaledMouseDelta.X > 0) W += (Int16)unscaledMouseDelta.X;
                if (H + unscaledMouseDelta.Y > 0) H += (Int16)unscaledMouseDelta.Y;
                result = true;
            }
            if (isMoving)
            {
                Move(unscaledMouseDelta);
                result = true;
            }
            MouseLocation.X += unscaledMouseDelta.X * AnimationConstants._ScaleFactor;
            MouseLocation.Y += unscaledMouseDelta.Y * AnimationConstants._ScaleFactor;
            return result;
        }

        public override void MouseUp()
        {
            isMouseUp = true;
            isMoving = false;
            isResizing = false;
            isVertexMoving = -1;
        }

        #endregion

        public Point[] ScreenGetVertices()
        {
            Point[] vertices = GetVertices();

            for(int index = 0; index < vertices.Length; index++)
            {
                vertices[index].X *= AnimationConstants._ScaleFactor;
                vertices[index].Y *= AnimationConstants._ScaleFactor;
            }

            return vertices;
        }

        public Point[] ScreenGetVerticesMinusRadius()
        {
            Point[] vertices = GetVerticesMinusRadius();

            for (int index = 0; index < vertices.Length; index++)
            {
                vertices[index].X *= AnimationConstants._ScaleFactor;
                vertices[index].Y *= AnimationConstants._ScaleFactor;
            }

            return vertices;
        }

        public bool IsPointNearPerimeterLine(Point point, Double margin)
        {
            UInt16 halfWidth = (UInt16)(W / 2);
            UInt16 halfHeight = (UInt16)(H / 2);
            UInt16 screenHalfWidth = (UInt16)(halfWidth * AnimationConstants._ScaleFactor);
            UInt16 screenHalfHeight = (UInt16)(halfHeight * AnimationConstants._ScaleFactor);
            UInt16 screenMidX = (UInt16)(X0 * AnimationConstants._ScaleFactor + screenHalfWidth);
            UInt16 screenMidY = (UInt16)(Y0 * AnimationConstants._ScaleFactor + screenHalfHeight);
            int xInRange = Math.Abs(point.X - screenMidX);
            int yInRange = Math.Abs(point.Y - screenMidY);
            if ((Math.Abs(point.Y - ScreenY0)) < margin && xInRange < halfWidth) return true;
            if ((Math.Abs(point.Y - (ScreenY0 + ScreenH))) < margin && xInRange < halfWidth) return true;
            if ((Math.Abs(point.X - ScreenX0)) < margin && yInRange < halfHeight) return true;
            if ((Math.Abs(point.X - (ScreenX0 + ScreenW))) < margin && yInRange < halfHeight) return true;
            return false;
        }

        public int SelectedVertex(Point point, int margin)
        {
            Point[] vertices = ScreenGetVerticesMinusRadius();
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

        public bool IsPointNearBottomRight(Point point, Double margin)
        {
            Point bottomRight = new Point(X0 + W, Y0 + H);

            return Math.Abs(point.X - (bottomRight.X * AnimationConstants._ScaleFactor)) < margin * AnimationConstants._ScaleFactor
                    && Math.Abs(point.Y - (bottomRight.Y * AnimationConstants._ScaleFactor)) < margin * AnimationConstants._ScaleFactor;
        }

        public bool IsPointInside(Point point)
        {
            GraphicsPath path = GetRectanglePath();
            return path.IsVisible(point);
        }

        public GraphicsPath GetRectanglePath()
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
