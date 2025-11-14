using ST7735Point85;
using System.Buffers.Binary;
using System.Drawing.Drawing2D;

namespace VectorCubeAnimationEditor
{
    internal class Circle : Primitive
    {
        private Int16 x0;
        private Int16 y0;
        private Int16 r;
        private byte quadrants;
        private Int16 delta;
        private UInt16 color;

        public const byte TopLeft = 1;
        public const byte TopRight = 2;
        public const byte BottomRight = 4;
        public const byte BottomLeft = 8;

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

        public Int16 R
        {
            get { return r; }
            set { r = (value < 1) ? (Int16)1 : value; }
        }

        public byte Quadrants
        {
            get { return quadrants; }
            set { quadrants = value; }
        }

        public Int16 Delta
        {
            get { return delta; }
            set { delta = value; }
        }

        public override UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public Circle()
        {
            r = AnimationConstants.DEFAULT_PRIMITIVE_RADIUS;
            x0 = AnimationConstants.SCREEN_CENTER_X;
            y0 = AnimationConstants.SCREEN_CENTER_Y;
            quadrants = BottomLeft | BottomRight | TopRight | TopLeft;
            delta = 0;
            color = 0;
        }

        public Circle(Circle circle)
        {
            x0 = circle.X0;
            y0 = circle.Y0;
            r = circle.R;
            quadrants = circle.Quadrants;
            delta = circle.Delta;
            color = circle.Color;
        }

        public override Primitive Clone()
        {
            return new Circle(this);
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {
            Color drawColor = Utility.GetColorFromUIint16(color);
            Brush brush = new SolidBrush(drawColor);
            Pen pen = new Pen(drawColor.ColorToInverse());
            pen.DashStyle = DashStyle.Dash;
            int ScreenX = X0 * AnimationConstants._ScaleFactor;
            int ScreenY = Y0 * AnimationConstants._ScaleFactor;
            int ScreenR = R * AnimationConstants._ScaleFactor;
            int boundingX = ScreenX - ScreenR;
            int boundingY = ScreenY - ScreenR;
            if ((quadrants & TopLeft) == TopLeft) e.FillPie(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR, 180, 90);
            if ((quadrants & TopRight) == TopRight) e.FillPie(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR, 270, 90);
            if ((quadrants & BottomRight) == BottomRight) e.FillPie(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR, 0, 90);
            if ((quadrants & BottomLeft) == BottomLeft) e.FillPie(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR, 90, 90);
            if (isHighlighted) e.DrawEllipse(pen, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR);

        }

        public override void Move(Point offset)
        {
            x0 += (Int16)offset.X;
            y0 += (Int16)offset.Y;
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AnimationConstants._QuarterCircle);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), x0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), r);
            bytePosition += 2;
            animationBytes[bytePosition] = quadrants;
            bytePosition += 1;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), delta);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), color);
            bytePosition += 5;
        }

        public override void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            x0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            y0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            r = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            quadrants = animationBytes[bytePosition];
            bytePosition += 1;
            delta = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 5;
        }

        #region Screen mapped methods

        public bool IsPointNearCenter(Point point, Double margin)
        {
            return Math.Abs(point.X - (x0 * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * 2
                    && Math.Abs(point.Y - (y0 * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * 2;
        }

        public bool IsPointOnRadius(Point point, Double margin)
        {
            int c = r * AnimationConstants._ScaleFactor;
            int a = ((x0 * AnimationConstants._ScaleFactor) - point.X);
            int b = ((y0 * AnimationConstants._ScaleFactor) - point.Y);
            int cSquare = c * c;
            int aSquare = a * a;
            int bSquare = b * b;
            if ((aSquare + bSquare > cSquare * (1 - margin)) && (aSquare + bSquare < cSquare * (1 + margin))) return true;
            return false;
        }

        #endregion

    }
}
