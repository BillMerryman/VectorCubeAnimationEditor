using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorCubeAnimationEditor
{
    internal class Primitive
    {
        Circle circle;
        QuarterCircle quarterCircle;
        Triangle triangle;
        RoundRect roundRect;

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

        public Primitive()
        {
            circle = new Circle();
            quarterCircle = new QuarterCircle();
            triangle = new Triangle();
            roundRect = new RoundRect();
        }

    }
}
