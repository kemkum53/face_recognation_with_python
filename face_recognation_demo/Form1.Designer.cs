namespace face_recognation_demo
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
            pic_main = new PictureBox();
            label1 = new Label();
            pic_test = new PictureBox();
            pictureBox1 = new PictureBox();
            btn_old = new Button();
            ((System.ComponentModel.ISupportInitialize)pic_main).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_test).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pic_main
            // 
            pic_main.Location = new Point(12, 12);
            pic_main.Name = "pic_main";
            pic_main.Size = new Size(960, 540);
            pic_main.TabIndex = 0;
            pic_main.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(12, 555);
            label1.Name = "label1";
            label1.Size = new Size(90, 37);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // pic_test
            // 
            pic_test.Location = new Point(12, 595);
            pic_test.Name = "pic_test";
            pic_test.Size = new Size(1880, 290);
            pic_test.TabIndex = 2;
            pic_test.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(978, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(914, 540);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // btn_old
            // 
            btn_old.Location = new Point(12, 922);
            btn_old.Name = "btn_old";
            btn_old.Size = new Size(75, 23);
            btn_old.TabIndex = 5;
            btn_old.Text = "button1";
            btn_old.UseVisualStyleBackColor = true;
            btn_old.Click += btn_old_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(btn_old);
            Controls.Add(pictureBox1);
            Controls.Add(pic_test);
            Controls.Add(label1);
            Controls.Add(pic_main);
            Name = "Form1";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)pic_main).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_test).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public PictureBox pic_main;
        private Label label1;
        public PictureBox pic_test;
        public PictureBox pictureBox1;
        private Button btn_old;
    }
}
