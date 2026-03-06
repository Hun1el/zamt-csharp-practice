using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class GameForm : Form
    {
        private int difficulty;
        private GameLogic gameLogic;
        private Timer gameTimer;
        private int cellsize = 20;
        private bool isPaused;

        private SoundManager soundManager;

        public GameForm(int difficulty)
        {
            InitializeComponent();
            this.difficulty = difficulty;
            int width = pictureBox1.Width / cellsize;
            int height = pictureBox1.Height / cellsize;
            gameLogic = new GameLogic(width, height);

            soundManager = new SoundManager();

            gameTimer = new Timer();
            switch (difficulty)
            {
                case 1:
                    gameTimer.Interval = 260;
                    label1.Text = "Сложность: Легкая";
                    break;
                case 2:
                    gameTimer.Interval = 125;
                    label1.Text = "Сложность: Средняя";
                    break;
                case 3:
                    gameTimer.Interval = 50;
                    label1.Text = "Сложность: Сложная";
                    break;
            }
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            isPaused = false;
            this.KeyDown += GameForm_KeyDown;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Paint += PictureBox1_Paint;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (gameTimer != null)
            {
                gameTimer.Stop();
                gameTimer.Dispose();
            }
            base.OnFormClosing(e);
        }

        private void MaxScoreGame()
        {
            int maxscore = Properties.Settings.Default.MaxScore;
            label4.Text = $"Рекорд: {maxscore}";
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            int previousScore = gameLogic.Score;
            bool isAlive = gameLogic.UpdateGame();
            label2.Text = $"Счет: {gameLogic.Score}";

            if (!isAlive)
            {
                gameTimer.Stop();

                soundManager.PlayGameOverSound();

                int currentscore = gameLogic.Score;

                if (currentscore > Properties.Settings.Default.MaxScore)
                {
                    Properties.Settings.Default.MaxScore = currentscore;
                    Properties.Settings.Default.Save();
                }

                MessageBox.Show($"Game Over! Your score: {gameLogic.Score}");
                this.Close();
            }
            else if (gameLogic.Score > previousScore)
            {
                soundManager.PlayEatSound();
            }

            pictureBox1.Invalidate();
            MaxScoreGame();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                TogglePause();
            }
            else if (!isPaused)
            {
                string newDirection = gameLogic.Snake.Movement.Direction;

                if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) newDirection = "Up";
                if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) newDirection = "Down";
                if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) newDirection = "Left";
                if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) newDirection = "Right";

                gameLogic.Snake.Movement.SetDirection(newDirection);
            }
        }

        private void TogglePause()
        {
            if (isPaused)
            {
                gameTimer.Start();
                UpdateLabelForDifficulty();
                isPaused = false;
            }
            else
            {
                gameTimer.Stop();
                label1.Text = "Игра на паузе";
                PauseForm pauseForm = new PauseForm();
                pauseForm.StartPosition = FormStartPosition.CenterParent;
                if (pauseForm.ShowDialog() == DialogResult.OK)
                {
                    isPaused = false;
                    gameTimer.Start();
                    UpdateLabelForDifficulty();
                }
                else
                {
                    isPaused = true;
                }
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (Point segment in gameLogic.Snake.Body)
            {
                g.FillRectangle(Brushes.Blue, segment.X * cellsize, segment.Y * cellsize, cellsize, cellsize);
            }

            g.FillRectangle(Brushes.Red, gameLogic.Food.Position.X * cellsize, gameLogic.Food.Position.Y * cellsize, cellsize, cellsize);
        }

        private void UpdateLabelForDifficulty()
        {
            if (!isPaused)
            {
                switch (difficulty)
                {
                    case 1:
                        label1.Text = "Сложность: Легкая";
                        break;
                    case 2:
                        label1.Text = "Сложность: Средняя";
                        break;
                    case 3:
                        label1.Text = "Сложность: Сложная";
                        break;
                }
            }
        }
    }
}
