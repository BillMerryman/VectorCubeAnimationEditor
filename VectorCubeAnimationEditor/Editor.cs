using System.Globalization;
using System.Net.Sockets;
using System.IO;

namespace VectorCubeAnimationEditor
{
    public partial class Editor : Form
    {
        Animation animation;
        AnimationFrame? currentFrame;
        Primitive? currentPrimitive;
        bool isResizing = false;
        bool isMoving = false;
        int isVertexMoving = -1;
        Point MouseLocation = new Point(0, 0);

        public Editor()
        {
            InitializeComponent();
            animation = new Animation();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            txtFrameCount.Text = animation.FrameCount.ToString();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                byte[] serialized = File.ReadAllBytes(openFile.FileName);
                animation.deserialize(serialized);

                if (animation.FrameCount == 0) return;
                AnimationFrame frame = animation.GetFrameNumber(1);
                btnRemoveCurrentFrame.Enabled = true;
                if (animation.FrameCount > 1) EnableFrameNavigation();

                txtFrameCount.Text = animation.FrameCount.ToString();
                txtCurrentFrame.Text = animation.FrameCount.ToString();
                SetCurrentFrame(frame);
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                byte[] serialized = animation.serialize();
                File.WriteAllBytes(saveFile.FileName, serialized);
            }
        }

