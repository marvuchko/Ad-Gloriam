namespace Ad_Gloriam
{
    partial class FormInternetMenu
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
            this.btnJoinGame = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
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
            this.btnCreateGame.Location = new System.Drawing.Point(113, 127);
            this.btnCreateGame.Name = "btnCreateGame";
            this.btnCreateGame.Size = new System.Drawing.Size(125, 38);
            this.btnCreateGame.TabIndex = 1;
            this.btnCreateGame.UseVisualStyleBackColor = false;
            this.btnCreateGame.Click += new System.EventHandler(this.btnCreateGame_Click);
            this.btnCreateGame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseDown);
            this.btnCreateGame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseMove);
            this.btnCreateGame.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseUp);
            // 
            // btnJoinGame
            // 
            this.btnJoinGame.BackColor = System.Drawing.Color.Transparent;
            this.btnJoinGame.BackgroundImage = global::Ad_Gloriam.Properties.Resources.menu_button_join_game;
            this.btnJoinGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnJoinGame.FlatAppearance.BorderSize = 0;
            this.btnJoinGame.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnJoinGame.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnJoinGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnJoinGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoinGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoinGame.Location = new System.Drawing.Point(113, 197);
            this.btnJoinGame.Name = "btnJoinGame";
            this.btnJoinGame.Size = new System.Drawing.Size(125, 38);
            this.btnJoinGame.TabIndex = 2;
            this.btnJoinGame.UseVisualStyleBackColor = false;
            this.btnJoinGame.Click += new System.EventHandler(this.btnJoinGame_Click);
            this.btnJoinGame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseDown);
            this.btnJoinGame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseMove);
            this.btnJoinGame.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseUp);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BackgroundImage = global::Ad_Gloriam.Properties.Resources.menu_button_back;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(113, 267);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(125, 38);
            this.btnBack.TabIndex = 3;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseDown);
            this.btnBack.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseMove);
            this.btnBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnCreateGame_MouseUp);
            // 
            // FormLanMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackgroundImage = global::Ad_Gloriam.Properties.Resources.main_menu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(349, 379);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnJoinGame);
            this.Controls.Add(this.btnCreateGame);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLanMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormLanMenu";
            this.Load += new System.EventHandler(this.FormLanMenu_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormLanMenu_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormLanMenu_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormLanMenu_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateGame;
        private System.Windows.Forms.Button btnJoinGame;
        private System.Windows.Forms.Button btnBack;
    }
}