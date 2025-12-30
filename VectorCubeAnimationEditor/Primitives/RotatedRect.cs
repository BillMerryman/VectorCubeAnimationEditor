using System;
using System.Buffers.Binary;
using System.Drawing.Drawing2D;

namespace VectorCubeAnimationEditor
{
    internal class RotatedRect : Primitive
    {
        private Int16 cenX;
        private Int16 cenY;
        private Int16 w;
        private Int16 h;
        private Int16 angleDeg;
        private UInt16 color;

        public Int16 CenX
        {
            get { return cenX; }
            set { cenX = value; }
        }

        public Int16 CenY
        {
            get { return cenY; }
            set { cenY = value; }
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
            CenX = AnimationConstants.SCREEN_CENTER_X;
            CenY = AnimationConstants.SCREEN_CENTER_Y;
            W = AnimationConstants.DEFAULT_PRIMITIVE_SIZE;
            H = AnimationConstants.DEFAULT_PRIMITIVE_SIZE;
            AngleDeg = 0;
            Color = 0;
        }

        public RotatedRect(RotatedRect rotatingRectangle)
        {
            CenX = rotatingRectangle.CenX;
            CenY = rotatingRectangle.CenY;
            W = rotatingRectangle.W;
            H = rotatingRectangle.H;
            AngleDeg = rotatingRectangle.AngleDeg;
            Color = rotatingRectangle.Color;
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
            CenX += (Int16)offset.X;
            CenY += (Int16)offset.Y;
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], AnimationConstants._RotatedRect);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], CenX);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan()[bytePosition..], CenY);
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

        public override void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            CenX = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
            bytePosition += 2;
            CenY = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan()[bytePosition..]);
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

        #region Screen mapped methods

        public Point ScreenCen
        {
            get { return new Point(ScreenCenX, ScreenCenY); }
        }

        public Int16 ScreenCenX
        {
            get { return (Int16)(CenX * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenCenY
        {
            get { return (Int16)(CenY * AnimationConstants._ScaleFactor); }
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
                        mousedownScreenCen = ScreenCen;
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
                        CenX = (Int16)newCenter.X;
                        CenY = (Int16)newCenter.Y;
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
            matrix.Translate(ScreenCenX, ScreenCenY);
            matrix.TransformPoints(fltVertices);

            Point[] vertices = new Point[fltVertices.Length];
            for(int index  = 0; index < fltVertices.Length; index++)
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
            return Utility.GetAngleFromReferencePoint(ScreenCen, point);
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