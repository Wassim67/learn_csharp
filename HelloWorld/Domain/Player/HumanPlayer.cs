using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelloWorld.Players;

public class HumanPlayer : IPlayer
{
    public char Symbol { get; }

    public HumanPlayer(char symbol) => Symbol = symbol;

    public Task<(int row, int col)> PlayAsync(Board board, CancellationToken ct = default)
    {
        while (true)
        {
            int row = LireNombre("Ligne (1-3) : ") - 1;
            int col = LireNombre("Colonne (1-3) : ") - 1;

            if (board.IsEmpty(row, col))
                return Task.FromResult((row, col));

            Console.WriteLine("Case déjà prise !");
        }
    }

    private int LireNombre(string msg)
    {
        while (true)
        {
            Console.Write(msg);
            if (int.TryParse(Console.ReadLine(), out int v) && v is >= 1 and <= 3)
                return v;

            Console.WriteLine("Erreur : saisir un nombre entre 1 et 3.");
        }
    }
}