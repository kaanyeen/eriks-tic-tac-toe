
using System;

namespace TicTacToe
{
    public enum GameState
    {
        InProgress,
        Player1Win,
        Player2Win,
        Draw
    }

    public class Player
    {
        public string Name { get; }
        public char Symbol { get; }
        public ConsoleColor Color { get; }

        public Player(string name, char symbol, ConsoleColor color)
        {
            Name = name;
            Symbol = symbol;
            Color = color;
        }
    }

    public class Board
    {
        private readonly char[] _fields = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private readonly Player _player1;
        private readonly Player _player2;

        private static readonly int[][] WinConditions =
        {
            // Rows
            new[] { 0, 1, 2 }, new[] { 3, 4, 5 }, new[] { 6, 7, 8 },
            // Columns
            new[] { 0, 3, 6 }, new[] { 1, 4, 7 }, new[] { 2, 5, 8 },
            // Diagonals
            new[] { 0, 4, 8 }, new[] { 2, 4, 6 }
        };

        public Board(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        public void Display()
        {
            Console.WriteLine("-------------");
            for (int i = 0; i < 9; i += 3)
            {
                Console.Write("| ");
                PrintFieldSymbol(i);
                Console.Write(" | ");
                PrintFieldSymbol(i + 1);
                Console.Write(" | ");
                PrintFieldSymbol(i + 2);
                Console.WriteLine(" |");
                Console.WriteLine("-------------");
            }
        }

        private void PrintFieldSymbol(int index)
        {
            char field = _fields[index];
            if (field == _player1.Symbol)
            {
                Console.ForegroundColor = _player1.Color;
                Console.Write(field);
                Console.ResetColor();
            }
            else if (field == _player2.Symbol)
            {
                Console.ForegroundColor = _player2.Color;
                Console.Write(field);
                Console.ResetColor();
            }
            else
            {
                Console.Write(field);
            }
        }

        public bool IsFieldTaken(int position)
        {
            return _fields[position - 1] == 'X' || _fields[position - 1] == 'O';
        }

        public void MakeMove(int position, char symbol)
        {
            _fields[position - 1] = symbol;
        }

        public GameState CheckGameState(int turnCount)
        {
            foreach (var condition in WinConditions)
            {
                char firstSymbol = _fields[condition[0]];
                if (_fields[condition[1]] == firstSymbol && _fields[condition[2]] == firstSymbol)
                {
                    if (firstSymbol == _player1.Symbol) return GameState.Player1Win;
                    if (firstSymbol == _player2.Symbol) return GameState.Player2Win;
                }
            }

            if (turnCount == 9)
            {
                return GameState.Draw;
            }

            return GameState.InProgress;
        }
    }

    public class Game
    {
        private readonly Board _board;
        private readonly Player _player1;
        private readonly Player _player2;
        private Player _currentPlayer;
        private GameState _gameState;
        private int _turnCount;

        public Game()
        {
            _player1 = new Player("Гравець 1", 'X', ConsoleColor.Green);
            _player2 = new Player("Гравець 2", 'O', ConsoleColor.Magenta);
            _board = new Board(_player1, _player2);
            _currentPlayer = _player1;
            _gameState = GameState.InProgress;
            _turnCount = 0;
        }

        public void Start()
        {
            while (_gameState == GameState.InProgress)
            {
                Console.Clear();
                _board.Display();
                ProcessPlayerTurn();
                _gameState = _board.CheckGameState(_turnCount);

                if (_gameState == GameState.InProgress)
                {
                    SwitchPlayer();
                }
            }
            DisplayEndGameMessage();
        }

        private void ProcessPlayerTurn()
        {
            Console.ForegroundColor = _currentPlayer.Color;
            Console.WriteLine($"\nХід: {_currentPlayer.Name} ({_currentPlayer.Symbol})");
            Console.ResetColor();

            int position;
            while (true)
            {
                Console.Write("Оберіть поле (1-9): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out position) && position >= 1 && position <= 9)
                {
                    if (!_board.IsFieldTaken(position))
                    {
                        _board.MakeMove(position, _currentPlayer.Symbol);
                        _turnCount++;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Невірний хід. Поле вже зайняте. Спробуйте ще раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Невірний ввід. Будь ласка, введіть число від 1 до 9.");
                }
            }
        }

        private void SwitchPlayer()
        {
            _currentPlayer = (_currentPlayer == _player1) ? _player2 : _player1;
        }

        private void DisplayEndGameMessage()
        {
            Console.Clear();
            _board.Display();
            Console.WriteLine("\nГра закінчена!");

            switch (_gameState)
            {
                case GameState.Player1Win:
                    Console.ForegroundColor = _player1.Color;
                    Console.WriteLine($"{_player1.Name} ({_player1.Symbol}) переміг!");
                    Console.ResetColor();
                    break;
                case GameState.Player2Win:
                    Console.ForegroundColor = _player2.Color;
                    Console.WriteLine($"{_player2.Name} ({_player2.Symbol}) переміг!");
                    Console.ResetColor();
                    break;
                case GameState.Draw:
                    Console.WriteLine("Нічия!");
                    break;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.Start();
        }
    }
}