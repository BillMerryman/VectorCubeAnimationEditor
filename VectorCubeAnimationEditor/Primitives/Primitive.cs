
namespace VectorCubeAnimationEditor
{
    internal abstract class Primitive
    {
        public abstract UInt16 Color
        {
            get;
            set;
        }

        public abstract Primitive Clone();

        public abstract void Draw(Graphics e, bool isHighlighted);

        public abstract void MouseDown(Point point);

        public abstract bool MouseMove(Point point, PictureBox pctbxCanvas);

        public abstract void MouseUp();

        public abstract void Move(Point offset);

        public abstract void Serialize(ref int bytePosition, byte[] animationBytes);

        public abstract void Deserialize(ref int bytePosition, byte[] animationBytes);

    }
}
