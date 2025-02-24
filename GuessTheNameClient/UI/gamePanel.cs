using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GuessTheNameClient.UI
{
    public partial class gamePanel : Form
    {
        private string Word;
        private string player1 = "Khaled";
        private string player2 = "Roshdi";
        private string selectedCategory = "Countries";
        private StringBuilder display;
        private List<Button> KeyboardButtons = new List<Button>();
        private List<string> Words = new List<string>();

        public gamePanel()
        {
            InitializeComponent();
            LoadFromFile(selectedCategory);
            GenerateKeyboard();
        }
        private void GenerateKeyboard()
        {
            panel1.Controls.Clear();
            string[] keyboardRows = new string[]
            {
        "QWERTYUIOP",
        "ASDFGHJKL",
        "ZXCVBNM"
            };

            int startX = 10, startY = 10;
            int buttonWidth = 50, buttonHeight = 50;
            int padding = 5;

            for (int row = 0; row < keyboardRows.Length; row++)
            {
                int rowOffset = row * 20;
                for (int col = 0; col < keyboardRows[row].Length; col++)
                {
                    Button btn = new Button
                    {
                        Text = keyboardRows[row][col].ToString(),
                        Width = buttonWidth,
                        Height = buttonHeight,
                        Location = new Point(startX + col * (buttonWidth + padding) + rowOffset, startY + row * (buttonHeight + padding)),
                        Font = new Font("Arial", 14, FontStyle.Bold),
                        BackColor = Color.DimGray,  // Dark mode theme
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };

                    btn.FlatAppearance.BorderSize = 0; // Removes button border for a sleek look
                    btn.MouseEnter += (s, e) => btn.BackColor = Color.Silver;  // Hover effect
                    btn.MouseLeave += (s, e) => btn.BackColor = Color.DimGray; // Reset color on exit
                    btn.Click += KeyBoardClick;

                    panel1.Controls.Add(btn);
                    KeyboardButtons.Add(btn);
                }
            }
        }


        //private void GenerateKeyboard()
        //{
        //    panel1.Controls.Clear();
        //    string[] keyboardRows = new string[]
        //    {
        //        "QWERTYUIOP",
        //        "ASDFGHJKL",
        //        "ZXCVBNM"
        //    };

        //    int startX = 10, startY = 10;
        //    int buttonWidth = 40, buttonHeight = 40;
        //    int padding = 5;

        //    for (int row = 0; row < keyboardRows.Length; row++)
        //    {
        //        int rowOffset = row * 20;
        //        for (int col = 0; col < keyboardRows[row].Length; col++)
        //        {
        //            Button btn = new Button
        //            {
        //                Text = keyboardRows[row][col].ToString(),
        //                Width = buttonWidth,
        //                Height = buttonHeight,
        //                Location = new Point(startX + col * (buttonWidth + padding) + rowOffset, startY + row * (buttonHeight + padding))
        //            };
        //            btn.Click += KeyBoardClick;
        //            panel1.Controls.Add(btn);
        //            KeyboardButtons.Add(btn);
        //        }
        //    }
        //}

        private void KeyBoardClick(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                string Letter = clickedButton.Text.ToLower();
                clickedButton.Enabled = false;
                PutLetter(Letter);
            }
        }

        private void PutLetter(string Letter)
        {
            bool FoundLetter = false;
            char l = Letter[0];

            for (int i = 0; i < Word.Length; i++)
            {
                if (l == Word[i])
                {
                    FoundLetter = true;
                    display[i] = l;
                }
            }

            if (!FoundLetter)
            {
                MessageBox.Show($"{l} is a Wrong Letter =( ");
            }

            if (SelectedWord != null)
                SelectedWord.Text = display.ToString();

            if (!display.ToString().Contains("-"))
            {
                MessageBox.Show("WINNER!");
                ResetGame();
            }
        }

        private void LoadFromFile(string category)
        {
            if (CategoryLabel != null)
                CategoryLabel.Text = category;
            if (Player1name_label != null)
                Player1name_label.Text = player1;
            if (Player2name_label != null)
                Player2name_label.Text = player2;

            string filepath = $"C:\\ITI\\C#\\C# Project\\Guess-the-Name---Multiplayer-Word-Guessing-Game\\GuessTheNameClient\\{category}.txt";

            if (File.Exists(filepath))
            {
                Words = File.ReadAllLines(filepath).ToList();
                if (Words.Count > 0)
                {
                    Random random = new Random();
                    int chosenWordIndex = random.Next(Words.Count);
                    Word = Words[chosenWordIndex].ToLower();
                    display = new StringBuilder(new string('-', Word.Length));

                    if (SelectedWord != null)
                    {
                        SelectedWord.Text = display.ToString();
                        SelectedWord.Font = new Font("Arial", 32, FontStyle.Bold);
                        SelectedWord.AutoSize = false;
                        SelectedWord.Size = new Size(400, 100);
                        SelectedWord.TextAlign = ContentAlignment.MiddleCenter;
                    }
                }
                else
                {
                    MessageBox.Show("No words found in the file.");
                }
            }
            else
            {
                MessageBox.Show("Category file not found!");
            }
        }

        private void ResetGame()
        {
            LoadFromFile(selectedCategory);
            foreach (var button in KeyboardButtons)
            {
                button.Enabled = true;
            }
        }

        private void gamePanel_Load(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void Player1_Click(object sender, EventArgs e) { }
        private void Player2name_label_Click(object sender, EventArgs e) { }
    }
}