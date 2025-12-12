using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelloWorld.Players;

public class BotPlayer : IPlayer
{
    public char Symbol { get; }
    private readonly Random _random = new();

    public BotPlayer(char symbol) => Symbol = symbol;

    public async Task<(int row, int col)> PlayAsync(Board board, CancellationToken ct = default)
    {
        Console.WriteLine("Le robot réfléchit...");
        await Task.Delay(800, ct); 

        while (true)
        {
            int row = _random.Next(0, 3);
            int col = _random.Next(0, 3);

            if (board.IsEmpty(row, col))
                return (row, col);
        }
    }
}