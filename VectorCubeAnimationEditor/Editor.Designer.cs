﻿namespace VectorCubeAnimationEditor
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
            grpbxFrames = new GroupBox();
            txtAddFrameDuration = new TextBox();
            lblAddFrameDuration = new Label();
            btnAddFrameFillColor = new Button();
            txtAddFrameFillColor = new TextBox();
            lblAddFillColor = new Label();
            grpbxCurrentFrame = new GroupBox();
            btnUpdateCurrentFrame = new Button();
            txtFrameDuration = new TextBox();
            lblFrameDuration = new Label();
            btnFrameFillColor = new Button();
            txtFrameFillColor = new TextBox();
            lblFillColor = new Label();
            txtCurrentFrame = new TextBox();
            lblCurrentFrameNumber = new Label();
            btnRemoveCurrentFrame = new Button();
            btnNextFrame = new Button();
            btnPreviousFrame = new Button();
            btnAddFrame = new Button();
            txtFrameCount = new TextBox();
            lblFrameCount = new Label();
            pctbxCanvas = new PictureBox();
            grpbxPrimitives = new GroupBox();
            btnAddPrimitiveDrawColor = new Button();
            txtAddPrimitiveDrawColor = new TextBox();
            lblAddPrimitiveDrawColor = new Label();
            grpbxCurrentPrimitive = new GroupBox();
            grpbxCircle = new GroupBox();
            txtCircleRadius = new TextBox();
            txtCircleY0 = new TextBox();
            txtCircleX0 = new TextBox();
            lblCircleRadius = new Label();
            lblCircleY0 = new Label();
            lblCircleX0 = new Label();
            grpbxQuarterCircle = new GroupBox();
            txtQuarterCircleRadius = new TextBox();
            txtQuarterCircleDelta = new TextBox();
            txtQuarterCircleQuadrants = new TextBox();
            txtQuarterCircleY0 = new TextBox();
            txtQuarterCircleX0 = new TextBox();
            lblQuarterCircleRadius = new Label();
            lblQuarterCircleDelta = new Label();
            lblQuarterCircleQuadrants = new Label();
            lblQuarterCircleY0 = new Label();
            lblQuarterCircleX0 = new Label();
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
            btnUpdateCurrentPrimitive = new Button();
            btnPrimitiveDrawColor = new Button();
            txtPrimitiveDrawColor = new TextBox();
            lblPrimitiveDrawColor = new Label();
            txtCurrentIdentifiedPrimitive = new TextBox();
            lblCurrentPrimitiveNumber = new Label();
            btnAddRoundRectangle = new Button();
            btnAddTriangle = new Button();
            btnAddQuarterCircle = new Button();
            btnAddCircle = new Button();
            btnRemoveCurrentPrimitive = new Button();
            btnNextPrimitive = new Button();
            btnPreviousPrimitive = new Button();
            txtIdentifiedPrimitiveCount = new TextBox();
            lblPrimitiveCount = new Label();
            selectColor = new ColorDialog();
            grpbxFile = new GroupBox();
            txtIPAddress = new TextBox();
            lblIPAddress = new Label();
            btnTransmitFile = new Button();
            btnSaveFile = new Button();
            btnLoadFile = new Button();
            openFile = new OpenFileDialog();
            saveFile = new SaveFileDialog();
            grpbxFrames.SuspendLayout();
            grpbxCurrentFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctbxCanvas).BeginInit();
            grpbxPrimitives.SuspendLayout();
            grpbxCurrentPrimitive.SuspendLayout();
            grpbxCircle.SuspendLayout();
            grpbxQuarterCircle.SuspendLayout();
            grpbxRoundRect.SuspendLayout();
            grpbxTriangle.SuspendLayout();
            grpbxFile.SuspendLayout();
            SuspendLayout();
            // 
            // grpbxFrames
            // 
            grpbxFrames.Controls.Add(txtAddFrameDuration);
            grpbxFrames.Controls.Add(lblAddFrameDuration);
            grpbxFrames.Controls.Add(btnAddFrameFillColor);
            grpbxFrames.Controls.Add(txtAddFrameFillColor);
            grpbxFrames.Controls.Add(lblAddFillColor);
            grpbxFrames.Controls.Add(grpbxCurrentFrame);
            grpbxFrames.Controls.Add(btnRemoveCurrentFrame);
            grpbxFrames.Controls.Add(btnNextFrame);
            grpbxFrames.Controls.Add(btnPreviousFrame);
            grpbxFrames.Controls.Add(btnAddFrame);
            grpbxFrames.Controls.Add(txtFrameCount);
            grpbxFrames.Controls.Add(lblFrameCount);
            grpbxFrames.Font = new Font("Segoe UI", 8.25F);
            grpbxFrames.Location = new Point(10, 140);
            grpbxFrames.Name = "grpbxFrames";
            grpbxFrames.Size = new Size(170, 290);
            grpbxFrames.TabIndex = 1;
            grpbxFrames.TabStop = false;
            grpbxFrames.Text = "Frames";
            // 
            // txtAddFrameDuration
            // 
            txtAddFrameDuration.Font = new Font("Segoe UI", 8.25F);
            txtAddFrameDuration.Location = new Point(70, 88);
            txtAddFrameDuration.Name = "txtAddFrameDuration";
            txtAddFrameDuration.Size = new Size(85, 22);
            txtAddFrameDuration.TabIndex = 25;
            // 
            // lblAddFrameDuration
            // 
            lblAddFrameDuration.AutoSize = true;
            lblAddFrameDuration.Font = new Font("Segoe UI", 8.25F);
            lblAddFrameDuration.Location = new Point(8, 91);
            lblAddFrameDuration.Name = "lblAddFrameDuration";
            lblAddFrameDuration.Size = new Size(56, 13);
            lblAddFrameDuration.TabIndex = 24;
            lblAddFrameDuration.Text = "Duration:";
            lblAddFrameDuration.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnAddFrameFillColor
            // 
            btnAddFrameFillColor.Font = new Font("Segoe UI", 8.25F);
            btnAddFrameFillColor.Location = new Point(120, 64);
            btnAddFrameFillColor.Name = "btnAddFrameFillColor";
            btnAddFrameFillColor.Size = new Size(35, 22);
            btnAddFrameFillColor.TabIndex = 23;
            btnAddFrameFillColor.Text = "...";
            btnAddFrameFillColor.TextAlign = ContentAlignment.TopCenter;
            btnAddFrameFillColor.UseVisualStyleBackColor = true;
            btnAddFrameFillColor.Click += btnAddFrameFillColor_Click;
            // 
            // txtAddFrameFillColor
            // 
            txtAddFrameFillColor.Font = new Font("Segoe UI", 8.25F);
            txtAddFrameFillColor.Location = new Point(70, 64);
            txtAddFrameFillColor.Name = "txtAddFrameFillColor";
            txtAddFrameFillColor.Size = new Size(48, 22);
            txtAddFrameFillColor.TabIndex = 22;
            // 
            // lblAddFillColor
            // 
            lblAddFillColor.AutoSize = true;
            lblAddFillColor.Font = new Font("Segoe UI", 8.25F);
            lblAddFillColor.Location = new Point(9, 67);
            lblAddFillColor.Name = "lblAddFillColor";
            lblAddFillColor.Size = new Size(56, 13);
            lblAddFillColor.TabIndex = 21;
            lblAddFillColor.Text = "Fill Color:";
            // 
            // grpbxCurrentFrame
            // 
            grpbxCurrentFrame.Controls.Add(btnUpdateCurrentFrame);
            grpbxCurrentFrame.Controls.Add(txtFrameDuration);
            grpbxCurrentFrame.Controls.Add(lblFrameDuration);
            grpbxCurrentFrame.Controls.Add(btnFrameFillColor);
            grpbxCurrentFrame.Controls.Add(txtFrameFillColor);
            grpbxCurrentFrame.Controls.Add(lblFillColor);
            grpbxCurrentFrame.Controls.Add(txtCurrentFrame);
            grpbxCurrentFrame.Controls.Add(lblCurrentFrameNumber);
            grpbxCurrentFrame.Font = new Font("Segoe UI", 8.25F);
            grpbxCurrentFrame.Location = new Point(5, 160);
            grpbxCurrentFrame.Name = "grpbxCurrentFrame";
            grpbxCurrentFrame.Size = new Size(160, 120);
            grpbxCurrentFrame.TabIndex = 14;
            grpbxCurrentFrame.TabStop = false;
            grpbxCurrentFrame.Text = "Current Frame";
            // 
            // btnUpdateCurrentFrame
            // 
            btnUpdateCurrentFrame.Font = new Font("Segoe UI", 8.25F);
            btnUpdateCurrentFrame.Location = new Point(10, 92);
            btnUpdateCurrentFrame.Name = "btnUpdateCurrentFrame";
            btnUpdateCurrentFrame.Size = new Size(140, 22);
            btnUpdateCurrentFrame.TabIndex = 21;
            btnUpdateCurrentFrame.Text = "Update";
            btnUpdateCurrentFrame.UseVisualStyleBackColor = true;
            btnUpdateCurrentFrame.Click += btnUpdateCurrentFrame_Click;
            // 
            // txtFrameDuration
            // 
            txtFrameDuration.Font = new Font("Segoe UI", 8.25F);
            txtFrameDuration.Location = new Point(65, 66);
            txtFrameDuration.Name = "txtFrameDuration";
            txtFrameDuration.Size = new Size(85, 22);
            txtFrameDuration.TabIndex = 20;
            // 
            // lblFrameDuration
            // 
            lblFrameDuration.AutoSize = true;
            lblFrameDuration.Font = new Font("Segoe UI", 8.25F);
            lblFrameDuration.Location = new Point(3, 69);
            lblFrameDuration.Name = "lblFrameDuration";
            lblFrameDuration.Size = new Size(56, 13);
            lblFrameDuration.TabIndex = 19;
            lblFrameDuration.Text = "Duration:";
            lblFrameDuration.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnFrameFillColor
            // 
            btnFrameFillColor.Font = new Font("Segoe UI", 8.25F);
            btnFrameFillColor.Location = new Point(115, 40);
            btnFrameFillColor.Name = "btnFrameFillColor";
            btnFrameFillColor.Size = new Size(35, 22);
            btnFrameFillColor.TabIndex = 18;
            btnFrameFillColor.Text = "...";
            btnFrameFillColor.TextAlign = ContentAlignment.TopCenter;
            btnFrameFillColor.UseVisualStyleBackColor = true;
            btnFrameFillColor.Click += btnFrameFillColor_Click;
            // 
            // txtFrameFillColor
            // 
            txtFrameFillColor.Font = new Font("Segoe UI", 8.25F);
            txtFrameFillColor.Location = new Point(65, 40);
            txtFrameFillColor.Name = "txtFrameFillColor";
            txtFrameFillColor.Size = new Size(48, 22);
            txtFrameFillColor.TabIndex = 17;
            // 
            // lblFillColor
            // 
            lblFillColor.AutoSize = true;
            lblFillColor.Font = new Font("Segoe UI", 8.25F);
            lblFillColor.Location = new Point(3, 40);
            lblFillColor.Name = "lblFillColor";
            lblFillColor.Size = new Size(56, 13);
            lblFillColor.TabIndex = 16;
            lblFillColor.Text = "Fill Color:";
            // 
            // txtCurrentFrame
            // 
            txtCurrentFrame.Font = new Font("Segoe UI", 8.25F);
            txtCurrentFrame.Location = new Point(115, 15);
            txtCurrentFrame.Name = "txtCurrentFrame";
            txtCurrentFrame.Size = new Size(35, 22);
            txtCurrentFrame.TabIndex = 15;
            txtCurrentFrame.KeyDown += txtCurrentFrame_KeyDown;
            // 
            // lblCurrentFrameNumber
            // 
            lblCurrentFrameNumber.AutoSize = true;
            lblCurrentFrameNumber.Font = new Font("Segoe UI", 8.25F);
            lblCurrentFrameNumber.Location = new Point(58, 18);
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
            btnRemoveCurrentFrame.Location = new Point(15, 113);
            btnRemoveCurrentFrame.Name = "btnRemoveCurrentFrame";
            btnRemoveCurrentFrame.Size = new Size(140, 22);
            btnRemoveCurrentFrame.TabIndex = 7;
            btnRemoveCurrentFrame.Text = "Remove Current Frame";
            btnRemoveCurrentFrame.UseVisualStyleBackColor = true;
            btnRemoveCurrentFrame.Click += btnRemoveCurrentFrame_Click;
            // 
            // btnNextFrame
            // 
            btnNextFrame.Enabled = false;
            btnNextFrame.Font = new Font("Segoe UI", 8.25F);
            btnNextFrame.Location = new Point(90, 135);
            btnNextFrame.Name = "btnNextFrame";
            btnNextFrame.Size = new Size(65, 22);
            btnNextFrame.TabIndex = 6;
            btnNextFrame.Text = ">>";
            btnNextFrame.UseVisualStyleBackColor = true;
            btnNextFrame.Click += btnNextFrame_Click;
            // 
            // btnPreviousFrame
            // 
            btnPreviousFrame.Enabled = false;
            btnPreviousFrame.Font = new Font("Segoe UI", 8.25F);
            btnPreviousFrame.Location = new Point(15, 135);
            btnPreviousFrame.Name = "btnPreviousFrame";
            btnPreviousFrame.Size = new Size(65, 22);
            btnPreviousFrame.TabIndex = 5;
            btnPreviousFrame.Text = "<<";
            btnPreviousFrame.UseVisualStyleBackColor = true;
            btnPreviousFrame.Click += btnPreviousFrame_Click;
            // 
            // btnAddFrame
            // 
            btnAddFrame.Font = new Font("Segoe UI", 8.25F);
            btnAddFrame.Location = new Point(15, 40);
            btnAddFrame.Name = "btnAddFrame";
            btnAddFrame.Size = new Size(140, 22);
            btnAddFrame.TabIndex = 2;
            btnAddFrame.Text = "Add Frame";
            btnAddFrame.UseVisualStyleBackColor = true;
            btnAddFrame.Click += btnAddFrame_Click;
            // 
            // txtFrameCount
            // 
            txtFrameCount.Font = new Font("Segoe UI", 8.25F);
            txtFrameCount.Location = new Point(120, 15);
            txtFrameCount.Name = "txtFrameCount";
            txtFrameCount.Size = new Size(35, 22);
            txtFrameCount.TabIndex = 1;
            txtFrameCount.KeyDown += txtFrameCount_KeyDown;
            // 
            // lblFrameCount
            // 
            lblFrameCount.AutoSize = true;
            lblFrameCount.Font = new Font("Segoe UI", 8.25F);
            lblFrameCount.Location = new Point(37, 18);
            lblFrameCount.Name = "lblFrameCount";
            lblFrameCount.Size = new Size(76, 13);
            lblFrameCount.TabIndex = 0;
            lblFrameCount.Text = "Frame Count:";
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
            grpbxPrimitives.Controls.Add(btnAddPrimitiveDrawColor);
            grpbxPrimitives.Controls.Add(txtAddPrimitiveDrawColor);
            grpbxPrimitives.Controls.Add(lblAddPrimitiveDrawColor);
            grpbxPrimitives.Controls.Add(grpbxCurrentPrimitive);
            grpbxPrimitives.Controls.Add(btnAddRoundRectangle);
            grpbxPrimitives.Controls.Add(btnAddTriangle);
            grpbxPrimitives.Controls.Add(btnAddQuarterCircle);
            grpbxPrimitives.Controls.Add(btnAddCircle);
            grpbxPrimitives.Controls.Add(btnRemoveCurrentPrimitive);
            grpbxPrimitives.Controls.Add(btnNextPrimitive);
            grpbxPrimitives.Controls.Add(btnPreviousPrimitive);
            grpbxPrimitives.Controls.Add(txtIdentifiedPrimitiveCount);
            grpbxPrimitives.Controls.Add(lblPrimitiveCount);
            grpbxPrimitives.Font = new Font("Segoe UI", 8.25F);
            grpbxPrimitives.Location = new Point(585, 10);
            grpbxPrimitives.Name = "grpbxPrimitives";
            grpbxPrimitives.Size = new Size(170, 420);
            grpbxPrimitives.TabIndex = 3;
            grpbxPrimitives.TabStop = false;
            grpbxPrimitives.Text = "Primitives";
            // 
            // btnAddPrimitiveDrawColor
            // 
            btnAddPrimitiveDrawColor.Font = new Font("Segoe UI", 8.25F);
            btnAddPrimitiveDrawColor.Location = new Point(120, 140);
            btnAddPrimitiveDrawColor.Name = "btnAddPrimitiveDrawColor";
            btnAddPrimitiveDrawColor.Size = new Size(35, 22);
            btnAddPrimitiveDrawColor.TabIndex = 26;
            btnAddPrimitiveDrawColor.Text = "...";
            btnAddPrimitiveDrawColor.TextAlign = ContentAlignment.TopCenter;
            btnAddPrimitiveDrawColor.UseVisualStyleBackColor = true;
            btnAddPrimitiveDrawColor.Click += btnAddPrimitiveDrawColor_Click;
            // 
            // txtAddPrimitiveDrawColor
            // 
            txtAddPrimitiveDrawColor.Font = new Font("Segoe UI", 8.25F);
            txtAddPrimitiveDrawColor.Location = new Point(70, 140);
            txtAddPrimitiveDrawColor.Name = "txtAddPrimitiveDrawColor";
            txtAddPrimitiveDrawColor.Size = new Size(48, 22);
            txtAddPrimitiveDrawColor.TabIndex = 25;
            // 
            // lblAddPrimitiveDrawColor
            // 
            lblAddPrimitiveDrawColor.AutoSize = true;
            lblAddPrimitiveDrawColor.Font = new Font("Segoe UI", 8.25F);
            lblAddPrimitiveDrawColor.Location = new Point(26, 143);
            lblAddPrimitiveDrawColor.Name = "lblAddPrimitiveDrawColor";
            lblAddPrimitiveDrawColor.Size = new Size(38, 13);
            lblAddPrimitiveDrawColor.TabIndex = 24;
            lblAddPrimitiveDrawColor.Text = "Color:";
            // 
            // grpbxCurrentPrimitive
            // 
            grpbxCurrentPrimitive.Controls.Add(grpbxCircle);
            grpbxCurrentPrimitive.Controls.Add(grpbxQuarterCircle);
            grpbxCurrentPrimitive.Controls.Add(grpbxRoundRect);
            grpbxCurrentPrimitive.Controls.Add(grpbxTriangle);
            grpbxCurrentPrimitive.Controls.Add(btnUpdateCurrentPrimitive);
            grpbxCurrentPrimitive.Controls.Add(btnPrimitiveDrawColor);
            grpbxCurrentPrimitive.Controls.Add(txtPrimitiveDrawColor);
            grpbxCurrentPrimitive.Controls.Add(lblPrimitiveDrawColor);
            grpbxCurrentPrimitive.Controls.Add(txtCurrentIdentifiedPrimitive);
            grpbxCurrentPrimitive.Controls.Add(lblCurrentPrimitiveNumber);
            grpbxCurrentPrimitive.Font = new Font("Segoe UI", 8.25F);
            grpbxCurrentPrimitive.Location = new Point(5, 215);
            grpbxCurrentPrimitive.Name = "grpbxCurrentPrimitive";
            grpbxCurrentPrimitive.Size = new Size(160, 195);
            grpbxCurrentPrimitive.TabIndex = 23;
            grpbxCurrentPrimitive.TabStop = false;
            grpbxCurrentPrimitive.Text = "Current Primitive";
            // 
            // grpbxCircle
            // 
            grpbxCircle.Controls.Add(txtCircleRadius);
            grpbxCircle.Controls.Add(txtCircleY0);
            grpbxCircle.Controls.Add(txtCircleX0);
            grpbxCircle.Controls.Add(lblCircleRadius);
            grpbxCircle.Controls.Add(lblCircleY0);
            grpbxCircle.Controls.Add(lblCircleX0);
            grpbxCircle.Location = new Point(10, 67);
            grpbxCircle.Name = "grpbxCircle";
            grpbxCircle.Size = new Size(140, 96);
            grpbxCircle.TabIndex = 30;
            grpbxCircle.TabStop = false;
            grpbxCircle.Text = "Circle";
            grpbxCircle.Visible = false;
            // 
            // txtCircleRadius
            // 
            txtCircleRadius.Location = new Point(102, 40);
            txtCircleRadius.Name = "txtCircleRadius";
            txtCircleRadius.Size = new Size(30, 22);
            txtCircleRadius.TabIndex = 17;
            // 
            // txtCircleY0
            // 
            txtCircleY0.Location = new Point(102, 16);
            txtCircleY0.Name = "txtCircleY0";
            txtCircleY0.Size = new Size(30, 22);
            txtCircleY0.TabIndex = 16;
            // 
            // txtCircleX0
            // 
            txtCircleX0.Location = new Point(35, 16);
            txtCircleX0.Name = "txtCircleX0";
            txtCircleX0.Size = new Size(30, 22);
            txtCircleX0.TabIndex = 14;
            // 
            // lblCircleRadius
            // 
            lblCircleRadius.AutoSize = true;
            lblCircleRadius.Location = new Point(54, 45);
            lblCircleRadius.Name = "lblCircleRadius";
            lblCircleRadius.Size = new Size(45, 13);
            lblCircleRadius.TabIndex = 13;
            lblCircleRadius.Text = "Radius:";
            // 
            // lblCircleY0
            // 
            lblCircleY0.AutoSize = true;
            lblCircleY0.Location = new Point(77, 19);
            lblCircleY0.Name = "lblCircleY0";
            lblCircleY0.Size = new Size(21, 13);
            lblCircleY0.TabIndex = 2;
            lblCircleY0.Text = "Y0:";
            // 
            // lblCircleX0
            // 
            lblCircleX0.AutoSize = true;
            lblCircleX0.Location = new Point(6, 19);
            lblCircleX0.Name = "lblCircleX0";
            lblCircleX0.Size = new Size(22, 13);
            lblCircleX0.TabIndex = 0;
            lblCircleX0.Text = "X0:";
            // 
            // grpbxQuarterCircle
            // 
            grpbxQuarterCircle.Controls.Add(txtQuarterCircleRadius);
            grpbxQuarterCircle.Controls.Add(txtQuarterCircleDelta);
            grpbxQuarterCircle.Controls.Add(txtQuarterCircleQuadrants);
            grpbxQuarterCircle.Controls.Add(txtQuarterCircleY0);
            grpbxQuarterCircle.Controls.Add(txtQuarterCircleX0);
            grpbxQuarterCircle.Controls.Add(lblQuarterCircleRadius);
            grpbxQuarterCircle.Controls.Add(lblQuarterCircleDelta);
            grpbxQuarterCircle.Controls.Add(lblQuarterCircleQuadrants);
            grpbxQuarterCircle.Controls.Add(lblQuarterCircleY0);
            grpbxQuarterCircle.Controls.Add(lblQuarterCircleX0);
            grpbxQuarterCircle.Location = new Point(10, 67);
            grpbxQuarterCircle.Name = "grpbxQuarterCircle";
            grpbxQuarterCircle.Size = new Size(140, 96);
            grpbxQuarterCircle.TabIndex = 31;
            grpbxQuarterCircle.TabStop = false;
            grpbxQuarterCircle.Text = "Quarter Circle";
            grpbxQuarterCircle.Visible = false;
            // 
            // txtQuarterCircleRadius
            // 
            txtQuarterCircleRadius.Location = new Point(102, 64);
            txtQuarterCircleRadius.Name = "txtQuarterCircleRadius";
            txtQuarterCircleRadius.Size = new Size(30, 22);
            txtQuarterCircleRadius.TabIndex = 9;
            // 
            // txtQuarterCircleDelta
            // 
            txtQuarterCircleDelta.Location = new Point(102, 40);
            txtQuarterCircleDelta.Name = "txtQuarterCircleDelta";
            txtQuarterCircleDelta.Size = new Size(30, 22);
            txtQuarterCircleDelta.TabIndex = 8;
            // 
            // txtQuarterCircleQuadrants
            // 
            txtQuarterCircleQuadrants.Location = new Point(35, 40);
            txtQuarterCircleQuadrants.Name = "txtQuarterCircleQuadrants";
            txtQuarterCircleQuadrants.Size = new Size(30, 22);
            txtQuarterCircleQuadrants.TabIndex = 7;
            // 
            // txtQuarterCircleY0
            // 
            txtQuarterCircleY0.Location = new Point(102, 16);
            txtQuarterCircleY0.Name = "txtQuarterCircleY0";
            txtQuarterCircleY0.Size = new Size(30, 22);
            txtQuarterCircleY0.TabIndex = 6;
            // 
            // txtQuarterCircleX0
            // 
            txtQuarterCircleX0.Location = new Point(35, 16);
            txtQuarterCircleX0.Name = "txtQuarterCircleX0";
            txtQuarterCircleX0.Size = new Size(30, 22);
            txtQuarterCircleX0.TabIndex = 5;
            // 
            // lblQuarterCircleRadius
            // 
            lblQuarterCircleRadius.AutoSize = true;
            lblQuarterCircleRadius.Location = new Point(51, 67);
            lblQuarterCircleRadius.Name = "lblQuarterCircleRadius";
            lblQuarterCircleRadius.Size = new Size(45, 13);
            lblQuarterCircleRadius.TabIndex = 4;
            lblQuarterCircleRadius.Text = "Radius:";
            // 
            // lblQuarterCircleDelta
            // 
            lblQuarterCircleDelta.AutoSize = true;
            lblQuarterCircleDelta.Location = new Point(77, 43);
            lblQuarterCircleDelta.Name = "lblQuarterCircleDelta";
            lblQuarterCircleDelta.Size = new Size(18, 13);
            lblQuarterCircleDelta.TabIndex = 3;
            lblQuarterCircleDelta.Text = "D:";
            // 
            // lblQuarterCircleQuadrants
            // 
            lblQuarterCircleQuadrants.AutoSize = true;
            lblQuarterCircleQuadrants.Location = new Point(7, 43);
            lblQuarterCircleQuadrants.Name = "lblQuarterCircleQuadrants";
            lblQuarterCircleQuadrants.Size = new Size(18, 13);
            lblQuarterCircleQuadrants.TabIndex = 2;
            lblQuarterCircleQuadrants.Text = "Q:";
            // 
            // lblQuarterCircleY0
            // 
            lblQuarterCircleY0.AutoSize = true;
            lblQuarterCircleY0.Location = new Point(75, 19);
            lblQuarterCircleY0.Name = "lblQuarterCircleY0";
            lblQuarterCircleY0.Size = new Size(21, 13);
            lblQuarterCircleY0.TabIndex = 1;
            lblQuarterCircleY0.Text = "Y0:";
            // 
            // lblQuarterCircleX0
            // 
            lblQuarterCircleX0.AutoSize = true;
            lblQuarterCircleX0.Location = new Point(6, 19);
            lblQuarterCircleX0.Name = "lblQuarterCircleX0";
            lblQuarterCircleX0.Size = new Size(22, 13);
            lblQuarterCircleX0.TabIndex = 0;
            lblQuarterCircleX0.Text = "X0:";
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
            grpbxRoundRect.Location = new Point(10, 67);
            grpbxRoundRect.Name = "grpbxRoundRect";
            grpbxRoundRect.Size = new Size(140, 96);
            grpbxRoundRect.TabIndex = 26;
            grpbxRoundRect.TabStop = false;
            grpbxRoundRect.Text = "Round Rectangle";
            grpbxRoundRect.Visible = false;
            // 
            // txtRoundRectRadius
            // 
            txtRoundRectRadius.Location = new Point(102, 64);
            txtRoundRectRadius.Name = "txtRoundRectRadius";
            txtRoundRectRadius.Size = new Size(30, 22);
            txtRoundRectRadius.TabIndex = 9;
            // 
            // txtRoundRectH
            // 
            txtRoundRectH.Location = new Point(102, 40);
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
            txtRoundRectY0.Location = new Point(102, 16);
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
            lblRoundRectRadius.Location = new Point(51, 67);
            lblRoundRectRadius.Name = "lblRoundRectRadius";
            lblRoundRectRadius.Size = new Size(45, 13);
            lblRoundRectRadius.TabIndex = 4;
            lblRoundRectRadius.Text = "Radius:";
            // 
            // lblRoundRectH
            // 
            lblRoundRectH.AutoSize = true;
            lblRoundRectH.Location = new Point(77, 45);
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
            lblRoundRectY0.Location = new Point(77, 19);
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
            grpbxTriangle.Location = new Point(10, 67);
            grpbxTriangle.Name = "grpbxTriangle";
            grpbxTriangle.Size = new Size(140, 96);
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
            // btnUpdateCurrentPrimitive
            // 
            btnUpdateCurrentPrimitive.Enabled = false;
            btnUpdateCurrentPrimitive.Font = new Font("Segoe UI", 8.25F);
            btnUpdateCurrentPrimitive.Location = new Point(10, 167);
            btnUpdateCurrentPrimitive.Name = "btnUpdateCurrentPrimitive";
            btnUpdateCurrentPrimitive.Size = new Size(140, 22);
            btnUpdateCurrentPrimitive.TabIndex = 23;
            btnUpdateCurrentPrimitive.Text = "Update";
            btnUpdateCurrentPrimitive.UseVisualStyleBackColor = true;
            btnUpdateCurrentPrimitive.Click += btnUpdateCurrentPrimitive_Click;
            // 
            // btnPrimitiveDrawColor
            // 
            btnPrimitiveDrawColor.Font = new Font("Segoe UI", 8.25F);
            btnPrimitiveDrawColor.Location = new Point(115, 40);
            btnPrimitiveDrawColor.Name = "btnPrimitiveDrawColor";
            btnPrimitiveDrawColor.Size = new Size(35, 22);
            btnPrimitiveDrawColor.TabIndex = 22;
            btnPrimitiveDrawColor.Text = "...";
            btnPrimitiveDrawColor.TextAlign = ContentAlignment.TopCenter;
            btnPrimitiveDrawColor.UseVisualStyleBackColor = true;
            btnPrimitiveDrawColor.Click += btnPrimitiveDrawColor_Click;
            // 
            // txtPrimitiveDrawColor
            // 
            txtPrimitiveDrawColor.Font = new Font("Segoe UI", 8.25F);
            txtPrimitiveDrawColor.Location = new Point(65, 40);
            txtPrimitiveDrawColor.Name = "txtPrimitiveDrawColor";
            txtPrimitiveDrawColor.Size = new Size(48, 22);
            txtPrimitiveDrawColor.TabIndex = 21;
            // 
            // lblPrimitiveDrawColor
            // 
            lblPrimitiveDrawColor.AutoSize = true;
            lblPrimitiveDrawColor.Font = new Font("Segoe UI", 8.25F);
            lblPrimitiveDrawColor.Location = new Point(21, 44);
            lblPrimitiveDrawColor.Name = "lblPrimitiveDrawColor";
            lblPrimitiveDrawColor.Size = new Size(38, 13);
            lblPrimitiveDrawColor.TabIndex = 20;
            lblPrimitiveDrawColor.Text = "Color:";
            // 
            // txtCurrentIdentifiedPrimitive
            // 
            txtCurrentIdentifiedPrimitive.Font = new Font("Segoe UI", 8.25F);
            txtCurrentIdentifiedPrimitive.Location = new Point(115, 15);
            txtCurrentIdentifiedPrimitive.Name = "txtCurrentIdentifiedPrimitive";
            txtCurrentIdentifiedPrimitive.Size = new Size(36, 22);
            txtCurrentIdentifiedPrimitive.TabIndex = 19;
            txtCurrentIdentifiedPrimitive.KeyDown += txtCurrentPrimitive_KeyDown;
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
            // btnAddRoundRectangle
            // 
            btnAddRoundRectangle.Enabled = false;
            btnAddRoundRectangle.Font = new Font("Segoe UI", 8.25F);
            btnAddRoundRectangle.Location = new Point(15, 116);
            btnAddRoundRectangle.Name = "btnAddRoundRectangle";
            btnAddRoundRectangle.Size = new Size(140, 22);
            btnAddRoundRectangle.TabIndex = 21;
            btnAddRoundRectangle.Text = "Add Round Rectangle";
            btnAddRoundRectangle.UseVisualStyleBackColor = true;
            btnAddRoundRectangle.Click += btnAddRoundRectangle_Click;
            // 
            // btnAddTriangle
            // 
            btnAddTriangle.Enabled = false;
            btnAddTriangle.Font = new Font("Segoe UI", 8.25F);
            btnAddTriangle.Location = new Point(15, 92);
            btnAddTriangle.Name = "btnAddTriangle";
            btnAddTriangle.Size = new Size(140, 22);
            btnAddTriangle.TabIndex = 20;
            btnAddTriangle.Text = "Add Triangle";
            btnAddTriangle.UseVisualStyleBackColor = true;
            btnAddTriangle.Click += btnAddTriangle_Click;
            // 
            // btnAddQuarterCircle
            // 
            btnAddQuarterCircle.Enabled = false;
            btnAddQuarterCircle.Font = new Font("Segoe UI", 8.25F);
            btnAddQuarterCircle.Location = new Point(15, 68);
            btnAddQuarterCircle.Name = "btnAddQuarterCircle";
            btnAddQuarterCircle.Size = new Size(140, 22);
            btnAddQuarterCircle.TabIndex = 19;
            btnAddQuarterCircle.Text = "Add Quarter Circle";
            btnAddQuarterCircle.UseVisualStyleBackColor = true;
            btnAddQuarterCircle.Click += btnAddQuarterCircle_Click;
            // 
            // btnAddCircle
            // 
            btnAddCircle.Enabled = false;
            btnAddCircle.Font = new Font("Segoe UI", 8.25F);
            btnAddCircle.Location = new Point(15, 44);
            btnAddCircle.Name = "btnAddCircle";
            btnAddCircle.Size = new Size(140, 22);
            btnAddCircle.TabIndex = 18;
            btnAddCircle.Text = "Add Circle";
            btnAddCircle.UseVisualStyleBackColor = true;
            btnAddCircle.Click += btnAddCircle_Click;
            // 
            // btnRemoveCurrentPrimitive
            // 
            btnRemoveCurrentPrimitive.Enabled = false;
            btnRemoveCurrentPrimitive.Font = new Font("Segoe UI", 8.25F);
            btnRemoveCurrentPrimitive.Location = new Point(15, 164);
            btnRemoveCurrentPrimitive.Name = "btnRemoveCurrentPrimitive";
            btnRemoveCurrentPrimitive.Size = new Size(140, 22);
            btnRemoveCurrentPrimitive.TabIndex = 14;
            btnRemoveCurrentPrimitive.Text = "Remove Current Primitive";
            btnRemoveCurrentPrimitive.UseVisualStyleBackColor = true;
            btnRemoveCurrentPrimitive.Click += btnRemoveCurrentPrimitive_Click;
            // 
            // btnNextPrimitive
            // 
            btnNextPrimitive.Enabled = false;
            btnNextPrimitive.Font = new Font("Segoe UI", 8.25F);
            btnNextPrimitive.Location = new Point(90, 188);
            btnNextPrimitive.Name = "btnNextPrimitive";
            btnNextPrimitive.Size = new Size(65, 22);
            btnNextPrimitive.TabIndex = 13;
            btnNextPrimitive.Text = ">>";
            btnNextPrimitive.UseVisualStyleBackColor = true;
            btnNextPrimitive.Click += btnNextPrimitive_Click;
            // 
            // btnPreviousPrimitive
            // 
            btnPreviousPrimitive.Enabled = false;
            btnPreviousPrimitive.Font = new Font("Segoe UI", 8.25F);
            btnPreviousPrimitive.Location = new Point(15, 188);
            btnPreviousPrimitive.Name = "btnPreviousPrimitive";
            btnPreviousPrimitive.Size = new Size(65, 22);
            btnPreviousPrimitive.TabIndex = 12;
            btnPreviousPrimitive.Text = "<<";
            btnPreviousPrimitive.UseVisualStyleBackColor = true;
            btnPreviousPrimitive.Click += btnPreviousPrimitive_Click;
            // 
            // txtIdentifiedPrimitiveCount
            // 
            txtIdentifiedPrimitiveCount.Font = new Font("Segoe UI", 8.25F);
            txtIdentifiedPrimitiveCount.Location = new Point(120, 15);
            txtIdentifiedPrimitiveCount.Name = "txtIdentifiedPrimitiveCount";
            txtIdentifiedPrimitiveCount.Size = new Size(36, 22);
            txtIdentifiedPrimitiveCount.TabIndex = 8;
            txtIdentifiedPrimitiveCount.KeyDown += txtPrimitiveCount_KeyDown;
            // 
            // lblPrimitiveCount
            // 
            lblPrimitiveCount.AutoSize = true;
            lblPrimitiveCount.Font = new Font("Segoe UI", 8.25F);
            lblPrimitiveCount.Location = new Point(37, 18);
            lblPrimitiveCount.Name = "lblPrimitiveCount";
            lblPrimitiveCount.Size = new Size(88, 13);
            lblPrimitiveCount.TabIndex = 7;
            lblPrimitiveCount.Text = "Primitive Count:";
            lblPrimitiveCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // grpbxFile
            // 
            grpbxFile.Controls.Add(txtIPAddress);
            grpbxFile.Controls.Add(lblIPAddress);
            grpbxFile.Controls.Add(btnTransmitFile);
            grpbxFile.Controls.Add(btnSaveFile);
            grpbxFile.Controls.Add(btnLoadFile);
            grpbxFile.Font = new Font("Segoe UI", 8.25F);
            grpbxFile.Location = new Point(10, 10);
            grpbxFile.Name = "grpbxFile";
            grpbxFile.Size = new Size(170, 125);
            grpbxFile.TabIndex = 4;
            grpbxFile.TabStop = false;
            grpbxFile.Text = "File";
            // 
            // txtIPAddress
            // 
            txtIPAddress.Font = new Font("Segoe UI", 8.25F);
            txtIPAddress.Location = new Point(34, 94);
            txtIPAddress.Name = "txtIPAddress";
            txtIPAddress.Size = new Size(120, 22);
            txtIPAddress.TabIndex = 4;
            // 
            // lblIPAddress
            // 
            lblIPAddress.AutoSize = true;
            lblIPAddress.Font = new Font("Segoe UI", 8.25F);
            lblIPAddress.Location = new Point(15, 97);
            lblIPAddress.Name = "lblIPAddress";
            lblIPAddress.Size = new Size(19, 13);
            lblIPAddress.TabIndex = 3;
            lblIPAddress.Text = "IP:";
            lblIPAddress.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnTransmitFile
            // 
            btnTransmitFile.Font = new Font("Segoe UI", 8.25F);
            btnTransmitFile.Location = new Point(15, 68);
            btnTransmitFile.Name = "btnTransmitFile";
            btnTransmitFile.Size = new Size(140, 22);
            btnTransmitFile.TabIndex = 2;
            btnTransmitFile.Text = "Transmit File";
            btnTransmitFile.UseVisualStyleBackColor = true;
            btnTransmitFile.Click += btnTransmitFile_Click;
            // 
            // btnSaveFile
            // 
            btnSaveFile.Font = new Font("Segoe UI", 8.25F);
            btnSaveFile.Location = new Point(15, 44);
            btnSaveFile.Name = "btnSaveFile";
            btnSaveFile.Size = new Size(140, 22);
            btnSaveFile.TabIndex = 1;
            btnSaveFile.Text = "Save File";
            btnSaveFile.UseVisualStyleBackColor = true;
            btnSaveFile.Click += btnSaveFile_Click;
            // 
            // btnLoadFile
            // 
            btnLoadFile.Font = new Font("Segoe UI", 8.25F);
            btnLoadFile.Location = new Point(15, 20);
            btnLoadFile.Name = "btnLoadFile";
            btnLoadFile.Size = new Size(140, 22);
            btnLoadFile.TabIndex = 0;
            btnLoadFile.Text = "Load File";
            btnLoadFile.UseVisualStyleBackColor = true;
            btnLoadFile.Click += btnLoadFile_Click;
            // 
            // Editor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(786, 447);
            Controls.Add(grpbxFile);
            Controls.Add(grpbxPrimitives);
            Controls.Add(pctbxCanvas);
            Controls.Add(grpbxFrames);
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
            grpbxCircle.ResumeLayout(false);
            grpbxCircle.PerformLayout();
            grpbxQuarterCircle.ResumeLayout(false);
            grpbxQuarterCircle.PerformLayout();
            grpbxRoundRect.ResumeLayout(false);
            grpbxRoundRect.PerformLayout();
            grpbxTriangle.ResumeLayout(false);
            grpbxTriangle.PerformLayout();
            grpbxFile.ResumeLayout(false);
            grpbxFile.PerformLayout();
            ResumeLayout(false);
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
        private Button btnNextPrimitive;
        private Button btnPreviousPrimitive;
        private TextBox txtIdentifiedPrimitiveCount;
        private Label lblPrimitiveCount;
        private Button btnRemoveCurrentFrame;
        private ColorDialog selectColor;
        private Button btnRemoveCurrentPrimitive;
        private GroupBox grpbxFile;
        private Button btnTransmitFile;
        private Button btnSaveFile;
        private Button btnLoadFile;
        private TextBox txtIPAddress;
        private Label lblIPAddress;
        private Button btnAddRoundRectangle;
        private Button btnAddTriangle;
        private Button btnAddQuarterCircle;
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
        private TextBox txtFrameDuration;
        private Label lblFrameDuration;
        private Button btnFrameFillColor;
        private TextBox txtFrameFillColor;
        private Label lblFillColor;
        private TextBox txtCurrentFrame;
        private Label lblCurrentFrameNumber;
        private GroupBox grpbxCurrentPrimitive;
        private Button btnPrimitiveDrawColor;
        private TextBox txtPrimitiveDrawColor;
        private Label lblPrimitiveDrawColor;
        private TextBox txtCurrentIdentifiedPrimitive;
        private Label lblCurrentPrimitiveNumber;
        private TextBox txtAddFrameDuration;
        private Label lblAddFrameDuration;
        private Button btnAddFrameFillColor;
        private TextBox txtAddFrameFillColor;
        private Label lblAddFillColor;
        private Button btnUpdateCurrentPrimitive;
        private Button btnAddPrimitiveDrawColor;
        private TextBox txtAddPrimitiveDrawColor;
        private Label lblAddPrimitiveDrawColor;
        private TextBox txtQuarterCircleDelta;
        private TextBox txtQuarterCircleQuadrants;
        private TextBox txtQuarterCircleY0;
        private TextBox txtQuarterCircleX0;
        private Label lblQuarterCircleDelta;
        private GroupBox grpbxCircle;
        private TextBox txtCircleRadius;
        private TextBox txtCircleY0;
        private TextBox txtCircleX0;
        private Label lblCircleRadius;
        private Label lblCircleY0;
        private Label lblCircleX0;
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
        private GroupBox grpbxQuarterCircle;
        private TextBox txtQuarterCircleRadius;
        private Label lblQuarterCircleRadius;
        private Label lblQuarterCircleQuadrants;
        private Label lblQuarterCircleY0;
        private Label lblQuarterCircleX0;
        private SaveFileDialog saveFile;
    }
}