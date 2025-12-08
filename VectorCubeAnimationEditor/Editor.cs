using System.Globalization;
using System.Net.Sockets;

namespace VectorCubeAnimationEditor
{
    public partial class Editor : Form
    {
        Animation animation;
        AnimationFrame? currentFrame;
        Primitive? currentPrimitive;
        bool highlightCurrent = false;
        Point MouseLocation = new(0, 0);

        public Editor()
        {
            InitializeComponent();
            animation = new Animation();
            SetToolTips();
        }

        #region Control Delegates
        private void Editor_Load(object sender, EventArgs e)
        {
            txtFrameCount.Text = animation.FrameCount.ToString();
        }

        #region File handling delegates

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                byte[] animationBytes = File.ReadAllBytes(openFile.FileName);
                animation = new Animation();
                animation.Deserialize(animationBytes);

                if (animation.FrameCount == 0)
                {
                    currentFrame = null;
                    currentPrimitive = null;
                    DisableCurrentFrameManipulation();
                    DisableFrameNavigation();
                    DisablePrimitiveCreation();
                    DisablePrimitiveNavigation();
                    HideAllPrimitiveFields();
                    pctbxCanvas.Refresh();
                    return;
                }
                AnimationFrame? frame = animation.GetFrame(0);
                if (frame != null)
                {
                    SetCurrentFrame(frame);
                    EnableCurrentFrameManipulation();
                    SetFrameNavigation();
                }

