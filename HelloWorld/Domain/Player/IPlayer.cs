using System.Threading;
using System.Threading.Tasks;

namespace HelloWorld.Players;

public interface IPlayer
{
    char Symbol { get; }
    Task<(int row, int col)> PlayAsync(Board board, CancellationToken ct = default);
}