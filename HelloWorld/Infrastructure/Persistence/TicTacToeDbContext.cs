using Microsoft.EntityFrameworkCore;

namespace HelloWorld.Infrastructure.Persistence;

public class TicTacToeDbContext : DbContext
{
    public TicTacToeDbContext(DbContextOptions<TicTacToeDbContext> options) : base(options)
    {
    }

    public DbSet<GameSessionEntity> GameSessions => Set<GameSessionEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameSessionEntity>(entity =>
        {
            entity.ToTable("game_sessions");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.BoardState)
                .HasMaxLength(9)
                .IsRequired();

            entity.Property(e => e.CurrentPlayerSymbol)
                .HasConversion<string>()
                .HasMaxLength(1)
                .IsRequired();

            entity.Property(e => e.WinnerSymbol)
                .HasConversion(
                    value => value.HasValue ? value.Value.ToString() : null,
                    value => string.IsNullOrEmpty(value) ? null : value[0])
                .HasMaxLength(1);
        });
    }
}
