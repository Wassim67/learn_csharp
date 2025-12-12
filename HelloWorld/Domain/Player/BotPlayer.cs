using System;

namespace HelloWorld.Players;

public class BotPlayer : IPlayer
{
    public char Symbol { get; }

    private readonly Random _random = new Random();

    public BotPlayer(char symbol)
    {
        Symbol = symbol;
    }

    public (int row, int col) Play(Board board)
    {
        Console.WriteLine("Le robot réfléchit...");

        while (true)
        {
            int row = _random.Next(0, 3);
            int col = _random.Next(0, 3);

            if (board.IsEmpty(row, col))
                return (row, col);
        }
    }
}