using ST7735Point85;
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
            x0 = AnimationConstants.SCREEN_CENTER_X;
            y0 = AnimationConstants.DEFAULT_PRIMITIVE_TOP;
            x1 = AnimationConstants.DEFAULT_PRIMITIVE_LEFT;
            y1 = AnimationConstants.DEFAULT_PRIMITIVE_BOTTOM;
            x2 = AnimationConstants.DEFAULT_PRIMITIVE_RIGHT;
            y2 = AnimationConstants.DEFAULT_PRIMITIVE_BOTTOM;
            color = 0;
        }

        public Triangle(Triangle triangle)
        {
            x0 = triangle.X0;
            y0 = triangle.Y0;
            x1 = triangle.X1;
            y1 = triangle.Y1;
            x2 = triangle.X2;
            y2 = triangle.Y2;
            color = triangle.Color;
        }

        public override Primitive Clone()
        {
            return new Triangle(this);
        }

        public override void Move(Point offset)
        {
            x0 += (Int16)offset.X;
            y0 += (Int16)offset.Y;
            x1 += (Int16)offset.X;
            y1 += (Int16)offset.Y;
            x2 += (Int16)offset.X;
            y2 += (Int16)offset.Y;
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {

            Point[] trianglePoints = {
                new Point(x0 * AnimationConstants._ScaleFactor, y0 * AnimationConstants._ScaleFactor),
                new Point(x1 * AnimationConstants._ScaleFactor, y1 * AnimationConstants._ScaleFactor),
                new Point(x2 * AnimationConstants._ScaleFactor, y2 * AnimationConstants._ScaleFactor)
            };

            Color drawColor = Utility.GetColorFromUIint16(color);
            Brush brush = new SolidBrush(drawColor);
            Pen pen = new Pen(drawColor.ColorToInverse());
            pen.DashStyle = DashStyle.Dash;
            e.FillPolygon(brush, trianglePoints);
            if (isHighlighted) e.DrawPolygon(pen, trianglePoints);
        }

        public void MoveVertex(int vertexNum, Point offset)
        {
            if (vertexNum < 0 || vertexNum > 2) return;
            if (vertexNum == 0)
            {
                x0 += (Int16)offset.X;
                y0 += (Int16)offset.Y;
            }
            if (vertexNum == 1)
            {
                x1 += (Int16)offset.X;
                y1 += (Int16)offset.Y;
            }
            if (vertexNum == 2)
            {
                x2 += (Int16)offset.X;
                y2 += (Int16)offset.Y;
            }
        }

        public Point[] GetVerticesScreen()
        {
            Point vertex0 = new Point(x0 * AnimationConstants._ScaleFactor, y0 * AnimationConstants._ScaleFactor);
            Point vertex1 = new Point(x1 * AnimationConstants._ScaleFactor, y1 * AnimationConstants._ScaleFactor);
            Point vertex2 = new Point(x2 * AnimationConstants._ScaleFactor, y2 * AnimationConstants._ScaleFactor);

            Point[] vertices = new Point[] { vertex0, vertex1, vertex2 };

            return vertices;
        }

        public int IsPointNearVertex(Point point, Double margin)
        {
            Point[] vertices = GetVerticesScreen();
            int selectedVertex = -1;
            for (int index = 0; index < vertices.Length; index++)
            {
                if (Utility.ArePointsWithinMargin(point, vertices[index], margin))
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

        public Point GetCentroid()
        {
            int centerX = (x0 + x1 + x2) / 3;
            int centerY = (y0 + y1 + y2) / 3;
            return new Point(centerX, centerY);
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AnimationConstants._Triangle);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), x0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), x1);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), y1);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), x2);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), y2);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), color);
            bytePosition += 2;
        }

        public override void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            x0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            y0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            x1 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            y1 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            x2 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            y2 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
        }

    }
}
