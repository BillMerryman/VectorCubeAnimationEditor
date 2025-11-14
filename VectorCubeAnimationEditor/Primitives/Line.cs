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
            X0 = AnimationConstants.DEFAULT_PRIMITIVE_LEFT;
            Y0 = AnimationConstants.DEFAULT_PRIMITIVE_TOP;
            X1 = AnimationConstants.DEFAULT_PRIMITIVE_RIGHT;
            Y1 = AnimationConstants.DEFAULT_PRIMITIVE_BOTTOM;
            Color = 0;
        }

        public Line(Line line)
        {
            X0 = line.X0;
            Y0 = line.Y0;
            X1 = line.X1;
            Y1 = line.Y1;
            Color = line.Color;
        }

        public override Primitive Clone()
        {
            return new Line(this);
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {
            Color drawColor = Utility.GetColorFromUIint16(color);
            Pen pen = new Pen(drawColor);
            if (isHighlighted) pen.DashStyle = DashStyle.Dash;
            e.DrawLine(pen, X0 * AnimationConstants._ScaleFactor, Y0 * AnimationConstants._ScaleFactor, X1 * AnimationConstants._ScaleFactor, Y1 * AnimationConstants._ScaleFactor);
            pen.Dispose();
        }

        public override void Move(Point offset)
        {
            X0 += (Int16)offset.X;
            Y0 += (Int16)offset.Y;
            X1 += (Int16)offset.X;
            Y1 += (Int16)offset.Y;
        }

        public void MoveVertex(int vertexNum, Point offset)
        {
            if (vertexNum < 0 || vertexNum > 1) return;
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
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AnimationConstants._Line);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), X0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), X1);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Y1);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Color);
            bytePosition += 6;
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
            Color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition)); ;
            bytePosition += 6;
        }

        #region Screen mapped methods

        public int IsPointNearVertex(Point point, Double margin)
        {
            if (Math.Abs(point.X - (X0 * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * margin
                && Math.Abs(point.Y - (Y0 * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * margin) return 0;
            if (Math.Abs(point.X - (X1 * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * margin
                && Math.Abs(point.Y - (Y1 * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * margin) return 1;
            return -1;
        }

        public bool IsPointNearLine(Point point)
        {
            return Utility.IsPointNearLine(new Point(X0 * AnimationConstants._ScaleFactor, Y0 * AnimationConstants._ScaleFactor), new Point(X1 * AnimationConstants._ScaleFactor, Y1 * AnimationConstants._ScaleFactor), point, AnimationConstants._ScaleFactor * 2);
        }

        #endregion

    }
}
