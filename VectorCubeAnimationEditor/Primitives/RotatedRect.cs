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
            set { w = value; }
        }

        public Int16 H
        {
            get { return h; }
            set { h = value; }
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
            Pen pen = new Pen(drawColor.ColorToInverse());
            pen.DashStyle = DashStyle.Dash;

            // Split rectangle into two triangles (diagonal from top-left to bottom-right)
            Point[] vertices = GetScreenVertices();
            Point[] triangle1 = new Point[] { vertices[0], vertices[1], vertices[2] };
            Point[] triangle2 = new Point[] { vertices[2], vertices[3], vertices[0] };
            e.FillPolygon(brush, triangle1);
            e.FillPolygon(brush, triangle2);
            if (isHighlighted)
            {
                e.DrawEllipse(pen,
                                (float)(ScreenCenX - ScreenHalfDiagonal),
                                (float)(ScreenCenY - ScreenHalfDiagonal),
                                (float)ScreenHalfDiagonal * 2,
                                (float)ScreenHalfDiagonal * 2);
            }
        }

        public override void Move(Point offset)
        {
            CenX += (Int16)offset.X;
            CenY += (Int16)offset.Y;
        }

        public override void Serialize(ref int bytePosition, byte[] animationBytes)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AnimationConstants._RotatedRect);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), CenX);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), CenY);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), W);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), H);
            bytePosition += 2;
            BinaryPrimitives.WriteInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), AngleDeg);
            bytePosition += 2;
            BinaryPrimitives.WriteUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition), Color);
            bytePosition += 4;
        }

        public override void Deserialize(ref int bytePosition, byte[] animationBytes)
        {
            CenX = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            CenY = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            W = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            H = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            AngleDeg = BinaryPrimitives.ReadInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
            bytePosition += 2;
            Color = BinaryPrimitives.ReadUInt16LittleEndian(animationBytes.AsSpan().Slice(bytePosition));
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

        public Int16 ScreenW
        {
            get { return (Int16)(W * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenH
        {
            get { return (Int16)(H * AnimationConstants._ScaleFactor); }
        }

        public Point ScreenRectangleCenter
        {
            get { return new Point((W / 2) * AnimationConstants._ScaleFactor, (H / 2) * AnimationConstants._ScaleFactor); }
        }

        public Int16 ScreenHalfDiagonal
        {
            get { return (Int16)(Math.Sqrt(Math.Pow(ScreenRectangleCenter.X + 1, 2) + Math.Pow(ScreenRectangleCenter.Y + 1, 2))); }
        }


        #region Mouse handling

        private Point MouseLocation = new Point(0, 0);
        private bool isMouseUp = true;
        private bool isMoving = false;
        private int SelectedSide = -1;
        private int SelectedVertex = -1;
        private Int16 offsetAngle = 0;
        private Point mousedownScreenCen;
        private Int16 mousedownW;
        private Int16 mousedownH;


        public override void MouseDown(Point point)
        {
            MouseLocation = point;
            isMouseUp = false;
            SelectedVertex = GetSelectedVertex(MouseLocation);

            if (SelectedVertex < 0)
            {
                SelectedSide = GetSelectedSide(MouseLocation);
                if (SelectedSide < 0)
                {
                    if (IsPointNearCenter(MouseLocation))
                    {
                        isMoving = true;
                    }
                }
                else
                {
                    mousedownScreenCen = ScreenCen;
                    mousedownW = W;
                    mousedownH = H;
                }
            }
            else
            {
                offsetAngle = (Int16)(AngleDeg - GetAngle(MouseLocation));
            }

        }

        public override bool MouseMove(Point point, PictureBox pctbxCanvas)
        {
            Point mouseDelta = new Point(point.X - MouseLocation.X, point.Y - MouseLocation.Y);
            Point unscaledMouseDelta = new Point((int)Math.Floor((double)mouseDelta.X / AnimationConstants._ScaleFactor),
                                                (int)Math.Floor((double)mouseDelta.Y / AnimationConstants._ScaleFactor));

            if (isMouseUp)
            {
                if (IsPointNearCenter(point)) pctbxCanvas.Cursor = Cursors.Hand;
                else if (GetSelectedSide(point) > -1) pctbxCanvas.Cursor = Cursors.Hand;
                else if (GetSelectedVertex(point) > -1) pctbxCanvas.Cursor = Cursors.Hand;
                else pctbxCanvas.Cursor = Cursors.Arrow;
                return false;
            }

            if (SelectedVertex > -1)
            {
                AngleDeg = (Int16)(GetAngle(point) + offsetAngle);
            }
            if (SelectedSide > -1)
            {
                Point[] vertices = GetScreenVertices();
                switch (SelectedSide)
                {
                    case 0:
                        //rotate the mouse to unrotated position and find distance from center, all in screen units
                        Point unrotatedMousesPosition = Utility.RotateFromReferencePoint(mousedownScreenCen, point, (Int16)(-angleDeg));
                        Int16 ScreenDistanceFromCenter = (Int16)(unrotatedMousesPosition.Y - mousedownScreenCen.Y);

                        Int16 DistanceFromCenter = (Int16)((ScreenDistanceFromCenter / AnimationConstants._ScaleFactor) + (mousedownH % 2));
                        Int16 newH = (Int16)(DistanceFromCenter + (mousedownH / 2));
                        Int16 dH = (Int16)(newH - mousedownH);
                        Point newCenter = mousedownScreenCen;

                        if ((mousedownH % 2) == 0)
                        {
                            if (dH > 0)
                            {
                                newCenter.Y += (((dH / 2) + (newH % 2)) * AnimationConstants._ScaleFactor);
                            }
                            else
                            {
                                newCenter.Y += ((dH / 2) * AnimationConstants._ScaleFactor);
                            }
                        }
                        else
                        {
                            if (dH > 0)
                            {
                                newCenter.Y += ((dH / 2) * AnimationConstants._ScaleFactor);
                            }
                            else
                            {
                                newCenter.Y += (((dH / 2) + (newH % 2)) * AnimationConstants._ScaleFactor);
                            }
                        }

                        newCenter = Utility.RotateFromReferencePoint(mousedownScreenCen, newCenter, angleDeg);
                        newCenter.X /= AnimationConstants._ScaleFactor;
                        newCenter.Y /= AnimationConstants._ScaleFactor;
                        CenX = (Int16)newCenter.X;
                        CenY = (Int16)newCenter.Y;
                        H = newH;
                        break;
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                }
            }
            if (isMoving) Move(unscaledMouseDelta);
  
            MouseLocation.X += unscaledMouseDelta.X * AnimationConstants._ScaleFactor;
            MouseLocation.Y += unscaledMouseDelta.Y * AnimationConstants._ScaleFactor;

            return true;
        }

        public override void MouseUp()
        {
            isMouseUp = true;
            isMoving = false;
            SelectedSide = -1;
            SelectedVertex = -1;
        }

        #endregion

        public bool IsPointNearCenter(Point point)
        {
            int margin = 4;
            return Utility.ArePointsWithinMargin(ScreenCen, point, margin);
        }

        public bool IsPointOnRadius(Point point)
        {
            int margin = 4;
            return Utility.IsPointOnRadius(ScreenCen, point, ScreenHalfDiagonal, margin);
        }


        public PointF[] GetVertices()
        {
            float halfWidth = ((float)W / 2) - (((W % 2) == 0) ? (float).5 : 0);
            float halfHeight = ((float)H / 2) - (((H % 2) == 0) ? (float).5 : 0);
            PointF bottomRight = new PointF(W - halfWidth, H - halfHeight);
            PointF bottomLeft = new PointF(-halfWidth, H - halfHeight);
            PointF topLeft = new PointF(-halfWidth, -halfHeight);
            PointF topRight = new PointF(W - halfWidth, -halfHeight);

            PointF[] vertices = new PointF[] { bottomRight, bottomLeft, topLeft, topRight };

            Matrix matrix = new Matrix();
            matrix.Rotate(angleDeg);
            matrix.TransformPoints(vertices);
            return vertices;
        }

        public Point[] GetScreenVertices()
        {
            PointF[] fltVertices = GetVertices();

            Matrix matrix = new Matrix();
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