        private void btnTransmitFile_Click(object sender, EventArgs e)
        {
            byte[] serialized = animation.serialize();
            string IPAddress = txtIPFirstOctet.Text;
            IPAddress += ".";
            IPAddress += txtIPSecondOctet.Text;
            IPAddress += ".";
            IPAddress += txtIPThirdOctet.Text;
            IPAddress += ".";
            IPAddress += txtIPFourthOctet.Text;
            try
            {
                using (TcpClient client = new TcpClient(IPAddress, 80))
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        // Transmit the byte array
                        stream.Write(serialized, 0, serialized.Length);

                        Console.WriteLine("Data transmitted successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void txtIPFirstOctet_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyValue = e.KeyChar.ToString();
            if (!char.IsDigit(Convert.ToChar(keyValue)) && Convert.ToChar(keyValue) != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtIPFirstOctet_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(txtIPFirstOctet.Text, out int intValue))
            {
                if (intValue < 0 || intValue > 255)
                {
                    MessageBox.Show("First octet must be between 0 and 255 inclusive.", "Alert!");
                    txtIPFirstOctet.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("First octet cannot be empty.", "Alert!");
                txtIPFirstOctet.Focus();
            }
        }

        private void txtIPSecondOctet_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyValue = e.KeyChar.ToString();
            if (!char.IsDigit(Convert.ToChar(keyValue)) && Convert.ToChar(keyValue) != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtIPSecondOctet_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(txtIPSecondOctet.Text, out int intValue))
            {
                if (intValue < 0 || intValue > 255)
                {
                    MessageBox.Show("Second octet must be between 0 and 255 inclusive.", "Alert!");
                    txtIPSecondOctet.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Second octet cannot be empty.", "Alert!");
                txtIPSecondOctet.Focus();
            }
        }

        private void txtIPThirdOctet_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyValue = e.KeyChar.ToString();
            if (!char.IsDigit(Convert.ToChar(keyValue)) && Convert.ToChar(keyValue) != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtIPThirdOctet_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(txtIPThirdOctet.Text, out int intValue))
            {
                if (intValue < 0 || intValue > 255)
                {
                    MessageBox.Show("Third octet must be between 0 and 255 inclusive.", "Alert!");
                    txtIPThirdOctet.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Third octet cannot be empty.", "Alert!");
                txtIPThirdOctet.Focus();
            }
        }

        private void txtIPFourthOctet_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyValue = e.KeyChar.ToString();
            if (!char.IsDigit(Convert.ToChar(keyValue)) && Convert.ToChar(keyValue) != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtIPFourthOctet_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(txtIPFourthOctet.Text, out int intValue))
            {
                if (intValue < 0 || intValue > 255)
                {
                    MessageBox.Show("Fourth octet must be between 0 and 255 inclusive.", "Alert!");
                    txtIPFourthOctet.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Fourth octet cannot be empty.", "Alert!");
                txtIPFourthOctet.Focus();
            }
        }

        private void btnAddFrame_Click(object sender, EventArgs e)
        {
            UInt16 fillColor;
            UInt32 duration;
            if (!Utility.GetUInt16FromRGBString(txtAddFrameFillColor.Text, out fillColor))
            {
                MessageBox.Show("Invalid fill color", "Alert!");
                return;
            }
            if (!Utility.GetUInt32FromString(txtAddFrameDuration.Text, out duration))
            {
                MessageBox.Show("Invalid duration", "Alert!");
                return;
            };
            AnimationFrame? newFrame = animation.AddFrame(fillColor, duration);
            if (newFrame == null) { return; }
            if (animation.FrameCount > 0) btnRemoveCurrentFrame.Enabled = true;
            if (animation.FrameCount > 1) EnableFrameNavigation();

            txtFrameCount.Text = animation.FrameCount.ToString();
            txtCurrentFrame.Text = animation.FrameCount.ToString();
            SetCurrentFrame(newFrame);
        }

        private void btnAddFrameFillColor_Click(object sender, EventArgs e)
        {
            DialogResult result = selectColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtAddFrameFillColor.Text = selectColor.Color.R.ToString("X2") +
                                          selectColor.Color.G.ToString("X2") +
                                          selectColor.Color.B.ToString("X2");
            }
        }

        private void btnRemoveCurrentFrame_Click(object sender, EventArgs e)
        {
            int currentFrameNumber = animation.RemoveFrame(currentFrame);
            txtFrameCount.Text = animation.FrameCount.ToString();

            if (animation.FrameCount < 2) DisableFrameNavigation();
            if (animation.FrameCount < 1)
            {
                currentFrame = null;
                txtCurrentFrame.Text = string.Empty;
                txtCurrentPrimitive.Text = string.Empty;
                btnRemoveCurrentPrimitive.Enabled = false;
                DisablePrimitiveCreation();
                DisablePrimitiveNavigation();
                HideAllPrimitiveFields();
                pctbxCanvas.Refresh();
                return;
            }

            while (currentFrameNumber > animation.FrameCount) --currentFrameNumber;
            txtCurrentFrame.Text = currentFrameNumber.ToString();
            SetCurrentFrame(animation.GetFrameNumber(currentFrameNumber));
        }

        private void btnPreviousFrame_Click(object sender, EventArgs e)
        {
            int currentFrameNumber = animation.GetNumberOfFrame(currentFrame);
            if (currentFrameNumber < 0) return;
            if (currentFrameNumber == 1) return;
            currentFrameNumber--;
            txtCurrentFrame.Text = currentFrameNumber.ToString();
            SetCurrentFrame(animation.GetFrameNumber(currentFrameNumber));
        }

        private void btnNextFrame_Click(object sender, EventArgs e)
        {
            int currentFrameNumber = animation.GetNumberOfFrame(currentFrame);
            if (currentFrameNumber == animation.FrameCount) return;
            currentFrameNumber++;
            txtCurrentFrame.Text = currentFrameNumber.ToString();
            SetCurrentFrame(animation.GetFrameNumber(currentFrameNumber));
        }

        private void btnUpdateCurrentFrame_Click(object sender, EventArgs e)
        {
            if (currentFrame == null) return;
            UInt32 duration;
            UInt16 fillColor;
            if (!Utility.GetUInt32FromString(txtFrameDuration.Text, out duration)) return;
            if (!Utility.GetUInt16FromRGBString(txtFrameFillColor.Text, out fillColor)) return;
            currentFrame.Duration = duration;
            currentFrame.FillColor = fillColor;
            pctbxCanvas.Refresh();
        }

        private void btnFrameFillColor_Click(object sender, EventArgs e)
        {
            DialogResult result = selectColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFrameFillColor.Text = selectColor.Color.R.ToString("X2") +
                                          selectColor.Color.G.ToString("X2") +
                                          selectColor.Color.B.ToString("X2");
            }
        }

        private void btnAddCircle_Click(object sender, EventArgs e)
        {
            AddPrimitive(AnimationConstants._Circle);
        }

        private void btnAddQuarterCircle_Click(object sender, EventArgs e)
        {
            AddPrimitive(AnimationConstants._QuarterCircle);
        }

        private void btnAddTriangle_Click(object sender, EventArgs e)
        {
            AddPrimitive(AnimationConstants._Triangle);
        }

        private void btnAddRoundRectangle_Click(object sender, EventArgs e)
        {
            AddPrimitive(AnimationConstants._RoundRect);
        }

        private void btnAddLine_Click(object sender, EventArgs e)
        {
            AddPrimitive(AnimationConstants._Line);
        }

        private void btnAddPrimitiveDrawColor_Click(object sender, EventArgs e)
        {
            DialogResult result = selectColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtAddPrimitiveDrawColor.Text = selectColor.Color.R.ToString("X2") +
                                                  selectColor.Color.G.ToString("X2") +
                                                  selectColor.Color.B.ToString("X2");
            }
        }

