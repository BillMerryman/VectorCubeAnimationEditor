using System.Buffers.Binary;
using System.Drawing.Drawing2D;

namespace VectorCubeAnimationEditor
{
    internal class Line : Primitive
    {
        private Int16 x0;
        private Int16 y0;
        private Int16 x1;
        private Int16 y1;
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

        public override UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public Line()
        {
            x0 = AnimationConstants.DEFAULT_PRIMITIVE_LEFT;
            y0 = AnimationConstants.DEFAULT_PRIMITIVE_TOP;
            x1 = AnimationConstants.DEFAULT_PRIMITIVE_RIGHT;
            y1 = AnimationConstants.DEFAULT_PRIMITIVE_BOTTOM;
            color = 0;
        }

        public Line(Line line)
        {
            x0 = line.X0;
            y0 = line.Y0;
            x1 = line.X1;
            y1 = line.Y1;
            color = line.Color;
        }

        public override Primitive Clone()
        {
            return new Line(this);
        }

        public override void Move(Point offset)
        {
            x0 += (Int16)offset.X;
            y0 += (Int16)offset.Y;
            x1 += (Int16)offset.X;
            y1 += (Int16)offset.Y;
        }

        public void MoveVertex(int vertexNum, Point offset)
        {
            if (vertexNum < 0 || vertexNum > 1) return;
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
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {
            Color drawColor = Utility.GetColorFromUIint16(color);
            Pen pen = new Pen(drawColor);
            if (isHighlighted) pen.DashStyle = DashStyle.Dash;
            e.DrawLine(pen, X0 * AnimationConstants._ScaleFactor, Y0 * AnimationConstants._ScaleFactor, X1 * AnimationConstants._ScaleFactor, Y1 * AnimationConstants._ScaleFactor);
            pen.Dispose();
        }
        public int IsPointNearVertex(Point point, Double margin)
        {
            if (Math.Abs(point.X - (x0 * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * margin
                && Math.Abs(point.Y - (y0 * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * margin) return 0;
            if (Math.Abs(point.X - (x1 * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * margin
                && Math.Abs(point.Y - (y1 * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * margin) return 1;
            return -1;
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AnimationConstants._Line);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), x0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), x1);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), y1);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), color);
            bytePosition += 6;
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
            color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition)); ;
            bytePosition += 6;
        }

    }
}
