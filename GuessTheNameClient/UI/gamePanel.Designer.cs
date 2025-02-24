namespace GuessTheNameClient.UI
{
    partial class gamePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label5 = new Label();
            SelectedWord = new Label();
            PlayerName = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            Player2name_label = new Label();
            Player1name_label = new Label();
            Player2 = new Label();
            Player1 = new Label();
            CategoryLabel = new Label();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label5.Location = new Point(456, 149);
            label5.Name = "label5";
            label5.Size = new Size(100, 23);
            label5.TabIndex = 12;
            label5.Text = "Player Turn";
            // 
            // SelectedWord
            // 
            SelectedWord.AutoSize = true;
            SelectedWord.Font = new Font("Segoe UI", 15F);
            SelectedWord.Location = new Point(317, 226);
            SelectedWord.Name = "SelectedWord";
            SelectedWord.Size = new Size(0, 35);
            SelectedWord.TabIndex = 11;
            // 
            // PlayerName
            // 
            PlayerName.AutoSize = true;
            PlayerName.Location = new Point(404, 36);
            PlayerName.Name = "PlayerName";
            PlayerName.Size = new Size(0, 20);
            PlayerName.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlDarkDark;
            label1.Location = new Point(245, 315);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 6;
            label1.Text = "Keyboard";
            // 
            // panel1
            // 
            panel1.Location = new Point(211, 326);
            panel1.Name = "panel1";
            panel1.Size = new Size(558, 184);
            panel1.TabIndex = 7;
            // 
            // panel2
            // 
            panel2.Controls.Add(Player2name_label);
            panel2.Controls.Add(Player1name_label);
            panel2.Controls.Add(Player2);
            panel2.Controls.Add(Player1);
            panel2.Location = new Point(92, 108);
            panel2.Name = "panel2";
            panel2.Size = new Size(250, 125);
            panel2.TabIndex = 13;
            panel2.Paint += panel2_Paint;
            // 
            // Player2name_label
            // 
            Player2name_label.AutoSize = true;
            Player2name_label.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            Player2name_label.Location = new Point(97, 74);
            Player2name_label.Name = "Player2name_label";
            Player2name_label.Size = new Size(124, 23);
            Player2name_label.TabIndex = 3;
            Player2name_label.Text = "player2_Name";
            Player2name_label.Click += Player2name_label_Click;
            // 
            // Player1name_label
            // 
            Player1name_label.AutoSize = true;
            Player1name_label.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Player1name_label.ImageAlign = ContentAlignment.MiddleLeft;
            Player1name_label.Location = new Point(97, 24);
            Player1name_label.Name = "Player1name_label";
            Player1name_label.Size = new Size(124, 23);
            Player1name_label.TabIndex = 2;
            Player1name_label.Text = "player1_Name";
            // 
            // Player2
            // 
            Player2.AutoSize = true;
            Player2.Location = new Point(16, 74);
            Player2.Name = "Player2";
            Player2.Size = new Size(61, 20);
            Player2.TabIndex = 1;
            Player2.Text = "Player 2";
            Player2.Click += label4_Click;
            // 
            // Player1
            // 
            Player1.AutoSize = true;
            Player1.Location = new Point(15, 26);
            Player1.Name = "Player1";
            Player1.Size = new Size(61, 20);
            Player1.TabIndex = 0;
            Player1.Text = "Player 1";
            Player1.Click += Player1_Click;
            // 
            // CategoryLabel
            // 
            CategoryLabel.AutoSize = true;
            CategoryLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            CategoryLabel.Location = new Point(713, 108);
            CategoryLabel.Name = "CategoryLabel";
            CategoryLabel.Size = new Size(127, 23);
            CategoryLabel.TabIndex = 15;
            CategoryLabel.Text = "CategoryLabel";
            CategoryLabel.Click += label6_Click;
            // 
            // gamePanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(980, 522);
            Controls.Add(CategoryLabel);
            Controls.Add(panel2);
            Controls.Add(label5);
            Controls.Add(SelectedWord);
            Controls.Add(PlayerName);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "gamePanel";
            TransparencyKey = Color.Fuchsia;
            Load += gamePanel_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private Label SelectedWord;
        private Label PlayerName;
        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private Label Player2name_label;
        private Label Player1name_label;
        private Label Player2;
        private Label Player1;
        private Label CategoryLabel;
    }
}
