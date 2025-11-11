namespace VectorCubeAnimationEditor
{
    partial class Editor
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            grpbxFrames = new GroupBox();
            btnDuplicateCurrentFrame = new Button();
            txtFrameDuration = new TextBox();
            lblFrameDuration = new Label();
            btnFrameFillColor = new Button();
            txtFrameFillColor = new TextBox();
            lblAddFillColor = new Label();
            grpbxCurrentFrame = new GroupBox();
            btnMoveFrameUp = new Button();
            btnMoveFrameDown = new Button();
            btnUpdateCurrentFrame = new Button();
            txtCurrentFrameDuration = new TextBox();
            lblCurrentFrameDuration = new Label();
            btnCurrentFrameFillColor = new Button();
            txtCurrentFrameFillColor = new TextBox();
            lblCurrentFrameFillColor = new Label();
            txtCurrentFrameNumber = new TextBox();
            lblCurrentFrameNumber = new Label();
            btnRemoveCurrentFrame = new Button();
            btnNextFrame = new Button();
            btnPreviousFrame = new Button();
            btnAddFrame = new Button();
            txtFrameCount = new TextBox();
            lblFrameCount = new Label();
            pctbxCanvas = new PictureBox();
            grpbxPrimitives = new GroupBox();
            btnAddRotatedRect = new Button();
            btnAddLine = new Button();
            btnPrimitiveDrawColor = new Button();
            txtPrimitiveDrawColor = new TextBox();
            lblAddPrimitiveDrawColor = new Label();
            grpbxCurrentPrimitive = new GroupBox();
            grpbxRotatedRect = new GroupBox();
            txtRotatedRectAngleDeg = new TextBox();
            txtRotatedRectH = new TextBox();
            txtRotatedRectW = new TextBox();
            txtRotatedRectCenY = new TextBox();
            txtRotatedRectCenX = new TextBox();
            lblRotatedRectAngleDeg = new Label();
            lblRotatedRectH = new Label();
            lblRotatedRectW = new Label();
            lblRotatedRectCenY = new Label();
            lblRotatedRectCenX = new Label();
            grpbxRoundRect = new GroupBox();
            txtRoundRectRadius = new TextBox();
            txtRoundRectH = new TextBox();
            txtRoundRectW = new TextBox();
            txtRoundRectY0 = new TextBox();
            txtRoundRectX0 = new TextBox();
            lblRoundRectRadius = new Label();
            lblRoundRectH = new Label();
            lblRoundRectW = new Label();
            lblRoundRectY0 = new Label();
            lblRoundRectX0 = new Label();
            grpbxCircle = new GroupBox();
            chkCircleTopLeft = new CheckBox();
            chkCircleTopRight = new CheckBox();
            chkCircleBottomLeft = new CheckBox();
            chkCircleBottomRight = new CheckBox();
            txtCircleR = new TextBox();
            txtCircleDelta = new TextBox();
            txtCircleY0 = new TextBox();
            txtCircleX0 = new TextBox();
            lblCircleR = new Label();
            lblCircleDelta = new Label();
            lblCircleQuadrants = new Label();
            lblCircleY0 = new Label();
            lblCircleX0 = new Label();
            grpbxTriangle = new GroupBox();
            txtTriangleY1 = new TextBox();
            txtTriangleY0 = new TextBox();
            txtTriangleX1 = new TextBox();
            txtTriangleX0 = new TextBox();
            lblTriangleY1 = new Label();
            txtTriangleY2 = new TextBox();
            txtTriangleX2 = new TextBox();
            lblTriangleY2 = new Label();
            lblTriangleX2 = new Label();
            lblTriangleY0 = new Label();
            lblTriangleX1 = new Label();
            lblTriangleX0 = new Label();
            grpbxLine = new GroupBox();
            txtLineY1 = new TextBox();
            txtLineY0 = new TextBox();
            txtLineX1 = new TextBox();
            txtLineX0 = new TextBox();
            lblLineY1 = new Label();
            lblLineY0 = new Label();
            lblLineX1 = new Label();
            lblLineX0 = new Label();
            btnUpdateCurrentPrimitive = new Button();
            btnCurrentPrimitiveDrawColor = new Button();
            txtCurrentPrimitiveDrawColor = new TextBox();
            lblCurrentPrimitiveDrawColor = new Label();
            txtCurrentPrimitiveNumber = new TextBox();
            lblCurrentPrimitiveNumber = new Label();
            btnAddRoundRect = new Button();
            btnAddTriangle = new Button();
            btnAddCircle = new Button();
            btnRemoveCurrentPrimitive = new Button();
            btnNextPrimitive = new Button();
            btnPreviousPrimitive = new Button();
            txtPrimitiveCount = new TextBox();
            lblPrimitiveCount = new Label();
            selectColor = new ColorDialog();
            grpbxFile = new GroupBox();
            btnSendImage = new Button();
            btnSaveToHeaderFile = new Button();
            lblIPThirdOctetSeperator = new Label();
            lblIPSecondOctetSeperator = new Label();
            lblIPFirstOctetSeperator = new Label();
            txtIPFourthOctet = new TextBox();
            txtIPThirdOctet = new TextBox();
            txtIPSecondOctet = new TextBox();
            txtIPFirstOctet = new TextBox();
            lblIPAddress = new Label();
            btnTransmitFile = new Button();
            btnSaveFile = new Button();
            btnLoadFile = new Button();
            openFile = new OpenFileDialog();
            saveFile = new SaveFileDialog();
            primitiveHighlightTimer = new System.Windows.Forms.Timer(components);
            chkHighlightCurrentPrimitive = new CheckBox();
            grpbxFrames.SuspendLayout();
            grpbxCurrentFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctbxCanvas).BeginInit();
            grpbxPrimitives.SuspendLayout();
            grpbxCurrentPrimitive.SuspendLayout();
            grpbxRotatedRect.SuspendLayout();
            grpbxRoundRect.SuspendLayout();
            grpbxCircle.SuspendLayout();
            grpbxTriangle.SuspendLayout();
            grpbxLine.SuspendLayout();
            grpbxFile.SuspendLayout();
            SuspendLayout();
            // 
            // grpbxFrames
            // 
            grpbxFrames.Controls.Add(btnDuplicateCurrentFrame);
            grpbxFrames.Controls.Add(txtFrameDuration);
            grpbxFrames.Controls.Add(lblFrameDuration);
            grpbxFrames.Controls.Add(btnFrameFillColor);
            grpbxFrames.Controls.Add(txtFrameFillColor);
            grpbxFrames.Controls.Add(lblAddFillColor);
            grpbxFrames.Controls.Add(grpbxCurrentFrame);
            grpbxFrames.Controls.Add(btnRemoveCurrentFrame);
            grpbxFrames.Controls.Add(btnNextFrame);
            grpbxFrames.Controls.Add(btnPreviousFrame);
            grpbxFrames.Controls.Add(btnAddFrame);
            grpbxFrames.Controls.Add(txtFrameCount);
            grpbxFrames.Controls.Add(lblFrameCount);
            grpbxFrames.Font = new Font("Segoe UI", 8.25F);
            grpbxFrames.Location = new Point(10, 95);
            grpbxFrames.Name = "grpbxFrames";
            grpbxFrames.Size = new Size(170, 261);
            grpbxFrames.TabIndex = 1;
            grpbxFrames.TabStop = false;
            grpbxFrames.Text = "Frames";
            // 
            // btnDuplicateCurrentFrame
            // 
            btnDuplicateCurrentFrame.Enabled = false;
            btnDuplicateCurrentFrame.Font = new Font("Segoe UI", 8.25F);
            btnDuplicateCurrentFrame.Image = (System.Drawing.Image)resources.GetObject("btnDuplicateCurrentFrame.Image");
            btnDuplicateCurrentFrame.Location = new Point(84, 226);
            btnDuplicateCurrentFrame.Name = "btnDuplicateCurrentFrame";
            btnDuplicateCurrentFrame.Size = new Size(32, 22);
            btnDuplicateCurrentFrame.TabIndex = 26;
            btnDuplicateCurrentFrame.UseVisualStyleBackColor = true;
            btnDuplicateCurrentFrame.Click += btnDuplicateCurrentFrame_Click;
            // 
            // txtFrameDuration
            // 
            txtFrameDuration.Font = new Font("Segoe UI", 8.25F);
            txtFrameDuration.Location = new Point(78, 74);
            txtFrameDuration.Name = "txtFrameDuration";
            txtFrameDuration.Size = new Size(48, 22);
            txtFrameDuration.TabIndex = 25;
            // 
            // lblFrameDuration
            // 
            lblFrameDuration.Font = new Font("Segoe UI", 8.25F);
            lblFrameDuration.Location = new Point(15, 74);
            lblFrameDuration.Name = "lblFrameDuration";
            lblFrameDuration.Size = new Size(63, 22);
            lblFrameDuration.TabIndex = 24;
            lblFrameDuration.Text = "Duration:";
            lblFrameDuration.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnFrameFillColor
            // 
            btnFrameFillColor.Font = new Font("Segoe UI", 8.25F);
            btnFrameFillColor.Location = new Point(132, 46);
            btnFrameFillColor.Name = "btnFrameFillColor";
            btnFrameFillColor.Size = new Size(32, 22);
            btnFrameFillColor.TabIndex = 23;
            btnFrameFillColor.Text = "...";
            btnFrameFillColor.TextAlign = ContentAlignment.TopCenter;
            btnFrameFillColor.UseVisualStyleBackColor = true;
            btnFrameFillColor.Click += btnFrameFillColor_Click;
            // 
            // txtFrameFillColor
            // 
            txtFrameFillColor.Font = new Font("Segoe UI", 8.25F);
            txtFrameFillColor.Location = new Point(78, 46);
            txtFrameFillColor.Name = "txtFrameFillColor";
            txtFrameFillColor.Size = new Size(48, 22);
            txtFrameFillColor.TabIndex = 22;
            // 
            // lblAddFillColor
            // 
            lblAddFillColor.Font = new Font("Segoe UI", 8.25F);
            lblAddFillColor.Location = new Point(15, 46);
            lblAddFillColor.Name = "lblAddFillColor";
            lblAddFillColor.Size = new Size(63, 22);
            lblAddFillColor.TabIndex = 21;
            lblAddFillColor.Text = "Fill Color:";
            lblAddFillColor.TextAlign = ContentAlignment.MiddleRight;
            // 
            // grpbxCurrentFrame
            // 
            grpbxCurrentFrame.Controls.Add(btnMoveFrameUp);
            grpbxCurrentFrame.Controls.Add(btnMoveFrameDown);
            grpbxCurrentFrame.Controls.Add(btnUpdateCurrentFrame);
            grpbxCurrentFrame.Controls.Add(txtCurrentFrameDuration);
            grpbxCurrentFrame.Controls.Add(lblCurrentFrameDuration);
            grpbxCurrentFrame.Controls.Add(btnCurrentFrameFillColor);
            grpbxCurrentFrame.Controls.Add(txtCurrentFrameFillColor);
            grpbxCurrentFrame.Controls.Add(lblCurrentFrameFillColor);
            grpbxCurrentFrame.Controls.Add(txtCurrentFrameNumber);
            grpbxCurrentFrame.Controls.Add(lblCurrentFrameNumber);
            grpbxCurrentFrame.Font = new Font("Segoe UI", 8.25F);
            grpbxCurrentFrame.Location = new Point(5, 102);
            grpbxCurrentFrame.Name = "grpbxCurrentFrame";
            grpbxCurrentFrame.Size = new Size(160, 120);
            grpbxCurrentFrame.TabIndex = 14;
            grpbxCurrentFrame.TabStop = false;
            grpbxCurrentFrame.Text = "Current Frame";
            // 
            // btnMoveFrameUp
            // 
            btnMoveFrameUp.Enabled = false;
            btnMoveFrameUp.Font = new Font("Segoe UI", 8.25F);
            btnMoveFrameUp.Location = new Point(102, 92);
            btnMoveFrameUp.Name = "btnMoveFrameUp";
            btnMoveFrameUp.Size = new Size(32, 22);
            btnMoveFrameUp.TabIndex = 23;
            btnMoveFrameUp.Text = ">>";
            btnMoveFrameUp.UseVisualStyleBackColor = true;
            btnMoveFrameUp.Click += btnMoveFrameUp_Click;
            // 
            // btnMoveFrameDown
            // 
            btnMoveFrameDown.Enabled = false;
            btnMoveFrameDown.Font = new Font("Segoe UI", 8.25F);
            btnMoveFrameDown.Location = new Point(38, 92);
            btnMoveFrameDown.Name = "btnMoveFrameDown";
            btnMoveFrameDown.Size = new Size(32, 22);
            btnMoveFrameDown.TabIndex = 22;
            btnMoveFrameDown.Text = "<<";
            btnMoveFrameDown.UseVisualStyleBackColor = true;
            btnMoveFrameDown.Click += btnMoveFrameDown_Click;
            // 
            // btnUpdateCurrentFrame
            // 
            btnUpdateCurrentFrame.Enabled = false;
            btnUpdateCurrentFrame.Font = new Font("Segoe UI", 8.25F);
            btnUpdateCurrentFrame.Image = (System.Drawing.Image)resources.GetObject("btnUpdateCurrentFrame.Image");
            btnUpdateCurrentFrame.Location = new Point(70, 92);
            btnUpdateCurrentFrame.Name = "btnUpdateCurrentFrame";
            btnUpdateCurrentFrame.Size = new Size(32, 22);
            btnUpdateCurrentFrame.TabIndex = 21;
            btnUpdateCurrentFrame.UseVisualStyleBackColor = true;
            btnUpdateCurrentFrame.Click += btnUpdateCurrentFrame_Click;
            // 
            // txtCurrentFrameDuration
            // 
            txtCurrentFrameDuration.Enabled = false;
            txtCurrentFrameDuration.Font = new Font("Segoe UI", 8.25F);
            txtCurrentFrameDuration.Location = new Point(65, 66);
            txtCurrentFrameDuration.Name = "txtCurrentFrameDuration";
            txtCurrentFrameDuration.Size = new Size(85, 22);
            txtCurrentFrameDuration.TabIndex = 20;
            // 
            // lblCurrentFrameDuration
            // 
            lblCurrentFrameDuration.AutoSize = true;
            lblCurrentFrameDuration.Font = new Font("Segoe UI", 8.25F);
            lblCurrentFrameDuration.Location = new Point(3, 69);
            lblCurrentFrameDuration.Name = "lblCurrentFrameDuration";
            lblCurrentFrameDuration.Size = new Size(56, 13);
            lblCurrentFrameDuration.TabIndex = 19;
            lblCurrentFrameDuration.Text = "Duration:";
            lblCurrentFrameDuration.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCurrentFrameFillColor
            // 
            btnCurrentFrameFillColor.Enabled = false;
            btnCurrentFrameFillColor.Font = new Font("Segoe UI", 8.25F);
            btnCurrentFrameFillColor.Location = new Point(115, 40);
            btnCurrentFrameFillColor.Name = "btnCurrentFrameFillColor";
            btnCurrentFrameFillColor.Size = new Size(35, 22);
            btnCurrentFrameFillColor.TabIndex = 18;
            btnCurrentFrameFillColor.Text = "...";
            btnCurrentFrameFillColor.TextAlign = ContentAlignment.TopCenter;
            btnCurrentFrameFillColor.UseVisualStyleBackColor = true;
            btnCurrentFrameFillColor.Click += btnCurrentFrameFillColor_Click;
            // 
            // txtCurrentFrameFillColor
            // 
            txtCurrentFrameFillColor.Enabled = false;
            txtCurrentFrameFillColor.Font = new Font("Segoe UI", 8.25F);
            txtCurrentFrameFillColor.Location = new Point(65, 40);
            txtCurrentFrameFillColor.Name = "txtCurrentFrameFillColor";
            txtCurrentFrameFillColor.Size = new Size(48, 22);
            txtCurrentFrameFillColor.TabIndex = 17;
            // 
            // lblCurrentFrameFillColor
            // 
            lblCurrentFrameFillColor.AutoSize = true;
            lblCurrentFrameFillColor.Font = new Font("Segoe UI", 8.25F);
            lblCurrentFrameFillColor.Location = new Point(3, 40);
            lblCurrentFrameFillColor.Name = "lblCurrentFrameFillColor";
            lblCurrentFrameFillColor.Size = new Size(56, 13);
            lblCurrentFrameFillColor.TabIndex = 16;
            lblCurrentFrameFillColor.Text = "Fill Color:";
            // 
            // txtCurrentFrameNumber
            // 
            txtCurrentFrameNumber.Enabled = false;
            txtCurrentFrameNumber.Font = new Font("Segoe UI", 8.25F);
            txtCurrentFrameNumber.Location = new Point(65, 15);
            txtCurrentFrameNumber.Name = "txtCurrentFrameNumber";
            txtCurrentFrameNumber.Size = new Size(35, 22);
            txtCurrentFrameNumber.TabIndex = 15;
            txtCurrentFrameNumber.KeyDown += txtCurrentFrame_KeyDown;
            // 
            // lblCurrentFrameNumber
            // 
            lblCurrentFrameNumber.AutoSize = true;
            lblCurrentFrameNumber.Font = new Font("Segoe UI", 8.25F);
            lblCurrentFrameNumber.Location = new Point(8, 18);
            lblCurrentFrameNumber.Name = "lblCurrentFrameNumber";
            lblCurrentFrameNumber.Size = new Size(51, 13);
            lblCurrentFrameNumber.TabIndex = 14;
            lblCurrentFrameNumber.Text = "Number:";
            lblCurrentFrameNumber.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnRemoveCurrentFrame
            // 
            btnRemoveCurrentFrame.Enabled = false;
            btnRemoveCurrentFrame.Font = new Font("Segoe UI", 8.25F);
            btnRemoveCurrentFrame.Image = (System.Drawing.Image)resources.GetObject("btnRemoveCurrentFrame.Image");
            btnRemoveCurrentFrame.Location = new Point(46, 226);
            btnRemoveCurrentFrame.Name = "btnRemoveCurrentFrame";
            btnRemoveCurrentFrame.Size = new Size(32, 22);
            btnRemoveCurrentFrame.TabIndex = 7;
            btnRemoveCurrentFrame.UseVisualStyleBackColor = true;
            btnRemoveCurrentFrame.Click += btnRemoveCurrentFrame_Click;
            // 
            // btnNextFrame
            // 
            btnNextFrame.Enabled = false;
            btnNextFrame.Font = new Font("Segoe UI", 8.25F);
            btnNextFrame.Location = new Point(126, 226);
            btnNextFrame.Name = "btnNextFrame";
            btnNextFrame.Size = new Size(32, 22);
            btnNextFrame.TabIndex = 6;
            btnNextFrame.Text = ">>";
            btnNextFrame.UseVisualStyleBackColor = true;
            btnNextFrame.Click += btnNextFrame_Click;
            // 
            // btnPreviousFrame
            // 
            btnPreviousFrame.Enabled = false;
            btnPreviousFrame.Font = new Font("Segoe UI", 8.25F);
            btnPreviousFrame.Location = new Point(8, 226);
            btnPreviousFrame.Name = "btnPreviousFrame";
            btnPreviousFrame.Size = new Size(32, 22);
            btnPreviousFrame.TabIndex = 5;
            btnPreviousFrame.Text = "<<";
            btnPreviousFrame.UseVisualStyleBackColor = true;
            btnPreviousFrame.Click += btnPreviousFrame_Click;
            // 
            // btnAddFrame
            // 
            btnAddFrame.Font = new Font("Segoe UI", 8.25F);
            btnAddFrame.Image = (System.Drawing.Image)resources.GetObject("btnAddFrame.Image");
            btnAddFrame.Location = new Point(132, 21);
            btnAddFrame.Margin = new Padding(0);
            btnAddFrame.Name = "btnAddFrame";
            btnAddFrame.Size = new Size(32, 22);
            btnAddFrame.TabIndex = 2;
            btnAddFrame.UseVisualStyleBackColor = true;
            btnAddFrame.Click += btnAddFrame_Click;
            // 
            // txtFrameCount
            // 
            txtFrameCount.Font = new Font("Segoe UI", 8.25F);
            txtFrameCount.Location = new Point(78, 20);
            txtFrameCount.Name = "txtFrameCount";
            txtFrameCount.Size = new Size(48, 22);
            txtFrameCount.TabIndex = 1;
            txtFrameCount.KeyDown += txtFrameCount_KeyDown;
            // 
            // lblFrameCount
            // 
            lblFrameCount.Font = new Font("Segoe UI", 8.25F);
            lblFrameCount.Location = new Point(15, 20);
            lblFrameCount.Name = "lblFrameCount";
            lblFrameCount.Size = new Size(63, 22);
            lblFrameCount.TabIndex = 0;
            lblFrameCount.Text = "Count:";
            lblFrameCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pctbxCanvas
            // 
            pctbxCanvas.BackColor = Color.Black;
            pctbxCanvas.Location = new Point(190, 18);
            pctbxCanvas.Name = "pctbxCanvas";
            pctbxCanvas.Size = new Size(384, 384);
            pctbxCanvas.TabIndex = 2;
            pctbxCanvas.TabStop = false;
            pctbxCanvas.Paint += pctbxCanvas_Paint;
            pctbxCanvas.MouseDown += pctbxCanvas_MouseDown;
            pctbxCanvas.MouseMove += pctbxCanvas_MouseMove;
            pctbxCanvas.MouseUp += pctbxCanvas_MouseUp;
            // 
            // grpbxPrimitives
            // 
            grpbxPrimitives.Controls.Add(btnAddRotatedRect);
            grpbxPrimitives.Controls.Add(btnAddLine);
            grpbxPrimitives.Controls.Add(btnPrimitiveDrawColor);
            grpbxPrimitives.Controls.Add(txtPrimitiveDrawColor);
            grpbxPrimitives.Controls.Add(lblAddPrimitiveDrawColor);
            grpbxPrimitives.Controls.Add(grpbxCurrentPrimitive);
            grpbxPrimitives.Controls.Add(btnAddRoundRect);
            grpbxPrimitives.Controls.Add(btnAddTriangle);
            grpbxPrimitives.Controls.Add(btnAddCircle);
            grpbxPrimitives.Controls.Add(btnRemoveCurrentPrimitive);
            grpbxPrimitives.Controls.Add(btnNextPrimitive);
            grpbxPrimitives.Controls.Add(btnPreviousPrimitive);
            grpbxPrimitives.Controls.Add(txtPrimitiveCount);
            grpbxPrimitives.Controls.Add(lblPrimitiveCount);
            grpbxPrimitives.Font = new Font("Segoe UI", 8.25F);
            grpbxPrimitives.Location = new Point(585, 10);
            grpbxPrimitives.Name = "grpbxPrimitives";
            grpbxPrimitives.Size = new Size(170, 367);
            grpbxPrimitives.TabIndex = 3;
            grpbxPrimitives.TabStop = false;
            grpbxPrimitives.Text = "Primitives";
            // 
            // btnAddRotatedRect
            // 
            btnAddRotatedRect.Enabled = false;
            btnAddRotatedRect.Font = new Font("Segoe UI", 8.25F);
            btnAddRotatedRect.Image = (System.Drawing.Image)resources.GetObject("btnAddRotatedRect.Image");
            btnAddRotatedRect.Location = new Point(100, 80);
            btnAddRotatedRect.Name = "btnAddRotatedRect";
            btnAddRotatedRect.Size = new Size(32, 22);
            btnAddRotatedRect.TabIndex = 28;
            btnAddRotatedRect.UseVisualStyleBackColor = true;
            btnAddRotatedRect.Click += btnAddRotatedRect_Click;
            // 
            // btnAddLine
            // 
            btnAddLine.Enabled = false;
            btnAddLine.Font = new Font("Segoe UI", 8.25F);
            btnAddLine.Image = (System.Drawing.Image)resources.GetObject("btnAddLine.Image");
            btnAddLine.Location = new Point(4, 80);
            btnAddLine.Name = "btnAddLine";
            btnAddLine.Size = new Size(32, 22);
            btnAddLine.TabIndex = 27;
            btnAddLine.UseVisualStyleBackColor = true;
            btnAddLine.Click += btnAddLine_Click;
            // 
            // btnPrimitiveDrawColor
            // 
            btnPrimitiveDrawColor.Font = new Font("Segoe UI", 8.25F);
            btnPrimitiveDrawColor.Location = new Point(132, 46);
            btnPrimitiveDrawColor.Name = "btnPrimitiveDrawColor";
            btnPrimitiveDrawColor.Size = new Size(32, 22);
            btnPrimitiveDrawColor.TabIndex = 26;
            btnPrimitiveDrawColor.Text = "...";
            btnPrimitiveDrawColor.TextAlign = ContentAlignment.TopCenter;
            btnPrimitiveDrawColor.UseVisualStyleBackColor = true;
            btnPrimitiveDrawColor.Click += btnPrimitiveDrawColor_Click;
            // 
            // txtPrimitiveDrawColor
            // 
            txtPrimitiveDrawColor.Font = new Font("Segoe UI", 8.25F);
            txtPrimitiveDrawColor.Location = new Point(78, 46);
            txtPrimitiveDrawColor.Name = "txtPrimitiveDrawColor";
            txtPrimitiveDrawColor.Size = new Size(48, 22);
            txtPrimitiveDrawColor.TabIndex = 25;
            // 
            // lblAddPrimitiveDrawColor
            // 
            lblAddPrimitiveDrawColor.Font = new Font("Segoe UI", 8.25F);
            lblAddPrimitiveDrawColor.Location = new Point(15, 46);
            lblAddPrimitiveDrawColor.Name = "lblAddPrimitiveDrawColor";
            lblAddPrimitiveDrawColor.Size = new Size(63, 22);
            lblAddPrimitiveDrawColor.TabIndex = 24;
            lblAddPrimitiveDrawColor.Text = "Color:";
            lblAddPrimitiveDrawColor.TextAlign = ContentAlignment.MiddleRight;
            // 
            // grpbxCurrentPrimitive
            // 
            grpbxCurrentPrimitive.Controls.Add(grpbxRoundRect);
            grpbxCurrentPrimitive.Controls.Add(grpbxRotatedRect);
            grpbxCurrentPrimitive.Controls.Add(grpbxLine);
            grpbxCurrentPrimitive.Controls.Add(grpbxCircle);
            grpbxCurrentPrimitive.Controls.Add(grpbxTriangle);
            grpbxCurrentPrimitive.Controls.Add(btnUpdateCurrentPrimitive);
            grpbxCurrentPrimitive.Controls.Add(btnCurrentPrimitiveDrawColor);
            grpbxCurrentPrimitive.Controls.Add(txtCurrentPrimitiveDrawColor);
            grpbxCurrentPrimitive.Controls.Add(lblCurrentPrimitiveDrawColor);
            grpbxCurrentPrimitive.Controls.Add(txtCurrentPrimitiveNumber);
            grpbxCurrentPrimitive.Controls.Add(lblCurrentPrimitiveNumber);
            grpbxCurrentPrimitive.Font = new Font("Segoe UI", 8.25F);
            grpbxCurrentPrimitive.Location = new Point(6, 110);
            grpbxCurrentPrimitive.Name = "grpbxCurrentPrimitive";
            grpbxCurrentPrimitive.Size = new Size(160, 223);
            grpbxCurrentPrimitive.TabIndex = 23;
            grpbxCurrentPrimitive.TabStop = false;
            grpbxCurrentPrimitive.Text = "Current Primitive";
            // 
            // grpbxRotatedRect
            // 
            grpbxRotatedRect.Controls.Add(txtRotatedRectAngleDeg);
            grpbxRotatedRect.Controls.Add(txtRotatedRectH);
            grpbxRotatedRect.Controls.Add(txtRotatedRectW);
            grpbxRotatedRect.Controls.Add(txtRotatedRectCenY);
            grpbxRotatedRect.Controls.Add(txtRotatedRectCenX);
            grpbxRotatedRect.Controls.Add(lblRotatedRectAngleDeg);
            grpbxRotatedRect.Controls.Add(lblRotatedRectH);
            grpbxRotatedRect.Controls.Add(lblRotatedRectW);
            grpbxRotatedRect.Controls.Add(lblRotatedRectCenY);
            grpbxRotatedRect.Controls.Add(lblRotatedRectCenX);
            grpbxRotatedRect.Location = new Point(8, 70);
            grpbxRotatedRect.Name = "grpbxRotatedRect";
            grpbxRotatedRect.Size = new Size(146, 119);
            grpbxRotatedRect.TabIndex = 33;
            grpbxRotatedRect.TabStop = false;
            grpbxRotatedRect.Text = "Rotated Rectangle";
            grpbxRotatedRect.Visible = false;
            // 
            // txtRotatedRectAngleDeg
            // 
            txtRotatedRectAngleDeg.Location = new Point(109, 64);
            txtRotatedRectAngleDeg.Name = "txtRotatedRectAngleDeg";
            txtRotatedRectAngleDeg.Size = new Size(30, 22);
            txtRotatedRectAngleDeg.TabIndex = 9;
            // 
            // txtRotatedRectH
            // 
            txtRotatedRectH.Location = new Point(109, 40);
            txtRotatedRectH.Name = "txtRotatedRectH";
            txtRotatedRectH.Size = new Size(30, 22);
            txtRotatedRectH.TabIndex = 8;
            // 
            // txtRotatedRectW
            // 
            txtRotatedRectW.Location = new Point(35, 40);
            txtRotatedRectW.Name = "txtRotatedRectW";
            txtRotatedRectW.Size = new Size(30, 22);
            txtRotatedRectW.TabIndex = 7;
            // 
            // txtRotatedRectCenY
            // 
            txtRotatedRectCenY.Location = new Point(109, 16);
            txtRotatedRectCenY.Name = "txtRotatedRectCenY";
            txtRotatedRectCenY.Size = new Size(30, 22);
            txtRotatedRectCenY.TabIndex = 6;
            // 
            // txtRotatedRectCenX
            // 
            txtRotatedRectCenX.Location = new Point(35, 16);
            txtRotatedRectCenX.Name = "txtRotatedRectCenX";
            txtRotatedRectCenX.Size = new Size(30, 22);
            txtRotatedRectCenX.TabIndex = 5;
            // 
            // lblRotatedRectAngleDeg
            // 
            lblRotatedRectAngleDeg.AutoSize = true;
            lblRotatedRectAngleDeg.Location = new Point(51, 67);
            lblRotatedRectAngleDeg.Name = "lblRotatedRectAngleDeg";
            lblRotatedRectAngleDeg.Size = new Size(61, 13);
            lblRotatedRectAngleDeg.TabIndex = 4;
            lblRotatedRectAngleDeg.Text = "AngleDeg:";
            // 
            // lblRotatedRectH
            // 
            lblRotatedRectH.AutoSize = true;
            lblRotatedRectH.Location = new Point(94, 43);
            lblRotatedRectH.Name = "lblRotatedRectH";
            lblRotatedRectH.Size = new Size(18, 13);
            lblRotatedRectH.TabIndex = 3;
            lblRotatedRectH.Text = "H:";
            // 
            // lblRotatedRectW
            // 
            lblRotatedRectW.AutoSize = true;
            lblRotatedRectW.Location = new Point(6, 45);
            lblRotatedRectW.Name = "lblRotatedRectW";
            lblRotatedRectW.Size = new Size(21, 13);
            lblRotatedRectW.TabIndex = 2;
            lblRotatedRectW.Text = "W:";
            // 
            // lblRotatedRectCenY
            // 
            lblRotatedRectCenY.AutoSize = true;
            lblRotatedRectCenY.Location = new Point(77, 19);
            lblRotatedRectCenY.Name = "lblRotatedRectCenY";
            lblRotatedRectCenY.Size = new Size(35, 13);
            lblRotatedRectCenY.TabIndex = 1;
            lblRotatedRectCenY.Text = "CenY:";
            // 
            // lblRotatedRectCenX
            // 
            lblRotatedRectCenX.AutoSize = true;
            lblRotatedRectCenX.Location = new Point(0, 19);
            lblRotatedRectCenX.Name = "lblRotatedRectCenX";
            lblRotatedRectCenX.Size = new Size(36, 13);
            lblRotatedRectCenX.TabIndex = 0;
            lblRotatedRectCenX.Text = "CenX:";
            // 
            // grpbxRoundRect
            // 
            grpbxRoundRect.Controls.Add(txtRoundRectRadius);
            grpbxRoundRect.Controls.Add(txtRoundRectH);
            grpbxRoundRect.Controls.Add(txtRoundRectW);
            grpbxRoundRect.Controls.Add(txtRoundRectY0);
            grpbxRoundRect.Controls.Add(txtRoundRectX0);
            grpbxRoundRect.Controls.Add(lblRoundRectRadius);
            grpbxRoundRect.Controls.Add(lblRoundRectH);
            grpbxRoundRect.Controls.Add(lblRoundRectW);
            grpbxRoundRect.Controls.Add(lblRoundRectY0);
            grpbxRoundRect.Controls.Add(lblRoundRectX0);
            grpbxRoundRect.Location = new Point(8, 70);
            grpbxRoundRect.Name = "grpbxRoundRect";
            grpbxRoundRect.Size = new Size(146, 119);
            grpbxRoundRect.TabIndex = 26;
            grpbxRoundRect.TabStop = false;
            grpbxRoundRect.Text = "Rectangle";
            grpbxRoundRect.Visible = false;
            // 
            // txtRoundRectRadius
            // 
            txtRoundRectRadius.Location = new Point(109, 64);
            txtRoundRectRadius.Name = "txtRoundRectRadius";
            txtRoundRectRadius.Size = new Size(30, 22);
            txtRoundRectRadius.TabIndex = 9;
            // 
            // txtRoundRectH
            // 
            txtRoundRectH.Location = new Point(109, 40);
            txtRoundRectH.Name = "txtRoundRectH";
            txtRoundRectH.Size = new Size(30, 22);
            txtRoundRectH.TabIndex = 8;
            // 
            // txtRoundRectW
            // 
            txtRoundRectW.Location = new Point(35, 40);
            txtRoundRectW.Name = "txtRoundRectW";
            txtRoundRectW.Size = new Size(30, 22);
            txtRoundRectW.TabIndex = 7;
            // 
            // txtRoundRectY0
            // 
            txtRoundRectY0.Location = new Point(109, 16);
            txtRoundRectY0.Name = "txtRoundRectY0";
            txtRoundRectY0.Size = new Size(30, 22);
            txtRoundRectY0.TabIndex = 6;
            // 
            // txtRoundRectX0
            // 
            txtRoundRectX0.Location = new Point(35, 16);
            txtRoundRectX0.Name = "txtRoundRectX0";
            txtRoundRectX0.Size = new Size(30, 22);
            txtRoundRectX0.TabIndex = 5;
            // 
            // lblRoundRectRadius
            // 
            lblRoundRectRadius.AutoSize = true;
            lblRoundRectRadius.Location = new Point(67, 67);
            lblRoundRectRadius.Name = "lblRoundRectRadius";
            lblRoundRectRadius.Size = new Size(45, 13);
            lblRoundRectRadius.TabIndex = 4;
            lblRoundRectRadius.Text = "Radius:";
            // 
            // lblRoundRectH
            // 
            lblRoundRectH.AutoSize = true;
            lblRoundRectH.Location = new Point(94, 43);
            lblRoundRectH.Name = "lblRoundRectH";
            lblRoundRectH.Size = new Size(18, 13);
            lblRoundRectH.TabIndex = 3;
            lblRoundRectH.Text = "H:";
            // 
            // lblRoundRectW
            // 
            lblRoundRectW.AutoSize = true;
            lblRoundRectW.Location = new Point(6, 45);
            lblRoundRectW.Name = "lblRoundRectW";
            lblRoundRectW.Size = new Size(21, 13);
            lblRoundRectW.TabIndex = 2;
            lblRoundRectW.Text = "W:";
            // 
            // lblRoundRectY0
            // 
            lblRoundRectY0.AutoSize = true;
            lblRoundRectY0.Location = new Point(91, 19);
            lblRoundRectY0.Name = "lblRoundRectY0";
            lblRoundRectY0.Size = new Size(21, 13);
            lblRoundRectY0.TabIndex = 1;
            lblRoundRectY0.Text = "Y0:";
            // 
            // lblRoundRectX0
            // 
            lblRoundRectX0.AutoSize = true;
            lblRoundRectX0.Location = new Point(6, 19);
            lblRoundRectX0.Name = "lblRoundRectX0";
            lblRoundRectX0.Size = new Size(22, 13);
            lblRoundRectX0.TabIndex = 0;
            lblRoundRectX0.Text = "X0:";
            // 
            // grpbxCircle
            // 
            grpbxCircle.Controls.Add(chkCircleTopLeft);
            grpbxCircle.Controls.Add(chkCircleTopRight);
            grpbxCircle.Controls.Add(chkCircleBottomLeft);
            grpbxCircle.Controls.Add(chkCircleBottomRight);
            grpbxCircle.Controls.Add(txtCircleR);
            grpbxCircle.Controls.Add(txtCircleDelta);
            grpbxCircle.Controls.Add(txtCircleY0);
            grpbxCircle.Controls.Add(txtCircleX0);
            grpbxCircle.Controls.Add(lblCircleR);
            grpbxCircle.Controls.Add(lblCircleDelta);
            grpbxCircle.Controls.Add(lblCircleQuadrants);
            grpbxCircle.Controls.Add(lblCircleY0);
            grpbxCircle.Controls.Add(lblCircleX0);
            grpbxCircle.Location = new Point(8, 70);
            grpbxCircle.Name = "grpbxCircle";
            grpbxCircle.Size = new Size(146, 119);
            grpbxCircle.TabIndex = 31;
            grpbxCircle.TabStop = false;
            grpbxCircle.Text = "Circle";
            grpbxCircle.Visible = false;
            // 
            // chkCircleTopLeft
            // 
            chkCircleTopLeft.Appearance = Appearance.Button;
            chkCircleTopLeft.AutoSize = true;
            chkCircleTopLeft.CheckAlign = ContentAlignment.MiddleCenter;
            chkCircleTopLeft.FlatAppearance.CheckedBackColor = Color.LightGreen;
            chkCircleTopLeft.FlatStyle = FlatStyle.Flat;
            chkCircleTopLeft.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkCircleTopLeft.Image = (System.Drawing.Image)resources.GetObject("chkCircleTopLeft.Image");
            chkCircleTopLeft.ImageAlign = ContentAlignment.TopLeft;
            chkCircleTopLeft.Location = new Point(107, 77);
            chkCircleTopLeft.Margin = new Padding(0);
            chkCircleTopLeft.Name = "chkCircleTopLeft";
            chkCircleTopLeft.Size = new Size(18, 18);
            chkCircleTopLeft.TabIndex = 15;
            chkCircleTopLeft.UseVisualStyleBackColor = true;
            chkCircleTopLeft.CheckedChanged += chkCircleTopLeft_CheckedChanged;
            // 
            // chkCircleTopRight
            // 
            chkCircleTopRight.Appearance = Appearance.Button;
            chkCircleTopRight.AutoSize = true;
            chkCircleTopRight.CheckAlign = ContentAlignment.MiddleCenter;
            chkCircleTopRight.FlatAppearance.CheckedBackColor = Color.LightGreen;
            chkCircleTopRight.FlatStyle = FlatStyle.Flat;
            chkCircleTopRight.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkCircleTopRight.Image = (System.Drawing.Image)resources.GetObject("chkCircleTopRight.Image");
            chkCircleTopRight.ImageAlign = ContentAlignment.TopRight;
            chkCircleTopRight.Location = new Point(125, 77);
            chkCircleTopRight.Margin = new Padding(0);
            chkCircleTopRight.Name = "chkCircleTopRight";
            chkCircleTopRight.Size = new Size(18, 18);
            chkCircleTopRight.TabIndex = 16;
            chkCircleTopRight.UseVisualStyleBackColor = true;
            chkCircleTopRight.CheckedChanged += chkCircleTopRight_CheckedChanged;
            // 
            // chkCircleBottomLeft
            // 
            chkCircleBottomLeft.Appearance = Appearance.Button;
            chkCircleBottomLeft.AutoSize = true;
            chkCircleBottomLeft.CheckAlign = ContentAlignment.MiddleCenter;
            chkCircleBottomLeft.FlatAppearance.CheckedBackColor = Color.LightGreen;
            chkCircleBottomLeft.FlatStyle = FlatStyle.Flat;
            chkCircleBottomLeft.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkCircleBottomLeft.Image = (System.Drawing.Image)resources.GetObject("chkCircleBottomLeft.Image");
            chkCircleBottomLeft.ImageAlign = ContentAlignment.BottomLeft;
            chkCircleBottomLeft.Location = new Point(107, 95);
            chkCircleBottomLeft.Margin = new Padding(0);
            chkCircleBottomLeft.Name = "chkCircleBottomLeft";
            chkCircleBottomLeft.Size = new Size(18, 18);
            chkCircleBottomLeft.TabIndex = 17;
            chkCircleBottomLeft.UseVisualStyleBackColor = true;
            chkCircleBottomLeft.CheckedChanged += chkCircleBottomLeft_CheckedChanged;
            // 
            // chkCircleBottomRight
            // 
            chkCircleBottomRight.Appearance = Appearance.Button;
            chkCircleBottomRight.AutoSize = true;
            chkCircleBottomRight.CheckAlign = ContentAlignment.MiddleCenter;
            chkCircleBottomRight.FlatAppearance.CheckedBackColor = Color.LightGreen;
            chkCircleBottomRight.FlatStyle = FlatStyle.Flat;
            chkCircleBottomRight.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkCircleBottomRight.Image = (System.Drawing.Image)resources.GetObject("chkCircleBottomRight.Image");
            chkCircleBottomRight.ImageAlign = ContentAlignment.BottomRight;
            chkCircleBottomRight.Location = new Point(125, 95);
            chkCircleBottomRight.Margin = new Padding(0);
            chkCircleBottomRight.Name = "chkCircleBottomRight";
            chkCircleBottomRight.Size = new Size(18, 18);
            chkCircleBottomRight.TabIndex = 18;
            chkCircleBottomRight.UseVisualStyleBackColor = true;
            chkCircleBottomRight.CheckedChanged += chkCircleBottomRight_CheckedChanged;
            // 
            // txtCircleR
            // 
            txtCircleR.Location = new Point(50, 40);
            txtCircleR.Name = "txtCircleR";
            txtCircleR.Size = new Size(30, 22);
            txtCircleR.TabIndex = 9;
            // 
            // txtCircleDelta
            // 
            txtCircleDelta.Location = new Point(109, 40);
            txtCircleDelta.Name = "txtCircleDelta";
            txtCircleDelta.Size = new Size(30, 22);
            txtCircleDelta.TabIndex = 8;
            // 
            // txtCircleY0
            // 
            txtCircleY0.Location = new Point(109, 16);
            txtCircleY0.Name = "txtCircleY0";
            txtCircleY0.Size = new Size(30, 22);
            txtCircleY0.TabIndex = 6;
            // 
            // txtCircleX0
            // 
            txtCircleX0.Location = new Point(50, 16);
            txtCircleX0.Name = "txtCircleX0";
            txtCircleX0.Size = new Size(30, 22);
            txtCircleX0.TabIndex = 5;
            // 
            // lblCircleR
            // 
            lblCircleR.AutoSize = true;
            lblCircleR.Location = new Point(34, 45);
            lblCircleR.Name = "lblCircleR";
            lblCircleR.Size = new Size(17, 13);
            lblCircleR.TabIndex = 4;
            lblCircleR.Text = "R:";
            // 
            // lblCircleDelta
            // 
            lblCircleDelta.AutoSize = true;
            lblCircleDelta.Location = new Point(94, 45);
            lblCircleDelta.Name = "lblCircleDelta";
            lblCircleDelta.Size = new Size(18, 13);
            lblCircleDelta.TabIndex = 3;
            lblCircleDelta.Text = "D:";
            // 
            // lblCircleQuadrants
            // 
            lblCircleQuadrants.AutoSize = true;
            lblCircleQuadrants.Location = new Point(41, 89);
            lblCircleQuadrants.Name = "lblCircleQuadrants";
            lblCircleQuadrants.Size = new Size(64, 13);
            lblCircleQuadrants.TabIndex = 2;
            lblCircleQuadrants.Text = "Quadrants:";
            // 
            // lblCircleY0
            // 
            lblCircleY0.AutoSize = true;
            lblCircleY0.Location = new Point(91, 19);
            lblCircleY0.Name = "lblCircleY0";
            lblCircleY0.Size = new Size(21, 13);
            lblCircleY0.TabIndex = 1;
            lblCircleY0.Text = "Y0:";
            // 
            // lblCircleX0
            // 
            lblCircleX0.AutoSize = true;
            lblCircleX0.Location = new Point(29, 19);
            lblCircleX0.Name = "lblCircleX0";
            lblCircleX0.Size = new Size(22, 13);
            lblCircleX0.TabIndex = 0;
            lblCircleX0.Text = "X0:";
            // 
            // grpbxTriangle
            // 
            grpbxTriangle.Controls.Add(txtTriangleY1);
            grpbxTriangle.Controls.Add(txtTriangleY0);
            grpbxTriangle.Controls.Add(txtTriangleX1);
            grpbxTriangle.Controls.Add(txtTriangleX0);
            grpbxTriangle.Controls.Add(lblTriangleY1);
            grpbxTriangle.Controls.Add(txtTriangleY2);
            grpbxTriangle.Controls.Add(txtTriangleX2);
            grpbxTriangle.Controls.Add(lblTriangleY2);
            grpbxTriangle.Controls.Add(lblTriangleX2);
            grpbxTriangle.Controls.Add(lblTriangleY0);
            grpbxTriangle.Controls.Add(lblTriangleX1);
            grpbxTriangle.Controls.Add(lblTriangleX0);
            grpbxTriangle.Location = new Point(8, 70);
            grpbxTriangle.Name = "grpbxTriangle";
            grpbxTriangle.Size = new Size(146, 119);
            grpbxTriangle.TabIndex = 29;
            grpbxTriangle.TabStop = false;
            grpbxTriangle.Text = "Triangle";
            grpbxTriangle.Visible = false;
            // 
            // txtTriangleY1
            // 
            txtTriangleY1.Location = new Point(102, 40);
            txtTriangleY1.Name = "txtTriangleY1";
            txtTriangleY1.Size = new Size(30, 22);
            txtTriangleY1.TabIndex = 17;
            // 
            // txtTriangleY0
            // 
            txtTriangleY0.Location = new Point(102, 16);
            txtTriangleY0.Name = "txtTriangleY0";
            txtTriangleY0.Size = new Size(30, 22);
            txtTriangleY0.TabIndex = 16;
            // 
            // txtTriangleX1
            // 
            txtTriangleX1.Location = new Point(35, 40);
            txtTriangleX1.Name = "txtTriangleX1";
            txtTriangleX1.Size = new Size(30, 22);
            txtTriangleX1.TabIndex = 15;
            // 
            // txtTriangleX0
            // 
            txtTriangleX0.Location = new Point(35, 16);
            txtTriangleX0.Name = "txtTriangleX0";
            txtTriangleX0.Size = new Size(30, 22);
            txtTriangleX0.TabIndex = 14;
            // 
            // lblTriangleY1
            // 
            lblTriangleY1.AutoSize = true;
            lblTriangleY1.Location = new Point(77, 43);
            lblTriangleY1.Name = "lblTriangleY1";
            lblTriangleY1.Size = new Size(21, 13);
            lblTriangleY1.TabIndex = 13;
            lblTriangleY1.Text = "Y1:";
            // 
            // txtTriangleY2
            // 
            txtTriangleY2.Location = new Point(102, 64);
            txtTriangleY2.Name = "txtTriangleY2";
            txtTriangleY2.Size = new Size(30, 22);
            txtTriangleY2.TabIndex = 12;
            // 
            // txtTriangleX2
            // 
            txtTriangleX2.Location = new Point(35, 64);
            txtTriangleX2.Name = "txtTriangleX2";
            txtTriangleX2.Size = new Size(30, 22);
            txtTriangleX2.TabIndex = 11;
            // 
            // lblTriangleY2
            // 
            lblTriangleY2.AutoSize = true;
            lblTriangleY2.Location = new Point(77, 67);
            lblTriangleY2.Name = "lblTriangleY2";
            lblTriangleY2.Size = new Size(21, 13);
            lblTriangleY2.TabIndex = 10;
            lblTriangleY2.Text = "Y2:";
            // 
            // lblTriangleX2
            // 
            lblTriangleX2.AutoSize = true;
            lblTriangleX2.Location = new Point(6, 67);
            lblTriangleX2.Name = "lblTriangleX2";
            lblTriangleX2.Size = new Size(22, 13);
            lblTriangleX2.TabIndex = 9;
            lblTriangleX2.Text = "X2:";
            // 
            // lblTriangleY0
            // 
            lblTriangleY0.AutoSize = true;
            lblTriangleY0.Location = new Point(77, 19);
            lblTriangleY0.Name = "lblTriangleY0";
            lblTriangleY0.Size = new Size(21, 13);
            lblTriangleY0.TabIndex = 2;
            lblTriangleY0.Text = "Y0:";
            // 
            // lblTriangleX1
            // 
            lblTriangleX1.AutoSize = true;
            lblTriangleX1.Location = new Point(6, 43);
            lblTriangleX1.Name = "lblTriangleX1";
            lblTriangleX1.Size = new Size(22, 13);
            lblTriangleX1.TabIndex = 1;
            lblTriangleX1.Text = "X1:";
            // 
            // lblTriangleX0
            // 
            lblTriangleX0.AutoSize = true;
            lblTriangleX0.Location = new Point(6, 19);
            lblTriangleX0.Name = "lblTriangleX0";
            lblTriangleX0.Size = new Size(22, 13);
            lblTriangleX0.TabIndex = 0;
            lblTriangleX0.Text = "X0:";
            // 
            // grpbxLine
            // 
            grpbxLine.Controls.Add(txtLineY1);
            grpbxLine.Controls.Add(txtLineY0);
            grpbxLine.Controls.Add(txtLineX1);
            grpbxLine.Controls.Add(txtLineX0);
            grpbxLine.Controls.Add(lblLineY1);
            grpbxLine.Controls.Add(lblLineY0);
            grpbxLine.Controls.Add(lblLineX1);
            grpbxLine.Controls.Add(lblLineX0);
            grpbxLine.Location = new Point(8, 70);
            grpbxLine.Name = "grpbxLine";
            grpbxLine.Size = new Size(146, 119);
            grpbxLine.TabIndex = 32;
            grpbxLine.TabStop = false;
            grpbxLine.Text = "Line";
            grpbxLine.Visible = false;
            // 
            // txtLineY1
            // 
            txtLineY1.Location = new Point(109, 40);
            txtLineY1.Name = "txtLineY1";
            txtLineY1.Size = new Size(30, 22);
            txtLineY1.TabIndex = 17;
            // 
            // txtLineY0
            // 
            txtLineY0.Location = new Point(109, 16);
            txtLineY0.Name = "txtLineY0";
            txtLineY0.Size = new Size(30, 22);
            txtLineY0.TabIndex = 16;
            // 
            // txtLineX1
            // 
            txtLineX1.Location = new Point(35, 40);
            txtLineX1.Name = "txtLineX1";
            txtLineX1.Size = new Size(30, 22);
            txtLineX1.TabIndex = 15;
            // 
            // txtLineX0
            // 
            txtLineX0.Location = new Point(35, 16);
            txtLineX0.Name = "txtLineX0";
            txtLineX0.Size = new Size(30, 22);
            txtLineX0.TabIndex = 14;
            // 
            // lblLineY1
            // 
            lblLineY1.AutoSize = true;
            lblLineY1.Location = new Point(77, 43);
            lblLineY1.Name = "lblLineY1";
            lblLineY1.Size = new Size(21, 13);
            lblLineY1.TabIndex = 13;
            lblLineY1.Text = "Y1:";
            // 
            // lblLineY0
            // 
            lblLineY0.AutoSize = true;
            lblLineY0.Location = new Point(77, 19);
            lblLineY0.Name = "lblLineY0";
            lblLineY0.Size = new Size(21, 13);
            lblLineY0.TabIndex = 2;
            lblLineY0.Text = "Y0:";
            // 
            // lblLineX1
            // 
            lblLineX1.AutoSize = true;
            lblLineX1.Location = new Point(6, 43);
            lblLineX1.Name = "lblLineX1";
            lblLineX1.Size = new Size(22, 13);
            lblLineX1.TabIndex = 1;
            lblLineX1.Text = "X1:";
            // 
            // lblLineX0
            // 
            lblLineX0.AutoSize = true;
            lblLineX0.Location = new Point(6, 19);
            lblLineX0.Name = "lblLineX0";
            lblLineX0.Size = new Size(22, 13);
            lblLineX0.TabIndex = 0;
            lblLineX0.Text = "X0:";
            // 
            // btnUpdateCurrentPrimitive
            // 
            btnUpdateCurrentPrimitive.Enabled = false;
            btnUpdateCurrentPrimitive.Font = new Font("Segoe UI", 8.25F);
            btnUpdateCurrentPrimitive.Image = (System.Drawing.Image)resources.GetObject("btnUpdateCurrentPrimitive.Image");
            btnUpdateCurrentPrimitive.Location = new Point(64, 195);
            btnUpdateCurrentPrimitive.Name = "btnUpdateCurrentPrimitive";
            btnUpdateCurrentPrimitive.Size = new Size(32, 22);
            btnUpdateCurrentPrimitive.TabIndex = 23;
            btnUpdateCurrentPrimitive.UseVisualStyleBackColor = true;
            btnUpdateCurrentPrimitive.Click += btnUpdateCurrentPrimitive_Click;
            // 
            // btnCurrentPrimitiveDrawColor
            // 
            btnCurrentPrimitiveDrawColor.Enabled = false;
            btnCurrentPrimitiveDrawColor.Font = new Font("Segoe UI", 8.25F);
            btnCurrentPrimitiveDrawColor.Location = new Point(115, 40);
            btnCurrentPrimitiveDrawColor.Name = "btnCurrentPrimitiveDrawColor";
            btnCurrentPrimitiveDrawColor.Size = new Size(35, 22);
            btnCurrentPrimitiveDrawColor.TabIndex = 22;
            btnCurrentPrimitiveDrawColor.Text = "...";
            btnCurrentPrimitiveDrawColor.TextAlign = ContentAlignment.TopCenter;
            btnCurrentPrimitiveDrawColor.UseVisualStyleBackColor = true;
            btnCurrentPrimitiveDrawColor.Click += btnCurrentPrimitiveDrawColor_Click;
            // 
            // txtCurrentPrimitiveDrawColor
            // 
            txtCurrentPrimitiveDrawColor.Enabled = false;
            txtCurrentPrimitiveDrawColor.Font = new Font("Segoe UI", 8.25F);
            txtCurrentPrimitiveDrawColor.Location = new Point(65, 40);
            txtCurrentPrimitiveDrawColor.Name = "txtCurrentPrimitiveDrawColor";
            txtCurrentPrimitiveDrawColor.Size = new Size(48, 22);
            txtCurrentPrimitiveDrawColor.TabIndex = 21;
            // 
            // lblCurrentPrimitiveDrawColor
            // 
            lblCurrentPrimitiveDrawColor.AutoSize = true;
            lblCurrentPrimitiveDrawColor.Font = new Font("Segoe UI", 8.25F);
            lblCurrentPrimitiveDrawColor.Location = new Point(21, 40);
            lblCurrentPrimitiveDrawColor.Name = "lblCurrentPrimitiveDrawColor";
            lblCurrentPrimitiveDrawColor.Size = new Size(38, 13);
            lblCurrentPrimitiveDrawColor.TabIndex = 20;
            lblCurrentPrimitiveDrawColor.Text = "Color:";
            // 
            // txtCurrentPrimitiveNumber
            // 
            txtCurrentPrimitiveNumber.Enabled = false;
            txtCurrentPrimitiveNumber.Font = new Font("Segoe UI", 8.25F);
            txtCurrentPrimitiveNumber.Location = new Point(115, 15);
            txtCurrentPrimitiveNumber.Name = "txtCurrentPrimitiveNumber";
            txtCurrentPrimitiveNumber.Size = new Size(36, 22);
            txtCurrentPrimitiveNumber.TabIndex = 19;
            txtCurrentPrimitiveNumber.KeyDown += txtCurrentPrimitive_KeyDown;
            // 
            // lblCurrentPrimitiveNumber
            // 
            lblCurrentPrimitiveNumber.AutoSize = true;
            lblCurrentPrimitiveNumber.Font = new Font("Segoe UI", 8.25F);
            lblCurrentPrimitiveNumber.Location = new Point(58, 18);
            lblCurrentPrimitiveNumber.Name = "lblCurrentPrimitiveNumber";
            lblCurrentPrimitiveNumber.Size = new Size(51, 13);
            lblCurrentPrimitiveNumber.TabIndex = 18;
            lblCurrentPrimitiveNumber.Text = "Number:";
            lblCurrentPrimitiveNumber.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnAddRoundRect
            // 
            btnAddRoundRect.Enabled = false;
            btnAddRoundRect.Font = new Font("Segoe UI", 8.25F);
            btnAddRoundRect.Image = (System.Drawing.Image)resources.GetObject("btnAddRoundRect.Image");
            btnAddRoundRect.Location = new Point(68, 80);
            btnAddRoundRect.Name = "btnAddRoundRect";
            btnAddRoundRect.Size = new Size(32, 22);
            btnAddRoundRect.TabIndex = 21;
            btnAddRoundRect.UseVisualStyleBackColor = true;
            btnAddRoundRect.Click += btnAddRoundRect_Click;
            // 
            // btnAddTriangle
            // 
            btnAddTriangle.Enabled = false;
            btnAddTriangle.Font = new Font("Segoe UI", 8.25F);
            btnAddTriangle.Image = (System.Drawing.Image)resources.GetObject("btnAddTriangle.Image");
            btnAddTriangle.Location = new Point(36, 80);
            btnAddTriangle.Name = "btnAddTriangle";
            btnAddTriangle.Size = new Size(32, 22);
            btnAddTriangle.TabIndex = 20;
            btnAddTriangle.UseVisualStyleBackColor = true;
            btnAddTriangle.Click += btnAddTriangle_Click;
            // 
            // btnAddCircle
            // 
            btnAddCircle.Enabled = false;
            btnAddCircle.Font = new Font("Segoe UI", 8.25F);
            btnAddCircle.Image = (System.Drawing.Image)resources.GetObject("btnAddCircle.Image");
            btnAddCircle.Location = new Point(134, 80);
            btnAddCircle.Name = "btnAddCircle";
            btnAddCircle.Size = new Size(32, 22);
            btnAddCircle.TabIndex = 18;
            btnAddCircle.UseVisualStyleBackColor = true;
            btnAddCircle.Click += btnAddCircle_Click;
            // 
            // btnRemoveCurrentPrimitive
            // 
            btnRemoveCurrentPrimitive.Enabled = false;
            btnRemoveCurrentPrimitive.Font = new Font("Segoe UI", 8.25F);
            btnRemoveCurrentPrimitive.Image = (System.Drawing.Image)resources.GetObject("btnRemoveCurrentPrimitive.Image");
            btnRemoveCurrentPrimitive.Location = new Point(70, 339);
            btnRemoveCurrentPrimitive.Name = "btnRemoveCurrentPrimitive";
            btnRemoveCurrentPrimitive.Size = new Size(32, 22);
            btnRemoveCurrentPrimitive.TabIndex = 14;
            btnRemoveCurrentPrimitive.UseVisualStyleBackColor = true;
            btnRemoveCurrentPrimitive.Click += btnRemoveCurrentPrimitive_Click;
            // 
            // btnNextPrimitive
            // 
            btnNextPrimitive.Enabled = false;
            btnNextPrimitive.Font = new Font("Segoe UI", 8.25F);
            btnNextPrimitive.Location = new Point(127, 339);
            btnNextPrimitive.Name = "btnNextPrimitive";
            btnNextPrimitive.Size = new Size(32, 22);
            btnNextPrimitive.TabIndex = 13;
            btnNextPrimitive.Text = ">>";
            btnNextPrimitive.UseVisualStyleBackColor = true;
            btnNextPrimitive.Click += btnNextPrimitive_Click;
            // 
            // btnPreviousPrimitive
            // 
            btnPreviousPrimitive.Enabled = false;
            btnPreviousPrimitive.Font = new Font("Segoe UI", 8.25F);
            btnPreviousPrimitive.Location = new Point(9, 339);
            btnPreviousPrimitive.Name = "btnPreviousPrimitive";
            btnPreviousPrimitive.Size = new Size(32, 22);
            btnPreviousPrimitive.TabIndex = 12;
            btnPreviousPrimitive.Text = "<<";
            btnPreviousPrimitive.UseVisualStyleBackColor = true;
            btnPreviousPrimitive.Click += btnPreviousPrimitive_Click;
            // 
            // txtPrimitiveCount
            // 
            txtPrimitiveCount.Font = new Font("Segoe UI", 8.25F);
            txtPrimitiveCount.Location = new Point(78, 20);
            txtPrimitiveCount.Name = "txtPrimitiveCount";
            txtPrimitiveCount.Size = new Size(48, 22);
            txtPrimitiveCount.TabIndex = 8;
            txtPrimitiveCount.KeyDown += txtPrimitiveCount_KeyDown;
            // 
            // lblPrimitiveCount
            // 
            lblPrimitiveCount.Font = new Font("Segoe UI", 8.25F);
            lblPrimitiveCount.Location = new Point(15, 20);
            lblPrimitiveCount.Name = "lblPrimitiveCount";
            lblPrimitiveCount.Size = new Size(63, 22);
            lblPrimitiveCount.TabIndex = 7;
            lblPrimitiveCount.Text = "Count:";
            lblPrimitiveCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // grpbxFile
            // 
            grpbxFile.Controls.Add(btnSendImage);
            grpbxFile.Controls.Add(btnSaveToHeaderFile);
            grpbxFile.Controls.Add(lblIPThirdOctetSeperator);
            grpbxFile.Controls.Add(lblIPSecondOctetSeperator);
            grpbxFile.Controls.Add(lblIPFirstOctetSeperator);
            grpbxFile.Controls.Add(txtIPFourthOctet);
            grpbxFile.Controls.Add(txtIPThirdOctet);
            grpbxFile.Controls.Add(txtIPSecondOctet);
            grpbxFile.Controls.Add(txtIPFirstOctet);
            grpbxFile.Controls.Add(lblIPAddress);
            grpbxFile.Controls.Add(btnTransmitFile);
            grpbxFile.Controls.Add(btnSaveFile);
            grpbxFile.Controls.Add(btnLoadFile);
            grpbxFile.Font = new Font("Segoe UI", 8.25F);
            grpbxFile.Location = new Point(10, 10);
            grpbxFile.Name = "grpbxFile";
            grpbxFile.Size = new Size(170, 79);
            grpbxFile.TabIndex = 4;
            grpbxFile.TabStop = false;
            grpbxFile.Text = "File";
            // 
            // btnSendImage
            // 
            btnSendImage.Image = (System.Drawing.Image)resources.GetObject("btnSendImage.Image");
            btnSendImage.Location = new Point(134, 20);
            btnSendImage.Name = "btnSendImage";
            btnSendImage.Size = new Size(32, 22);
            btnSendImage.TabIndex = 12;
            btnSendImage.UseVisualStyleBackColor = true;
            btnSendImage.Click += btnSendImage_Click;
            // 
            // btnSaveToHeaderFile
            // 
            btnSaveToHeaderFile.Font = new Font("Segoe UI", 8.25F);
            btnSaveToHeaderFile.Image = (System.Drawing.Image)resources.GetObject("btnSaveToHeaderFile.Image");
            btnSaveToHeaderFile.Location = new Point(102, 20);
            btnSaveToHeaderFile.Name = "btnSaveToHeaderFile";
            btnSaveToHeaderFile.Size = new Size(32, 22);
            btnSaveToHeaderFile.TabIndex = 11;
            btnSaveToHeaderFile.UseVisualStyleBackColor = true;
            btnSaveToHeaderFile.Click += btnSaveToHeaderFile_Click;
            // 
            // lblIPThirdOctetSeperator
            // 
            lblIPThirdOctetSeperator.Location = new Point(118, 53);
            lblIPThirdOctetSeperator.Name = "lblIPThirdOctetSeperator";
            lblIPThirdOctetSeperator.Size = new Size(8, 15);
            lblIPThirdOctetSeperator.TabIndex = 10;
            lblIPThirdOctetSeperator.Text = ".";
            // 
            // lblIPSecondOctetSeperator
            // 
            lblIPSecondOctetSeperator.Location = new Point(84, 53);
            lblIPSecondOctetSeperator.Name = "lblIPSecondOctetSeperator";
            lblIPSecondOctetSeperator.Size = new Size(8, 15);
            lblIPSecondOctetSeperator.TabIndex = 9;
            lblIPSecondOctetSeperator.Text = ".";
            // 
            // lblIPFirstOctetSeperator
            // 
            lblIPFirstOctetSeperator.Location = new Point(50, 53);
            lblIPFirstOctetSeperator.Name = "lblIPFirstOctetSeperator";
            lblIPFirstOctetSeperator.Size = new Size(8, 15);
            lblIPFirstOctetSeperator.TabIndex = 8;
            lblIPFirstOctetSeperator.Text = ".";
            // 
            // txtIPFourthOctet
            // 
            txtIPFourthOctet.Font = new Font("Segoe UI", 8.25F);
            txtIPFourthOctet.Location = new Point(126, 46);
            txtIPFourthOctet.Name = "txtIPFourthOctet";
            txtIPFourthOctet.Size = new Size(26, 22);
            txtIPFourthOctet.TabIndex = 7;
            txtIPFourthOctet.Text = "1";
            txtIPFourthOctet.KeyPress += txtIPFourthOctet_KeyPress;
            txtIPFourthOctet.Leave += txtIPFourthOctet_Leave;
            // 
            // txtIPThirdOctet
            // 
            txtIPThirdOctet.Font = new Font("Segoe UI", 8.25F);
            txtIPThirdOctet.Location = new Point(92, 46);
            txtIPThirdOctet.Name = "txtIPThirdOctet";
            txtIPThirdOctet.Size = new Size(26, 22);
            txtIPThirdOctet.TabIndex = 6;
            txtIPThirdOctet.Text = "1";
            txtIPThirdOctet.KeyPress += txtIPThirdOctet_KeyPress;
            txtIPThirdOctet.Leave += txtIPThirdOctet_Leave;
            // 
            // txtIPSecondOctet
            // 
            txtIPSecondOctet.Font = new Font("Segoe UI", 8.25F);
            txtIPSecondOctet.Location = new Point(58, 46);
            txtIPSecondOctet.Name = "txtIPSecondOctet";
            txtIPSecondOctet.Size = new Size(26, 22);
            txtIPSecondOctet.TabIndex = 5;
            txtIPSecondOctet.Text = "168";
            txtIPSecondOctet.KeyPress += txtIPSecondOctet_KeyPress;
            txtIPSecondOctet.Leave += txtIPSecondOctet_Leave;
            // 
            // txtIPFirstOctet
            // 
            txtIPFirstOctet.Font = new Font("Segoe UI", 8.25F);
            txtIPFirstOctet.Location = new Point(24, 46);
            txtIPFirstOctet.Name = "txtIPFirstOctet";
            txtIPFirstOctet.Size = new Size(26, 22);
            txtIPFirstOctet.TabIndex = 4;
            txtIPFirstOctet.Text = "192";
            txtIPFirstOctet.KeyPress += txtIPFirstOctet_KeyPress;
            txtIPFirstOctet.Leave += txtIPFirstOctet_Leave;
            // 
            // lblIPAddress
            // 
            lblIPAddress.Font = new Font("Segoe UI", 8.25F);
            lblIPAddress.Location = new Point(2, 49);
            lblIPAddress.Margin = new Padding(0);
            lblIPAddress.Name = "lblIPAddress";
            lblIPAddress.Size = new Size(20, 13);
            lblIPAddress.TabIndex = 3;
            lblIPAddress.Text = "IP:";
            lblIPAddress.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnTransmitFile
            // 
            btnTransmitFile.Font = new Font("Segoe UI", 8.25F);
            btnTransmitFile.Image = (System.Drawing.Image)resources.GetObject("btnTransmitFile.Image");
            btnTransmitFile.Location = new Point(70, 20);
            btnTransmitFile.Name = "btnTransmitFile";
            btnTransmitFile.Size = new Size(32, 22);
            btnTransmitFile.TabIndex = 2;
            btnTransmitFile.UseVisualStyleBackColor = true;
            btnTransmitFile.Click += btnTransmitFile_Click;
            // 
            // btnSaveFile
            // 
            btnSaveFile.Font = new Font("Segoe UI", 8.25F);
            btnSaveFile.Image = (System.Drawing.Image)resources.GetObject("btnSaveFile.Image");
            btnSaveFile.Location = new Point(38, 20);
            btnSaveFile.Name = "btnSaveFile";
            btnSaveFile.Size = new Size(32, 22);
            btnSaveFile.TabIndex = 1;
            btnSaveFile.UseVisualStyleBackColor = true;
            btnSaveFile.Click += btnSaveFile_Click;
            // 
            // btnLoadFile
            // 
            btnLoadFile.Font = new Font("Segoe UI", 8.25F);
            btnLoadFile.Image = (System.Drawing.Image)resources.GetObject("btnLoadFile.Image");
            btnLoadFile.Location = new Point(4, 20);
            btnLoadFile.Name = "btnLoadFile";
            btnLoadFile.Size = new Size(32, 22);
            btnLoadFile.TabIndex = 0;
            btnLoadFile.UseVisualStyleBackColor = true;
            btnLoadFile.Click += btnLoadFile_Click;
            // 
            // primitiveHighlightTimer
            // 
            primitiveHighlightTimer.Interval = 200;
            primitiveHighlightTimer.Tick += primitiveHighlightTimer_Tick;
            // 
            // chkHighlightCurrentPrimitive
            // 
            chkHighlightCurrentPrimitive.AutoSize = true;
            chkHighlightCurrentPrimitive.Location = new Point(585, 383);
            chkHighlightCurrentPrimitive.Name = "chkHighlightCurrentPrimitive";
            chkHighlightCurrentPrimitive.Size = new Size(119, 19);
            chkHighlightCurrentPrimitive.TabIndex = 5;
            chkHighlightCurrentPrimitive.Text = "Highlight Current";
            chkHighlightCurrentPrimitive.UseVisualStyleBackColor = true;
            chkHighlightCurrentPrimitive.CheckedChanged += chkHighlightCurrentPrimitive_CheckedChanged;
            // 
            // Editor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(786, 414);
            Controls.Add(chkHighlightCurrentPrimitive);
            Controls.Add(grpbxFile);
            Controls.Add(grpbxPrimitives);
            Controls.Add(pctbxCanvas);
            Controls.Add(grpbxFrames);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Editor";
            Text = "Vector Cube Animation Editor";
            Load += Editor_Load;
            grpbxFrames.ResumeLayout(false);
            grpbxFrames.PerformLayout();
            grpbxCurrentFrame.ResumeLayout(false);
            grpbxCurrentFrame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pctbxCanvas).EndInit();
            grpbxPrimitives.ResumeLayout(false);
            grpbxPrimitives.PerformLayout();
            grpbxCurrentPrimitive.ResumeLayout(false);
            grpbxCurrentPrimitive.PerformLayout();
            grpbxRotatedRect.ResumeLayout(false);
            grpbxRotatedRect.PerformLayout();
            grpbxRoundRect.ResumeLayout(false);
            grpbxRoundRect.PerformLayout();
            grpbxCircle.ResumeLayout(false);
            grpbxCircle.PerformLayout();
            grpbxTriangle.ResumeLayout(false);
            grpbxTriangle.PerformLayout();
            grpbxLine.ResumeLayout(false);
            grpbxLine.PerformLayout();
            grpbxFile.ResumeLayout(false);
            grpbxFile.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox grpbxFrames;
        private TextBox txtFrameCount;
        private Label lblFrameCount;
        private Button btnAddFrame;
        private Button btnNextFrame;
        private Button btnPreviousFrame;
        private PictureBox pctbxCanvas;
        private GroupBox grpbxPrimitives;
        private TextBox txtPrimitiveCount;
        private Label lblPrimitiveCount;
        private Button btnRemoveCurrentFrame;
        private ColorDialog selectColor;
        private GroupBox grpbxFile;
        private Button btnTransmitFile;
        private Button btnSaveFile;
        private Button btnLoadFile;
        private TextBox txtIPFirstOctet;
        private Label lblIPAddress;
        private Button btnAddRoundRect;
        private Button btnAddTriangle;
        private Button btnAddCircle;
        private OpenFileDialog openFile;
        private GroupBox grpbxRoundRect;
        private Label lblRoundRectH;
        private Label lblRoundRectW;
        private Label lblRoundRectY0;
        private Label lblRoundRectX0;
        private TextBox txtRoundRectY0;
        private TextBox txtRoundRectX0;
        private Label lblRoundRectRadius;
        private TextBox txtRoundRectH;
        private TextBox txtRoundRectW;
        private TextBox txtRoundRectRadius;
        private GroupBox grpbxCurrentFrame;
        private Button btnUpdateCurrentFrame;
        private TextBox txtCurrentFrameDuration;
        private Label lblCurrentFrameDuration;
        private Button btnCurrentFrameFillColor;
        private TextBox txtCurrentFrameFillColor;
        private Label lblCurrentFrameFillColor;
        private TextBox txtCurrentFrameNumber;
        private Label lblCurrentFrameNumber;
        private GroupBox grpbxCurrentPrimitive;
        private Button btnCurrentPrimitiveDrawColor;
        private TextBox txtCurrentPrimitiveDrawColor;
        private Label lblCurrentPrimitiveDrawColor;
        private TextBox txtCurrentPrimitiveNumber;
        private Label lblCurrentPrimitiveNumber;
        private TextBox txtFrameDuration;
        private Label lblFrameDuration;
        private Button btnFrameFillColor;
        private TextBox txtFrameFillColor;
        private Label lblAddFillColor;
        private Button btnUpdateCurrentPrimitive;
        private Button btnPrimitiveDrawColor;
        private TextBox txtPrimitiveDrawColor;
        private Label lblAddPrimitiveDrawColor;
        private GroupBox grpbxCircle;
        private TextBox txtCircleDelta;
        private TextBox txtCircleQuadrants;
        private TextBox txtCircleY0;
        private TextBox txtCircleX0;
        private TextBox txtCircleR;
        private Label lblCircleY0;
        private Label lblCircleX0;
        private Label lblCircleDelta;
        private Label lblCircleR;
        private Label lblCircleQuadrants;
        private GroupBox grpbxTriangle;
        private TextBox txtTriangleY1;
        private TextBox txtTriangleY0;
        private TextBox txtTriangleX1;
        private TextBox txtTriangleX0;
        private Label lblTriangleY1;
        private TextBox txtTriangleY2;
        private TextBox txtTriangleX2;
        private Label lblTriangleY2;
        private Label lblTriangleX2;
        private Label lblTriangleY0;
        private Label lblTriangleX1;
        private Label lblTriangleX0;
        private SaveFileDialog saveFile;
        private TextBox txtIPFourthOctet;
        private TextBox txtIPThirdOctet;
        private TextBox txtIPSecondOctet;
        private Label lblIPSecondOctetSeperator;
        private Label lblIPFirstOctetSeperator;
        private Label lblIPThirdOctetSeperator;
        private Button btnRemoveCurrentPrimitive;
        private Button btnNextPrimitive;
        private Button btnPreviousPrimitive;
        private Button btnAddLine;
        private GroupBox grpbxLine;
        private TextBox txtLineY1;
        private TextBox txtLineY0;
        private TextBox txtLineX1;
        private TextBox txtLineX0;
        private Label lblLineY1;
        private Label lblLineY0;
        private Label lblLineX1;
        private Label lblLineX0;
        private Button btnSaveToHeaderFile;
        private Button btnDuplicateCurrentFrame;
        private Button btnSendImage;
        private Button btnMoveFrameUp;
        private Button btnMoveFrameDown;
        private System.Windows.Forms.Timer primitiveHighlightTimer;
        private CheckBox chkHighlightCurrentPrimitive;
        private CheckBox chkCircleTopLeft;
        private CheckBox chkCircleTopRight;
        private CheckBox chkCircleBottomLeft;
        private CheckBox chkCircleBottomRight;
        private Button btnAddRotatedRect;
        private GroupBox grpbxRotatedRect;
        private TextBox txtRotatedRectAngleDeg;
        private TextBox txtRotatedRectH;
        private TextBox txtRotatedRectW;
        private TextBox txtRotatedRectCenY;
        private TextBox txtRotatedRectCenX;
        private Label lblRotatedRectAngleDeg;
        private Label lblRotatedRectH;
        private Label lblRotatedRectW;
        private Label lblRotatedRectCenY;
        private Label lblRotatedRectCenX;
    }
}
