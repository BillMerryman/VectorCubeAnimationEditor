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
            int boundingX = ScreenX0 - ScreenR;
            int boundingY = ScreenY0 - ScreenR;
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

        public Point ScreenCen
        {
            get { return new Point(ScreenX0, ScreenY0); }
        }

        public Int16 ScreenX0
        {
            get { return (Int16)(X0 * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenY0
        {
            get { return (Int16)(Y0 * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenR
        {
            get { return (Int16)(R * AnimationConstants._ScaleFactor); }
        }

        #region Mouse handling

        private Point mouseLocation = new Point(0, 0);
        private bool isMouseUp = true;
        private bool isMoving = false;
        private bool isResizing = false;

        public override void MouseDown(Point point)
        {
            mouseLocation = point;
            isMouseUp = false;

            if (IsPointNearCenter(mouseLocation)) isMoving = true;
            else isResizing = IsPointOnRadius(mouseLocation);
        }

        public override bool MouseMove(Point point, PictureBox pctbxCanvas)
        {
            Point mouseDelta = new Point(point.X - mouseLocation.X, point.Y - mouseLocation.Y);
            Point unscaledMouseDelta = new Point((int)Math.Floor((double)mouseDelta.X / AnimationConstants._ScaleFactor),
                                                (int)Math.Floor((double)mouseDelta.Y / AnimationConstants._ScaleFactor));

            if (isMouseUp)
            {
                if (IsPointNearCenter(point)) pctbxCanvas.Cursor = Cursors.SizeAll;
                else
                {
                    if (IsPointOnRadius(point))
                    {
                        int angle = GetAngle(point);
                        angle %= 180;
                        pctbxCanvas.Cursor = (angle < 90) ? Cursors.SizeNWSE : Cursors.SizeNESW;
                    }
                    else pctbxCanvas.Cursor = Cursors.Arrow;
                }
                return false;
            }
            else
            {
                if (isMoving) Move(unscaledMouseDelta);
                else
                {
                    if (isResizing)
                    {
                        int circleXOffset = ((X0 * AnimationConstants._ScaleFactor) - point.X) / AnimationConstants._ScaleFactor;
                        int circleYOffset = ((Y0 * AnimationConstants._ScaleFactor) - point.Y) / AnimationConstants._ScaleFactor;
                        R = (short)Math.Sqrt((circleXOffset * circleXOffset) + (circleYOffset * circleYOffset));
                    }
                }
            }

            mouseLocation.X += unscaledMouseDelta.X * AnimationConstants._ScaleFactor;
            mouseLocation.Y += unscaledMouseDelta.Y * AnimationConstants._ScaleFactor;

            return true;
        }

        public override void MouseUp()
        {
            isMouseUp = true;
            isMoving = false;
            isResizing = false;
        }

        #endregion
        public Int16 GetAngle(Point point)
        {
            return Utility.GetAngleFromReferencePoint(ScreenCen, point);
        }

        public bool IsPointOnRadius(Point point)
        {
            int margin = 4;
            int cLower = ScreenR - margin;
            int cUpper = ScreenR + margin;
            int a = ((X0 * AnimationConstants._ScaleFactor) - point.X);
            int b = ((Y0 * AnimationConstants._ScaleFactor) - point.Y);
            int cSquare = ScreenR * ScreenR;
            int cLowerSquare = cLower * cLower;
            int cUpperSquare = cUpper * cUpper;
            int aSquare = a * a;
            int bSquare = b * b;
            if ((aSquare + bSquare > cLowerSquare) && (aSquare + bSquare < cUpperSquare)) return true;
            return false;
        }

        public bool IsPointNearCenter(Point point)
        {
            int margin = 4;
            return Utility.ArePointsWithinMargin(ScreenCen, point, margin);
        }

        #endregion

    }
}
