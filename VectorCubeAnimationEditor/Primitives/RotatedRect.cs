using AnimationFlatbuffer;
using Google.FlatBuffers;
using System.Buffers.Binary;
using System.Text.Json.Serialization;
using System.Drawing.Drawing2D;

namespace VectorCubeAnimationEditor
{
    internal class RotatedRect : Primitive
    {
        private AnimationFrame? parent;

        private Int16 x0;
        private Int16 y0;
        private Int16 w;
        private Int16 h;
        private Int16 angleDeg;
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

        public Int16 W
        {
            get { return w; }
            set { w = (value < 1) ? (Int16)1 : value; }
        }

        public Int16 H
        {
            get { return h; }
            set { h = (value < 1) ? (Int16)1 : value; }
        }

        public Int16 AngleDeg
        {
            get { return angleDeg; }
            set { angleDeg = (value < 0) ? (Int16)(360 + (value % 360)) : (Int16)(value % 360); }
        }

        public override UInt16 Color
        {
            get { return color; }
            set { color = value; }
        }

        public RotatedRect()
        {
            X0 = 0;
            Y0 = 0;
            W = AnimationConstants.DEFAULT_PRIMITIVE_SIZE;
            H = AnimationConstants.DEFAULT_PRIMITIVE_SIZE;
            AngleDeg = 0;
            Color = 0;
        }

        public RotatedRect(AnimationFrame parent) : this()
        {
            this.parent = parent;
        }

        public RotatedRect(RotatedRect rotatedRect)
        {
            X0 = rotatedRect.X0;
            Y0 = rotatedRect.Y0;
            W = rotatedRect.W;
            H = rotatedRect.H;
            AngleDeg = rotatedRect.AngleDeg;
            Color = rotatedRect.Color;
            parent = rotatedRect.Parent;
        }

        public override Primitive Clone()
        {
            return new RotatedRect(this);
        }

        public override void Draw(Graphics e, bool isHighlighted)
        {
            Color drawColor = Utility.GetColorFromUIint16(Color);
            Brush brush = new SolidBrush(drawColor);
            Pen pen = new(drawColor.ColorToInverse())
            {
                DashStyle = DashStyle.Dash
            };

            // Split rectangle into two triangles (diagonal from top-left to bottom-right)
            Point[] vertices = GetScreenVertices();
            Point[] triangle1 = [vertices[0], vertices[1], vertices[2]];
            Point[] triangle2 = [vertices[2], vertices[3], vertices[0]];
            e.FillPolygon(brush, triangle1);
            e.FillPolygon(brush, triangle2);
            if (isHighlighted) e.DrawLines(pen, vertices);
        }

        public override void Move(Point offset)
        {
            X0 += (Int16)offset.X;
            Y0 += (Int16)offset.Y;
        }

