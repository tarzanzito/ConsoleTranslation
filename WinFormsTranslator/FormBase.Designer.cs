namespace WinFormsTranslator
{
    partial class FormBase
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
            buttonClose = new Button();
            progressBar1 = new ProgressBar();
            buttonProcessCancel = new Button();
            buttonExploreFile = new Button();
            labelFilter1 = new Label();
            textBoxFilter1 = new TextBox();
            panelActions = new Panel();
            labelActions = new Label();
            panelLogInfos = new Panel();
            listBoxLogInfos = new ListBox();
            labelLogInfos = new Label();
            panelFilters = new Panel();
            labelFilters = new Label();
            panelActions.SuspendLayout();
            panelLogInfos.SuspendLayout();
            panelFilters.SuspendLayout();
            SuspendLayout();
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonClose.Location = new Point(32, 308);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(72, 33);
            buttonClose.TabIndex = 32;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // progressBar1
            // 
            progressBar1.Dock = DockStyle.Top;
            progressBar1.Location = new Point(10, 10);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(721, 10);
            progressBar1.TabIndex = 6;
            // 
            // buttonProcessCancel
            // 
            buttonProcessCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonProcessCancel.Location = new Point(9, 46);
            buttonProcessCancel.Name = "buttonProcessCancel";
            buttonProcessCancel.Size = new Size(95, 33);
            buttonProcessCancel.TabIndex = 31;
            buttonProcessCancel.Text = "Process";
            buttonProcessCancel.UseVisualStyleBackColor = true;
            buttonProcessCancel.Click += buttonProcessCancel_Click;
            // 
            // buttonExploreFile
            // 
            buttonExploreFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonExploreFile.Location = new Point(569, 50);
            buttonExploreFile.Name = "buttonExploreFile";
            buttonExploreFile.Size = new Size(28, 25);
            buttonExploreFile.TabIndex = 14;
            buttonExploreFile.Text = "...";
            buttonExploreFile.UseVisualStyleBackColor = true;
            // 
            // labelFilter1
            // 
            labelFilter1.Location = new Point(9, 52);
            labelFilter1.Name = "labelFilter1";
            labelFilter1.Size = new Size(70, 22);
            labelFilter1.TabIndex = 13;
            labelFilter1.Text = "String:";
            labelFilter1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBoxFilter1
            // 
            textBoxFilter1.Location = new Point(85, 52);
            textBoxFilter1.Name = "textBoxFilter1";
            textBoxFilter1.Size = new Size(472, 23);
            textBoxFilter1.TabIndex = 12;
            // 
            // panelActions
            // 
            panelActions.BackColor = SystemColors.GradientActiveCaption;
            panelActions.Controls.Add(labelActions);
            panelActions.Controls.Add(buttonClose);
            panelActions.Controls.Add(buttonProcessCancel);
            panelActions.Dock = DockStyle.Right;
            panelActions.Location = new Point(616, 20);
            panelActions.Name = "panelActions";
            panelActions.Padding = new Padding(6);
            panelActions.Size = new Size(115, 350);
            panelActions.TabIndex = 33;
            // 
            // labelActions
            // 
            labelActions.BackColor = SystemColors.Desktop;
            labelActions.Dock = DockStyle.Top;
            labelActions.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelActions.ForeColor = SystemColors.ControlLightLight;
            labelActions.Location = new Point(6, 6);
            labelActions.Margin = new Padding(3);
            labelActions.Name = "labelActions";
            labelActions.Size = new Size(103, 33);
            labelActions.TabIndex = 33;
            labelActions.Text = "Actions";
            labelActions.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelLogInfos
            // 
            panelLogInfos.BackColor = SystemColors.ActiveCaption;
            panelLogInfos.Controls.Add(listBoxLogInfos);
            panelLogInfos.Controls.Add(labelLogInfos);
            panelLogInfos.Dock = DockStyle.Bottom;
            panelLogInfos.Location = new Point(10, 120);
            panelLogInfos.Name = "panelLogInfos";
            panelLogInfos.Padding = new Padding(6);
            panelLogInfos.Size = new Size(606, 250);
            panelLogInfos.TabIndex = 44;
            // 
            // listBoxLogInfos
            // 
            listBoxLogInfos.Dock = DockStyle.Fill;
            listBoxLogInfos.FormattingEnabled = true;
            listBoxLogInfos.Location = new Point(6, 39);
            listBoxLogInfos.Margin = new Padding(0, 20, 0, 0);
            listBoxLogInfos.Name = "listBoxLogInfos";
            listBoxLogInfos.Size = new Size(594, 205);
            listBoxLogInfos.TabIndex = 43;
            // 
            // labelLogInfos
            // 
            labelLogInfos.BackColor = SystemColors.Desktop;
            labelLogInfos.Dock = DockStyle.Top;
            labelLogInfos.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelLogInfos.ForeColor = SystemColors.ControlLightLight;
            labelLogInfos.Location = new Point(6, 6);
            labelLogInfos.Margin = new Padding(3, 3, 3, 30);
            labelLogInfos.Name = "labelLogInfos";
            labelLogInfos.Size = new Size(594, 33);
            labelLogInfos.TabIndex = 33;
            labelLogInfos.Text = "Log / Infos";
            labelLogInfos.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelFilters
            // 
            panelFilters.BackColor = SystemColors.GradientInactiveCaption;
            panelFilters.Controls.Add(labelFilters);
            panelFilters.Controls.Add(buttonExploreFile);
            panelFilters.Controls.Add(labelFilter1);
            panelFilters.Controls.Add(textBoxFilter1);
            panelFilters.Dock = DockStyle.Fill;
            panelFilters.Location = new Point(10, 20);
            panelFilters.Name = "panelFilters";
            panelFilters.Padding = new Padding(6);
            panelFilters.Size = new Size(606, 100);
            panelFilters.TabIndex = 45;
            // 
            // labelFilters
            // 
            labelFilters.BackColor = SystemColors.Desktop;
            labelFilters.Dock = DockStyle.Top;
            labelFilters.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelFilters.ForeColor = SystemColors.ControlLightLight;
            labelFilters.Location = new Point(6, 6);
            labelFilters.Margin = new Padding(3);
            labelFilters.Name = "labelFilters";
            labelFilters.Size = new Size(594, 33);
            labelFilters.TabIndex = 34;
            labelFilters.Text = "Filters";
            labelFilters.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FormBase
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(741, 380);
            Controls.Add(panelFilters);
            Controls.Add(panelLogInfos);
            Controls.Add(panelActions);
            Controls.Add(progressBar1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormBase";
            Padding = new Padding(10);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Translator";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            DragDrop += Form1_DragDrop;
            DragEnter += Form1_DragEnter;
            DragOver += Form1_DragOver;
            DragLeave += Form1_DragLeave;
            panelActions.ResumeLayout(false);
            panelLogInfos.ResumeLayout(false);
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonClose;
        private ProgressBar progressBar1;
        private Button buttonProcessCancel;
        private GroupBox groupBoxLogInfo;
        private Button buttonExploreFile;
        private Label labelFilter1;
        private TextBox textBoxFilter1;
        private Panel panelActions;
        private Label labelActions;
        private Panel panelLogInfos;
        private Label labelLogInfos;
        private ListBox listBoxLogInfos;
        private Panel panelFilters;
        private Label labelFilters;
    }
}
