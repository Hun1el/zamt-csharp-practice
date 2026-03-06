using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class GameLogic
    {
        public Snake Snake { get; private set; }
        public Food Food { get; private set; }
        public int Score { get; private set; }

        private int width;
        private int height;

        public GameLogic(int width, int height)
        {
            this.width = width;
            this.height = height;
            Snake = new Snake(5, 5, width, height);
            Food = new Food(width, height);
            Score = 0;
        }

        public bool UpdateGame()
        {
            bool grow = Snake.Body[0] == Food.Position;
            Snake.Move(grow);

            if (grow)
            {
                Score++;
                Food.NewGenerate(width, height, Snake.Body);
            }

            if (Snake.CheckSelfCollision() || Snake.CheckWallCollision())
            {
                return false;
            }

            return true;
        }
    }
}
