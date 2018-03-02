namespace Ad_Gloriam
{
    partial class FormCreateGameInternet
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
            this.btnCreateGame = new System.Windows.Forms.Button();
            this.tbxGameName = new System.Windows.Forms.TextBox();
            this.tbxGamePassword = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRandomName = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCreateGame
            // 
            this.btnCreateGame.BackColor = System.Drawing.Color.Transparent;
            this.btnCreateGame.BackgroundImage = global::Ad_Gloriam.Properties.Resources.menu_button_create_game;
            this.btnCreateGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCreateGame.FlatAppearance.BorderSize = 0;
            this.btnCreateGame.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCreateGame.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCreateGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCreateGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateGame.Location = new System.Drawing.Point(153, 461);
            this.btnCreateGame.Name = "btnCreateGame";
            this.btnCreateGame.Size = new System.Drawing.Size(125, 38);
            this.btnCreateGame.TabIndex = 2;
            this.btnCreateGame.UseVisualStyleBackColor = false;
            this.btnCreateGame.Click += new System.EventHandler(this.btnCreateGame_Click);
            this.btnCreateGame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseDown);
            this.btnCreateGame.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseUp);
            // 
            // tbxGameName
            // 
            this.tbxGameName.BackColor = System.Drawing.Color.White;
            this.tbxGameName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxGameName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxGameName.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.tbxGameName.Location = new System.Drawing.Point(394, 142);
            this.tbxGameName.MaxLength = 20;
            this.tbxGameName.Name = "tbxGameName";
            this.tbxGameName.Size = new System.Drawing.Size(179, 17);
            this.tbxGameName.TabIndex = 4;
            this.tbxGameName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxGameName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxGameName_KeyPress);
            // 
            // tbxGamePassword
            // 
            this.tbxGamePassword.BackColor = System.Drawing.Color.White;
            this.tbxGamePassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxGamePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxGamePassword.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.tbxGamePassword.Location = new System.Drawing.Point(394, 233);
            this.tbxGamePassword.MaxLength = 20;
            this.tbxGamePassword.Name = "tbxGamePassword";
            this.tbxGamePassword.PasswordChar = '*';
            this.tbxGamePassword.Size = new System.Drawing.Size(179, 17);
            this.tbxGamePassword.TabIndex = 5;
            this.tbxGamePassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxGamePassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxGameName_KeyPress);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = global::Ad_Gloriam.Properties.Resources.menu_button_back;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(431, 461);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 38);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseDown);
            this.btnCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseUp);
            // 
            // btnRandomName
            // 
            this.btnRandomName.BackColor = System.Drawing.Color.Transparent;
            this.btnRandomName.BackgroundImage = global::Ad_Gloriam.Properties.Resources.menu_button_random_name;
            this.btnRandomName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRandomName.FlatAppearance.BorderSize = 0;
            this.btnRandomName.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRandomName.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRandomName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandomName.Location = new System.Drawing.Point(292, 461);
            this.btnRandomName.Name = "btnRandomName";
            this.btnRandomName.Size = new System.Drawing.Size(125, 38);
            this.btnRandomName.TabIndex = 8;
            this.btnRandomName.UseVisualStyleBackColor = false;
            this.btnRandomName.Click += new System.EventHandler(this.btnRandomName_Click);
            this.btnRandomName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseDown);
            this.btnRandomName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseUp);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.BackColor = System.Drawing.Color.Transparent;
            this.lblHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHint.ForeColor = System.Drawing.Color.Maroon;
            this.lblHint.Location = new System.Drawing.Point(206, 361);
            this.lblHint.MaximumSize = new System.Drawing.Size(300, 50);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(281, 36);
            this.lblHint.TabIndex = 9;
            this.lblHint.Text = "Game name and password can have only letters and numbers!";
            this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormCreateGameInternet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Ad_Gloriam.Properties.Resources.menu_create_game;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(700, 544);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnRandomName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbxGamePassword);
            this.Controls.Add(this.tbxGameName);
            this.Controls.Add(this.btnCreateGame);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCreateGameInternet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCreateGame";
            this.Load += new System.EventHandler(this.FormCreateGame_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormCreateGame_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormCreateGame_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormCreateGame_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCreateGame;
        private System.Windows.Forms.TextBox tbxGameName;
        private System.Windows.Forms.TextBox tbxGamePassword;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRandomName;
        private System.Windows.Forms.Label lblHint;
    }
}