namespace KohonenSOM
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_Points = new System.Windows.Forms.Button();
            this.button_Kohonen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 600);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button_Points
            // 
            this.button_Points.Location = new System.Drawing.Point(664, 12);
            this.button_Points.Name = "button_Points";
            this.button_Points.Size = new System.Drawing.Size(116, 38);
            this.button_Points.TabIndex = 2;
            this.button_Points.Text = "Puncte";
            this.button_Points.UseVisualStyleBackColor = true;
            this.button_Points.Click += new System.EventHandler(this.button_Points_Click);
            // 
            // button_Kohonen
            // 
            this.button_Kohonen.Location = new System.Drawing.Point(664, 81);
            this.button_Kohonen.Name = "button_Kohonen";
            this.button_Kohonen.Size = new System.Drawing.Size(116, 38);
            this.button_Kohonen.TabIndex = 3;
            this.button_Kohonen.Text = "Kohonen";
            this.button_Kohonen.UseVisualStyleBackColor = true;
            this.button_Kohonen.Click += new System.EventHandler(this.button_Kohonen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 630);
            this.Controls.Add(this.button_Kohonen);
            this.Controls.Add(this.button_Points);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_Points;
        private System.Windows.Forms.Button button_Kohonen;
    }
}