        private void btnRemoveCurrentPrimitive_Click(object sender, EventArgs e)
        {
            if (currentFrame == null) return;
            int currentPrimitiveNumber = currentFrame.RemovePrimitive(currentPrimitive);
            txtPrimitiveCount.Text = currentFrame.PrimitiveCount.ToString();

            if (currentFrame.PrimitiveCount < 2) DisablePrimitiveNavigation();
            if (currentFrame.PrimitiveCount < 1)
            {
                txtCurrentPrimitive.Text = string.Empty;
                DisablePrimitiveNavigation();
                HideAllPrimitiveFields();
                pctbxCanvas.Refresh();
                return;
            }

            while (currentPrimitiveNumber > currentFrame.PrimitiveCount) --currentPrimitiveNumber;
            txtCurrentPrimitive.Text = currentPrimitiveNumber.ToString();
            SetCurrentPrimitive(currentFrame.GetPrimitiveNumber(currentPrimitiveNumber));
        }

        private void btnPreviousPrimitive_Click(object sender, EventArgs e)
        {
            if (currentFrame == null) return;
            int currentPrimitiveNumber = currentFrame.GetNumberOfPrimitive(currentPrimitive);
            if (currentPrimitiveNumber < 0) return;
            if (currentPrimitiveNumber == 1) return;
            currentPrimitiveNumber--;
            txtCurrentPrimitive.Text = currentPrimitiveNumber.ToString();
            SetCurrentPrimitive(currentFrame.GetPrimitiveNumber(currentPrimitiveNumber));
        }

        private void btnNextPrimitive_Click(object sender, EventArgs e)
        {
            if (currentFrame == null) return;
            int currentPrimitiveNumber = currentFrame.GetNumberOfPrimitive(currentPrimitive);
            if (currentPrimitiveNumber == currentFrame.PrimitiveCount) return;
            currentPrimitiveNumber++;
            txtCurrentPrimitive.Text = currentPrimitiveNumber.ToString();
            SetCurrentPrimitive(currentFrame.GetPrimitiveNumber(currentPrimitiveNumber));
        }

