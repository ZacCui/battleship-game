using System;
using System.Collections.Generic;
using System.Linq;

namespace battleship_dotnet
{
    class Game
    {
        private Boolean running = false;
        private int currentPlayer = 0;
        private List<string> playWhoLost = new List<string>();
        private List<string> playOrder = new List<string>();
        private Boolean showBoard = false;
        private Boolean started = false;
        public Dictionary<string, Player> players = new Dictionary<string, Player>();


        // start the game
        public void start()
        {
            Console.WriteLine("Game stated! Please give instructions!");
            string input = Console.ReadLine();
            this.running = true;
            while (this.running)
            {
                if (input == null)
                {
                    input = Console.ReadLine();
                    continue;
                }
                var cmd = input.Split(' ');
                switch (cmd[0])
                {
                    case "Player":
                        if (!this.started) this.addPlayer(cmd[1]);
                        break;
                    case "Ship":
                        if (!this.started)
                        {
                            int x1 = Int32.Parse(cmd[2]);
                            int y1 = Int32.Parse(cmd[3]);
                            int x2 = Int32.Parse(cmd[4]);
                            int y2 = Int32.Parse(cmd[5]);
                            this.addBattleShip(cmd[1], x1, y1, x2, y2);
                        }
                        break;
                    case "Start":
                        if (playOrder.Count < 2)
                        {
                            Console.WriteLine($"Please add more players first");
                        }
                        else
                        {
                            this.started = true;
                        }
                        break;
                    case "Attack":
                        if (this.started)
                        {
                            if (this.showBoard) players[playOrder[currentPlayer]].showBoard();
                            int x = Int32.Parse(cmd[2]);
                            int y = Int32.Parse(cmd[3]);
                            this.attack(cmd[1], x, y);
                            this.next();
                        }
                        break;
                    case "End":
                        this.end();
                        break;
                    default:
                        Console.WriteLine("Please enter correct instructions");
                        break;
                }
                if (this.started && this.running)
                {
                    Console.WriteLine($"Current Player: {playOrder[currentPlayer]}");
                }
                input = Console.ReadLine();
            }
        }

        public Boolean showBoardSwitcher()
        {
            return this.showBoard = this.showBoard ? false : true;
        }

        // end the game
        public void end()
        {
            running = false;
            currentPlayer = 0;
            playOrder.Clear();
            playWhoLost.Clear();
            players.Clear();
        }

        // take the next round
        public void next()
        {
            if (playWhoLost.Count != 0 && players.Count - playWhoLost.Count == 1)
            {
                Console.WriteLine($"{playOrder[currentPlayer]} is the winner!!!");
                this.end();
                return;
            }
            currentPlayer = (currentPlayer + 1) % playOrder.Count;
            while (this.lost(playOrder[currentPlayer]))
            {
                currentPlayer = (currentPlayer + 1) % playOrder.Count;
            }
        }

        // tell whether the player is lost or not
        public Boolean lost(string player)
        {
            return players[player].lost();
        }

        public void addPlayer(string name)
        {
            if (players.ContainsKey(name))
            {
                Console.WriteLine($"User name: ({name}) already existed.");
                return;
            }
            Player player = new Player(name);
            players.Add(player.name, player);
            playOrder.Add(player.name);
        }

        private Boolean isVaildBattleShip(string player, int x1, int y1, int x2, int y2)
        {
            // The value of bottom right point cannot larger than the top left point
            if (x1 > x2 || y1 > y2)
            {
                Console.WriteLine($"Invaild input point! The value of bottom right point cannot larger than the top left point");
                return false;
            }
            // Check the range of the points
            if (!players[player].isVaildInBoardRange(new List<int>() { x1, x2 }, new List<int>() { y1, y2 }))
            {
                Console.WriteLine($"Invaild input point! Invaild range.");
                return false;
            }
            // check overlapped points
            if (players[player].isShipOverlapped(x1, y1, x2, y2))
            {
                Console.WriteLine($"Invaild input point! Overlapped with Other ships");
                return false;
            }

            return true;
        }

        // Take the top-left and bottom-right points to create a new battleShip 
        public void addBattleShip(string player, int x1, int y1, int x2, int y2)
        {
            if (!isVaildBattleShip(player, x1, y1, x2, y2)) return;
            players[player].addBattleShips(x1, y1, x2, y2);
        }

        public void attack(string player, int x, int y)
        {

            if (!players.ContainsKey(player))
            {
                Console.WriteLine($"Player {player} not existed, please enter a correct player name");
                return;
            }
            if (player == playOrder[currentPlayer])
            {
                Console.WriteLine($"Uable to attack yourself");
                return;
            }
            foreach (var ship in players[player].allBattleShips)
            {
                if (ship.isAttacked(x, y))
                {
                    Console.WriteLine($"Attack to {player} at ({x},{y}) hit");
                    if (ship.isSunk())
                    {
                        players[player].addToSunklist(ship);
                        if (lost(player))
                        {
                            playWhoLost.Add(player);
                            Console.WriteLine($"Player: {player} has lost");
                        }
                    }
                    players[player].markAttackedOnBoard(x, y);
                    return;
                }
            }
            Console.WriteLine($"Attack to {player} at ({x},{y}) missed");
        }
    }
}
