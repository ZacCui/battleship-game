using System;
using System.Collections.Generic;
using System.Linq;

namespace battleship_dotnet
{
    class Player
    {
        private static int idNumberSeed = 0;
        public string id { get; }
        public string name { get; }
        private Board board;
        public List<BattleShip> allBattleShips = new List<BattleShip>();
        public List<BattleShip> sunkedShips = new List<BattleShip>();
        public Player(string name)
        {
            this.name = name;
            this.id = idNumberSeed.ToString();
            this.board = new Board(10, 10);
            ++idNumberSeed;
        }

        public Boolean isVaildInBoardRange(List<int> xs, List<int> ys)
        {
            foreach (var x in xs)
            {
                if (!Enumerable.Range(0, this.board.height).Contains(x)) return false;
            }

            foreach (var y in ys)
            {
                if (!Enumerable.Range(0, this.board.width).Contains(y)) return false;
            }
            return true;
        }

        public Boolean isShipOverlapped(int x1, int y1, int x2, int y2)
        {
            return this.board.isOverlapped(x1, y1, x2, y2);
        }

        public void addToSunklist(BattleShip bs)
        {
            sunkedShips.Add(bs);
        }
        public void addBattleShips(int x1, int y1, int x2, int y2)
        {
            BattleShip bs = new BattleShip(x1, y1, x2, y2);
            allBattleShips.Add(bs);
            this.board.drawShip(x1, y1, x2, y2);
        }

        public void markAttackedOnBoard(int x, int y)
        {
            this.board.markAsAttacked(x, y);
        }

        public void showBoard()
        {
            Console.WriteLine($"Current Board of Player: {this.name}");
            this.board.showBoard();
        }

        public int getBoardWidth()
        {
            return board.width;
        }

        public int getBoardHeight()
        {
            return board.height;
        }

        public Boolean lost()
        {
            return allBattleShips.Count > 0 && sunkedShips.Count == allBattleShips.Count;
        }
    }
};