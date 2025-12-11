using System;

namespace HelloWorld;

public class Game : IGame
{
    private readonly Board _board = new();
    private char _currentPlayer = 'O';

    public void Lancer()
    {
        while (true)
        {
            AfficherEcran();

            var (row, col) = LireCoupJoueur();

            _board.Play(row, col, _currentPlayer);

            if (_board.HasWinner(_currentPlayer))
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

    private void AfficherEcran()
    {
        Console.Clear();
        Console.WriteLine("Tic Tac Toe | WASSIM");
        Console.WriteLine();
        _board.Print();
        Console.WriteLine();
        Console.WriteLine($"Au tour du joueur {_currentPlayer}");
    }

    private (int row, int col) LireCoupJoueur()
    {
        while (true)
        {
            int row = LireNombreEntre1Et3("Ligne (1-3) : ") - 1;
            int col = LireNombreEntre1Et3("Colonne (1-3) : ") - 1;

            if (_board.IsEmpty(row, col))
                return (row, col);

            Console.WriteLine("Case déjà prise !");
        }
    }

    private int LireNombreEntre1Et3(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value) && value is >= 1 and <= 3)
                return value;

            Console.WriteLine("Erreur : saisir un nombre entre 1 et 3.");
        }
    }

    private void ChangerJoueur() =>
        _currentPlayer = _currentPlayer == 'O' ? 'X' : 'O';

    private void AfficherVictoire()
    {
        Console.Clear();
        _board.Print();
        Console.WriteLine();
        Console.WriteLine($"{_currentPlayer} a gagné !");
    }

    private void AfficherMatchNul()
    {
        Console.Clear();
        _board.Print();
        Console.WriteLine();
        Console.WriteLine("Match nul !");
    }
}
