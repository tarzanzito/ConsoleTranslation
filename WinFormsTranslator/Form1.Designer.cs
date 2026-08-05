namespace WinFormsTranslator
{
    partial class Form1
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
            comboBoxSourceLang = new ComboBox();
            comboBoxTargeLang = new ComboBox();
            textBoxFileName = new TextBox();
            label1 = new Label();
            progressBar1 = new ProgressBar();
            label2 = new Label();
            label3 = new Label();
            listBox1 = new ListBox();
            buttonExploreFile = new Button();
            buttonProcessCancel = new Button();
            buttonOpenFolder = new Button();
            label4 = new Label();
            SuspendLayout();
            // 
            // buttonClose
            // 
            buttonClose.Location = new Point(490, 68);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(72, 33);
            buttonClose.TabIndex = 32;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // comboBoxSourceLang
            // 
            comboBoxSourceLang.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSourceLang.FormattingEnabled = true;
            comboBoxSourceLang.ItemHeight = 15;
            comboBoxSourceLang.Location = new Point(102, 52);
            comboBoxSourceLang.Name = "comboBoxSourceLang";
            comboBoxSourceLang.Size = new Size(120, 23);
            comboBoxSourceLang.TabIndex = 2;
            comboBoxSourceLang.SelectedIndexChanged += comboBoxSourceLang_TextChanged;
            comboBoxSourceLang.TextChanged += comboBoxSourceLang_TextChanged;
            // 
            // comboBoxTargeLang
            // 
            comboBoxTargeLang.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTargeLang.FormattingEnabled = true;
            comboBoxTargeLang.ItemHeight = 15;
            comboBoxTargeLang.Location = new Point(102, 80);
            comboBoxTargeLang.Name = "comboBoxTargeLang";
            comboBoxTargeLang.Size = new Size(120, 23);
            comboBoxTargeLang.TabIndex = 3;
            comboBoxTargeLang.SelectedIndexChanged += Form1_DragLeave;
            comboBoxTargeLang.TextChanged += comboBoxTargeLang_TextChanged;
            // 
            // textBoxFileName
            // 
            textBoxFileName.Location = new Point(102, 25);
            textBoxFileName.Name = "textBoxFileName";
            textBoxFileName.Size = new Size(425, 23);
            textBoxFileName.TabIndex = 1;
            textBoxFileName.TextChanged += textBoxFileName_TextChanged;
            textBoxFileName.DoubleClick += textBoxFileName_DoubleClick;
            // 
            // label1
            // 
            label1.Location = new Point(10, 24);
            label1.Name = "label1";
            label1.Size = new Size(86, 22);
            label1.TabIndex = 5;
            label1.Text = "File Name:";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            progressBar1.Dock = DockStyle.Top;
            progressBar1.Location = new Point(0, 0);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(572, 10);
            progressBar1.TabIndex = 6;
            // 
            // label2
            // 
            label2.Location = new Point(10, 52);
            label2.Name = "label2";
            label2.Size = new Size(86, 21);
            label2.TabIndex = 7;
            label2.Text = "Source Lang:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.Location = new Point(10, 79);
            label3.Name = "label3";
            label3.Size = new Size(86, 22);
            label3.TabIndex = 8;
            label3.Text = "Target Lang:";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(10, 135);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(552, 169);
            listBox1.TabIndex = 41;
            // 
            // buttonExploreFile
            // 
            buttonExploreFile.Location = new Point(534, 25);
            buttonExploreFile.Name = "buttonExploreFile";
            buttonExploreFile.Size = new Size(28, 25);
            buttonExploreFile.TabIndex = 11;
            buttonExploreFile.Text = "...";
            buttonExploreFile.UseVisualStyleBackColor = true;
            buttonExploreFile.Click += buttonExploreFile_Click;
            // 
            // buttonProcessCancel
            // 
            buttonProcessCancel.Location = new Point(389, 68);
            buttonProcessCancel.Name = "buttonProcessCancel";
            buttonProcessCancel.Size = new Size(95, 33);
            buttonProcessCancel.TabIndex = 31;
            buttonProcessCancel.Text = "Translate File";
            buttonProcessCancel.UseVisualStyleBackColor = true;
            buttonProcessCancel.Click += buttonProcessCancel_Click;
            // 
            // buttonOpenFolder
            // 
            buttonOpenFolder.Location = new Point(249, 68);
            buttonOpenFolder.Name = "buttonOpenFolder";
            buttonOpenFolder.Size = new Size(95, 33);
            buttonOpenFolder.TabIndex = 21;
            buttonOpenFolder.Text = "Open Folder";
            buttonOpenFolder.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.Location = new Point(10, 110);
            label4.Name = "label4";
            label4.Size = new Size(86, 22);
            label4.TabIndex = 12;
            label4.Text = "Log:";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(572, 315);
            Controls.Add(label4);
            Controls.Add(buttonExploreFile);
            Controls.Add(listBox1);
            Controls.Add(buttonOpenFolder);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(progressBar1);
            Controls.Add(label1);
            Controls.Add(textBoxFileName);
            Controls.Add(comboBoxTargeLang);
            Controls.Add(comboBoxSourceLang);
            Controls.Add(buttonProcessCancel);
            Controls.Add(buttonClose);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Translator";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            DragDrop += Form1_DragDrop;
            DragEnter += Form1_DragEnter;
            DragOver += Form1_DragOver;
            DragLeave += Form1_DragLeave;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonClose;
        private ComboBox comboBoxSourceLang;
        private ComboBox comboBoxTargeLang;
        private TextBox textBoxFileName;
        private Label label1;
        private ProgressBar progressBar1;
        private Label label2;
        private Label label3;
        private ListBox listBox1;
        private Button buttonExploreFile;
        private Button buttonProcessCancel;
        private Button buttonOpenFolder;
        private Label label4;
    }
}
