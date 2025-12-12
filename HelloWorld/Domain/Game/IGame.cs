namespace HelloWorld;

public interface IGame
{
    Task Lancer(CancellationToken ct = default);
}