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

        public void serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), type);
            bytePosition += 2;
            switch (type)
            {
                case AnimationConstants._Circle:
                    circle.serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._QuarterCircle:
                    quarterCircle.serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Triangle:
                    triangle.serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._RoundRect:
                    roundRect.serialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Line:
                    line.serialize(ref bytePosition, animationBytes);
                    break;
                default:
                    bytePosition += 14;
                    break;
            }
        }

        public void deserialize(ref int bytePosition, byte[] animationBytes)
        {
            type = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            switch (type)
            {
                case AnimationConstants._Circle:
                    circle.deserialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._QuarterCircle:
                    quarterCircle.deserialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Triangle:
                    triangle.deserialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._RoundRect:
                    roundRect.deserialize(ref bytePosition, animationBytes);
                    break;
                case AnimationConstants._Line:
                    line.deserialize(ref bytePosition, animationBytes);
                    break;
                default:
                    bytePosition += 14;
                    break;
            }
        }

    }
}