                txtFrameCount.Text = animation.FrameCount.ToString();
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            saveFile.DefaultExt = ".a4v";
            saveFile.Filter = "Animations 4 Vector (*.a4v)|*.a4v";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                byte[] animationBytes = animation.Serialize();
                File.WriteAllBytes(saveFile.FileName, animationBytes);
            }
        }

        private void btnTransmitFile_Click(object sender, EventArgs e)
        {
            byte[] commandBytes = Utility.getCommandBytes(AnimationConstants._Animation);
            byte[] animationBytes = animation.Serialize();
            string IPAddress = txtIPFirstOctet.Text;
            IPAddress += ".";
            IPAddress += txtIPSecondOctet.Text;
            IPAddress += ".";
            IPAddress += txtIPThirdOctet.Text;
            IPAddress += ".";
            IPAddress += txtIPFourthOctet.Text;
            try
            {
                using TcpClient client = new(IPAddress, 80);
                using NetworkStream stream = client.GetStream();
                stream.Write(commandBytes, 0, commandBytes.Length);
                stream.Write(animationBytes, 0, animationBytes.Length);
                Console.WriteLine("Data transmitted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void btnSaveToHeaderFile_Click(object sender, EventArgs e)
        {
            saveFile.DefaultExt = ".h";
            saveFile.Filter = "C Header Files (*.h)|*.h";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                byte[] animationBytes = animation.Serialize();
                try
                {
                    using (StreamWriter writer = new(saveFile.FileName))
                    {
                        writer.WriteLine("static const uint8_t animation_" + Path.GetFileNameWithoutExtension(saveFile.FileName) + "[] PROGMEM = {");
                        int index = 0;
                        while (index < animationBytes.Length)
                        {
                            writer.Write("0x");
                            writer.Write(animationBytes[index++].ToString("X2"));
                            if (index < animationBytes.Length) writer.Write(", ");
                            if (index % 16 == 0) writer.WriteLine("");
                        }
                        writer.WriteLine("");
                        writer.WriteLine("};");
                    }

                    Console.WriteLine("Text written to the file successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private void btnSendImage_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                Image image = new();
                Bitmap originalImage = new(openFile.FileName);
                Bitmap resizedImage = new(originalImage, new Size(AnimationConstants.SCREEN_WIDTH, AnimationConstants.SCREEN_HEIGHT));
                Utility.ConvertToRGB565(resizedImage, image.DisplayBuffer);
                byte[] commandBytes = Utility.getCommandBytes(AnimationConstants._Image);
                byte[] imageBytes = image.serialize();
                string IPAddress = txtIPFirstOctet.Text;
                IPAddress += ".";
                IPAddress += txtIPSecondOctet.Text;
                IPAddress += ".";
                IPAddress += txtIPThirdOctet.Text;
                IPAddress += ".";
                IPAddress += txtIPFourthOctet.Text;
                try
                {
                    using TcpClient client = new(IPAddress, 80);
                    using NetworkStream stream = client.GetStream();
                    stream.Write(commandBytes, 0, commandBytes.Length);
                    stream.Write(imageBytes, 0, imageBytes.Length);
                    Console.WriteLine("Data transmitted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        #endregion

        #region IP validation

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

        #endregion

        #region Frame management delegates

        private void btnAddFrame_Click(object sender, EventArgs e)
        {
            AddFrame();
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

        private void btnCurrentFrameFillColor_Click(object sender, EventArgs e)
        {
            DialogResult result = selectColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCurrentFrameFillColor.Text = selectColor.Color.R.ToString("X2") +
                                          selectColor.Color.G.ToString("X2") +
                                          selectColor.Color.B.ToString("X2");
            }
        }

        private void btnMoveFrameDown_Click(object sender, EventArgs e)
        {
            animation.MoveFrameDown(currentFrame);
            int currentFrameNumber = animation.IndexOf(currentFrame) + 1;
            txtCurrentFrameNumber.Text = currentFrameNumber.ToString();
            SetFrameNavigation();
        }

        private void btnUpdateCurrentFrame_Click(object sender, EventArgs e)
        {
            if (currentFrame == null) return;
            if (!Utility.GetUInt16FromRGBString(txtCurrentFrameFillColor.Text, out ushort fillColor))
            {
                MessageBox.Show("Enter or select a valid fill color for the current frame", "Alert!");
                return;
            }
            if (!Utility.GetUInt32FromString(txtCurrentFrameDuration.Text, out uint duration))
            {
                MessageBox.Show("Enter a valid duration for the current frame", "Alert!");
                return;
            }
            currentFrame.Duration = duration;
            currentFrame.FillColor = fillColor;
            pctbxCanvas.Refresh();
        }

        private void btnMoveFrameUp_Click(object sender, EventArgs e)
        {
            animation.MoveFrameUp(currentFrame);
            int currentFrameNumber = animation.IndexOf(currentFrame) + 1;
            txtCurrentFrameNumber.Text = currentFrameNumber.ToString();
            SetFrameNavigation();
        }

        private void btnPreviousFrame_Click(object sender, EventArgs e)
        {
            int currentFrameIndex = animation.IndexOf(currentFrame);
            if (currentFrameIndex < 1) return;
            currentFrameIndex--;
            AnimationFrame animationFrame = animation.GetFrame(currentFrameIndex);
            SetCurrentFrame(animationFrame);
        }

        private void btnRemoveCurrentFrame_Click(object sender, EventArgs e)
        {
            RemoveCurrentFrame();
        }

        private void btnDuplicateCurrentFrame_Click(object sender, EventArgs e)
        {
            if (currentFrame == null) return;
            AnimationFrame? newFrame = animation.DuplicateFrame(currentFrame);
            if (newFrame == null) { return; }
            SetFrameNavigation();

            txtFrameCount.Text = animation.FrameCount.ToString();
            txtCurrentFrameNumber.Text = (animation.IndexOf(newFrame) + 1).ToString();
            SetCurrentFrame(newFrame);
        }

        private void btnNextFrame_Click(object sender, EventArgs e)
        {
            int currentFrameIndex = animation.IndexOf(currentFrame);
            if (currentFrameIndex == animation.FrameCount - 1) return;
            currentFrameIndex++;
            AnimationFrame animationFrame = animation.GetFrame(currentFrameIndex);
            SetCurrentFrame(animationFrame);
        }

        #endregion

        #region Primitive handling delegates

        private void btnAddCircle_Click(object sender, EventArgs e)
        {
            AddPrimitive(typeof(Circle));
        }

        private void btnAddTriangle_Click(object sender, EventArgs e)
        {
            AddPrimitive(typeof(Triangle));
        }

        private void btnAddRoundRect_Click(object sender, EventArgs e)
        {
            AddPrimitive(typeof(RoundRect));
        }

        private void btnAddRotatedRect_Click(object sender, EventArgs e)
        {
            AddPrimitive(typeof(RotatedRect));
        }

        private void btnAddLine_Click(object sender, EventArgs e)
        {
            AddPrimitive(typeof(Line));
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

        private void btnRemoveCurrentPrimitive_Click(object sender, EventArgs e)
        {
            RemoveCurrentPrimitive();
        }

        private void btnPreviousPrimitive_Click(object sender, EventArgs e)
        {
            if (currentFrame == null) return;
            if (currentFrame.PrimitiveCount < 2) return;
            if (currentPrimitive == null) return;
            int currentPrimitiveIndex = currentFrame.IndexOf(currentPrimitive);
            if (currentPrimitiveIndex < 1) return;
            currentPrimitiveIndex--;
            txtCurrentPrimitiveNumber.Text = (currentPrimitiveIndex + 1).ToString();
            Primitive tmpPrimitive = currentFrame.GetPrimitive(currentPrimitiveIndex);
            if (tmpPrimitive != null) SetCurrentPrimitive(tmpPrimitive);
        }

        private void btnNextPrimitive_Click(object sender, EventArgs e)
        {
            if (currentFrame == null) return;
            int currentPrimitiveIndex = currentFrame.IndexOf(currentPrimitive);
            if (currentPrimitiveIndex == currentFrame.PrimitiveCount - 1) return;
            currentPrimitiveIndex++;
            txtCurrentPrimitiveNumber.Text = (currentPrimitiveIndex + 1).ToString();
            Primitive tmpPrimitive = currentFrame.GetPrimitive(currentPrimitiveIndex);
            if (tmpPrimitive != null) SetCurrentPrimitive(tmpPrimitive);
        }

        private void btnCurrentPrimitiveDrawColor_Click(object sender, EventArgs e)
        {
            DialogResult result = selectColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCurrentPrimitiveDrawColor.Text = selectColor.Color.R.ToString("X2") +
                                              selectColor.Color.G.ToString("X2") +
                                              selectColor.Color.B.ToString("X2");
            }
        }

        private void btnUpdateCurrentPrimitive_Click(object sender, EventArgs e)
        {
            if (currentPrimitive == null) return;
            switch (currentPrimitive)
            {
                case Circle:
                    SetCircleFromDisplayFields((Circle)currentPrimitive);
                    break;
                case Triangle:
                    SetTriangleFromDisplayFields((Triangle)currentPrimitive);
                    break;
                case RoundRect:
                    SetRoundRectFromDisplayFields((RoundRect)currentPrimitive);
                    break;
                case RotatedRect:
                    SetRotatedRectFromDisplayFields((RotatedRect)currentPrimitive);
                    break;
                case Line:
                    SetLineFromDisplayFields((Line)currentPrimitive);
                    break;
            }
            pctbxCanvas.Refresh();
        }

        private void chkCircleTopLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCircleTopLeft.Checked)
            {
                ((Circle)currentPrimitive).Quadrants |= Circle.TopLeft;
            }
            else
            {
                ((Circle)currentPrimitive).Quadrants &= unchecked((byte)~Circle.TopLeft);
            }
            pctbxCanvas.Refresh();
        }

        private void chkCircleTopRight_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCircleTopRight.Checked)
            {
                ((Circle)currentPrimitive).Quadrants |= Circle.TopRight;
            }
            else
            {
                ((Circle)currentPrimitive).Quadrants &= unchecked((byte)~Circle.TopRight);
            }
            pctbxCanvas.Refresh();
        }

        private void chkCircleBottomLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCircleBottomLeft.Checked)
            {
                ((Circle)currentPrimitive).Quadrants |= Circle.BottomLeft;
            }
            else
            {
                ((Circle)currentPrimitive).Quadrants &= unchecked((byte)~Circle.BottomLeft);
            }
            pctbxCanvas.Refresh();
        }

        private void chkCircleBottomRight_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCircleBottomRight.Checked)
            {
                ((Circle)currentPrimitive).Quadrants |= Circle.BottomRight;
            }
            else
            {
                ((Circle)currentPrimitive).Quadrants &= unchecked((byte)~Circle.BottomRight);
            }
            pctbxCanvas.Refresh();
        }

        private void primitiveHighlightTimer_Tick(object sender, EventArgs e)
        {
            highlightCurrent = !highlightCurrent;
            pctbxCanvas.Refresh();
        }

        private void chkHighlightCurrentPrimitive_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                primitiveHighlightTimer.Enabled = true;
            }
            else
            {
                primitiveHighlightTimer.Enabled = false;
                highlightCurrent = false;
                pctbxCanvas.Refresh();
            }
        }

        #endregion

        #region Canvas handling delegates

        private void pctbxCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentPrimitive == null) return;
            MouseLocation = e.Location;
            currentPrimitive.MouseDown(MouseLocation);
        }

        private void pctbxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentPrimitive == null) return;

            if (currentPrimitive.MouseMove(e.Location, pctbxCanvas)) {
                SetDisplayFields(currentPrimitive);
                pctbxCanvas.Refresh();
            }
        }

        private void pctbxCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentPrimitive == null) return;
            currentPrimitive.MouseUp();
        }

        private void pctbxCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (currentFrame != null)
            {
                string strRGB = Utility.GetRGBStringFromUIint16(currentFrame.FillColor);
                int red = int.Parse(strRGB[..2], NumberStyles.HexNumber);
                int green = int.Parse(strRGB.Substring(2, 2), NumberStyles.HexNumber);
                int blue = int.Parse(strRGB.Substring(4, 2), NumberStyles.HexNumber);
                Color color = Color.FromArgb(red, green, blue);
                e.Graphics.Clear(color);
                if (currentFrame.PrimitiveCount > 0)
                {
                    for (int index = 0; index < currentFrame.PrimitiveCount; index++)
                    {
                        Primitive? primitive = currentFrame.GetPrimitive(index);
                        primitive?.Draw(e.Graphics, object.ReferenceEquals(primitive, currentPrimitive) && highlightCurrent);
                    }
                }
            }
            else
            {
                e.Graphics.Clear(Color.Black);
            }
        }

        #endregion

        #region Disabled text field delegates

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

        #endregion

        #endregion

        #region Object management methods

        private void AddFrame()
        {
            //Validate fields to make frame

            if (!Utility.GetUInt16FromRGBString(txtFrameFillColor.Text, out ushort fillColor))
            {
                MessageBox.Show("Select or enter a valid frame fill color", "Alert!");
                return;
            }
            if (!Utility.GetUInt32FromString(txtFrameDuration.Text, out uint duration))
            {
                MessageBox.Show("Enter a valid frame duration", "Alert!");
                return;
            }
            ;

            //Make frame
            AnimationFrame? newFrame = animation.AddFrame(fillColor, duration);
            if (newFrame == null) { return; }

            //Update interface
            SetCurrentFrame(newFrame);
            txtFrameCount.Text = animation.FrameCount.ToString();

        }

        private void RemoveCurrentFrame()
        {
            int currentFrameIndex = animation.RemoveFrame(currentFrame);
            txtFrameCount.Text = animation.FrameCount.ToString();

            if (animation.FrameCount < 2) DisableFrameNavigation();
            if (animation.FrameCount < 1)
            {
                currentFrame = null;
                currentPrimitive = null;
                DisableCurrentFrameManipulation();
                DisablePrimitiveCreation();
                DisablePrimitiveNavigation();
                HideAllPrimitiveFields();
                pctbxCanvas.Refresh();
                return;
            }

            while (currentFrameIndex >= animation.FrameCount) --currentFrameIndex;
            AnimationFrame animationFrame = animation.GetFrame(currentFrameIndex);

            SetCurrentFrame(animationFrame);
        }

        private void SetCurrentFrame(AnimationFrame frame)
        {
            currentFrame = frame;
            int currentFrameNumber = animation.IndexOf(currentFrame) + 1;
            txtCurrentFrameNumber.Text = currentFrameNumber.ToString();
            txtCurrentFrameFillColor.Text = Utility.GetRGBStringFromUIint16(frame.FillColor);
            txtCurrentFrameDuration.Text = frame.Duration.ToString();
            txtPrimitiveCount.Text = frame.PrimitiveCount.ToString();

            if (animation.FrameCount > 0) EnableCurrentFrameManipulation();
            SetFrameNavigation();
            EnablePrimitiveCreation();
            if (currentFrame.PrimitiveCount > 1) EnablePrimitiveNavigation();
            if (currentFrame.PrimitiveCount > 0)
            {
                int primitiveIndex = 0;
                Primitive? primitive = currentFrame.GetPrimitive(primitiveIndex);
                if (primitive != null)
                {
                    SetCurrentPrimitive(primitive);
                    txtCurrentPrimitiveNumber.Text = (primitiveIndex + 1).ToString();
                    EnablePrimitiveManagement();
                }
            }
            else
            {
                DisablePrimitiveNavigation();
                DisablePrimitiveManagement();
                HideAllPrimitiveFields();
            }
            pctbxCanvas.Refresh();
        }

        private void EnableCurrentFrameManipulation()
        {
            txtCurrentFrameNumber.Enabled = true;
            txtCurrentFrameFillColor.Enabled = true;
            btnCurrentFrameFillColor.Enabled = true;
            txtCurrentFrameDuration.Enabled = true;
            btnUpdateCurrentFrame.Enabled = true;
            btnRemoveCurrentFrame.Enabled = true;
            btnDuplicateCurrentFrame.Enabled = true;
        }

        private void DisableCurrentFrameManipulation()
        {
            txtCurrentPrimitiveNumber.Text = string.Empty;
            txtCurrentFrameNumber.Text = string.Empty;
            txtCurrentFrameNumber.Enabled = false;
            txtCurrentFrameFillColor.Text = string.Empty;
            txtCurrentFrameFillColor.Enabled = false;
            btnCurrentFrameFillColor.Enabled = false;
            txtCurrentFrameDuration.Text = string.Empty;
            txtCurrentFrameDuration.Enabled = false;
            btnUpdateCurrentFrame.Enabled = false;
            btnRemoveCurrentFrame.Enabled = false;
            btnDuplicateCurrentFrame.Enabled = false;

            DisablePrimitiveCreation();
        }

        private void SetFrameNavigation()
        {
            int currentFrameIndex = animation.IndexOf(currentFrame);
            btnMoveFrameDown.Enabled = false;
            btnMoveFrameUp.Enabled = false;
            btnPreviousFrame.Enabled = false;
            btnNextFrame.Enabled = false;


            if (currentFrameIndex > 0)
            {
                btnPreviousFrame.Enabled = true;
                btnMoveFrameDown.Enabled = true;
            }
            if (currentFrameIndex < animation.FrameCount - 1)
            {
                btnNextFrame.Enabled = true;
                btnMoveFrameUp.Enabled = true;
            }
        }

        private void DisableFrameNavigation()
        {
            btnPreviousFrame.Enabled = false;
            btnNextFrame.Enabled = false;
        }

        //For primitives

        private void AddPrimitive(Type primitiveType)
        {
            //Validate fields to make primitive

            if (!Utility.GetUInt16FromRGBString(txtPrimitiveDrawColor.Text, out ushort color))
            {
                MessageBox.Show("Enter or select a valid primitive draw color", "Alert!");
                return;
            }

            //Make primitive
            Primitive? newPrimitive = currentFrame.AddPrimitive(primitiveType, color);
            if (newPrimitive == null) return;

            //Update interface
            SetCurrentPrimitive(newPrimitive);
            txtPrimitiveCount.Text = currentFrame.PrimitiveCount.ToString();

            if (currentFrame.PrimitiveCount == 1) EnablePrimitiveManagement();
            if (currentFrame.PrimitiveCount == 2) EnablePrimitiveNavigation();

            pctbxCanvas.Refresh();
        }

        private void RemoveCurrentPrimitive()
        {
            int currentPrimitiveIndex = currentFrame.RemovePrimitive(currentPrimitive);
            txtPrimitiveCount.Text = currentFrame.PrimitiveCount.ToString();

            if (currentFrame.PrimitiveCount < 2) DisablePrimitiveNavigation();
            if (currentFrame.PrimitiveCount < 1)
            {
                DisablePrimitiveManagement();
                HideAllPrimitiveFields();
                pctbxCanvas.Refresh();
                return;
            }

            while (currentPrimitiveIndex >= currentFrame.PrimitiveCount) --currentPrimitiveIndex;
            Primitive primitive = currentFrame.GetPrimitive(currentPrimitiveIndex);

            SetCurrentPrimitive(primitive);
            pctbxCanvas.Refresh();
        }

        private void SetCurrentPrimitive(Primitive primitive)
        {
            currentPrimitive = primitive;
            int currentPrimitiveIndex = currentFrame.IndexOf(primitive);
            txtCurrentPrimitiveNumber.Text = (currentPrimitiveIndex + 1).ToString();
            HideAllPrimitiveFields();
            switch (primitive)
            {
                case Circle:
                    grpbxCircle.Visible = true;
                    SetDisplayFieldsFromCircle((Circle)primitive);
                    break;
                case Triangle:
                    grpbxTriangle.Visible = true;
                    SetDisplayFieldsFromTriangle((Triangle)primitive);
                    break;
                case RoundRect:
                    grpbxRoundRect.Visible = true;
                    SetDisplayFieldsFromRoundRect((RoundRect)primitive);
                    break;
                case RotatedRect:
                    grpbxRotatedRect.Visible = true;
                    SetDisplayFieldsFromRotatedRect((RotatedRect)primitive);
                    break;
                case Line:
                    grpbxLine.Visible = true;
                    SetDisplayFieldsFromLine((Line)primitive);
                    break;
            }
        }

        private void EnablePrimitiveCreation()
        {
            txtPrimitiveCount.Enabled = true;
            txtPrimitiveDrawColor.Enabled = true;
            btnPrimitiveDrawColor.Enabled = true;
            btnAddCircle.Enabled = true;
            btnAddTriangle.Enabled = true;
            btnAddRoundRect.Enabled = true;
            btnAddRotatedRect.Enabled = true;
            btnAddLine.Enabled = true;
        }

        private void DisablePrimitiveCreation()
        {
            txtPrimitiveCount.Text = string.Empty;
            txtPrimitiveCount.Enabled = false;
            txtPrimitiveDrawColor.Text = string.Empty;
            txtPrimitiveDrawColor.Enabled = false;
            btnPrimitiveDrawColor.Enabled = false;
            btnAddCircle.Enabled = false;
            btnAddTriangle.Enabled = false;
            btnAddRoundRect.Enabled = false;
            btnAddRotatedRect.Enabled = false;
            btnAddLine.Enabled = false;

            DisablePrimitiveManagement();
            DisablePrimitiveNavigation();
            HideAllPrimitiveFields();
        }

        private void EnablePrimitiveManagement()
        {
            txtCurrentPrimitiveNumber.Enabled = true;
            txtCurrentPrimitiveDrawColor.Enabled = true;
            btnCurrentPrimitiveDrawColor.Enabled = true;
            btnUpdateCurrentPrimitive.Enabled = true;
            btnRemoveCurrentPrimitive.Enabled = true;
        }

        private void DisablePrimitiveManagement()
        {
            txtCurrentPrimitiveNumber.Text = string.Empty;
            txtCurrentPrimitiveNumber.Enabled = false;
            txtCurrentPrimitiveDrawColor.Text = string.Empty;
            txtCurrentPrimitiveDrawColor.Enabled = false;
            btnCurrentPrimitiveDrawColor.Enabled = false;
            btnUpdateCurrentPrimitive.Enabled = false;
            btnRemoveCurrentPrimitive.Enabled = false;
        }

        private void EnablePrimitiveNavigation()
        {
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
            grpbxTriangle.Visible = false;
            grpbxRoundRect.Visible = false;
            grpbxRotatedRect.Visible = false;
            grpbxLine.Visible = false;
        }

        private void SetDisplayFields(Primitive primitive)
        {
            switch (primitive)
            {
                case Line:
                    Line line = (Line)primitive;
                    SetDisplayFieldsFromLine(line);
                    break;
                case Triangle:
                    Triangle triangle = (Triangle)primitive;
                    SetDisplayFieldsFromTriangle(triangle);
                    break;
                case RoundRect:
                    RoundRect roundRect = (RoundRect)primitive;
                    SetDisplayFieldsFromRoundRect(roundRect);
                    break;
                case RotatedRect:
                    RotatedRect rotatedRect = (RotatedRect)primitive;
                    SetDisplayFieldsFromRotatedRect(rotatedRect);
                    break;
                case Circle:
                    Circle circle = (Circle)primitive;
                    SetDisplayFieldsFromCircle(circle);
                    break;
            }
        }

        private void SetDisplayFieldsFromLine(Line line)
        {
            txtLineX0.Text = line.X0.ToString();
            txtLineY0.Text = line.Y0.ToString();
            txtLineX1.Text = line.X1.ToString();
            txtLineY1.Text = line.Y1.ToString();
            txtCurrentPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(line.Color);
        }

        private void SetLineFromDisplayFields(Line line)
        {
            if (line == null) return;
            if (!Utility.GetInt16FromString(txtLineX0.Text, out Int16 X0)) return;
            if (!Utility.GetInt16FromString(txtLineY0.Text, out Int16 Y0)) return;
            if (!Utility.GetInt16FromString(txtLineX1.Text, out Int16 X1)) return;
            if (!Utility.GetInt16FromString(txtLineY1.Text, out Int16 Y1)) return;
            if (!Utility.GetUInt16FromRGBString(txtCurrentPrimitiveDrawColor.Text, out UInt16 Color)) return;

            line.X0 = X0;
            line.Y0 = Y0;
            line.X1 = X1;
            line.Y1 = Y1;
            line.Color = Color;
        }

        private void SetDisplayFieldsFromTriangle(Triangle triangle)
        {
            txtTriangleX0.Text = triangle.X0.ToString();
            txtTriangleY0.Text = triangle.Y0.ToString();
            txtTriangleX1.Text = triangle.X1.ToString();
            txtTriangleY1.Text = triangle.Y1.ToString();
            txtTriangleX2.Text = triangle.X2.ToString();
            txtTriangleY2.Text = triangle.Y2.ToString();
            txtCurrentPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(triangle.Color);
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
            if (!Utility.GetUInt16FromRGBString(txtCurrentPrimitiveDrawColor.Text, out UInt16 Color)) return;

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
            txtCurrentPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(roundRect.Color);
        }

        private void SetRoundRectFromDisplayFields(RoundRect roundRect)
        {
            if (roundRect == null) return;
            if (!Utility.GetInt16FromString(txtRoundRectX0.Text, out Int16 X0)) return;
            if (!Utility.GetInt16FromString(txtRoundRectY0.Text, out Int16 Y0)) return;
            if (!Utility.GetInt16FromString(txtRoundRectW.Text, out Int16 W)) return;
            if (!Utility.GetInt16FromString(txtRoundRectH.Text, out Int16 H)) return;
            if (!Utility.GetInt16FromString(txtRoundRectRadius.Text, out Int16 Radius)) return;
            if (!Utility.GetUInt16FromRGBString(txtCurrentPrimitiveDrawColor.Text, out UInt16 Color)) return;

            roundRect.X0 = X0;
            roundRect.Y0 = Y0;
            roundRect.W = W;
            roundRect.H = H;
            roundRect.Radius = Radius;
            roundRect.Color = Color;
        }

        private void SetDisplayFieldsFromRotatedRect(RotatedRect rotatedRect)
        {
            txtRotatedRectCenX.Text = rotatedRect.CenX.ToString();
            txtRotatedRectCenY.Text = rotatedRect.CenY.ToString();
            txtRotatedRectW.Text = rotatedRect.W.ToString();
            txtRotatedRectH.Text = rotatedRect.H.ToString();
            txtRotatedRectAngleDeg.Text = rotatedRect.AngleDeg.ToString();
            txtCurrentPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(rotatedRect.Color);
        }

        private void SetRotatedRectFromDisplayFields(RotatedRect rotatedRect)
        {
            if (rotatedRect == null) return;
            if (!Utility.GetInt16FromString(txtRotatedRectCenX.Text, out Int16 CenX)) return;
            if (!Utility.GetInt16FromString(txtRotatedRectCenY.Text, out Int16 CenY)) return;
            if (!Utility.GetInt16FromString(txtRotatedRectW.Text, out Int16 W)) return;
            if (!Utility.GetInt16FromString(txtRotatedRectH.Text, out Int16 H)) return;
            if (!Utility.GetInt16FromString(txtRotatedRectAngleDeg.Text, out Int16 AngleDeg)) return;
            if (!Utility.GetUInt16FromRGBString(txtCurrentPrimitiveDrawColor.Text, out UInt16 Color)) return;

            rotatedRect.CenX = CenX;
            rotatedRect.CenY = CenY;
            rotatedRect.W = W;
            rotatedRect.H = H;
            rotatedRect.AngleDeg = AngleDeg;
            rotatedRect.Color = Color;
        }

        private void SetDisplayFieldsFromCircle(Circle circle)
        {
            txtCircleX0.Text = circle.X0.ToString();
            txtCircleY0.Text = circle.Y0.ToString();
            txtCircleR.Text = circle.R.ToString();
            txtCircleDelta.Text = circle.Delta.ToString();
            txtCurrentPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(circle.Color);
            chkCircleTopLeft.Checked = ((circle.Quadrants & Circle.TopLeft) == Circle.TopLeft);
            chkCircleTopRight.Checked = ((circle.Quadrants & Circle.TopRight) == Circle.TopRight);
            chkCircleBottomLeft.Checked = ((circle.Quadrants & Circle.BottomLeft) == Circle.BottomLeft);
            chkCircleBottomRight.Checked = ((circle.Quadrants & Circle.BottomRight) == Circle.BottomRight);
        }

        private void SetCircleFromDisplayFields(Circle circle)
        {
            if (circle == null) return;
            if (!Utility.GetInt16FromString(txtCircleX0.Text, out Int16 X0)) return;
            if (!Utility.GetInt16FromString(txtCircleY0.Text, out Int16 Y0)) return;
            if (!Utility.GetInt16FromString(txtCircleR.Text, out Int16 R)) return;
            if (!Utility.GetInt16FromString(txtCircleDelta.Text, out Int16 Delta)) return;
            if (!Utility.GetUInt16FromRGBString(txtCurrentPrimitiveDrawColor.Text, out UInt16 Color)) return;

            circle.X0 = X0;
            circle.Y0 = Y0;
            circle.R = R;
            circle.Delta = Delta;
            circle.Color = Color;
        }

        //Set tool tips

        private void SetToolTips()
        {
            ToolTip ttLoadFile = new();
            ttLoadFile.SetToolTip(btnLoadFile, "Load animation from file");
            ToolTip ttSaveFile = new();
            ttSaveFile.SetToolTip(btnSaveFile, "Save animation as file");
            ToolTip ttTransmitFile = new();
            ttTransmitFile.SetToolTip(btnTransmitFile, "Transmit animation to cube at IP Address below");
            ToolTip ttSaveToHeaderFile = new();
            ttSaveToHeaderFile.SetToolTip(btnSaveToHeaderFile, "Save animation as C header file");
            ToolTip ttSendImage = new();
            ttSendImage.SetToolTip(btnSendImage, "Transmit an image file (jpeg, bitmap) to cube at IP Address below");
            ToolTip ttAddFrame = new();
            ttAddFrame.SetToolTip(btnAddFrame, "Add a frame to the animation");
            ToolTip ttRemoveCurrentFrame = new();
            ttRemoveCurrentFrame.SetToolTip(btnRemoveCurrentFrame, "Remove the current frame from the animation");
            ToolTip ttDuplicateCurrentFrame = new();
            ttDuplicateCurrentFrame.SetToolTip(btnDuplicateCurrentFrame, "Duplicate the current frame");
            ToolTip ttAddCircle = new();
            ttAddCircle.SetToolTip(btnAddCircle, "Add a circle");
            ToolTip ttAddTriangle = new();
            ttAddTriangle.SetToolTip(btnAddTriangle, "Add a triangle");
            ToolTip ttAddRoundRect = new();
            ttAddRoundRect.SetToolTip(btnAddRoundRect, "Add a rectangle (with optional corner radius)");
            ToolTip ttAddRotatedRect = new();
            ttAddRotatedRect.SetToolTip(btnAddRotatedRect, "Add a rotated rectangle");
            ToolTip ttAddLine = new();
            ttAddLine.SetToolTip(btnAddLine, "Add a line");
            ToolTip ttRemoveCurrentPrimitive = new();
            ttRemoveCurrentPrimitive.SetToolTip(btnRemoveCurrentPrimitive, "Remove the current shape");
        }

        #endregion

    }
}
