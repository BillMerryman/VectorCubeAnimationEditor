using System.Buffers.Binary;
using System.Drawing.Drawing2D;

namespace VectorCubeAnimationEditor
{
    internal class Triangle : Primitive
    {
        private Int16 x0;
        private Int16 y0;
        private Int16 x1;
        private Int16 y1;
        private Int16 x2;
        private Int16 y2;
        private UInt16 color;

        public Int16 X0 
        { 
            get { return x0; } 
            set {  x0 = value; } 
        }

        public Int16 Y0
        {
            get { return y0; }
            set { y0 = value; }
        }

        public Int16 X1
        {
            get { return x1; }
            set { x1 = value; }
        }

        public Int16 Y1
        {
            get { return y1; }
            set { y1 = value; }
        }

        public Int16 X2
        {
            get { return x2; }
            set { x2 = value; }
        }

        public Int16 Y2
        {
            get { return y2; }
            set { y2 = value; }
        }

        public override UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public Triangle()
        {
            X0 = AnimationConstants.SCREEN_CENTER_X;
            Y0 = AnimationConstants.DEFAULT_PRIMITIVE_TOP;
            X1 = AnimationConstants.DEFAULT_PRIMITIVE_LEFT;
            Y1 = AnimationConstants.DEFAULT_PRIMITIVE_BOTTOM;
            X2 = AnimationConstants.DEFAULT_PRIMITIVE_RIGHT;
            Y2 = AnimationConstants.DEFAULT_PRIMITIVE_BOTTOM;
            Color = 0;
        }

        public Triangle(Triangle triangle)
        {
            X0 = triangle.X0;
            Y0 = triangle.Y0;
            X1 = triangle.X1;
            Y1 = triangle.Y1;
            X2 = triangle.X2;
            Y2 = triangle.Y2;
            Color = triangle.Color;
        }

        public override Primitive Clone()
        {
            return new Triangle(this);
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {

            Point[] trianglePoints = {
                new Point(X0 * AnimationConstants._ScaleFactor, Y0 * AnimationConstants._ScaleFactor),
                new Point(X1 * AnimationConstants._ScaleFactor, Y1 * AnimationConstants._ScaleFactor),
                new Point(X2 * AnimationConstants._ScaleFactor, Y2 * AnimationConstants._ScaleFactor)
            };

            Color drawColor = Utility.GetColorFromUIint16(Color);
            Brush brush = new SolidBrush(drawColor);
            Pen pen = new Pen(drawColor.ColorToInverse());
            pen.DashStyle = DashStyle.Dash;
            e.FillPolygon(brush, trianglePoints);
            if (isHighlighted) e.DrawPolygon(pen, trianglePoints);
        }

        #region Mouse handling

        private Point MouseLocation = new Point(0, 0);
        private bool isMoving = false;
        private int isVertexMoving = -1;

        public override void MouseDown(Point point)
        {
            MouseLocation = point;
            isVertexMoving = IsPointNearVertex(MouseLocation, 2);
            isMoving = IsPointNearCentroid(MouseLocation, 2);
        }

        public override bool MouseMove(Point point, PictureBox pctbxCanvas)
        {
            Point mouseDelta = new Point(point.X - MouseLocation.X, point.Y - MouseLocation.Y);
            Point unscaledMouseDelta = new Point((int)Math.Floor((double)mouseDelta.X / AnimationConstants._ScaleFactor),
                                                (int)Math.Floor((double)mouseDelta.Y / AnimationConstants._ScaleFactor));

            bool result = false;
            if (isMoving)
            {
                Move(unscaledMouseDelta);
                result = true;
            }
            if (isVertexMoving > -1)
            {
                MoveVertex(isVertexMoving, unscaledMouseDelta);
                result = true;
            }
            MouseLocation.X += unscaledMouseDelta.X * AnimationConstants._ScaleFactor;
            MouseLocation.Y += unscaledMouseDelta.Y * AnimationConstants._ScaleFactor;
            return result;
        }

        public override void MouseUp()
        {
            isMoving = false;
            isVertexMoving = -1;
        }

        #endregion

        public override void Move(Point offset)
        {
            X0 += (Int16)offset.X;
            Y0 += (Int16)offset.Y;
            X1 += (Int16)offset.X;
            Y1 += (Int16)offset.Y;
            X2 += (Int16)offset.X;
            Y2 += (Int16)offset.Y;
        }

        public void MoveVertex(int vertexNum, Point offset)
        {
            if (vertexNum < 0 || vertexNum > 2) return;
            if (vertexNum == 0)
            {
                X0 += (Int16)offset.X;
                Y0 += (Int16)offset.Y;
            }
            if (vertexNum == 1)
            {
                X1 += (Int16)offset.X;
                Y1 += (Int16)offset.Y;
            }
            if (vertexNum == 2)
            {
                X2 += (Int16)offset.X;
                Y2 += (Int16)offset.Y;
            }
        }

        public Point GetCentroid()
        {
            int centerX = (X0 + X1 + X2) / 3;
            int centerY = (Y0 + Y1 + Y2) / 3;
            return new Point(centerX, centerY);
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AnimationConstants._Triangle);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), X0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), X1);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Y1);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), X2);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Y2);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Color);
            bytePosition += 2;
        }

        public override void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            X0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            Y0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            X1 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            Y1 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            X2 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            Y2 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            Color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
        }

        #region Screen mapped methods

        public Point[] GetVerticesScreen()
        {
            Point vertex0 = new Point(X0 * AnimationConstants._ScaleFactor, Y0 * AnimationConstants._ScaleFactor);
            Point vertex1 = new Point(X1 * AnimationConstants._ScaleFactor, Y1 * AnimationConstants._ScaleFactor);
            Point vertex2 = new Point(X2 * AnimationConstants._ScaleFactor, Y2 * AnimationConstants._ScaleFactor);

            Point[] vertices = new Point[] { vertex0, vertex1, vertex2 };

            return vertices;
        }

        public int IsPointNearVertex(Point point, Double margin)
        {
            Point[] vertices = GetVerticesScreen();
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

        public bool IsPointNearCentroid(Point point, Double margin)
        {
            Point centroid = GetCentroid();
            return (Math.Abs(point.X - (centroid.X * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * margin
                && Math.Abs(point.Y - (centroid.Y * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * margin);
        }

        #endregion

    }
}
