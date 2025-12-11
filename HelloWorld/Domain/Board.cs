using System;
using System.Linq;

namespace HelloWorld;

public class Board
{
    private const int TAILLE = 3;
    private readonly char[][] _cells;

    public Board()
    {
        _cells = Enumerable.Range(0, TAILLE)
            .Select(_ => Enumerable.Repeat('.', TAILLE).ToArray())
            .ToArray();
    }

    public void Print()
    {
        foreach (var row in _cells)
            Console.WriteLine(string.Join(" ", row));
    }

    public bool IsEmpty(int row, int col) => _cells[row][col] == '.';

    public void Play(int row, int col, char player) => _cells[row][col] = player;

    public bool HasWinner(char player)
    {
        // lignes
        if (_cells.Any(row => row.All(c => c == player)))
            return true;

        // colonnes
        for (int col = 0; col < TAILLE; col++)
        {
            if (Enumerable.Range(0, TAILLE).All(row => _cells[row][col] == player))
                return true;
        }

        // diagonale 1
        if (Enumerable.Range(0, TAILLE).All(i => _cells[i][i] == player))
            return true;

        // diagonale 2
        if (Enumerable.Range(0, TAILLE).All(i => _cells[i][TAILLE - 1 - i] == player))
            return true;

        return false;
    }

    public bool IsFull() => _cells.All(row => row.All(c => c != '.'));
}