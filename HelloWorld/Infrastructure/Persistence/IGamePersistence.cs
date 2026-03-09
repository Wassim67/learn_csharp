using System.Threading;
using System.Threading.Tasks;

namespace HelloWorld.Infrastructure.Persistence;

public interface IGamePersistence
{
    Task<long> CreateGameAsync(bool modeBot, string boardState, char currentPlayerSymbol, CancellationToken ct = default);
    Task UpdateGameStateAsync(long gameId, string boardState, char currentPlayerSymbol, CancellationToken ct = default);
    Task FinishGameAsync(long gameId, string boardState, char? winnerSymbol, CancellationToken ct = default);
    Task<GameSessionEntity?> GetLastInProgressGameAsync(bool modeBot, CancellationToken ct = default);
    Task<GameStatistics> GetStatisticsAsync(CancellationToken ct = default);
}
