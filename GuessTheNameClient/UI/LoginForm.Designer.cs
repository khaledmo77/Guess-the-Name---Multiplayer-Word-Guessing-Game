namespace GuessTheNameClient.UI
{
    partial class LoginForm
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
            // Designer-generated code
            this.button1 = new Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new Point(80, 60);
            this.button1.Name = "button1";
            this.button1.Size = new Size(120, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Test Connection";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += this.button1_Click;
            // 
            // LoginForm
            // 
            this.ClientSize = new Size(300, 200);
            this.Controls.Add(this.button1);
            this.Name = "LoginForm";
            this.ResumeLayout(false);
        }

        #endregion
    }
}