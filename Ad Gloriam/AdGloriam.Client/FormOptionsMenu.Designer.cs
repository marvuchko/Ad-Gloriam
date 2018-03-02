namespace Ad_Gloriam
{
    partial class FormOptionsMenu
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
            this.btnChangeTheme = new System.Windows.Forms.Button();
            this.lblAudio = new System.Windows.Forms.Label();
            this.btnAudio = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnChangeTheme
            // 
            this.btnChangeTheme.BackColor = System.Drawing.Color.Transparent;
            this.btnChangeTheme.BackgroundImage = global::Ad_Gloriam.Properties.Resources.menu_button_change_theme;
            this.btnChangeTheme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChangeTheme.FlatAppearance.BorderSize = 0;
            this.btnChangeTheme.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnChangeTheme.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnChangeTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeTheme.Location = new System.Drawing.Point(114, 124);
            this.btnChangeTheme.Name = "btnChangeTheme";
            this.btnChangeTheme.Size = new System.Drawing.Size(125, 38);
            this.btnChangeTheme.TabIndex = 0;
            this.btnChangeTheme.UseVisualStyleBackColor = false;
            this.btnChangeTheme.Click += new System.EventHandler(this.btnChangeTheme_Click);
            this.btnChangeTheme.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnChangeTheme_MouseDown);
            this.btnChangeTheme.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnChangeTheme_MouseUp);
            // 
            // lblAudio
            // 
            this.lblAudio.AutoSize = true;
            this.lblAudio.BackColor = System.Drawing.Color.Transparent;
            this.lblAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAudio.ForeColor = System.Drawing.Color.Silver;
            this.lblAudio.Location = new System.Drawing.Point(115, 175);
            this.lblAudio.Name = "lblAudio";
            this.lblAudio.Size = new System.Drawing.Size(122, 26);
            this.lblAudio.TabIndex = 1;
            this.lblAudio.Text = "Audio: ON";
            // 
            // btnAudio
            // 
            this.btnAudio.BackColor = System.Drawing.Color.Transparent;
            this.btnAudio.BackgroundImage = global::Ad_Gloriam.Properties.Resources.soundEnabled;
            this.btnAudio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAudio.FlatAppearance.BorderSize = 0;
            this.btnAudio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAudio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAudio.Location = new System.Drawing.Point(156, 210);
            this.btnAudio.Name = "btnAudio";
            this.btnAudio.Size = new System.Drawing.Size(40, 40);
            this.btnAudio.TabIndex = 2;
            this.btnAudio.UseVisualStyleBackColor = false;
            this.btnAudio.Click += new System.EventHandler(this.btnAudio_Click);
            this.btnAudio.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnChangeTheme_MouseDown);
            this.btnAudio.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnChangeTheme_MouseUp);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BackgroundImage = global::Ad_Gloriam.Properties.Resources.menu_button_back;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Location = new System.Drawing.Point(114, 263);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(125, 38);
            this.btnBack.TabIndex = 3;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnChangeTheme_MouseDown);
            this.btnBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnChangeTheme_MouseUp);
            // 
            // FormOptionsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Ad_Gloriam.Properties.Resources.main_menu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(349, 379);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAudio);
            this.Controls.Add(this.lblAudio);
            this.Controls.Add(this.btnChangeTheme);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormOptionsMenu";
            this.Text = "FormOptionsMenu";
            this.Load += new System.EventHandler(this.FormOptionsMenu_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormOptionsMenu_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormOptionsMenu_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormOptionsMenu_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChangeTheme;
        private System.Windows.Forms.Label lblAudio;
        private System.Windows.Forms.Button btnAudio;
        private System.Windows.Forms.Button btnBack;
    }
}