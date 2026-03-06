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
            gameLogic = new GameLogic(width, height, GetNumberOfRocksForDifficulty(difficulty));

            soundManager = new SoundManager();

            gameTimer = new Timer();
            switch (difficulty)
            {
                case 1:
                    gameTimer.Interval = 170;
                    label1.Text = "Сложность: Легкая";
                    break;
                case 2:
                    gameTimer.Interval = 90;
                    label1.Text = "Сложность: Средняя";
                    break;
                case 3:
                    gameTimer.Interval = 45;
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

        private int GetNumberOfRocksForDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case 1:
                    return 0;
                case 2:
                    return 7;
                case 3:
                    return 15;
                default:
                    return 0;
            }
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
                ShowGameOverForm(currentscore);
            }
            else if (gameLogic.Score > previousScore)
            {
                soundManager.PlayEatSound();
            }

            pictureBox1.Invalidate();
            MaxScoreGame();
        }

        private void ShowGameOverForm(int Score)
        {
            GameOverForm gameOverForm = new GameOverForm();
            gameOverForm.StartPosition = FormStartPosition.CenterParent;

            var result = gameOverForm.ShowDialog();
            if (result == DialogResult.Retry)
            {
                RestartGame();
            }
            else if (result == DialogResult.Abort)
            {
                Close();
            }
        }

        private void RestartGame()
        {
            gameTimer.Stop();
            gameLogic.ResetGame(GetNumberOfRocksForDifficulty(difficulty));
            label2.Text = $"Счёт: {gameLogic.Score}";
            UpdateLabelForDifficulty();
            pictureBox1.Invalidate();
            gameTimer.Start();
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
            int cellSize = 20;

            Point head = gameLogic.Snake.Body[0];
            Image headImage;
            switch (gameLogic.Snake.Movement.Direction)
            {
                case "Up":
                    headImage = Properties.Resources.head_up;
                    break;
                case "Down":
                    headImage = Properties.Resources.head_down;
                    break;
                case "Left":
                    headImage = Properties.Resources.head_left;
                    break;
                case "Right":
                    headImage = Properties.Resources.head_right;
                    break;
                default:
                    headImage = Properties.Resources.head_up;
                    break;
            }
            g.DrawImage(headImage, head.X * cellSize, head.Y * cellSize, cellSize, cellSize);

            for (int i = 1; i < gameLogic.Snake.Body.Count; i++)
            {
                Point prev = gameLogic.Snake.Body[i - 1];
                Point current = gameLogic.Snake.Body[i];
                Point next = i < gameLogic.Snake.Body.Count - 1 ? gameLogic.Snake.Body[i + 1] : current;

                Image bodyImage;

                if (prev.X == next.X)
                {
                    bodyImage = Properties.Resources.body_vertical;
                }
                else if (prev.Y == next.Y)
                {
                    bodyImage = Properties.Resources.body_horizontal;
                }
                else
                {
                    if ((prev.X < current.X && next.Y < current.Y) || (next.X < current.X && prev.Y < current.Y))
                    {
                        bodyImage = Properties.Resources.body_tl;
                    }
                    else if ((prev.X > current.X && next.Y < current.Y) || (next.X > current.X && prev.Y < current.Y))
                    {
                        bodyImage = Properties.Resources.body_tr;
                    }
                    else if ((prev.X > current.X && next.Y > current.Y) || (next.X > current.X && prev.Y > current.Y))
                    {
                        bodyImage = Properties.Resources.body_br;
                    }
                    else
                    {
                        bodyImage = Properties.Resources.body_bl;
                    }
                }

                g.DrawImage(bodyImage, current.X * cellSize, current.Y * cellSize, cellSize, cellSize);
            }
            g.DrawImage(Properties.Resources.apple, gameLogic.Food.Position.X * cellSize, gameLogic.Food.Position.Y * cellSize, cellSize, cellSize);

            foreach (var rock in gameLogic.Rocks)
            {
                g.DrawImage(Properties.Resources.rocks, rock.X * cellSize, rock.Y * cellSize, cellSize, cellSize);
            }
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

