using AnimationFlatbuffer;
using Google.FlatBuffers;
using System.Text.Json.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace VectorCubeAnimationEditor
{
    [JsonPolymorphic(
    TypeDiscriminatorPropertyName = "$discriminator",
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
)]
    [JsonDerivedType(typeof(Line), "Line")]
    [JsonDerivedType(typeof(Triangle), "Triangle")]
    [JsonDerivedType(typeof(RoundRect), "RoundRect")]
    [JsonDerivedType(typeof(RotatedRect), "RotatedRect")]
    [JsonDerivedType(typeof(Circle), "Circle")]
    abstract class Primitive
    {
        public abstract UInt16 Color
        {
            get;
            set;
        }

        public abstract AnimationFrame? Parent
        {
            get;
            internal set;
        }

        public abstract Primitive Clone();

        public abstract void Draw(Graphics e, bool isHighlighted);

        public abstract void MouseDown(Point point);

        public abstract bool MouseMove(Point point, PictureBox pctbxCanvas);

        public abstract void MouseUp();

        public abstract void Move(Point offset);

        public abstract void SerializeBinary(ref int bytePosition, byte[] animationBytes);

        public abstract void DeserializeBinary(ref int bytePosition, byte[] animationBytes);

        public abstract (PrimitiveFB, int) SerializeFB(FlatBufferBuilder builder);

        public abstract void DeserializeFB(Object data);

    }
}
