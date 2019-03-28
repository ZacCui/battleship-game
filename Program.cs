using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace battleship_dotnet
{

    class Program
    {
        static void Main(string[] args)
        {
            // basic test cases
            // tests();
            var game = new Game();
            // uncomment the code below to show the game boards
            // game.showBoardSwitcher();
            game.start();
        }

        static void tests()
        {
            testAddPlayers();
            testAddShips();
            testAddShipsWith2();
            testAttack();
        }
        static void testAddPlayers()
        {
            Console.WriteLine("testAddPlayers");
            var game = new Game();
            game.addPlayer("a");
            game.addPlayer("a");
            game.addPlayer("b");
            game.addPlayer("c");
            Debug.Assert(game.players.Count == 3);
            int id = 0;
            foreach (KeyValuePair<string, Player> item in game.players)
            {
                Debug.Assert(id++.ToString() == item.Value.id);
            }
            Console.WriteLine("Passed\n");

        }
        static void testAddShips()
        {
            Console.WriteLine("testAddShips");
            var game = new Game();
            game.addPlayer("a");
            game.addBattleShip("a", 0, 0, 0, 0);
            game.addBattleShip("a", 0, 0, 0, 0);
            Debug.Assert(game.players["a"].allBattleShips.Count == 1);
            Console.WriteLine("Passed\n");

        }

        static void testAddShipsWith2()
        {
            Console.WriteLine("testAddShipsWith2");
            var game = new Game();
            game.addPlayer("a");
            game.addPlayer("b");
            game.addBattleShip("a", 0, 0, 0, 0);
            game.addBattleShip("a", 0, 0, 0, 0);
            game.addBattleShip("b", 0, 0, 0, 0);
            game.addBattleShip("b", 0, 0, 1, 1);
            game.addBattleShip("b", 1, 1, 9, 9);
            game.addBattleShip("b", 2, 3, 5, 6);
            Debug.Assert(game.players["a"].allBattleShips.Count == 1);
            Debug.Assert(game.players["b"].allBattleShips.Count == 2);
            Console.WriteLine("Passed\n");
        }

        static void testAttack()
        {
            Console.WriteLine("testAttack");
            var game = new Game();
            game.addPlayer("a");
            game.addPlayer("b");
            game.addBattleShip("a", 0, 0, 0, 0);
            game.addBattleShip("a", 1, 1, 9, 9);
            game.addBattleShip("b", 0, 0, 0, 0);
            game.addBattleShip("b", 1, 1, 1, 1);
            game.attack("b", 0, 0);
            Debug.Assert(game.lost("b") == false);
            Debug.Assert(game.players["b"].sunkedShips.Count == 1);
            game.next();
            game.attack("a", 0, 1);
            Debug.Assert(game.players["a"].sunkedShips.Count == 0);
            game.next();
            game.attack("b", 1, 1);
            Debug.Assert(game.players["b"].sunkedShips.Count == 2);
            Debug.Assert(game.lost("b") == true);
            game.next();
            Console.WriteLine("Passed\n");
        }
    }
}
