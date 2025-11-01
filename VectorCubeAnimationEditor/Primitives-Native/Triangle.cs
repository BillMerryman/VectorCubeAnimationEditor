using System.Buffers.Binary;
using VectorCubeAnimationEditor;

namespace Adafruit
{
    internal class Triangle
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

        public UInt16 Color
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

        public void Serialize(ref int bytePosition, byte[] animationBytes)
        {
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

        public void Deserialize(ref int bytePosition, byte[] animationBytes)
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
