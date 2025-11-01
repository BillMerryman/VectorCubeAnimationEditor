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

    internal class Primitive
    {
        PrimitiveType type = 0;
        Line line;
        RoundRect roundRect;
        Triangle triangle;
        Circle circle;
        QuarterCircle quarterCircle;

        const int LARGEST_PRIMITIVE_BYTE_COUNT = 14;

        public PrimitiveType Type 
        { 
            get { return type; }
            set { type = value; }
        }

        public Line Line
        {
            get { return line; }
        }

        public RoundRect RoundRect
        {
            get { return roundRect; }
        }

        public Triangle Triangle
        {
            get { return triangle; }
        }

        public Circle Circle
        {
            get { return circle; }
        }

        public QuarterCircle QuarterCircle
        {
            get { return quarterCircle; }
        }

        public Primitive()
        {
            line = new Line();
            roundRect = new RoundRect();
            triangle = new Triangle();
            circle = new Circle();
            quarterCircle = new QuarterCircle();
        }

        public Primitive(Primitive primitive)
        {
            type = primitive.type;
            line = new Line(primitive.Line);
            roundRect = new RoundRect(primitive.RoundRect);
            triangle = new Triangle(primitive.Triangle);
            circle = new Circle(primitive.Circle);
            quarterCircle = new QuarterCircle(primitive.QuarterCircle);
        }

        public void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), type);
            bytePosition += 2;
            switch (type)
            {
                case AnimationConstants._Line:
                    line.Serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._RoundRect:
                    roundRect.Serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Triangle:
                    triangle.Serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Circle:
                    circle.Serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._QuarterCircle:
                    quarterCircle.Serialize(ref bytePosition, animationBytes);
                    break;
                default:
                    bytePosition += LARGEST_PRIMITIVE_BYTE_COUNT;
                    break;
            }
        }

        public void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            type = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            switch (type)
            {
                case AnimationConstants._Line:
                    line.Deserialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._RoundRect:
                    roundRect.Deserialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Triangle:
                    triangle.Deserialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Circle:
                    circle.Deserialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._QuarterCircle:
                    quarterCircle.Deserialize(ref bytePosition, animationBytes);
                    break;
                default:
                    bytePosition += LARGEST_PRIMITIVE_BYTE_COUNT;
                    break;
            }
        }

    }
}
