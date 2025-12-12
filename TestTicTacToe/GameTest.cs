using HelloWorld;

namespace TestTicTacToe;

public class GameTest
{
    [Fact]
    public void JoueurOGagneSurUneLigne()
    {
        var board = new Board();
        var playerO = new FakePlayer('O', new[] { (0, 0), (0, 1), (0, 2) });
        var playerX = new FakePlayer('X', new[] { (1, 0), (1, 1) });

        var game = new Game(board, playerO, playerX, display: false);

        game.Lancer();

        Assert.True(board.HasWinner('O'));
        Assert.False(board.HasWinner('X'));
    }
    
    [Fact]
    public void TestPartiMatchNul()
    {
        var board = new Board();

        var playerO = new FakePlayer('O', new[] { (0,0), (0,2), (1,0), (2,1), (2,2) });
        var playerX = new FakePlayer('X', new[] { (0,1), (1,1), (1,2), (2,0) });

        var game = new Game(board, playerO, playerX, display: false);

        game.Lancer();

        Assert.False(board.HasWinner('O'));
        Assert.False(board.HasWinner('X'));
        Assert.True(board.IsFull());
    }
    
    [Fact]
    public void CoupSurCaseDejaPris()
    {
        var board = new Board();

        var playerO = new FakePlayer('O', new[] { (0,0), (0,1), (0,2) });
        var playerX = new FakePlayer('X', new[] { (0,0), (1,1), (2,2) });

        var game = new Game(board, playerO, playerX, display: false);

        game.Lancer();

        Assert.True(board.HasWinner('O'));
    }
    
    
    [Fact]
    public void TestCoupHorsGrille()
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
        
        game.Lancer();
        
        Assert.True(board.HasWinner('O'));
        Assert.False(board.HasWinner('X'));

        Assert.False(board.IsEmpty(1, 0));
    }

}