namespace HelloWorld.Players;

public interface IPlayer
{
    char Symbol { get; }
    (int row, int col) Play(Board board);
}