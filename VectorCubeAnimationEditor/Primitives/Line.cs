using AnimationFlatbuffer;
using Google.FlatBuffers;
using System.Text.Json.Serialization;
using System.Drawing.Drawing2D;

namespace VectorCubeAnimationEditor
{
    internal class Line : Primitive
    {
        private AnimationFrame? parent;

        private Int16 x0;
        private Int16 y0;
        private Int16 x1;
        private Int16 y1;
        private UInt16 color;

        [JsonIgnore]
        public override AnimationFrame? Parent
        {
            get { return parent; }
            internal set { parent = value; }
        }

        public Int16 X0
        {
            get { return (Parent is not null) ? (Int16)(x0 + Parent.RelativeCenter.X) : x0; }
            set { x0 = (Parent is not null) ? (Int16)(value - Parent.RelativeCenter.X) : value; }
        }

        public Int16 Y0
        {
            get { return (Parent is not null) ? (Int16)(y0 + Parent.RelativeCenter.Y) : y0; }
            set { y0 = (Parent is not null) ? (Int16)(value - Parent.RelativeCenter.Y) : value; }
        }

        public Int16 X1
        {
            get { return (Parent is not null) ? (Int16)(x1 + Parent.RelativeCenter.X) : x1; }
            set { x1 = (Parent is not null) ? (Int16)(value - Parent.RelativeCenter.X) : value; }
        }

        public Int16 Y1
        {
            get { return (Parent is not null) ? (Int16)(y1 + Parent.RelativeCenter.Y) : y1; }
            set { y1 = (Parent is not null) ? (Int16)(value - Parent.RelativeCenter.Y) : value; }
        }

        public override UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public Line()
        {
            X0 = AnimationConstants.DEFAULT_PRIMITIVE_LEFT;
            Y0 = AnimationConstants.DEFAULT_PRIMITIVE_TOP;
            X1 = AnimationConstants.DEFAULT_PRIMITIVE_RIGHT;
            Y1 = AnimationConstants.DEFAULT_PRIMITIVE_BOTTOM;
            Color = 0;
        }

        public Line(AnimationFrame parent) : this()
        {
            this.parent = parent;
        }

        public Line(Line line)
        {
            X0 = line.X0;
            Y0 = line.Y0;
            X1 = line.X1;
            Y1 = line.Y1;
            Color = line.Color;
            Parent = line.Parent;
        }

        public override Primitive Clone()
        {
            return new Line(this);
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {
            Color drawColor = Utility.GetColorFromUIint16(Color);
            Pen pen = new(drawColor)
            {
                Width = AnimationConstants._ScaleFactor
            };
            if (isHighlighted) pen.DashStyle = DashStyle.Dash;
            e.DrawLine(pen, X0 * AnimationConstants._ScaleFactor, Y0 * AnimationConstants._ScaleFactor, X1 * AnimationConstants._ScaleFactor, Y1 * AnimationConstants._ScaleFactor);
            pen.Dispose();
        }

        public override void Move(Point offset)
        {
            X0 += (Int16)offset.X;
            Y0 += (Int16)offset.Y;
            X1 += (Int16)offset.X;
            Y1 += (Int16)offset.Y;
        }

        public void MoveEndPoint(int endPointNum, Point offset)
        {
            if (endPointNum < 0 || endPointNum > 1) return;
            if (endPointNum == 0)
            {
                X0 += (Int16)offset.X;
                Y0 += (Int16)offset.Y;
            }
            if (endPointNum == 1)
            {
                X1 += (Int16)offset.X;
                Y1 += (Int16)offset.Y;
            }
        }

        public override (PrimitiveFB, int) SerializeFB(FlatBufferBuilder builder)
        {
            return (PrimitiveFB.LineFB, LineFB.CreateLineFB(builder, X0, Y0, X1, Y1, Color).Value);
        }

        public override void DeserializeFB(Object data)
        {
            X0 = ((LineFB)data).X0;
            Y0 = ((LineFB)data).Y0;
            X1 = ((LineFB)data).X1;
            Y1 = ((LineFB)data).Y1;
        }

        #region Screen mapped methods

        [JsonIgnore]
        public Point ScreenCen
        {
            get { return new Point((ScreenX1 + ScreenX0) / 2, (ScreenY1 + ScreenY0) / 2); }
        }

        [JsonIgnore]
        public Int16 ScreenX0
        {
            get { return (Int16)(X0 * AnimationConstants._ScaleFactor); }
        }

        [JsonIgnore]
        public Int16 ScreenY0
        {
            get { return (Int16)(Y0 * AnimationConstants._ScaleFactor); }
        }

        [JsonIgnore]
        public Int16 ScreenX1
        {
            get { return (Int16)(X1 * AnimationConstants._ScaleFactor); }
        }

        [JsonIgnore]
        public Int16 ScreenY1
        {
            get { return (Int16)(Y1 * AnimationConstants._ScaleFactor); }
        }

        #region Mouse handling

        private Point mouseLocation = new(0, 0);
        private bool isMouseUp = true;
        private bool isMoving = false;
        private int selectedEndpoint = -1;

        public override void MouseDown(Point point)
        {
            mouseLocation = point;
            isMouseUp = false;

            if (IsPointNearCenter(point)) isMoving = true;
            else selectedEndpoint = GetSelectedEndpoint(mouseLocation);
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
                    if (GetSelectedEndpoint(point) > -1) pctbxCanvas.Cursor = Cursors.Hand;
                    else pctbxCanvas.Cursor = Cursors.Arrow;
                }
                return false;
            }
            else
            {
                if (isMoving) Move(unscaledMouseDelta);
                else
                {
                    if (selectedEndpoint > -1) MoveEndPoint(selectedEndpoint, unscaledMouseDelta);
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
            selectedEndpoint = -1;
        }

        #endregion

        public Point[] GetEndPoints()
        {
            Point endPoint1 = new(X0, Y0);
            Point endPoint2 = new(X1, Y1);

            Point[] endPoints = [endPoint1, endPoint2];

            return endPoints;
        }

        public Point[] GetScreenEndPoints()
        {
            Point[] endPoints = GetEndPoints();

            for (int index = 0; index < endPoints.Length; index++)
            {
                endPoints[index].X *= AnimationConstants._ScaleFactor;
                endPoints[index].Y *= AnimationConstants._ScaleFactor;
            }

            return endPoints;
        }

        public int GetSelectedEndpoint(Point point)
        {
            Point[] endpoints = GetScreenEndPoints();
            int margin = 4;
            int selectedEndpoint = -1;
            for (int index = 0; index < endpoints.Length; index++)
            {
                if (Utility.ArePointsWithinMargin(point, endpoints[index], margin))
                {
                    selectedEndpoint = index;
                }

            }
            return selectedEndpoint;
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
    BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], AnimationConstants._Line);
    bytePosition += 2;
    BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], X0);
    bytePosition += 2;
    BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], Y0);
    bytePosition += 2;
    BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], X1);
    bytePosition += 2;
    BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], Y1);
    bytePosition += 2;
    BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], Color);
    bytePosition += 6;
}

public override void DeserializeBinary(ref int bytePosition, byte[] animationBytes)
{
    X0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
    bytePosition += 2;
    Y0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
    bytePosition += 2;
    X1 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
    bytePosition += 2;
    Y1 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
    bytePosition += 2;
    Color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]); ;
    bytePosition += 6;
}

*/
