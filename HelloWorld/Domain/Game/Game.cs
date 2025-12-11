using System;
using HelloWorld.Players;

namespace HelloWorld;

public class Game : IGame
{
    private readonly Board _board = new();

    private readonly IPlayer _playerO;
    private readonly IPlayer _playerX;

    private IPlayer _currentPlayer;

    public Game(bool modeBot)
    {
        _playerO = new HumanPlayer('O');
        _playerX = modeBot ? new BotPlayer('X') : new HumanPlayer('X');
        
        _currentPlayer = _playerO;
    }

    public void Lancer()
    {
        while (true)
        {
            AfficherEcran();

            var (row, col) = _currentPlayer.Play(_board);

            _board.Play(row, col, _currentPlayer.Symbol);

            if (_board.HasWinner(_currentPlayer.Symbol))
            {
                AfficherVictoire();
                return;
            }

            if (_board.IsFull())
            {
                AfficherMatchNul();
                return;
            }

            ChangerJoueur();
        }
    }

    private void ChangerJoueur()
    {
        _currentPlayer = _currentPlayer == _playerO ? _playerX : _playerO;
    }
    
    private void AfficherEcran()
    {
        Console.Clear();
        Console.WriteLine("Tic Tac Toe | WASSIM");
        Console.WriteLine();
        _board.Print();
        Console.WriteLine();
        Console.WriteLine($"Au tour du joueur {_currentPlayer.Symbol}");
    }

    private void AfficherVictoire()
    {
        Console.Clear();
        _board.Print();
        Console.WriteLine();
        Console.WriteLine($"{_currentPlayer.Symbol} a gagné !");
    }

    private void AfficherMatchNul()
    {
        Console.Clear();
        _board.Print();
        Console.WriteLine();
        Console.WriteLine("Match nul !");
    }
}