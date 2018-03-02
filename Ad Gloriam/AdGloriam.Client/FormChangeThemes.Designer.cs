namespace Ad_Gloriam
{
    partial class FormChangeThemes
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
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlHolder = new System.Windows.Forms.Panel();
            this.twThemes = new System.Windows.Forms.TreeView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblThemes = new System.Windows.Forms.Label();
            this.pnlHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDefault
            // 
            this.btnDefault.BackColor = System.Drawing.Color.Transparent;
            this.btnDefault.BackgroundImage = global::Ad_Gloriam.Properties.Resources.button_gray;
            this.btnDefault.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDefault.FlatAppearance.BorderSize = 0;
            this.btnDefault.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDefault.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefault.ForeColor = System.Drawing.Color.White;
            this.btnDefault.Location = new System.Drawing.Point(7, 65);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(70, 31);
            this.btnDefault.TabIndex = 2;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = false;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            this.btnDefault.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDefault_MouseDown);
            this.btnDefault.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDefault_MouseUp);
            // 
            // btnChange
            // 
            this.btnChange.BackColor = System.Drawing.Color.Transparent;
            this.btnChange.BackgroundImage = global::Ad_Gloriam.Properties.Resources.button_gray;
            this.btnChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChange.FlatAppearance.BorderSize = 0;
            this.btnChange.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnChange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChange.ForeColor = System.Drawing.Color.White;
            this.btnChange.Location = new System.Drawing.Point(7, 150);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(70, 31);
            this.btnChange.TabIndex = 5;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            this.btnChange.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDefault_MouseDown);
            this.btnChange.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDefault_MouseUp);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BackgroundImage = global::Ad_Gloriam.Properties.Resources.button_gray;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(7, 235);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(70, 31);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDefault_MouseDown);
            this.btnBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDefault_MouseUp);
            // 
            // pnlHolder
            // 
            this.pnlHolder.BackColor = System.Drawing.Color.Transparent;
            this.pnlHolder.Controls.Add(this.btnBack);
            this.pnlHolder.Controls.Add(this.btnChange);
            this.pnlHolder.Controls.Add(this.btnDefault);
            this.pnlHolder.Location = new System.Drawing.Point(117, 120);
            this.pnlHolder.Name = "pnlHolder";
            this.pnlHolder.Size = new System.Drawing.Size(83, 320);
            this.pnlHolder.TabIndex = 7;
            this.pnlHolder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormChangeTokens_MouseDown);
            this.pnlHolder.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormChangeTokens_MouseMove);
            this.pnlHolder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormChangeTokens_MouseUp);
            // 
            // twThemes
            // 
            this.twThemes.BackColor = System.Drawing.Color.Silver;
            this.twThemes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twThemes.ItemHeight = 30;
            this.twThemes.Location = new System.Drawing.Point(313, 97);
            this.twThemes.Name = "twThemes";
            this.twThemes.Size = new System.Drawing.Size(256, 339);
            this.twThemes.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Ad_Gloriam.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(117, 74);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(83, 75);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // lblThemes
            // 
            this.lblThemes.AutoSize = true;
            this.lblThemes.BackColor = System.Drawing.Color.Transparent;
            this.lblThemes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThemes.ForeColor = System.Drawing.Color.Maroon;
            this.lblThemes.Location = new System.Drawing.Point(380, 65);
            this.lblThemes.Name = "lblThemes";
            this.lblThemes.Size = new System.Drawing.Size(130, 18);
            this.lblThemes.TabIndex = 10;
            this.lblThemes.Text = "Installed themes";
            // 
            // FormChangeThemes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.BackgroundImage = global::Ad_Gloriam.Properties.Resources.menu_change_theme;
            this.ClientSize = new System.Drawing.Size(700, 512);
            this.Controls.Add(this.lblThemes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.twThemes);
            this.Controls.Add(this.pnlHolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormChangeThemes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormChangeTokens_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormChangeTokens_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormChangeTokens_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormChangeTokens_MouseUp);
            this.pnlHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel pnlHolder;
        private System.Windows.Forms.TreeView twThemes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblThemes;
    }
}