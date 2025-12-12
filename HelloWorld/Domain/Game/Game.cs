using System;
using HelloWorld.Players;

namespace HelloWorld;

public class Game : IGame
{
    private readonly Board _board;

    private readonly IPlayer _playerO;
    private readonly IPlayer _playerX;
    private readonly bool _display;

    private IPlayer _currentPlayer;
    public Game(bool modeBot) : this(new Board(), new HumanPlayer('O'), modeBot ? new BotPlayer('X') : new HumanPlayer('X'), display: true)
    {
        
    }

    
    public Game(Board board, IPlayer playerO, IPlayer playerX, bool display = true)
    {
        _board = board;
        _playerO = playerO;
        _playerX = playerX;
        _currentPlayer = _playerO;
        _display = display;
    }
    
    private static bool DansLaGrille(int ligne, int colonne)
    {
        bool ligneOk = ligne >= 0 && ligne < 3;
        bool colonneOk = colonne >= 0 && colonne < 3;

        return ligneOk && colonneOk;
    }


    public void Lancer()
    {
        while (true)
        {
            AfficherEcran();

            var (row, col) = _currentPlayer.Play(_board);
            
            while (!DansLaGrille(row, col) || !_board.IsEmpty(row, col))
            {
                (row, col) = _currentPlayer.Play(_board);
            }

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
        if (!_display) return;
        Console.Clear();
        Console.WriteLine("Tic Tac Toe | WASSIM");
        Console.WriteLine();
        _board.Print();
        Console.WriteLine();
        Console.WriteLine($"Au tour du joueur {_currentPlayer.Symbol}");
    }

    private void AfficherVictoire()
    {
        if (!_display) return;
        
        Console.Clear();
        _board.Print();
        Console.WriteLine();
        Console.WriteLine($"{_currentPlayer.Symbol} a gagné !");
    }

    private void AfficherMatchNul()
    {
        if (!_display) return;
        
        Console.Clear();
        _board.Print();
        Console.WriteLine();
        Console.WriteLine("Match nul !");
    }
}