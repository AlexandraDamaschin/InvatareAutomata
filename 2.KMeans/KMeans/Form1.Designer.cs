namespace KMeans
{
    partial class Form1
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label = new System.Windows.Forms.Label();
            this.button_clear_picture_box = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox.Location = new System.Drawing.Point(2, 2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(600, 600);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(749, 317);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(90, 30);
            this.label.TabIndex = 1;
            this.label.Text = "label";
            // 
            // button_clear_picture_box
            // 
            this.button_clear_picture_box.Location = new System.Drawing.Point(740, 37);
            this.button_clear_picture_box.Name = "button_clear_picture_box";
            this.button_clear_picture_box.Size = new System.Drawing.Size(113, 34);
            this.button_clear_picture_box.TabIndex = 3;
            this.button_clear_picture_box.Text = "Clear picture box";
            this.button_clear_picture_box.UseVisualStyleBackColor = true;
            this.button_clear_picture_box.Click += new System.EventHandler(this.button_clear_picture_box_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(874, 637);
            this.Controls.Add(this.button_clear_picture_box);
            this.Controls.Add(this.label);
            this.Controls.Add(this.pictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button button_clear_picture_box;
    }
}