        private void btnPrimitiveDrawColor_Click(object sender, EventArgs e)
        {
            DialogResult result = selectColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPrimitiveDrawColor.Text = selectColor.Color.R.ToString("X2") +
                                              selectColor.Color.G.ToString("X2") +
                                              selectColor.Color.B.ToString("X2");
            }
        }

        private void btnUpdateCurrentPrimitive_Click(object sender, EventArgs e)
        {
            if (currentPrimitive == null) return;
            switch (currentPrimitive.Type)
            {
                case AnimationConstants._Circle:
                    SetCircleFromDisplayFields(currentPrimitive.Circle);
                    break;
                case AnimationConstants._QuarterCircle:
                    SetQuarterCircleFromDisplayFields(currentPrimitive.QuarterCircle);
                    break;
                case AnimationConstants._Triangle:
                    SetTriangleFromDisplayFields(currentPrimitive.Triangle);
                    break;
                case AnimationConstants._RoundRect:
                    SetRoundRectFromDisplayFields(currentPrimitive.RoundRect);
                    break;
                case AnimationConstants._Line:
                    SetLineFromDisplayFields(currentPrimitive.Line);
                    break;
            }
            pctbxCanvas.Refresh();
        }

        private void pctbxCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentPrimitive == null) return;
            MouseLocation = e.Location;
            switch (currentPrimitive.Type)
            {
                case AnimationConstants._Circle:
                    Circle circle = currentPrimitive.Circle;
                    if (Utility.IsScreenPointOnRadius(MouseLocation, new Point(circle.X0, circle.Y0), circle.R))
                    {
                        isResizing = true;
                    }
                    if (Utility.IsScreenPointNearPoint(MouseLocation, new Point(circle.X0, circle.Y0)))
                    {
                        isMoving = true;
                    }
                    break;
                case AnimationConstants._QuarterCircle:
                    QuarterCircle quarterCircle = currentPrimitive.QuarterCircle;
                    if (Utility.IsScreenPointOnRadius(MouseLocation, new Point(quarterCircle.X0, quarterCircle.Y0), quarterCircle.R))
                    {
                        isResizing = true;
                    }
                    if (Utility.IsScreenPointNearPoint(MouseLocation, new Point(quarterCircle.X0, quarterCircle.Y0)))
                    {
                        isMoving = true;
                    }
                    break;
                case AnimationConstants._Triangle:
                    Triangle triangle = currentPrimitive.Triangle;
                    if (Utility.IsScreenPointNearPoint(MouseLocation, new Point(triangle.X0, triangle.Y0))) isVertexMoving = 0;
                    if (Utility.IsScreenPointNearPoint(MouseLocation, new Point(triangle.X1, triangle.Y1))) isVertexMoving = 1;
                    if (Utility.IsScreenPointNearPoint(MouseLocation, new Point(triangle.X2, triangle.Y2))) isVertexMoving = 2;
                    if (Utility.IsScreenPointNearPoint(MouseLocation, Utility.GetCentroid(triangle))) isMoving = true;
                    break;
                case AnimationConstants._RoundRect:
                    RoundRect roundRect = currentPrimitive.RoundRect;
                    int roundRectBottomRightX = (roundRect.X0 + roundRect.W);
                    int roundRectBottomRightY = (roundRect.Y0 + roundRect.H);
                    Point roundRectBottomRight = new Point(roundRectBottomRightX, roundRectBottomRightY);
                    if (Utility.IsScreenPointNearPoint(MouseLocation, roundRectBottomRight))
                    {
                        isResizing = true;
                    }
                    else if (Utility.IsPointInsideRoundRect(MouseLocation, roundRect))
                    {
                        isMoving = true;
                    }
                    break;
                case AnimationConstants._Line:
                    Line line = currentPrimitive.Line;
                    if (Utility.IsScreenPointNearPoint(MouseLocation, new Point(line.X0, line.Y0))) isVertexMoving = 0;
                    if (Utility.IsScreenPointNearPoint(MouseLocation, new Point(line.X1, line.Y1))) isVertexMoving = 1;
                    break;
            }
        }

        private void pctbxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentPrimitive == null) return;
            int mouseDeltaX = e.Location.X - MouseLocation.X;
            int mouseDeltaY = e.Location.Y - MouseLocation.Y;
            int primitiveDeltaX = (int)Math.Floor((double)mouseDeltaX / AnimationConstants._ScaleFactor);
            int primitiveDeltaY = (int)Math.Floor((double)mouseDeltaY / AnimationConstants._ScaleFactor);
            if (isResizing)
            {
                switch (currentPrimitive.Type)
                {
                    case AnimationConstants._Circle:
                        Circle circle = currentPrimitive.Circle;
                        int cRadius = circle.R;
                        if (cRadius + primitiveDeltaY > 0) circle.R += (Int16)primitiveDeltaY;
                        SetDisplayFieldsFromCircle(circle);
                        break;
                    case AnimationConstants._QuarterCircle:
                        QuarterCircle quarterCircle = currentPrimitive.QuarterCircle;
                        int qcRadius = quarterCircle.R;
                        if (qcRadius + primitiveDeltaY > 0) quarterCircle.R += (Int16)primitiveDeltaY;
                        SetDisplayFieldsFromQuarterCircle(quarterCircle);
                        break;
                    case AnimationConstants._RoundRect:
                        RoundRect roundRect = currentPrimitive.RoundRect;
                        int width = roundRect.W;
                        int height = roundRect.H;
                        if (width + primitiveDeltaX > 0) roundRect.W += (Int16)primitiveDeltaX;
                        if (height + primitiveDeltaY > 0) roundRect.H += (Int16)primitiveDeltaY;
                        SetDisplayFieldsFromRoundRect(roundRect);
                        break;
                }
                MouseLocation.X += primitiveDeltaX * AnimationConstants._ScaleFactor;
                MouseLocation.Y += primitiveDeltaY * AnimationConstants._ScaleFactor;
                pctbxCanvas.Refresh();
            }
            if (isMoving)
            {
                switch (currentPrimitive.Type)
                {
                    case AnimationConstants._Circle:
                        Circle circle = currentPrimitive.Circle;
                        circle.X0 += (Int16)primitiveDeltaX;
                        circle.Y0 += (Int16)primitiveDeltaY;
                        SetDisplayFieldsFromCircle(circle);
                        break;
                    case AnimationConstants._QuarterCircle:
                        QuarterCircle quarterCircle = currentPrimitive.QuarterCircle;
                        quarterCircle.X0 += (Int16)primitiveDeltaX;
                        quarterCircle.Y0 += (Int16)primitiveDeltaY;
                        SetDisplayFieldsFromQuarterCircle(quarterCircle);
                        break;
                    case AnimationConstants._Triangle:
                        Triangle triangle = currentPrimitive.Triangle;
                        triangle.X0 += (Int16)primitiveDeltaX;
                        triangle.Y0 += (Int16)primitiveDeltaY;
                        triangle.X1 += (Int16)primitiveDeltaX;
                        triangle.Y1 += (Int16)primitiveDeltaY;
                        triangle.X2 += (Int16)primitiveDeltaX;
                        triangle.Y2 += (Int16)primitiveDeltaY;
                        SetDisplayFieldsFromTriangle(triangle);
                        break;
                    case AnimationConstants._RoundRect:
                        RoundRect roundRect = currentPrimitive.RoundRect;
                        roundRect.X0 += (Int16)primitiveDeltaX;
                        roundRect.Y0 += (Int16)primitiveDeltaY;
                        SetDisplayFieldsFromRoundRect(roundRect);
                        break;
                }
                MouseLocation.X += primitiveDeltaX * AnimationConstants._ScaleFactor;
                MouseLocation.Y += primitiveDeltaY * AnimationConstants._ScaleFactor;
                pctbxCanvas.Refresh();
            }
            if (isVertexMoving > -1)
            {
                switch (currentPrimitive.Type)
                {
                    case AnimationConstants._Triangle:
                        Triangle triangle = currentPrimitive.Triangle;
                        if (isVertexMoving == 0)
                        {
                            triangle.X0 += (Int16)primitiveDeltaX;
                            triangle.Y0 += (Int16)primitiveDeltaY;
                        }
                        if (isVertexMoving == 1)
                        {
                            triangle.X1 += (Int16)primitiveDeltaX;
                            triangle.Y1 += (Int16)primitiveDeltaY;
                        }
                        if (isVertexMoving == 2)
                        {
                            triangle.X2 += (Int16)primitiveDeltaX;
                            triangle.Y2 += (Int16)primitiveDeltaY;
                        }
                        SetDisplayFieldsFromTriangle(triangle);
                        MouseLocation.X += primitiveDeltaX * AnimationConstants._ScaleFactor;
                        MouseLocation.Y += primitiveDeltaY * AnimationConstants._ScaleFactor;
                        break;
                    case AnimationConstants._Line:
                        Line line = currentPrimitive.Line;
                        if (isVertexMoving == 0)
                        {
                            line.X0 += (Int16)primitiveDeltaX;
                            line.Y0 += (Int16)primitiveDeltaY;
                        }
                        if (isVertexMoving == 1)
                        {
                            line.X1 += (Int16)primitiveDeltaX;
                            line.Y1 += (Int16)primitiveDeltaY;
                        }
                        SetDisplayFieldsFromLine(line);
                        MouseLocation.X += primitiveDeltaX * AnimationConstants._ScaleFactor;
                        MouseLocation.Y += primitiveDeltaY * AnimationConstants._ScaleFactor;
                        break;
                }
                pctbxCanvas.Refresh();
            }
        }

        private void pctbxCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
            isResizing = false;
            isVertexMoving = -1;
        }

        private void pctbxCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (currentFrame != null)
            {
                string strRGB = Utility.GetRGBStringFromUIint16(currentFrame.FillColor);
                int red = int.Parse(strRGB.Substring(0, 2), NumberStyles.HexNumber);
                int green = int.Parse(strRGB.Substring(2, 2), NumberStyles.HexNumber);
                int blue = int.Parse(strRGB.Substring(4, 2), NumberStyles.HexNumber);
                Color color = Color.FromArgb(red, green, blue);
                e.Graphics.Clear(color);
                if (currentFrame.PrimitiveCount > 0)
                {
                    for (int index = 1; index <= currentFrame.PrimitiveCount; index++)
                    {
                        Primitive? Primitive = currentFrame.GetPrimitiveNumber(index);
                        if (Primitive != null)
                        {
                            Utility.DrawPrimitive(e.Graphics, Primitive);
                        }
                    }
                }
            }
            else
            {
                e.Graphics.Clear(Color.Black);
            }
        }

        //Non-delegate functions

        private void SetCurrentFrame(AnimationFrame? frame)
        {
            if (frame == null) return;
            currentFrame = frame;
            txtFrameFillColor.Text = Utility.GetRGBStringFromUIint16(frame.FillColor);
            txtFrameDuration.Text = frame.Duration.ToString();
            txtPrimitiveCount.Text = frame.PrimitiveCount.ToString();
            EnablePrimitiveCreation();
            if (currentFrame.PrimitiveCount > 1) EnablePrimitiveNavigation();
            if (currentFrame.PrimitiveCount > 0)
            {
                btnRemoveCurrentPrimitive.Enabled = true;
                int primitiveNumber = 1;
                txtCurrentPrimitive.Text = primitiveNumber.ToString();
                SetCurrentPrimitive(currentFrame.GetPrimitiveNumber(primitiveNumber));
                btnRemoveCurrentPrimitive.Enabled = true;
            }
            else
            {
                txtCurrentPrimitive.Text = string.Empty;
                btnRemoveCurrentPrimitive.Enabled = false;
                DisablePrimitiveNavigation();
                HideAllPrimitiveFields();
                pctbxCanvas.Refresh();
            }

        }

        private void EnableFrameNavigation()
        {
            btnPreviousFrame.Enabled = true;
            btnNextFrame.Enabled = true;
        }

        private void DisableFrameNavigation()
        {
            btnPreviousFrame.Enabled = false;
            btnNextFrame.Enabled = false;
        }

        private void EnablePrimitiveCreation()
        {
            btnAddCircle.Enabled = true;
            btnAddQuarterCircle.Enabled = true;
            btnAddTriangle.Enabled = true;
            btnAddRoundRectangle.Enabled = true;
            btnAddLine.Enabled = true;
        }

        private void DisablePrimitiveCreation()
        {
            btnAddCircle.Enabled = false;
            btnAddQuarterCircle.Enabled = false;
            btnAddTriangle.Enabled = false;
            btnAddRoundRectangle.Enabled = false;
            btnAddLine.Enabled = false;
        }

        private void SetCurrentPrimitive(Primitive? Primitive)
        {
            if (Primitive == null) return;
            HideAllPrimitiveFields();
            currentPrimitive = Primitive;
            txtPrimitiveDrawColor.Text = Utility.GetDrawColorStringFromPrimitive(currentPrimitive.Type, currentPrimitive);
            switch (Primitive.Type)
            {
                case AnimationConstants._Circle:
                    grpbxCircle.Visible = true;
                    SetDisplayFieldsFromCircle(Primitive.Circle);
                    break;
                case AnimationConstants._QuarterCircle:
                    grpbxQuarterCircle.Visible = true;
                    SetDisplayFieldsFromQuarterCircle(Primitive.QuarterCircle);
                    break;
                case AnimationConstants._Triangle:
                    grpbxTriangle.Visible = true;
                    SetDisplayFieldsFromTriangle(Primitive.Triangle);
                    break;
                case AnimationConstants._RoundRect:
                    grpbxRoundRect.Visible = true;
                    SetDisplayFieldsFromRoundRect(Primitive.RoundRect);
                    break;
                case AnimationConstants._Line:
                    grpbxLine.Visible = true;
                    SetDisplayFieldsFromLine(Primitive.Line);
                    break;
            }
            pctbxCanvas.Refresh();
        }

        private void AddPrimitive(ushort primitiveType)
        {
            if (currentFrame == null) { return; }
            if (currentFrame.PrimitiveCount > AnimationConstants._MaxPrimitiveCount) return;

            UInt16 color;
            if (!Utility.GetUInt16FromRGBString(txtAddPrimitiveDrawColor.Text, out color)) return;
            Primitive? Primitive = currentFrame.AddPrimitive();
            if (Primitive == null) return;
            Primitive.Type = primitiveType;
            switch (primitiveType)
            {
                case AnimationConstants._Circle:
                    Primitive.Circle.Color = color;
                    break;
                case AnimationConstants._QuarterCircle:
                    Primitive.QuarterCircle.Color = color;
                    break;
                case AnimationConstants._Triangle:
                    Primitive.Triangle.Color = color;
                    break;
                case AnimationConstants._RoundRect:
                    Primitive.RoundRect.Color = color;
                    break;
                case AnimationConstants._Line:
                    Primitive.Line.Color = color;
                    break;
            }
            if (currentFrame.PrimitiveCount == 1)
            {
                EnablePrimitiveNavigation();
            }

            txtPrimitiveCount.Text = currentFrame.PrimitiveCount.ToString();
            txtCurrentPrimitive.Text = currentFrame.PrimitiveCount.ToString();
            SetCurrentPrimitive(Primitive);
        }

        private void EnablePrimitiveNavigation()
        {
            btnUpdateCurrentPrimitive.Enabled = true;
            btnRemoveCurrentPrimitive.Enabled = true;
            btnPreviousPrimitive.Enabled = true;
            btnNextPrimitive.Enabled = true;
        }

        private void DisablePrimitiveNavigation()
        {
            btnPreviousPrimitive.Enabled = false;
            btnNextPrimitive.Enabled = false;
        }

        private void HideAllPrimitiveFields()
        {
            grpbxCircle.Visible = false;
            grpbxQuarterCircle.Visible = false;
            grpbxTriangle.Visible = false;
            grpbxRoundRect.Visible = false;
            grpbxLine.Visible = false;
        }

        private void SetDisplayFieldsFromCircle(Circle circle)
        {
            txtCircleX0.Text = circle.X0.ToString();
            txtCircleY0.Text = circle.Y0.ToString();
            txtCircleRadius.Text = circle.R.ToString();
        }

        private void SetCircleFromDisplayFields(Circle circle)
        {
            if (circle == null) return;
            if (!Utility.GetInt16FromString(txtCircleX0.Text, out Int16 X0)) return;
            if (!Utility.GetInt16FromString(txtCircleY0.Text, out Int16 Y0)) return;
            if (!Utility.GetInt16FromString(txtCircleRadius.Text, out Int16 R)) return;
            if (!Utility.GetUInt16FromRGBString(txtPrimitiveDrawColor.Text, out UInt16 Color)) return;

            circle.X0 = X0;
            circle.Y0 = Y0;
            circle.R = R;
            circle.Color = Color;
        }

        private void SetDisplayFieldsFromQuarterCircle(QuarterCircle quarterCircle)
        {
            txtQuarterCircleX0.Text = quarterCircle.X0.ToString();
            txtQuarterCircleY0.Text = quarterCircle.Y0.ToString();
            txtQuarterCircleRadius.Text = quarterCircle.R.ToString();
            txtQuarterCircleQuadrants.Text = quarterCircle.Quadrants.ToString();
            txtQuarterCircleDelta.Text = quarterCircle.Delta.ToString();
        }

        private void SetQuarterCircleFromDisplayFields(QuarterCircle quarterCircle)
        {
            if (quarterCircle == null) return;
            if (!Utility.GetInt16FromString(txtQuarterCircleX0.Text, out Int16 X0)) return;
            if (!Utility.GetInt16FromString(txtQuarterCircleY0.Text, out Int16 Y0)) return;
            if (!Utility.GetInt16FromString(txtQuarterCircleRadius.Text, out Int16 R)) return;
            if (!Utility.GetByteFromString(txtQuarterCircleQuadrants.Text, out Byte Quadrants)) return;
            if (!Utility.GetInt16FromString(txtQuarterCircleDelta.Text, out Int16 Delta)) return;
            if (!Utility.GetUInt16FromRGBString(txtPrimitiveDrawColor.Text, out UInt16 Color)) return;

            quarterCircle.X0 = X0;
            quarterCircle.Y0 = Y0;
            quarterCircle.R = R;
            quarterCircle.Quadrants = Quadrants;
            quarterCircle.Delta = Delta;
            quarterCircle.Color = Color;
        }

        private void SetDisplayFieldsFromTriangle(Triangle triangle)
        {
            txtTriangleX0.Text = triangle.X0.ToString();
            txtTriangleY0.Text = triangle.Y0.ToString();
            txtTriangleX1.Text = triangle.X1.ToString();
            txtTriangleY1.Text = triangle.Y1.ToString();
            txtTriangleX2.Text = triangle.X2.ToString();
            txtTriangleY2.Text = triangle.Y2.ToString();
        }

        private void SetTriangleFromDisplayFields(Triangle triangle)
        {
            if (triangle == null) return;
            if (!Utility.GetInt16FromString(txtTriangleX0.Text, out Int16 X0)) return;
            if (!Utility.GetInt16FromString(txtTriangleY0.Text, out Int16 Y0)) return;
            if (!Utility.GetInt16FromString(txtTriangleX1.Text, out Int16 X1)) return;
            if (!Utility.GetInt16FromString(txtTriangleY1.Text, out Int16 Y1)) return;
            if (!Utility.GetInt16FromString(txtTriangleX2.Text, out Int16 X2)) return;
            if (!Utility.GetInt16FromString(txtTriangleY2.Text, out Int16 Y2)) return;
            if (!Utility.GetUInt16FromRGBString(txtPrimitiveDrawColor.Text, out UInt16 Color)) return;

            triangle.X0 = X0;
            triangle.Y0 = Y0;
            triangle.X1 = X1;
            triangle.Y1 = Y1;
            triangle.X2 = X2;
            triangle.Y2 = Y2;
            triangle.Color = Color;
        }

        private void SetDisplayFieldsFromRoundRect(RoundRect roundRect)
        {
            txtRoundRectX0.Text = roundRect.X0.ToString();
            txtRoundRectY0.Text = roundRect.Y0.ToString();
            txtRoundRectW.Text = roundRect.W.ToString();
            txtRoundRectH.Text = roundRect.H.ToString();
            txtRoundRectRadius.Text = roundRect.Radius.ToString();
        }

        private void SetRoundRectFromDisplayFields(RoundRect roundRect)
        {
            if (roundRect == null) return;
            if (!Utility.GetInt16FromString(txtRoundRectX0.Text, out Int16 X0)) return;
            if (!Utility.GetInt16FromString(txtRoundRectY0.Text, out Int16 Y0)) return;
            if (!Utility.GetInt16FromString(txtRoundRectW.Text, out Int16 W)) return;
            if (!Utility.GetInt16FromString(txtRoundRectH.Text, out Int16 H)) return;
            if (!Utility.GetInt16FromString(txtRoundRectRadius.Text, out Int16 Radius)) return;
            if (!Utility.GetUInt16FromRGBString(txtPrimitiveDrawColor.Text, out UInt16 Color)) return;

            roundRect.X0 = X0;
            roundRect.Y0 = Y0;
            roundRect.W = W;
            roundRect.H = H;
            roundRect.Radius = Radius;
            roundRect.Color = Color;
        }

        private void SetDisplayFieldsFromLine(Line line)
        {
            txtLineX0.Text = line.X0.ToString();
            txtLineY0.Text = line.Y0.ToString();
            txtLineX1.Text = line.X1.ToString();
            txtLineY1.Text = line.Y1.ToString();
        }

        private void SetLineFromDisplayFields(Line line)
        {
            if (line == null) return;
            if (!Utility.GetInt16FromString(txtLineX0.Text, out Int16 X0)) return;
            if (!Utility.GetInt16FromString(txtLineY0.Text, out Int16 Y0)) return;
            if (!Utility.GetInt16FromString(txtLineX1.Text, out Int16 X1)) return;
            if (!Utility.GetInt16FromString(txtLineY1.Text, out Int16 Y1)) return;
            if (!Utility.GetUInt16FromRGBString(txtPrimitiveDrawColor.Text, out UInt16 Color)) return;

            line.X0 = X0;
            line.Y0 = Y0;
            line.X1 = X1;
            line.Y1 = Y1;
            line.Color = Color;
        }

        //Disabled Text Fields

        private void txtFrameCount_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void txtCurrentFrame_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void txtPrimitiveCount_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void txtCurrentPrimitive_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

    }
}
