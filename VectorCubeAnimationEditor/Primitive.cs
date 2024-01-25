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
        Circle circle;
        QuarterCircle quarterCircle;
        Triangle triangle;
        RoundRect roundRect;
        Line line;

        public PrimitiveType Type 
        { 
            get { return type; }
            set { type = value; }
        }

        public Circle Circle
        {
            get { return circle; }
        }

        public QuarterCircle QuarterCircle
        {
            get { return quarterCircle; }
        }

        public Triangle Triangle
        {
            get { return triangle; }
        }

        public RoundRect RoundRect
        {
            get { return roundRect; }
        }

        public Line Line
        {
            get { return line; }
        }

        public Primitive()
        {
            circle = new Circle();
            quarterCircle = new QuarterCircle();
            triangle = new Triangle();
            roundRect = new RoundRect();
            line = new Line();
        }

        public Primitive(Primitive primitive)
        {
            type = primitive.type;
            circle = new Circle(primitive.Circle);
            quarterCircle = new QuarterCircle(primitive.QuarterCircle);
            triangle = new Triangle(primitive.Triangle);
            roundRect = new RoundRect(primitive.RoundRect);
            line = new Line(primitive.Line);
        }

        public void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), type);
            bytePosition += 2;
            switch (type)
            {
                case AnimationConstants._Circle:
                    circle.Serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._QuarterCircle:
                    quarterCircle.Serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Triangle:
                    triangle.Serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._RoundRect:
                    roundRect.Serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Line:
                    line.Serialize(ref bytePosition, animationBytes);
                    break;
                default:
                    bytePosition += 14;
                    break;
            }
        }

        public void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            type = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            switch (type)
            {
                case AnimationConstants._Circle:
                    circle.Deserialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._QuarterCircle:
                    quarterCircle.Deserialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Triangle:
                    triangle.Deserialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._RoundRect:
                    roundRect.Deserialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Line:
                    line.Deserialize(ref bytePosition, animationBytes);
                    break;
                default:
                    bytePosition += 14;
                    break;
            }
        }

    }
}
