using HelloWorld;

namespace TestTicTacToe;

public class GameTest
{
    [Fact]
    public async Task JoueurOGagneSurUneLigne()
    {
        var board = new Board();
        var playerO = new FakePlayer('O', new[] { (0, 0), (0, 1), (0, 2) });
        var playerX = new FakePlayer('X', new[] { (1, 0), (1, 1) });

        var game = new Game(board, playerO, playerX, display: false);

        await game.Lancer();

        Assert.True(board.HasWinner('O'));
        Assert.False(board.HasWinner('X'));
    }
    
    [Fact]
    public async Task TestPartiMatchNul()
    {
        var board = new Board();

        var playerO = new FakePlayer('O', new[] { (0,0), (0,2), (1,0), (2,1), (2,2) });
        var playerX = new FakePlayer('X', new[] { (0,1), (1,1), (1,2), (2,0) });

        var game = new Game(board, playerO, playerX, display: false);

        await game.Lancer();

        Assert.False(board.HasWinner('O'));
        Assert.False(board.HasWinner('X'));
        Assert.True(board.IsFull());
    }
    
    [Fact]
    public async Task CoupSurCaseDejaPris()
    {
        var board = new Board();

        var playerO = new FakePlayer('O', new[] { (0,0), (0,1), (0,2) });
        var playerX = new FakePlayer('X', new[] { (0,0), (1,1), (2,2) });

        var game = new Game(board, playerO, playerX, display: false);

        await game.Lancer();

        Assert.True(board.HasWinner('O'));
    }
    
    
    [Fact]
    public async Task TestCoupHorsGrille()
    {
        var board = new Board();
        var playerO = new FakePlayer('O', new[] { (0, 0), (0, 1), (0, 2) });
        var playerX = new FakePlayer('X', new[]
        {
            (-1, 0),
            (1, 0),  
            (1, 1)   
        });

        var game = new Game(board, playerO, playerX, display: false);
        
        await game.Lancer();
        
        Assert.True(board.HasWinner('O'));
        Assert.False(board.HasWinner('X'));

        Assert.False(board.IsEmpty(1, 0));
    }

}
