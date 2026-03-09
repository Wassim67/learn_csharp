using System;

namespace HelloWorld.Infrastructure.Persistence;

public class GameSessionEntity
{
    public long Id { get; set; }
    public bool ModeBot { get; set; }
    public string BoardState { get; set; } = ".........";
    public char CurrentPlayerSymbol { get; set; } = 'O';
    public char? WinnerSymbol { get; set; }
    public bool IsFinished { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}
