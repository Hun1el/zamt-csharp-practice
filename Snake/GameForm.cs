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
        private int cellsize = 40;

        public GameForm(int difficulty)
        {
            InitializeComponent();
            this.difficulty = difficulty;
            int width = pictureBox1.Width / cellsize;
            int height = pictureBox1.Height / cellsize;
            gameLogic = new GameLogic(width, height);

            gameTimer = new Timer();
            switch (difficulty)
            {
                case 1:
                    gameTimer.Interval = 250;
                    break;
                case 2:
                    gameTimer.Interval = 125;
                    break;
                case 3:
                    gameTimer.Interval = 50;
                    break;
            }
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

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

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            bool isAlive = gameLogic.UpdateGame();

            if (!isAlive)
            {
                gameTimer.Stop();
                MessageBox.Show($"Game Over! Your score: {gameLogic.Score}");
                this.Close();
            }

            pictureBox1.Invalidate();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            string newDirection = gameLogic.Snake.Movement.Direction;

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) newDirection = "Up";
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) newDirection = "Down";
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) newDirection = "Left";
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) newDirection = "Right";

            gameLogic.Snake.Movement.SetDirection(newDirection);
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
    }
}
