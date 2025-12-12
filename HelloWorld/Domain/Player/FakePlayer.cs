using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

    public Task<(int row, int col)> PlayAsync(Board board, CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();
        return Task.FromResult(_moves.Dequeue());
    }
}