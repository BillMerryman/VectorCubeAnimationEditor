using System.Buffers.Binary;
using System.Drawing.Drawing2D;

namespace VectorCubeAnimationEditor
{
    internal class RotatedRect : Primitive
    {
        private Int16 cenX;
        private Int16 cenY;
        private Int16 w;
        private Int16 h;
        private Int16 angleDeg;
        private UInt16 color;

        public Int16 CenX
        {
            get { return cenX; }
            set { cenX = value; }
        }

        public Int16 CenY
        {
            get { return cenY; }
            set { cenY = value; }
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

        public Int16 AngleDeg
        {
            get { return angleDeg; }
            set { angleDeg = (value < 0) ? (Int16)(360 + (value % 360)) : (Int16)(value % 360); }
        }

        public override UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public RotatedRect()
        {
            CenX = AnimationConstants.SCREEN_CENTER_X;
            CenY = AnimationConstants.SCREEN_CENTER_Y;
            W = AnimationConstants.DEFAULT_PRIMITIVE_SIZE;
            H = AnimationConstants.DEFAULT_PRIMITIVE_SIZE;
            AngleDeg = 0;
            Color = 0;
        }

        public RotatedRect(RotatedRect rotatingRectangle)
        {
            CenX = rotatingRectangle.CenX;
            CenY = rotatingRectangle.CenY;
            W = rotatingRectangle.W;
            H = rotatingRectangle.H;
            AngleDeg = rotatingRectangle.AngleDeg;
            Color = rotatingRectangle.Color;
        }

        public override Primitive Clone()
        {
            return new RotatedRect(this);
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {
            Color drawColor = Utility.GetColorFromUIint16(Color);
            Brush brush = new SolidBrush(drawColor);
            Pen pen = new Pen(drawColor.ColorToInverse());
            pen.DashStyle = DashStyle.Dash;

            // Split rectangle into two triangles (diagonal from top-left to bottom-right)
            Point[] corners = ScreenGetVertices();
            Point[] triangle1 = new Point[] { corners[0], corners[1], corners[2] };
            Point[] triangle2 = new Point[] { corners[1], corners[2], corners[3] };
            e.FillPolygon(brush, triangle1);
            e.FillPolygon(brush, triangle2);
            if (isHighlighted)
            {
                e.DrawEllipse(pen,
                                (float)(ScreenCenX - ScreenHalfDiagonal),
                                (float)(ScreenCenY - ScreenHalfDiagonal),
                                (float)ScreenHalfDiagonal * 2,
                                (float)ScreenHalfDiagonal * 2);
            }
        }

        #region Mouse handling

        private Point MouseLocation = new Point(0, 0);
        private bool isMouseUp = true;
        private bool isMoving = false;
        private bool isResizing = false;
        private bool isRotating = false;
        private Int16 offsetAngle = 0;
        private int isVertexMoving = -1;

        public override void MouseDown(Point point)
        {
            MouseLocation = point;
            isMouseUp = false;
            isMoving = IsPointNearCenter(MouseLocation, 2);
            isVertexMoving = IsPointNearVertex(MouseLocation, 2);
            if (isVertexMoving < 0)
            {
                isRotating = IsPointOnRadius(MouseLocation, 2);
                if (isRotating) offsetAngle = (Int16)(AngleDeg - GetAngle(MouseLocation));
            }
            else
            {
                isResizing = true;
            }
        }

        public override bool MouseMove(Point point, PictureBox pctbxCanvas)
        {
            Point mouseDelta = new Point(point.X - MouseLocation.X, point.Y - MouseLocation.Y);
            Point unscaledMouseDelta = new Point((int)Math.Floor((double)mouseDelta.X / AnimationConstants._ScaleFactor),
                                                (int)Math.Floor((double)mouseDelta.Y / AnimationConstants._ScaleFactor));

            if (isMouseUp)
            {
                if (IsPointNearVertex(point, 2) > -1) pctbxCanvas.Cursor = Cursors.Hand;
                else if (IsPointOnRadius(point, 2)) pctbxCanvas.Cursor = Cursors.Cross;
                else pctbxCanvas.Cursor = Cursors.Arrow;
            }

            bool result = false;
            if (isResizing)
            {
                Point rotatedMouseLocation = Utility.RotateFromReferencePoint(ScreenCen, point, (Int16)(-AngleDeg));
                rotatedMouseLocation.X = (Math.Abs(rotatedMouseLocation.X) / AnimationConstants._ScaleFactor) * 2;
                rotatedMouseLocation.Y = (Math.Abs(rotatedMouseLocation.Y) / AnimationConstants._ScaleFactor) * 2;
                if (rotatedMouseLocation.X > 0) W = (Int16)rotatedMouseLocation.X;
                if (rotatedMouseLocation.Y > 0) H = (Int16)rotatedMouseLocation.Y;
                result = true;
            }
            if (isMoving)
            {
                Move(unscaledMouseDelta);
                result = true;
            }
            if (isRotating)
            {
                AngleDeg = (Int16)(GetAngle(point) + offsetAngle);
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
            isRotating = false;
        }

        #endregion

        public override void Move(Point offset)
        {
            CenX += (Int16)offset.X;
            CenY += (Int16)offset.Y;
        }

        public Point[] GetVertices()
        {
            Point center = new Point(W / 2, H / 2);
            Point topLeft = new Point(center.X - W, center.Y - H);
            Point topRight = new Point(W - center.X, center.Y - H);
            Point bottomRight = new Point(W - center.X, H - center.Y);
            Point bottomLeft = new Point(center.X - W, H - center.Y);

            Point[] vertices = new Point[] { topRight, topLeft, bottomRight, bottomLeft };

            Matrix matrix = new Matrix();
            matrix.Rotate(angleDeg);
            matrix.TransformPoints(vertices);
            return vertices;
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AnimationConstants._RotatedRect);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), CenX);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), CenY);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), W);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), H);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AngleDeg);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Color);
            bytePosition += 4;
        }

        public override void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            CenX = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            CenY = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            W = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            H = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            AngleDeg = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            Color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 4;
        }

        #region Screen mapped methods

        public Point ScreenCen
        {
            get { return new Point(ScreenCenX, ScreenCenY); }
        }

        public Int16 ScreenCenX
        {
            get { return (Int16)(CenX * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenCenY
        {
            get { return (Int16)(CenY * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenW
        {
            get { return (Int16)(W * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenH
        {
            get { return (Int16)(H * AnimationConstants._ScaleFactor); }
        }

        public Point ScreenRectangleCenter
        {
            get { return new Point((W / 2) * AnimationConstants._ScaleFactor, (H / 2) * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenHalfDiagonal
        {
            get { return (Int16)(Math.Sqrt(Math.Pow(ScreenRectangleCenter.X + 1, 2) + Math.Pow(ScreenRectangleCenter.Y + 1, 2))); }
        }

        public Point[] ScreenGetVertices()
        {
            Point[] vertices = GetVertices();

            Matrix matrix = new Matrix();
            matrix.Scale(AnimationConstants._ScaleFactor, AnimationConstants._ScaleFactor);
            matrix.TransformPoints(vertices);
            matrix.Reset();
            matrix.Translate(ScreenCenX, ScreenCenY);
            matrix.TransformPoints(vertices);

            return vertices;
        }

        public bool IsPointNearCenter(Point point, Double margin)
        {
            return Utility.ArePointsWithinMargin(point, ScreenCen, AnimationConstants._ScaleFactor * margin);
        }

        public bool IsPointOnRadius(Point point, int margin)
        {
            return Utility.IsPointOnRadius(ScreenCen, point, ScreenHalfDiagonal, margin);
        }

        public int IsPointNearVertex(Point point, Double margin)
        {
            Point[] vertices = ScreenGetVertices();
            int selectedVertex = -1;
            for (int index = 0; index < vertices.Length; index++)
            {
                if (Utility.ArePointsWithinMargin(point, vertices[index], AnimationConstants._ScaleFactor * margin))
                {
                    selectedVertex = index;
                }

            }
            return selectedVertex;
        }

        public Int16 GetAngle(Point point)
        {
            return Utility.GetAngleFromReferencePoint(ScreenCen, point);
        }

        #endregion

    }
}
