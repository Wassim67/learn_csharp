using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld.Infrastructure.Persistence;

public class GamePersistence : IGamePersistence
{
    private readonly TicTacToeDbContext _dbContext;

    public GamePersistence(TicTacToeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<long> CreateGameAsync(
        bool modeBot,
        string boardState,
        char currentPlayerSymbol,
        CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        var game = new GameSessionEntity
        {
            ModeBot = modeBot,
            BoardState = boardState,
            CurrentPlayerSymbol = currentPlayerSymbol,
            IsFinished = false,
            CreatedAtUtc = now,
            UpdatedAtUtc = now
        };

        _dbContext.GameSessions.Add(game);
        await _dbContext.SaveChangesAsync(ct);
        return game.Id;
    }

    public async Task UpdateGameStateAsync(
        long gameId,
        string boardState,
        char currentPlayerSymbol,
        CancellationToken ct = default)
    {
        var game = await _dbContext.GameSessions.FirstOrDefaultAsync(g => g.Id == gameId, ct);
        if (game is null || game.IsFinished)
        {
            return;
        }

        game.BoardState = boardState;
        game.CurrentPlayerSymbol = currentPlayerSymbol;
        game.UpdatedAtUtc = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task FinishGameAsync(long gameId, string boardState, char? winnerSymbol, CancellationToken ct = default)
    {
        var game = await _dbContext.GameSessions.FirstOrDefaultAsync(g => g.Id == gameId, ct);
        if (game is null)
        {
            return;
        }

        game.BoardState = boardState;
        game.WinnerSymbol = winnerSymbol;
        game.IsFinished = true;
        game.UpdatedAtUtc = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(ct);
    }

    public Task<GameSessionEntity?> GetLastInProgressGameAsync(bool modeBot, CancellationToken ct = default)
    {
        return _dbContext.GameSessions
            .Where(g => g.ModeBot == modeBot && !g.IsFinished)
            .OrderByDescending(g => g.UpdatedAtUtc)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<GameStatistics> GetStatisticsAsync(CancellationToken ct = default)
    {
        var partiesJouees = await _dbContext.GameSessions.CountAsync(g => g.IsFinished, ct);
        var victoiresHumain = await _dbContext.GameSessions.CountAsync(
            g => g.IsFinished && g.ModeBot && g.WinnerSymbol == 'O',
            ct);
        var victoiresBot = await _dbContext.GameSessions.CountAsync(
            g => g.IsFinished && g.ModeBot && g.WinnerSymbol == 'X',
            ct);

        return new GameStatistics(partiesJouees, victoiresHumain, victoiresBot);
    }
}
