using System;
using System.Collections.Generic;
using System.Linq;

namespace battleship_dotnet
{
    class BattleShip
    {
        // Top left point
        public int x1 { get; }
        public int y1 { get; }
        // Bottom right point
        public int x2 { get; }
        public int y2 { get; }
        public int length { get; }

        public HashSet<string> hitPoints = new HashSet<string>();
        public BattleShip(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            this.length = (Math.Abs(x1 - x2) + 1) * (Math.Abs(y1 - y2) + 1);
        }

        public Boolean isAttacked(int x, int y)
        {
            if (x1 <= x && x2 >= x && y1 <= y && y2 >= y)
            {
                this.markAttack($"({x},{y})");
                return true;
            }
            return false;
        }

        private void markAttack(string point)
        {
            hitPoints.Add(point);
        }

        public Boolean isSunk()
        {
            return length == hitPoints.Count;
        }
    }
};