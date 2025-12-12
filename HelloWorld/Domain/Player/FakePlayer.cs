using System.Collections.Generic;
using HelloWorld;
using HelloWorld.Players;

namespace TestTicTacToe;

public class FakePlayer : IPlayer
{
    public char Symbol { get; }

    private readonly Queue<(int row, int col)> _moves;

    public FakePlayer(char symbol, IEnumerable<(int row, int col)> moves)
    {
        Symbol = symbol;
        _moves = new Queue<(int row, int col)>(moves);
    }

    public (int row, int col) Play(Board board)
    {
        return _moves.Dequeue();
    }
}