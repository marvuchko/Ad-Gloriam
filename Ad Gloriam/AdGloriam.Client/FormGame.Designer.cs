namespace Ad_Gloriam
{
    partial class FormGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.pnlGame = new System.Windows.Forms.Panel();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.tbxChatbox = new System.Windows.Forms.TextBox();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblOnTheMove = new System.Windows.Forms.Label();
            this.pboxLogo = new System.Windows.Forms.PictureBox();
            this.lblChat = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGame
            // 
            this.pnlGame.BackColor = System.Drawing.Color.White;
            this.pnlGame.Location = new System.Drawing.Point(78, 58);
            this.pnlGame.Name = "pnlGame";
            this.pnlGame.Size = new System.Drawing.Size(635, 634);
            this.pnlGame.TabIndex = 0;
            this.pnlGame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseDown);
            this.pnlGame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseMove);
            this.pnlGame.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseUp);
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.BackColor = System.Drawing.Color.Transparent;
            this.btnSendMessage.BackgroundImage = global::Ad_Gloriam.Properties.Resources.button_gray;
            this.btnSendMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSendMessage.FlatAppearance.BorderSize = 0;
            this.btnSendMessage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSendMessage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSendMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMessage.ForeColor = System.Drawing.Color.White;
            this.btnSendMessage.Location = new System.Drawing.Point(775, 619);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(70, 31);
            this.btnSendMessage.TabIndex = 1;
            this.btnSendMessage.Text = "SEND";
            this.btnSendMessage.UseVisualStyleBackColor = false;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            this.btnSendMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSendMessage_MouseDown);
            this.btnSendMessage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbxChatbox_MouseMove);
            this.btnSendMessage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnSendMessage_MouseUp);
            // 
            // tbxChatbox
            // 
            this.tbxChatbox.Location = new System.Drawing.Point(733, 309);
            this.tbxChatbox.Multiline = true;
            this.tbxChatbox.Name = "tbxChatbox";
            this.tbxChatbox.ReadOnly = true;
            this.tbxChatbox.Size = new System.Drawing.Size(153, 281);
            this.tbxChatbox.TabIndex = 2;
            this.tbxChatbox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseDown);
            this.tbxChatbox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbxChatbox_MouseMove);
            this.tbxChatbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseUp);
            // 
            // tbxMessage
            // 
            this.tbxMessage.Location = new System.Drawing.Point(748, 594);
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.Size = new System.Drawing.Size(123, 20);
            this.tbxMessage.TabIndex = 3;
            this.tbxMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxMessage_KeyUp);
            this.tbxMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseDown);
            this.tbxMessage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseUp);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = global::Ad_Gloriam.Properties.Resources.button_gray;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(775, 654);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 31);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSendMessage_MouseDown);
            this.btnExit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbxChatbox_MouseMove);
            this.btnExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnSendMessage_MouseUp);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.Transparent;
            this.lblScore.Font = new System.Drawing.Font("MingLiU", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.Color.Yellow;
            this.lblScore.Location = new System.Drawing.Point(774, 181);
            this.lblScore.MaximumSize = new System.Drawing.Size(80, 50);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(68, 36);
            this.lblScore.TabIndex = 6;
            this.lblScore.Text = "Score  2 : 2";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblScore.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseDown);
            this.lblScore.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseMove);
            this.lblScore.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseUp);
            // 
            // lblOnTheMove
            // 
            this.lblOnTheMove.AutoSize = true;
            this.lblOnTheMove.BackColor = System.Drawing.Color.Transparent;
            this.lblOnTheMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOnTheMove.Font = new System.Drawing.Font("MingLiU", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnTheMove.ForeColor = System.Drawing.Color.DarkRed;
            this.lblOnTheMove.Location = new System.Drawing.Point(775, 226);
            this.lblOnTheMove.MaximumSize = new System.Drawing.Size(100, 0);
            this.lblOnTheMove.Name = "lblOnTheMove";
            this.lblOnTheMove.Size = new System.Drawing.Size(71, 28);
            this.lblOnTheMove.TabIndex = 7;
            this.lblOnTheMove.Text = "Playing: player 1";
            this.lblOnTheMove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOnTheMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseDown);
            this.lblOnTheMove.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseMove);
            this.lblOnTheMove.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseUp);
            // 
            // pboxLogo
            // 
            this.pboxLogo.BackColor = System.Drawing.Color.Transparent;
            this.pboxLogo.BackgroundImage = global::Ad_Gloriam.Properties.Resources.logo;
            this.pboxLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pboxLogo.Location = new System.Drawing.Point(757, 88);
            this.pboxLogo.Name = "pboxLogo";
            this.pboxLogo.Size = new System.Drawing.Size(100, 90);
            this.pboxLogo.TabIndex = 5;
            this.pboxLogo.TabStop = false;
            this.pboxLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseDown);
            this.pboxLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseMove);
            this.pboxLogo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseUp);
            // 
            // lblChat
            // 
            this.lblChat.AutoSize = true;
            this.lblChat.BackColor = System.Drawing.Color.Transparent;
            this.lblChat.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblChat.Location = new System.Drawing.Point(786, 286);
            this.lblChat.Name = "lblChat";
            this.lblChat.Size = new System.Drawing.Size(52, 16);
            this.lblChat.TabIndex = 8;
            this.lblChat.Text = "CHAT";
            this.lblChat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseDown);
            this.lblChat.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseMove);
            this.lblChat.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseUp);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(965, 739);
            this.Controls.Add(this.lblChat);
            this.Controls.Add(this.lblOnTheMove);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.pboxLogo);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbxMessage);
            this.Controls.Add(this.tbxChatbox);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.pnlGame);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormGame";
            this.Load += new System.EventHandler(this.FormGame_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGame_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pboxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlGame;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TextBox tbxChatbox;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pboxLogo;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblOnTheMove;
        private System.Windows.Forms.Label lblChat;
    }
}

