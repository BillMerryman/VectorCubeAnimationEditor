using AnimationFlatbuffer;
using Google.FlatBuffers;
using System.Text.Json.Serialization;
using System.Drawing.Drawing2D;

namespace VectorCubeAnimationEditor
{
    internal class Circle : Primitive
    {
        private AnimationFrame? parent;

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

        [JsonIgnore]
        public override AnimationFrame? Parent
        {
            get { return parent; }
            internal set { parent = value; }
        }

        [JsonIgnore]
        public Int16 X0_Abs
        {
            get { return (Parent is not null) ? (Int16)(X0 + Parent.RelativeCenter.X) : X0; }
            set { X0 = (Parent is not null) ? (Int16)(value - Parent.RelativeCenter.X) : value; }
        }

        [JsonIgnore]
        public Int16 Y0_Abs
        {
            get { return (Parent is not null) ? (Int16)(Y0 + Parent.RelativeCenter.Y) : Y0; }
            set { Y0 = (Parent is not null) ? (Int16)(value - Parent.RelativeCenter.Y) : value; }
        }

        public Circle()
        {
            X0 = 0;
            Y0 = 0;
            R = AnimationConstants.DEFAULT_PRIMITIVE_RADIUS;
            Quadrants = BottomLeft | BottomRight | TopRight | TopLeft;
            Delta = 0;
            Color = 0;
        }

        public Circle(AnimationFrame parent) : this()
        {
            this.parent = parent;
        }

        public Circle(Circle circle)
        {
            X0 = circle.X0;
            Y0 = circle.Y0;
            R = circle.R;
            Quadrants = circle.Quadrants;
            Delta = circle.Delta;
            Color = circle.Color;
            parent = circle.Parent;
        }

        public override Primitive Clone()
        {
            return new Circle(this);
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {
            Color drawColor = Utility.GetColorFromUIint16(Color);
            Brush brush = new SolidBrush(drawColor);
            Pen pen = new(drawColor.ColorToInverse())
            {
                DashStyle = DashStyle.Dash
            };
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
            X0_Abs += (Int16)offset.X;
            Y0_Abs += (Int16)offset.Y;
        }

        public override (PrimitiveFB, int) SerializeFB(FlatBufferBuilder builder)
        {
            return (PrimitiveFB.CircleFB, CircleFB.CreateCircleFB(builder, X0, Y0, R, Quadrants, Delta, Color).Value);
        }

        public override void DeserializeFB(Object data)
        {
            X0 = ((CircleFB)data).X0;
            Y0 = ((CircleFB)data).Y0;
            R = ((CircleFB)data).R;
            Quadrants = ((CircleFB)data).Quadrants;
            Delta = ((CircleFB)data).Delta;
        }

        #region Screen mapped methods

        [JsonIgnore]
        public Point ScreenCen
        {
            get { return new Point(ScreenX0, ScreenY0); }
        }

        [JsonIgnore]
        public Int16 ScreenX0
        {
            get { return (Int16)(X0_Abs * AnimationConstants._ScaleFactor); }
        }

        [JsonIgnore]
        public Int16 ScreenY0
        {
            get { return (Int16)(Y0_Abs * AnimationConstants._ScaleFactor); }
        }

        [JsonIgnore]
        public Int16 ScreenR
        {
            get { return (Int16)(R * AnimationConstants._ScaleFactor); }
        }

        #region Mouse handling

        private Point mouseLocation = new(0, 0);
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
            Point mouseDelta = new(point.X - mouseLocation.X, point.Y - mouseLocation.Y);
            Point unscaledMouseDelta = new((int)Math.Floor((double)mouseDelta.X / AnimationConstants._ScaleFactor),
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
                        int circleXOffset = ((X0_Abs * AnimationConstants._ScaleFactor) - point.X) / AnimationConstants._ScaleFactor;
                        int circleYOffset = ((Y0_Abs * AnimationConstants._ScaleFactor) - point.Y) / AnimationConstants._ScaleFactor;
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
            int a = ((X0_Abs * AnimationConstants._ScaleFactor) - point.X);
            int b = ((Y0_Abs * AnimationConstants._ScaleFactor) - point.Y);
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


/*
 * Old serialization method
 * 

public override void SerializeBinary(ref int bytePosition, byte[] animationBytes)
{
    BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], AnimationConstants._QuarterCircle);
    bytePosition += 2;
    BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], X0);
    bytePosition += 2;
    BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], Y0);
    bytePosition += 2;
    BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], R);
    bytePosition += 2;
    animationBytes[bytePosition] = Quadrants;
    bytePosition += 1;
    BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], Delta);
    bytePosition += 2;
    BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], Color);
    bytePosition += 5;
}

public override void DeserializeBinary(ref int bytePosition, byte[] animationBytes)
{
    X0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
    bytePosition += 2;
    Y0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
    bytePosition += 2;
    R = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
    bytePosition += 2;
    Quadrants = animationBytes[bytePosition];
    bytePosition += 1;
    Delta = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
    bytePosition += 2;
    Color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
    bytePosition += 5;
}



*/