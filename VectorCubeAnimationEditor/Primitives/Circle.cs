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
            R = AnimationConstants.DEFAULT_PRIMITIVE_RADIUS;
            X0 = AnimationConstants.SCREEN_CENTER_X;
            Y0 = AnimationConstants.SCREEN_CENTER_Y;
            Quadrants = BottomLeft | BottomRight | TopRight | TopLeft;
            Delta = 0;
            Color = 0;
        }

        public Circle(Circle circle)
        {
            X0 = circle.X0;
            Y0 = circle.Y0;
            R = circle.R;
            Quadrants = circle.Quadrants;
            Delta = circle.Delta;
            Color = circle.Color;
        }

        public override Primitive Clone()
        {
            return new Circle(this);
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {
            Color drawColor = Utility.GetColorFromUIint16(Color);
            Brush brush = new SolidBrush(drawColor);
            Pen pen = new Pen(drawColor.ColorToInverse());
            pen.DashStyle = DashStyle.Dash;
            int ScreenX = X0 * AnimationConstants._ScaleFactor;
            int ScreenY = Y0 * AnimationConstants._ScaleFactor;
            int ScreenR = R * AnimationConstants._ScaleFactor;
            int boundingX = ScreenX - ScreenR;
            int boundingY = ScreenY - ScreenR;
            if ((Quadrants & TopLeft) == TopLeft) e.FillPie(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR, 180, 90);
            if ((Quadrants & TopRight) == TopRight) e.FillPie(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR, 270, 90);
            if ((Quadrants & BottomRight) == BottomRight) e.FillPie(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR, 0, 90);
            if ((Quadrants & BottomLeft) == BottomLeft) e.FillPie(brush, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR, 90, 90);
            if (isHighlighted) e.DrawEllipse(pen, boundingX, boundingY, 2 * ScreenR, 2 * ScreenR);

        }

        public override void Move(Point offset)
        {
            X0 += (Int16)offset.X;
            Y0 += (Int16)offset.Y;
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AnimationConstants._QuarterCircle);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), X0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), R);
            bytePosition += 2;
            animationBytes[bytePosition] = Quadrants;
            bytePosition += 1;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Delta);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Color);
            bytePosition += 5;
        }

        public override void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            X0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            Y0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            R = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            Quadrants = animationBytes[bytePosition];
            bytePosition += 1;
            Delta = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            Color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 5;
        }

        #region Screen mapped methods


        #region Mouse handling

        private Point MouseLocation = new Point(0, 0);
        private bool isMoving = false;
        private bool isResizing = false;

        public override void MouseDown(Point point)
        {
            MouseLocation = point;
            isResizing = IsPointOnRadius(MouseLocation, .1);
            isMoving = IsPointNearCenter(MouseLocation, 2);
        }

        public override bool MouseMove(Point point, PictureBox pctbxCanvas)
        {
            Point mouseDelta = new Point(point.X - MouseLocation.X, point.Y - MouseLocation.Y);
            Point unscaledMouseDelta = new Point((int)Math.Floor((double)mouseDelta.X / AnimationConstants._ScaleFactor),
                                                (int)Math.Floor((double)mouseDelta.Y / AnimationConstants._ScaleFactor));

            bool result = false;
            if (isResizing)
            {
                //Old way of resizing...
                //int cRadius = circle.R;
                //if (cRadius + primitiveDeltaY > 0) circle.R += (Int16)primitiveDeltaY;
                int circleXOffset = ((X0 * AnimationConstants._ScaleFactor) - point.X) / AnimationConstants._ScaleFactor;
                int circleYOffset = ((Y0 * AnimationConstants._ScaleFactor) - point.Y) / AnimationConstants._ScaleFactor;
                R = (short)Math.Sqrt((circleXOffset * circleXOffset) + (circleYOffset * circleYOffset));
                result = true;
            }
            if (isMoving)
            {
                Move(unscaledMouseDelta);
                result = true;
            }
            MouseLocation.X += unscaledMouseDelta.X * AnimationConstants._ScaleFactor;
            MouseLocation.Y += unscaledMouseDelta.Y * AnimationConstants._ScaleFactor;
            return result;
        }

        public override void MouseUp()
        {
            isMoving = false;
            isResizing = false;
        }

        #endregion

        public bool IsPointNearCenter(Point point, Double margin)
        {
            return Math.Abs(point.X - (X0 * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * 2
                    && Math.Abs(point.Y - (Y0 * AnimationConstants._ScaleFactor)) < AnimationConstants._ScaleFactor * 2;
        }

        public bool IsPointOnRadius(Point point, Double margin)
        {
            int c = R * AnimationConstants._ScaleFactor;
            int a = ((X0 * AnimationConstants._ScaleFactor) - point.X);
            int b = ((Y0 * AnimationConstants._ScaleFactor) - point.Y);
            int cSquare = c * c;
            int aSquare = a * a;
            int bSquare = b * b;
            if ((aSquare + bSquare > cSquare * (1 - margin)) && (aSquare + bSquare < cSquare * (1 + margin))) return true;
            return false;
        }

        #endregion

    }
}
