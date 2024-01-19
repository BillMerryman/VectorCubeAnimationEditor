using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorCubeAnimationEditor
{
    using PrimitiveType = UInt16;
    internal class IdentifiedPrimitive
    {
        PrimitiveType primitiveType = 0;
        Primitive primitive;

        public PrimitiveType PrimitiveType 
        { 
            get { return primitiveType; }
            set { primitiveType = value; }
        }

        public Primitive Primitive 
        { 
            get { return primitive; }
            set { primitive = value; }
        }

        public IdentifiedPrimitive()
        {
            primitive = new Primitive();
        }

        public void serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), primitiveType);
            bytePosition += 2;
            switch (primitiveType)
            {
                case AnimationConstants._Circle:
                    primitive.Circle.serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._QuarterCircle:
                    primitive.QuarterCircle.serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Triangle:
                    primitive.Triangle.serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._RoundRect:
                    primitive.RoundRect.serialize(ref bytePosition, animationBytes);
                    break;
                default:
                    bytePosition += 14;
                    break;
            }
        }

    }
}
