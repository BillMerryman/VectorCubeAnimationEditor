using System.Globalization;
using System.Net.Sockets;
using System.Text.Json;

namespace VectorCubeAnimationEditor
{
    public partial class Editor : Form
    {
        Animation animation;
        AnimationFrame? currentFrame;
        Primitive? currentPrimitive;
        bool highlightCurrent = false;
        Point MouseLocation = new(0, 0);

        byte[] IP = [192, 168, 1, 1];

        UInt16 fillColor = 0;
        UInt32 duration = 0;
        UInt16 primitiveDrawColor = 0;

        public Editor()
        {
            InitializeComponent();
            animation = new Animation();
            SetToolTips();
            txtIPFirstOctet.Text = IP[0].ToString();
            txtIPSecondOctet.Text = IP[1].ToString();
            txtIPThirdOctet.Text = IP[2].ToString();
            txtIPFourthOctet.Text = IP[3].ToString();
            txtFrameFillColor.Text = Utility.GetRGBStringFromUIint16(fillColor);
            txtFrameDuration.Text = duration.ToString();
            txtPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(primitiveDrawColor);
        }

        #region Control Handlers

        private void Editor_Load(object sender, EventArgs e)
        {
            txtFrameCount.Text = animation.FrameCount.ToString();
        }

        #region File handling

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                byte[] flatbuffer = File.ReadAllBytes(openFile.FileName);
                animation = new Animation();
                animation.DeserializeFB(flatbuffer);
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
                if (frame is not null)
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
            saveFile.DefaultExt = ".fb";
            saveFile.Filter = "Flatbuffers (*.fb)|*.fb";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                byte[] flatbuffer = animation.SerializeFB();
                File.WriteAllBytes(saveFile.FileName, flatbuffer);
            }
        }

        private void btnLoadFromJSONFile_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string animationJSON = File.ReadAllText(openFile.FileName);
                animation = JsonSerializer.Deserialize<Animation>(animationJSON) ?? new Animation();

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
                if (frame is not null)
                {
                    SetCurrentFrame(frame);
                    EnableCurrentFrameManipulation();
                    SetFrameNavigation();
                }

                txtFrameCount.Text = animation.FrameCount.ToString();
            }

        }

        private void btnSaveToJSONFile_Click(object sender, EventArgs e)
        {
            saveFile.DefaultExt = ".json";
            saveFile.Filter = "JSON (*.json)|*.json";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                JsonSerializerOptions jsonSerializerOptions = new()
                {
                    WriteIndented = true
                };
                var options = jsonSerializerOptions;
                string animationJSON = JsonSerializer.Serialize(animation, options);
                File.WriteAllText(saveFile.FileName, animationJSON);
            }
        }

        private void btnTransmitFile_Click(object sender, EventArgs e)
        {
            byte[] commandBytes = Utility.getCommandBytes(AnimationConstants._Animation);
            byte[] animationBytes = animation.SerializeFB();
            string IPAddress = IP[0].ToString();
            IPAddress += ".";
            IPAddress += IP[1].ToString();
            IPAddress += ".";
            IPAddress += IP[2].ToString();
            IPAddress += ".";
            IPAddress += IP[3].ToString();
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
                byte[] animationBytes = animation.SerializeFB();
                try
                {
                    using (StreamWriter writer = new(saveFile.FileName))
                    {
                        writer.WriteLine("static const uint8_t animation_" + Path.GetFileNameWithoutExtension(saveFile.FileName) + "[] PROGMEM = {");
                        int index = 0;
                        while (index < animationBytes.Length)
                        {
                            writer.Write("0x");
                            writer.Write(animationBytes[index].ToString("X2"));
                            index++;
                            if (index < animationBytes.Length) writer.Write(", ");
                            if (index % 16 == 0 && index < animationBytes.Length) writer.WriteLine("");
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

        private void txtIPFirstOctet_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateByte((TextBox)sender, e, ref IP[0]);
        }

        private void txtIPFirstOctet_Leave(object sender, EventArgs e)
        {
            txtIPFirstOctet.Text = IP[0].ToString();
        }

        private void txtIPSecondOctet_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateByte((TextBox)sender, e, ref IP[1]);
        }

        private void txtIPSecondOctet_Leave(object sender, EventArgs e)
        {
            txtIPSecondOctet.Text = IP[1].ToString();
        }

        private void txtIPThirdOctet_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateByte((TextBox)sender, e, ref IP[2]);
        }

        private void txtIPThirdOctet_Leave(object sender, EventArgs e)
        {
            txtIPThirdOctet.Text = IP[2].ToString();
        }

        private void txtIPFourthOctet_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateByte((TextBox)sender, e, ref IP[3]);
        }

        private void txtIPFourthOctet_Leave(object sender, EventArgs e)
        {
            txtIPFourthOctet.Text = IP[3].ToString();
        }

        #endregion

        #region Frame management

        private void btnAddFrame_Click(object sender, EventArgs e)
        {
            AddFrame();
        }

        private void txtFrameFillColor_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateColor((TextBox)sender, e, ref fillColor);
        }

        private void txtFrameFillColor_Leave(object sender, EventArgs e)
        {
            txtFrameFillColor.Text = Utility.GetRGBStringFromUIint16(fillColor);
        }

        private void btnFrameFillColor_Click(object sender, EventArgs e)
        {
            DialogResult result = selectColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                string color = selectColor.Color.R.ToString("X2") +
                                          selectColor.Color.G.ToString("X2") +
                                          selectColor.Color.B.ToString("X2");
                Utility.GetUInt16FromRGBString(color, out UInt16 outColor);
                fillColor = outColor;
                txtFrameFillColor.Text = Utility.GetRGBStringFromUIint16(fillColor);
            }
        }

        private void txtFrameDuration_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateUInt32((TextBox)sender, e, ref duration);
        }

        private void txtFrameDuration_Leave(object sender, EventArgs e)
        {
            txtFrameDuration.Text = duration.ToString();
        }

        private void txtCurrentFrameFillColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentFrame is not null)
            {
                UInt16 fillColor = currentFrame.FillColor;
                ValidateColor((TextBox)sender, e, ref fillColor);
                currentFrame.FillColor = fillColor;
                pctbxCanvas.Refresh();
            }
        }

        private void txtCurrentFrameFillColor_Leave(object sender, EventArgs e)
        {
            if (currentFrame is not null) txtCurrentFrameFillColor.Text = Utility.GetRGBStringFromUIint16(currentFrame.FillColor);
            pctbxCanvas.Refresh();
        }

        private void btnCurrentFrameFillColor_Click(object sender, EventArgs e)
        {
            DialogResult result = selectColor.ShowDialog();
            if (result == DialogResult.OK && currentFrame is not null)
            {
                string color = selectColor.Color.R.ToString("X2") +
                                selectColor.Color.G.ToString("X2") +
                                selectColor.Color.B.ToString("X2");
                if (Utility.GetUInt16FromRGBString(color, out UInt16 outColor))
                {
                    currentFrame.FillColor = outColor;
                }
                txtCurrentFrameFillColor.Text = Utility.GetRGBStringFromUIint16(currentFrame.FillColor);
                pctbxCanvas.Refresh();
            }
        }

        private void txtCurrentFrameDuration_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentFrame is not null)
            {
                UInt32 duration = currentFrame.Duration;
                ValidateUInt32((TextBox)sender, e, ref duration);
                currentFrame.Duration = duration;
            }
        }

        private void txtCurrentFrameDuration_Leave(object sender, EventArgs e)
        {
            if (currentFrame is not null) txtCurrentFrameDuration.Text = currentFrame.Duration.ToString();
        }

        private void btnMoveFrameDown_Click(object sender, EventArgs e)
        {
            animation.MoveFrameDown(currentFrame);
            int currentFrameNumber = animation.IndexOf(currentFrame) + 1;
            txtCurrentFrameNumber.Text = currentFrameNumber.ToString();
            SetFrameNavigation();
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
            AnimationFrame? animationFrame = animation.GetFrame(currentFrameIndex);
            if (animationFrame is not null) SetCurrentFrame(animationFrame);
        }

        private void btnRemoveCurrentFrame_Click(object sender, EventArgs e)
        {
            RemoveCurrentFrame();
        }

        private void btnDuplicateCurrentFrame_Click(object sender, EventArgs e)
        {
            if (currentFrame is not null)
            {
                AnimationFrame? newFrame = animation.DuplicateFrame(currentFrame);
                if (newFrame is not null)
                {
                    SetFrameNavigation();
                    txtFrameCount.Text = animation.FrameCount.ToString();
                    txtCurrentFrameNumber.Text = (animation.IndexOf(newFrame) + 1).ToString();
                    SetCurrentFrame(newFrame);
                }
            }
        }

        private void btnNextFrame_Click(object sender, EventArgs e)
        {
            int currentFrameIndex = animation.IndexOf(currentFrame);
            if (currentFrameIndex == animation.FrameCount - 1) return;
            currentFrameIndex++;
            AnimationFrame? animationFrame = animation.GetFrame(currentFrameIndex);
            if (animationFrame is not null) SetCurrentFrame(animationFrame);
        }

        #endregion

        #region Primitive handling

        private void txtPrimitiveDrawColor_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateColor((TextBox)sender, e, ref primitiveDrawColor);
        }

        private void txtPrimitiveDrawColor_Leave(object sender, EventArgs e)
        {
            txtPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(primitiveDrawColor);
        }

        private void btnPrimitiveDrawColor_Click(object sender, EventArgs e)
        {
            DialogResult result = selectColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                string color = selectColor.Color.R.ToString("X2") +
                                selectColor.Color.G.ToString("X2") +
                                selectColor.Color.B.ToString("X2");
                Utility.GetUInt16FromRGBString(color, out UInt16 outColor);
                primitiveDrawColor = outColor;
                txtPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(primitiveDrawColor);
            }
        }

        private void btnAddLine_Click(object sender, EventArgs e)
        {
            AddPrimitive(typeof(Line));
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

        private void btnAddCircle_Click(object sender, EventArgs e)
        {
            AddPrimitive(typeof(Circle));
        }

        private void txtCurrentPrimitiveDrawColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentPrimitive is not null)
            {
                UInt16 color = currentPrimitive.Color;
                ValidateColor((TextBox)sender, e, ref color);
                currentPrimitive.Color = color;
                pctbxCanvas.Refresh();
            }
        }

        private void txtCurrentPrimitiveDrawColor_Leave(object sender, EventArgs e)
        {
            if (currentPrimitive is not null) txtCurrentPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(currentPrimitive.Color);
            pctbxCanvas.Refresh();
        }

        private void btnCurrentPrimitiveDrawColor_Click(object sender, EventArgs e)
        {
            DialogResult result = selectColor.ShowDialog();
            if (result == DialogResult.OK && currentPrimitive is not null)
            {
                string color = selectColor.Color.R.ToString("X2") +
                                selectColor.Color.G.ToString("X2") +
                                selectColor.Color.B.ToString("X2");
                if (Utility.GetUInt16FromRGBString(color, out UInt16 outColor))
                {
                    currentPrimitive.Color = outColor;
                }
                txtCurrentPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(currentPrimitive.Color);
                pctbxCanvas.Refresh();
            }
        }

        private void txtLineX0_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Line)))
            {
                Int16 x0 = ((Line)currentPrimitive).X0_Abs;
                ValidateInt16((TextBox)sender, e, ref x0);
                if (x0 != ((Line)currentPrimitive).X0_Abs)
                {
                    ((Line)currentPrimitive).X0_Abs = x0;
                    ((TextBox)sender).Text = ((Line)currentPrimitive).X0_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtLineX0_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Line)))
            {
                ((TextBox)sender).Text = ((Line)currentPrimitive).X0_Abs.ToString();
            }
        }

        private void txtLineY0_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Line)))
            {
                Int16 y0 = ((Line)currentPrimitive).Y0_Abs;
                ValidateInt16((TextBox)sender, e, ref y0);
                if (y0 != ((Line)currentPrimitive).Y0_Abs)
                {
                    ((Line)currentPrimitive).Y0_Abs = y0;
                    ((TextBox)sender).Text = ((Line)currentPrimitive).Y0_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtLineY0_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Line)))
            {
                ((TextBox)sender).Text = ((Line)currentPrimitive).Y0_Abs.ToString();
            }
        }

        private void txtLineX1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Line)))
            {
                Int16 x1 = ((Line)currentPrimitive).X1_Abs;
                ValidateInt16((TextBox)sender, e, ref x1);
                if (x1 != ((Line)currentPrimitive).X1_Abs)
                {
                    ((Line)currentPrimitive).X1_Abs = x1;
                    ((TextBox)sender).Text = ((Line)currentPrimitive).X1_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtLineX1_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Line)))
            {
                ((TextBox)sender).Text = ((Line)currentPrimitive).X1_Abs.ToString();
            }
        }

        private void txtLineY1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Line)))
            {
                Int16 y1 = ((Line)currentPrimitive).Y1_Abs;
                ValidateInt16((TextBox)sender, e, ref y1);
                if (y1 != ((Line)currentPrimitive).Y1_Abs)
                {
                    ((Line)currentPrimitive).Y1_Abs = y1;
                    ((TextBox)sender).Text = ((Line)currentPrimitive).Y1_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtLineY1_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Line)))
            {
                ((TextBox)sender).Text = ((Line)currentPrimitive).Y1_Abs.ToString();
            }
        }

        private void txtTriangleX0_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Triangle)))
            {
                Int16 x0 = ((Triangle)currentPrimitive).X0_Abs;
                ValidateInt16((TextBox)sender, e, ref x0);
                if (x0 != ((Triangle)currentPrimitive).X0_Abs)
                {
                    ((Triangle)currentPrimitive).X0_Abs = x0;
                    ((TextBox)sender).Text = ((Triangle)currentPrimitive).X0_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtTriangleX0_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Triangle)))
            {
                ((TextBox)sender).Text = ((Triangle)currentPrimitive).X0_Abs.ToString();
            }
        }

        private void txtTriangleY0_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Triangle)))
            {
                Int16 y0 = ((Triangle)currentPrimitive).Y0_Abs;
                ValidateInt16((TextBox)sender, e, ref y0);
                if (y0 != ((Triangle)currentPrimitive).Y0_Abs)
                {
                    ((Triangle)currentPrimitive).Y0_Abs = y0;
                    ((TextBox)sender).Text = ((Triangle)currentPrimitive).Y0_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtTriangleY0_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Triangle)))
            {
                ((TextBox)sender).Text = ((Triangle)currentPrimitive).Y0_Abs.ToString();
            }
        }

        private void txtTriangleX1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Triangle)))
            {
                Int16 x1 = ((Triangle)currentPrimitive).X1_Abs;
                ValidateInt16((TextBox)sender, e, ref x1);
                if (x1 != ((Triangle)currentPrimitive).X1_Abs)
                {
                    ((Triangle)currentPrimitive).X1_Abs = x1;
                    ((TextBox)sender).Text = ((Triangle)currentPrimitive).X1_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtTriangleX1_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Triangle)))
            {
                ((TextBox)sender).Text = ((Triangle)currentPrimitive).X1_Abs.ToString();
            }
        }

        private void txtTriangleY1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Triangle)))
            {
                Int16 y1 = ((Triangle)currentPrimitive).Y1_Abs;
                ValidateInt16((TextBox)sender, e, ref y1);
                if (y1 != ((Triangle)currentPrimitive).Y1_Abs)
                {
                    ((Triangle)currentPrimitive).Y1_Abs = y1;
                    ((TextBox)sender).Text = ((Triangle)currentPrimitive).Y1_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtTriangleY1_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Triangle)))
            {
                ((TextBox)sender).Text = ((Triangle)currentPrimitive).Y1_Abs.ToString();
            }
        }

        private void txtTriangleX2_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Triangle)))
            {
                Int16 x2 = ((Triangle)currentPrimitive).X2_Abs;
                ValidateInt16((TextBox)sender, e, ref x2);
                if (x2 != ((Triangle)currentPrimitive).X2_Abs)
                {
                    ((Triangle)currentPrimitive).X2_Abs = x2;
                    ((TextBox)sender).Text = ((Triangle)currentPrimitive).X2_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtTriangleX2_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Triangle)))
            {
                ((TextBox)sender).Text = ((Triangle)currentPrimitive).X2_Abs.ToString();
            }
        }

        private void txtTriangleY2_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Triangle)))
            {
                Int16 y2 = ((Triangle)currentPrimitive).Y2_Abs;
                ValidateInt16((TextBox)sender, e, ref y2);
                if (y2 != ((Triangle)currentPrimitive).Y2_Abs)
                {
                    ((Triangle)currentPrimitive).Y2_Abs = y2;
                    ((TextBox)sender).Text = ((Triangle)currentPrimitive).Y2_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtTriangleY2_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Triangle)))
            {
                ((TextBox)sender).Text = ((Triangle)currentPrimitive).Y2_Abs.ToString();
            }
        }

        private void txtRoundRectX0_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RoundRect)))
            {
                Int16 x0 = ((RoundRect)currentPrimitive).X0_Abs;
                ValidateInt16((TextBox)sender, e, ref x0);
                if (x0 != ((RoundRect)currentPrimitive).X0_Abs)
                {
                    ((RoundRect)currentPrimitive).X0_Abs = x0;
                    ((TextBox)sender).Text = ((RoundRect)currentPrimitive).X0_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtRoundRectX0_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RoundRect)))
            {
                ((TextBox)sender).Text = ((RoundRect)currentPrimitive).X0_Abs.ToString();
            }
        }

        private void txtRoundRectY0_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RoundRect)))
            {
                Int16 y0 = ((RoundRect)currentPrimitive).Y0_Abs;
                ValidateInt16((TextBox)sender, e, ref y0);
                if (y0 != ((RoundRect)currentPrimitive).Y0_Abs)
                {
                    ((RoundRect)currentPrimitive).Y0_Abs = y0;
                    ((TextBox)sender).Text = ((RoundRect)currentPrimitive).Y0_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtRoundRectY0_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RoundRect)))
            {
                ((TextBox)sender).Text = ((RoundRect)currentPrimitive).Y0_Abs.ToString();
            }
        }

        private void txtRoundRectW_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RoundRect)))
            {
                Int16 w = ((RoundRect)currentPrimitive).W;
                ValidateInt16((TextBox)sender, e, ref w);
                if (w != ((RoundRect)currentPrimitive).W)
                {
                    ((RoundRect)currentPrimitive).W = w;
                    ((TextBox)sender).Text = ((RoundRect)currentPrimitive).W.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtRoundRectW_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RoundRect)))
            {
                ((TextBox)sender).Text = ((RoundRect)currentPrimitive).W.ToString();
            }
        }

        private void txtRoundRectH_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RoundRect)))
            {
                Int16 h = ((RoundRect)currentPrimitive).H;
                ValidateInt16((TextBox)sender, e, ref h);
                if (h != ((RoundRect)currentPrimitive).H)
                {
                    ((RoundRect)currentPrimitive).H = h;
                    ((TextBox)sender).Text = ((RoundRect)currentPrimitive).H.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtRoundRectH_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RoundRect)))
            {
                ((TextBox)sender).Text = ((RoundRect)currentPrimitive).H.ToString();
            }
        }

        private void txtRoundRectRadius_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RoundRect)))
            {
                Int16 radius = ((RoundRect)currentPrimitive).Radius;
                ValidateInt16((TextBox)sender, e, ref radius);
                if (radius != ((RoundRect)currentPrimitive).Radius)
                {
                    ((RoundRect)currentPrimitive).Radius = radius;
                    ((TextBox)sender).Text = ((RoundRect)currentPrimitive).Radius.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtRoundRectRadius_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RoundRect)))
            {
                ((TextBox)sender).Text = ((RoundRect)currentPrimitive).Radius.ToString();
            }
        }

        private void txtRotatedRectCenX_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RotatedRect)))
            {
                Int16 cenX = ((RotatedRect)currentPrimitive).X0_Abs;
                ValidateInt16((TextBox)sender, e, ref cenX);
                if (cenX != ((RotatedRect)currentPrimitive).X0_Abs)
                {
                    ((RotatedRect)currentPrimitive).X0_Abs = cenX;
                    ((TextBox)sender).Text = ((RotatedRect)currentPrimitive).X0_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtRotatedRectCenX_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RotatedRect)))
            {
                ((TextBox)sender).Text = ((RotatedRect)currentPrimitive).X0_Abs.ToString();
            }
        }

        private void txtRotatedRectCenY_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RotatedRect)))
            {
                Int16 cenY = ((RotatedRect)currentPrimitive).Y0_Abs;
                ValidateInt16((TextBox)sender, e, ref cenY);
                if (cenY != ((RotatedRect)currentPrimitive).Y0_Abs)
                {
                    ((RotatedRect)currentPrimitive).Y0_Abs = cenY;
                    ((TextBox)sender).Text = ((RotatedRect)currentPrimitive).Y0_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtRotatedRectCenY_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RotatedRect)))
            {
                ((TextBox)sender).Text = ((RotatedRect)currentPrimitive).Y0_Abs.ToString();
            }
        }

        private void txtRotatedRectW_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RotatedRect)))
            {
                Int16 w = ((RotatedRect)currentPrimitive).W;
                ValidateInt16((TextBox)sender, e, ref w);
                if (w != ((RotatedRect)currentPrimitive).W)
                {
                    ((RotatedRect)currentPrimitive).W = w;
                    ((TextBox)sender).Text = ((RotatedRect)currentPrimitive).W.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtRotatedRectW_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RotatedRect)))
            {
                ((TextBox)sender).Text = ((RotatedRect)currentPrimitive).W.ToString();
            }
        }

        private void txtRotatedRectH_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RotatedRect)))
            {
                Int16 h = ((RotatedRect)currentPrimitive).H;
                ValidateInt16((TextBox)sender, e, ref h);
                if (h != ((RotatedRect)currentPrimitive).H)
                {
                    ((RotatedRect)currentPrimitive).H = h;
                    ((TextBox)sender).Text = ((RotatedRect)currentPrimitive).H.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtRotatedRectH_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RotatedRect)))
            {
                ((TextBox)sender).Text = ((RotatedRect)currentPrimitive).H.ToString();
            }
        }

        private void txtRotatedRectAngleDeg_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RotatedRect)))
            {
                Int16 angleDeg = ((RotatedRect)currentPrimitive).AngleDeg;
                ValidateInt16((TextBox)sender, e, ref angleDeg);
                if (angleDeg != ((RotatedRect)currentPrimitive).AngleDeg)
                {
                    ((RotatedRect)currentPrimitive).AngleDeg = angleDeg;
                    ((TextBox)sender).Text = ((RotatedRect)currentPrimitive).AngleDeg.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtRotatedRectAngleDeg_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(RotatedRect)))
            {
                ((TextBox)sender).Text = ((RotatedRect)currentPrimitive).AngleDeg.ToString();
            }
        }
        
        private void txtCircleX0_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Circle)))
            {
                Int16 x0 = ((Circle)currentPrimitive).X0_Abs;
                ValidateInt16((TextBox)sender, e, ref x0);
                if (x0 != ((Circle)currentPrimitive).X0_Abs)
                {
                    ((Circle)currentPrimitive).X0_Abs = x0;
                    ((TextBox)sender).Text = ((Circle)currentPrimitive).X0_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtCircleX0_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Circle)))
            {
                ((TextBox)sender).Text = ((Circle)currentPrimitive).X0_Abs.ToString();
            }
        }

        private void txtCircleY0_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Circle)))
            {
                Int16 y0 = ((Circle)currentPrimitive).Y0_Abs;
                ValidateInt16((TextBox)sender, e, ref y0);
                if (y0 != ((Circle)currentPrimitive).Y0_Abs)
                {
                    ((Circle)currentPrimitive).Y0_Abs = y0;
                    ((TextBox)sender).Text = ((Circle)currentPrimitive).Y0_Abs.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtCircleY0_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Circle)))
            {
                ((TextBox)sender).Text = ((Circle)currentPrimitive).Y0_Abs.ToString();
            }
        }

        private void txtCircleR_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Circle)))
            {
                Int16 r = ((Circle)currentPrimitive).R;
                ValidateInt16((TextBox)sender, e, ref r);
                if (r != ((Circle)currentPrimitive).R)
                {
                    ((Circle)currentPrimitive).R = r;
                    ((TextBox)sender).Text = ((Circle)currentPrimitive).R.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtCircleR_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Circle)))
            {
                ((TextBox)sender).Text = ((Circle)currentPrimitive).R.ToString();
            }
        }

        private void txtCircleDelta_KeyDown(object sender, KeyEventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Circle)))
            {
                Int16 delta = ((Circle)currentPrimitive).Delta;
                ValidateInt16((TextBox)sender, e, ref delta);
                if (delta != ((Circle)currentPrimitive).Delta)
                {
                    ((Circle)currentPrimitive).Delta = delta;
                    ((TextBox)sender).Text = ((Circle)currentPrimitive).Delta.ToString();
                    pctbxCanvas.Refresh();
                }
            }
        }

        private void txtCircleDelta_Leave(object sender, EventArgs e)
        {
            if ((currentPrimitive is not null) && (currentPrimitive.GetType() == typeof(Circle)))
            {
                ((TextBox)sender).Text = ((Circle)currentPrimitive).Delta.ToString();
            }
        }

        private void chkCircleTopLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (currentPrimitive is not null)
            {
                if (chkCircleTopLeft.Checked)
                {
                    ((Circle)currentPrimitive).Quadrants |= Circle.TopLeft;
                }
                else
                {
                    ((Circle)currentPrimitive).Quadrants &= unchecked((byte)~Circle.TopLeft);
                }
            }
            pctbxCanvas.Refresh();
        }

        private void chkCircleTopRight_CheckedChanged(object sender, EventArgs e)
        {
            if (currentPrimitive is not null)
            {
                if (chkCircleTopRight.Checked)
                {
                    ((Circle)currentPrimitive).Quadrants |= Circle.TopRight;
                }
                else
                {
                    ((Circle)currentPrimitive).Quadrants &= unchecked((byte)~Circle.TopRight);
                }
            }
            pctbxCanvas.Refresh();
        }

        private void chkCircleBottomLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (currentPrimitive is not null)
            {
                if (chkCircleBottomLeft.Checked)
                {
                    ((Circle)currentPrimitive).Quadrants |= Circle.BottomLeft;
                }
                else
                {
                    ((Circle)currentPrimitive).Quadrants &= unchecked((byte)~Circle.BottomLeft);
                }
            }
            pctbxCanvas.Refresh();
        }

        private void chkCircleBottomRight_CheckedChanged(object sender, EventArgs e)
        {
            if (currentPrimitive is not null)
            {
                if (chkCircleBottomRight.Checked)
                {
                    ((Circle)currentPrimitive).Quadrants |= Circle.BottomRight;
                }
                else
                {
                    ((Circle)currentPrimitive).Quadrants &= unchecked((byte)~Circle.BottomRight);
                }
            }
            pctbxCanvas.Refresh();
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
            Primitive? tmpPrimitive = currentFrame.GetPrimitive(currentPrimitiveIndex);
            if (tmpPrimitive is not null) SetCurrentPrimitive(tmpPrimitive);
        }

        private void btnNextPrimitive_Click(object sender, EventArgs e)
        {
            if (currentFrame == null) return;
            int currentPrimitiveIndex = currentFrame.IndexOf(currentPrimitive);
            if (currentPrimitiveIndex == currentFrame.PrimitiveCount - 1) return;
            currentPrimitiveIndex++;
            txtCurrentPrimitiveNumber.Text = (currentPrimitiveIndex + 1).ToString();
            Primitive? tmpPrimitive = currentFrame.GetPrimitive(currentPrimitiveIndex);
            if (tmpPrimitive is not null) SetCurrentPrimitive(tmpPrimitive);
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

        #region Canvas handling

        private void pctbxCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentPrimitive == null) return;
            MouseLocation = e.Location;
            currentPrimitive.MouseDown(MouseLocation);
        }

        private void pctbxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentPrimitive == null) return;

            if (currentPrimitive.MouseMove(e.Location, pctbxCanvas))
            {
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
            if (currentFrame is not null)
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

        #region Disabled text field

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
            if (!UInt32.TryParse(txtFrameDuration.Text, out UInt32 duration))
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
            AnimationFrame? animationFrame = animation.GetFrame(currentFrameIndex);
            if (animationFrame is not null) SetCurrentFrame(animationFrame);
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
                if (primitive is not null)
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

            if (currentFrame is not null)
            {
                //Make primitive
                Primitive? newPrimitive = currentFrame.AddPrimitive(primitiveType, color);
                if (newPrimitive is not null)
                {
                    //Update interface
                    SetCurrentPrimitive(newPrimitive);
                    txtPrimitiveCount.Text = currentFrame.PrimitiveCount.ToString();

                    if (currentFrame.PrimitiveCount == 1) EnablePrimitiveManagement();
                    if (currentFrame.PrimitiveCount == 2) EnablePrimitiveNavigation();

                    pctbxCanvas.Refresh();
                }
            }
        }

        private void RemoveCurrentPrimitive()
        {
            if (currentFrame is not null && currentPrimitive is not null)
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
                Primitive? primitive = currentFrame.GetPrimitive(currentPrimitiveIndex);
                if (primitive is not null) SetCurrentPrimitive(primitive);
                pctbxCanvas.Refresh();
            }
        }

        private void SetCurrentPrimitive(Primitive primitive)
        {
            if (currentFrame is not null)
            {
                currentPrimitive = primitive;
                int currentPrimitiveIndex = currentFrame.IndexOf(primitive);
                txtCurrentPrimitiveNumber.Text = (currentPrimitiveIndex + 1).ToString();
                HideAllPrimitiveFields();
                SetDisplayFields(currentPrimitive);
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
            btnRemoveCurrentPrimitive.Enabled = true;
        }

        private void DisablePrimitiveManagement()
        {
            txtCurrentPrimitiveNumber.Text = string.Empty;
            txtCurrentPrimitiveNumber.Enabled = false;
            txtCurrentPrimitiveDrawColor.Text = string.Empty;
            txtCurrentPrimitiveDrawColor.Enabled = false;
            btnCurrentPrimitiveDrawColor.Enabled = false;
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
                    grpbxLine.Visible = true;
                    break;
                case Triangle:
                    Triangle triangle = (Triangle)primitive;
                    SetDisplayFieldsFromTriangle(triangle);
                    grpbxTriangle.Visible = true;
                    break;
                case RoundRect:
                    RoundRect roundRect = (RoundRect)primitive;
                    SetDisplayFieldsFromRoundRect(roundRect);
                    grpbxRoundRect.Visible = true;
                    break;
                case RotatedRect:
                    RotatedRect rotatedRect = (RotatedRect)primitive;
                    SetDisplayFieldsFromRotatedRect(rotatedRect);
                    grpbxRotatedRect.Visible = true;
                    break;
                case Circle:
                    Circle circle = (Circle)primitive;
                    SetDisplayFieldsFromCircle(circle);
                    grpbxCircle.Visible = true;
                    break;
            }
        }

        private void SetDisplayFieldsFromLine(Line line)
        {
            txtLineX0.Text = line.X0_Abs.ToString();
            txtLineY0.Text = line.Y0_Abs.ToString();
            txtLineX1.Text = line.X1_Abs.ToString();
            txtLineY1.Text = line.Y1_Abs.ToString();
            txtCurrentPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(line.Color);
        }

        private void SetDisplayFieldsFromTriangle(Triangle triangle)
        {
            txtTriangleX0.Text = triangle.X0_Abs.ToString();
            txtTriangleY0.Text = triangle.Y0_Abs.ToString();
            txtTriangleX1.Text = triangle.X1_Abs.ToString();
            txtTriangleY1.Text = triangle.Y1_Abs.ToString();
            txtTriangleX2.Text = triangle.X2_Abs.ToString();
            txtTriangleY2.Text = triangle.Y2_Abs.ToString();
            txtCurrentPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(triangle.Color);
        }

        private void SetDisplayFieldsFromRoundRect(RoundRect roundRect)
        {
            txtRoundRectX0.Text = roundRect.X0_Abs.ToString();
            txtRoundRectY0.Text = roundRect.Y0_Abs.ToString();
            txtRoundRectW.Text = roundRect.W.ToString();
            txtRoundRectH.Text = roundRect.H.ToString();
            txtRoundRectRadius.Text = roundRect.Radius.ToString();
            txtCurrentPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(roundRect.Color);
        }

        private void SetDisplayFieldsFromRotatedRect(RotatedRect rotatedRect)
        {
            txtRotatedRectCenX.Text = rotatedRect.X0_Abs.ToString();
            txtRotatedRectCenY.Text = rotatedRect.Y0_Abs.ToString();
            txtRotatedRectW.Text = rotatedRect.W.ToString();
            txtRotatedRectH.Text = rotatedRect.H.ToString();
            txtRotatedRectAngleDeg.Text = rotatedRect.AngleDeg.ToString();
            txtCurrentPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(rotatedRect.Color);
        }

        private void SetDisplayFieldsFromCircle(Circle circle)
        {
            txtCircleX0.Text = circle.X0_Abs.ToString();
            txtCircleY0.Text = circle.Y0_Abs.ToString();
            txtCircleR.Text = circle.R.ToString();
            txtCircleDelta.Text = circle.Delta.ToString();
            txtCurrentPrimitiveDrawColor.Text = Utility.GetRGBStringFromUIint16(circle.Color);
            chkCircleTopLeft.Checked = ((circle.Quadrants & Circle.TopLeft) == Circle.TopLeft);
            chkCircleTopRight.Checked = ((circle.Quadrants & Circle.TopRight) == Circle.TopRight);
            chkCircleBottomLeft.Checked = ((circle.Quadrants & Circle.BottomLeft) == Circle.BottomLeft);
            chkCircleBottomRight.Checked = ((circle.Quadrants & Circle.BottomRight) == Circle.BottomRight);
        }

        //Set tool tips

        private void SetToolTips()
        {
            ToolTip ttLoadFile = new();
            ttLoadFile.SetToolTip(btnLoadFromBinaryFile, "Load animation from file");
            ToolTip ttSaveFile = new();
            ttSaveFile.SetToolTip(btnSaveToBinaryFile, "Save animation as file");
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

        #region Validation helpers

        private static bool IsDigit(KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)) return true;
            return false;
        }

        private static bool IsHexDigit(KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.A && e.KeyCode <= Keys.F) || IsDigit(e)) return true;
            return false;
        }

        private static bool IsEdit(KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A || e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                return true;
            }
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete ||
                e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Home || e.KeyCode == Keys.End ||
                e.KeyCode == Keys.Tab)
            {
                return true;
            }
            return false;
        }

        private static void ValidateColor(TextBox txtColorField, KeyEventArgs e, ref UInt16 fillColor)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!Utility.GetUInt16FromRGBString(txtColorField.Text, out ushort outColor))
                {
                    MessageBox.Show("Enter or select a valid color", "Alert!");
                    txtColorField.Text = Utility.GetRGBStringFromUIint16(fillColor);
                }
                else
                {
                    fillColor = outColor;
                    e.SuppressKeyPress = true;
                }
                return;
            }
            if (!(IsEdit(e) || IsHexDigit(e))) e.SuppressKeyPress = true;
        }

        private static void ValidateByte(TextBox txtOctet, KeyEventArgs e, ref Byte byteIn)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool validByte = Byte.TryParse(txtOctet.Text, out byte byteValue);
                if (!validByte)
                {
                    MessageBox.Show("Enter a valid 8 bit value", "Alert!");
                    txtOctet.Text = byteIn.ToString();
                }
                else
                {
                    byteIn = byteValue;
                    e.SuppressKeyPress = true;
                }
                return;
            }
            if (!(IsEdit(e) || IsDigit(e))) e.SuppressKeyPress = true;
        }

        private static void ValidateInt16(TextBox txtInt16Field, KeyEventArgs e, ref Int16 value)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!Int16.TryParse(txtInt16Field.Text, out Int16 outValue))
                {
                    MessageBox.Show("Enter a valid 16 bit value", "Alert!");
                    txtInt16Field.Text = value.ToString();
                }
                else
                {
                    value = outValue;
                    e.SuppressKeyPress = true;
                }
                return;
            }

            bool isSign = ((e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus) &&
                            txtInt16Field.SelectionStart == 0 &&
                            txtInt16Field.Text.IndexOf('-') < 0);

            if (!(IsEdit(e) || IsDigit(e) || isSign)) e.SuppressKeyPress = true;
        }

        private static void ValidateUInt32(TextBox txtUInt32Field, KeyEventArgs e, ref UInt32 value)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!UInt32.TryParse(txtUInt32Field.Text, out UInt32 outValue))
                {
                    MessageBox.Show("Enter a valid 32 bit value", "Alert!");
                    txtUInt32Field.Text = value.ToString();
                }
                else
                {
                    value = outValue;
                    e.SuppressKeyPress = true;
                }
                return;
            }
            if (!(IsEdit(e) || IsDigit(e))) e.SuppressKeyPress = true;
        }

        #endregion

    }
}
