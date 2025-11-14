using System.Buffers.Binary;

namespace VectorCubeAnimationEditor
{
    using PrimitiveType = UInt16;

    internal abstract class Primitive
    {
        public abstract UInt16 Color
        {
            get;
            set;
        }

        public abstract Primitive Clone();

        public abstract void Draw(Graphics e, bool isHighlighted);

        public abstract void Move(Point offset);

        public abstract void Serialize(ref int bytePosition, byte[] animationBytes);

        public abstract void Deserialize(ref int bytePosition, byte[] animationBytes);

    }
}
