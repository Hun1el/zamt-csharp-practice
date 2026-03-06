using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Snake
{
    public class GameLogic
    {
        public Snake Snake { get; private set; }
        public Food Food { get; private set; }
        public HashSet<Point> Rocks { get; private set; } = new HashSet<Point>();
        public int Score { get; private set; }

        private int width;
        private int height;

        public GameLogic(int width, int height, int numberOfRocks)
        {
            this.width = width;
            this.height = height;
            Snake = new Snake(5, 5, width, height);
            Food = new Food(width, height, Rocks.ToList());
            Score = 0;
            GenerateRocks(numberOfRocks);
        }

        private void GenerateRocks(int numberOfRocks)
        {
            Random random = new Random();
            Rocks.Clear();

            for (int i = 0; i < numberOfRocks; i++)
            {
                Point rock;
                do
                {
                    rock = new Point(random.Next(0, width), random.Next(0, height));
                }
                while (Rocks.Contains(rock) || rock.Equals(Food.Position));

                Rocks.Add(rock);
            }
        }

        public bool UpdateGame()
        {
            bool grow = Snake.Body[0] == Food.Position;
            Snake.Move(grow);

            if (grow)
            {
                Score++;
                Food.NewGenerate(width, height, Rocks.ToList());
            }

            if (Snake.CheckSelfCollision() || Snake.CheckWallCollision() || Rocks.Contains(Snake.Body[0]))
            {
                return false;
            }

            return true;
        }

        public void ResetGame(int numberOfRocks)
        {
            Score = 0;
            Snake = new Snake(width / 2, height / 2, width, height);
            GenerateRocks(numberOfRocks);
            Food.NewGenerate(width, height, Rocks.ToList());
        }
    }

}