        public override void SerializeBinary(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], AnimationConstants._RotatedRect);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], X0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], Y0);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], W);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], H);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], AngleDeg);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], Color);
            bytePosition += 4;
        }

        public override void DeserializeBinary(ref int bytePosition, byte[] animationBytes)
        {
            X0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            Y0 = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            W = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            H = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            AngleDeg = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            Color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 4;
        }

        public override (PrimitiveFB, int) SerializeFB(FlatBufferBuilder builder)
        {
            return (PrimitiveFB.RotatedRectFB, RotatedRectFB.CreateRotatedRectFB(builder, X0, Y0, W, H, AngleDeg, Color).Value);
        }

        public override void DeserializeFB(Object data)
        {
            X0 = ((RotatedRectFB)data).X0;
            Y0 = ((RotatedRectFB)data).Y0;
            W = ((RotatedRectFB)data).W;
            H = ((RotatedRectFB)data).H;
            AngleDeg = ((RotatedRectFB)data).AngleDeg;
        }

        #region Screen mapped methods

        [JsonIgnore]
        public Point ScreenCenter
        {
            get { return new Point(ScreenX0, ScreenY0); }
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

        #region Mouse handling

        private Point mouseLocation = new(0, 0);
        private bool isMouseUp = true;
        private bool isMoving = false;
        private int selectedSide = -1;
        private int selectedVertex = -1;
        private Int16 offsetAngle = 0;
        private Point mousedownScreenCen;
        private Int16 mousedownW;
        private Int16 mousedownH;


        public override void MouseDown(Point point)
        {
            mouseLocation = point;
            isMouseUp = false;

            if (IsPointNearCenter(mouseLocation)) isMoving = true;
            else
            {
                selectedVertex = GetSelectedVertex(mouseLocation);
                if (selectedVertex > -1) offsetAngle = (Int16)(AngleDeg - GetAngle(mouseLocation));
                else
                {
                    selectedSide = GetSelectedSide(mouseLocation);
                    if (selectedSide > -1)
                    {
                        mousedownScreenCen = ScreenCenter;
                        mousedownW = W;
                        mousedownH = H;
                    }
                }
            }
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
                    int selectedVertex = GetSelectedVertex(point);
                    if (selectedVertex > -1)
                    {
                        int angle = GetAngle(point);
                        angle %= 180;
                        pctbxCanvas.Cursor = (angle < 90) ? Cursors.SizeNESW : Cursors.SizeNWSE;
                    }
                    else
                    {
                        int selectedSide = GetSelectedSide(point);
                        if (selectedSide > -1) pctbxCanvas.Cursor = Cursors.Hand;
                        else pctbxCanvas.Cursor = Cursors.Arrow;
                    }
                }
                return false;
            }

            if (isMoving) Move(unscaledMouseDelta);
            else
            {
                if (selectedVertex > -1)
                {
                    int angle = GetAngle(point);
                    AngleDeg = (Int16)(angle + offsetAngle);
                    angle %= 180;
                    pctbxCanvas.Cursor = (angle < 90) ? Cursors.SizeNESW : Cursors.SizeNWSE;
                }
                else
                {
                    if (selectedSide > -1)
                    {
                        Point newCenter = mousedownScreenCen;
                        Point unrotatedMousePosition = Utility.RotateFromReferencePoint(mousedownScreenCen, point, (Int16)(-angleDeg));
                        int polarity = ((selectedSide % 3) == 0) ? 1 : -1;
                        Point screenDistanceFromCenter = new((unrotatedMousePosition.X - mousedownScreenCen.X) * polarity, (unrotatedMousePosition.Y - mousedownScreenCen.Y) * polarity);
                        Point distanceFromCenter = new(screenDistanceFromCenter.X / AnimationConstants._ScaleFactor, screenDistanceFromCenter.Y / AnimationConstants._ScaleFactor);

                        if ((selectedSide % 2) == 0) H = (Int16)(distanceFromCenter.Y + (mousedownH / 2));
                        else W = (Int16)(distanceFromCenter.X + (mousedownW / 2));

                        int dH = H - mousedownH;
                        int dW = W - mousedownW;
                        int offsetX = ((dW + ((((mousedownW % 2) == 0) ^ (polarity < 0)) ? ((dW < 0) ? 0 : 1) : ((dW < 0) ? -1 : 0))) / 2);
                        int offsetY = ((dH + ((((mousedownH % 2) == 0) ^ (polarity < 0)) ? ((dH < 0) ? 0 : 1) : ((dH < 0) ? -1 : 0))) / 2);

                        offsetY *= AnimationConstants._ScaleFactor;
                        offsetX *= AnimationConstants._ScaleFactor;
                        newCenter.Y += (offsetY * polarity);
                        newCenter.X += (offsetX * polarity);
                        newCenter = Utility.RotateFromReferencePoint(mousedownScreenCen, newCenter, angleDeg);
                        newCenter.X /= AnimationConstants._ScaleFactor;
                        newCenter.Y /= AnimationConstants._ScaleFactor;
                        X0 = (Int16)newCenter.X;
                        Y0 = (Int16)newCenter.Y;
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
            selectedSide = -1;
            selectedVertex = -1;
        }

        #endregion

        public PointF[] GetVertices()
        {
            float halfWidth = ((float)W / 2) - (((W % 2) == 0) ? (float).5 : 0);
            float halfHeight = ((float)H / 2) - (((H % 2) == 0) ? (float).5 : 0);
            PointF bottomRight = new(W - halfWidth, H - halfHeight);
            PointF bottomLeft = new(-halfWidth, H - halfHeight);
            PointF topLeft = new(-halfWidth, -halfHeight);
            PointF topRight = new(W - halfWidth, -halfHeight);

            PointF[] vertices = [bottomRight, bottomLeft, topLeft, topRight];

            Matrix matrix = new();
            matrix.Rotate(angleDeg);
            matrix.TransformPoints(vertices);
            return vertices;
        }

        public Point[] GetScreenVertices()
        {
            PointF[] fltVertices = GetVertices();

            Matrix matrix = new();
            matrix.Scale(AnimationConstants._ScaleFactor, AnimationConstants._ScaleFactor);
            matrix.TransformPoints(fltVertices);
            matrix.Reset();
            matrix.Translate(ScreenX0, ScreenY0);
            matrix.TransformPoints(fltVertices);

            Point[] vertices = new Point[fltVertices.Length];
            for (int index = 0; index < fltVertices.Length; index++)
            {
                vertices[index] = new Point((int)fltVertices[index].X, (int)fltVertices[index].Y);
            }

            return vertices;
        }

        public int GetSelectedVertex(Point point)
        {
            Point[] vertices = GetScreenVertices();
            int margin = 4;
            int selectedVertex = -1;
            for (int index = 0; index < vertices.Length; index++)
            {
                if (Utility.ArePointsWithinMargin(point, vertices[index], margin * AnimationConstants._ScaleFactor))
                {
                    selectedVertex = index;
                }

            }
            return selectedVertex;
        }

        public int GetSelectedSide(Point point)
        {
            Point[] lines = GetScreenVertices();
            int margin = 4;
            int selectedLine = -1;
            for (int index = 0; index < lines.Length; index++)
            {
                if (Utility.DistanceFromLine(lines[index], lines[(index + 1) % 4], point) < margin)
                {
                    selectedLine = index;
                }

            }
            return selectedLine;
        }

        public Int16 GetAngle(Point point)
        {
            return Utility.GetAngleFromReferencePoint(ScreenCenter, point);
        }

        public bool IsPointNearCenter(Point point)
        {
            int margin = 4;
            return Utility.ArePointsWithinMargin(ScreenCenter, point, margin);
        }

        #endregion

    }
}


/*
        Original vertex handling code

        //public Point[] GetVertices()
        //{
        //    Point center = new Point(W / 2, H / 2);
        //    Point topLeft = new Point(-center.X, -center.Y);
        //    Point topRight = new Point(center.X, -center.Y);
        //    Point bottomRight = new Point(center.X, center.Y);
        //    Point bottomLeft = new Point(-center.X, center.Y);

        //    Point[] vertices = new Point[] { bottomRight, bottomLeft, topLeft, topRight };

        //    Matrix matrix = new Matrix();
        //    matrix.Rotate(angleDeg);
        //    matrix.TransformPoints(vertices);
        //    return vertices;
        //}

        //public Point[] GetScreenVertices()
        //{
        //    Point[] vertices = GetVertices();

        //    Matrix matrix = new Matrix();
        //    matrix.Scale(AnimationConstants._ScaleFactor, AnimationConstants._ScaleFactor);
        //    matrix.TransformPoints(vertices);
        //    matrix.Reset();
        //    matrix.Translate(ScreenCenX, ScreenCenY);
        //    matrix.TransformPoints(vertices);

        //    return vertices;
        //}

 */