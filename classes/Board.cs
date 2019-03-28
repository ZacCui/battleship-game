using System;
using System.Collections.Generic;
using System.Linq;

namespace battleship_dotnet
{

    class Board
    {
        private const int emptyPoint = 0;
        private const int shipPoint = 1;
        private const int attackedPoint = 2;
        public int height { get; }
        public int width { get; }
        private int[,] gameBoard;


        public Board(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.gameBoard = new int[height, width];
        }

        public Boolean isVaildInBoardRange(List<int> xs, List<int> ys)
        {
            foreach (var x in xs)
            {
                if (!Enumerable.Range(0, this.height).Contains(x)) return false;
            }

            foreach (var y in ys)
            {
                if (!Enumerable.Range(0, this.width).Contains(y)) return false;
            }
            return true;
        }

        public Boolean isOverlapped(int x1, int y1, int x2, int y2)
        {
            for (int i = y1; i <= y2; ++i)
            {
                for (int j = x1; j <= x2; ++j)
                {
                    if (gameBoard[i, j] == shipPoint) return true;
                }
            }
            return false;
        }

        public void showBoard()
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    Console.Write($"{gameBoard[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        public void drawShip(int x1, int y1, int x2, int y2)
        {
            for (int i = y1; i <= y2; ++i)
            {
                for (int j = x1; j <= x2; ++j)
                {
                    gameBoard[i, j] = shipPoint;
                }
            }
        }
        public void markAsAttacked(int x, int y)
        {
            gameBoard[y, x] = attackedPoint;
        }
    }
}



