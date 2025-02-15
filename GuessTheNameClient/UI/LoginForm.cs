using System;
using System.Drawing;
using System.Windows.Forms;
using GuessTheNameClient.ClientCore;

namespace GuessTheNameClient.UI
{
    public partial class LoginForm : Form
    {
        private readonly GameClient _client = new();
        private Button? button1; // Mark as nullable

        public LoginForm()
        {
            InitializeComponent(); // 1. Call designer initialization first
            InitializeCustomComponents();
            MessageBox.Show("Welcome to Guess The Name!", "Welcome",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InitializeCustomComponents()
        {
            // Configure form (complementary to designer settings)
            this.Text = "Server Connection Test";
            this.ClientSize = new Size(300, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //private void InitializeComponent()
        //{
        //    // Designer-generated code
        //    this.button1 = new Button();
        //    this.SuspendLayout();
        //    // 
        //    // button1
        //    // 
        //    this.button1.Location = new Point(80, 60);
        //    this.button1.Name = "button1";
        //    this.button1.Size = new Size(120, 40);
        //    this.button1.TabIndex = 0;
        //    this.button1.Text = "Test Connection";
        //    this.button1.UseVisualStyleBackColor = true;
        //    this.button1.Click += this.button1_Click;
        //    // 
        //    // LoginForm
        //    // 
        //    this.ClientSize = new Size(300, 200);
        //    this.Controls.Add(this.button1);
        //    this.Name = "LoginForm";
        //    this.ResumeLayout(false);
        //}

        private async void button1_Click(object? sender, EventArgs? e)
        {
            try
            {
                if (!_client.IsConnected)
                {
                    await _client.Connect("127.0.0.1", 8888);
                    MessageBox.Show("Connected to server!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Already connected!", "Info",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Connection Failed",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}