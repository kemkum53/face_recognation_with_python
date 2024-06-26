namespace face_recognation_demo
{
    partial class EntranceExitFrom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lbx_list = new ListBox();
            pic_image = new PictureBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            silToolStripMenuItem = new ToolStripMenuItem();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pic_image).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lbx_list
            // 
            lbx_list.FormattingEnabled = true;
            lbx_list.ItemHeight = 15;
            lbx_list.Location = new Point(12, 12);
            lbx_list.Name = "lbx_list";
            lbx_list.Size = new Size(220, 409);
            lbx_list.TabIndex = 0;
            lbx_list.SelectedIndexChanged += lbx_list_SelectedIndexChanged;
            // 
            // pic_image
            // 
            pic_image.Location = new Point(238, 12);
            pic_image.Name = "pic_image";
            pic_image.Size = new Size(711, 439);
            pic_image.TabIndex = 1;
            pic_image.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { silToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(87, 26);
            // 
            // silToolStripMenuItem
            // 
            silToolStripMenuItem.Name = "silToolStripMenuItem";
            silToolStripMenuItem.Size = new Size(86, 22);
            silToolStripMenuItem.Text = "Sil";
            silToolStripMenuItem.Click += silToolStripMenuItem_Click;
            // 
            // button1
            // 
            button1.Location = new Point(12, 424);
            button1.Name = "button1";
            button1.Size = new Size(220, 27);
            button1.TabIndex = 2;
            button1.Text = "Sil";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // EntranceExitFrom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(961, 469);
            Controls.Add(button1);
            Controls.Add(pic_image);
            Controls.Add(lbx_list);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "EntranceExitFrom";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EntranceExitFrom";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)pic_image).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox lbx_list;
        private PictureBox pic_image;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem silToolStripMenuItem;
        private Button button1;
    }
}