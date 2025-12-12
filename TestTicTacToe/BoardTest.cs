using HelloWorld;

namespace TestTicTacToe;

public class BoardTest
{
    [Fact]
    public void JouerUnCoup()
    {
        // Arrange
        var board = new Board();

        // Act
        board.Play(0,0, 'X');
        
        // Assert
        Assert.False(board.IsEmpty(0, 0));
    }

    [Fact]
    public void VictoireSurUneLigne()
    {
        // Arrange
        var board = new Board();
        board.Play(0, 0, 'X');
        board.Play(0, 1, 'X');
        board.Play(0, 2, 'X');

        // Act
        bool result = board.HasWinner('X');

        // Assert
        Assert.True(result);
    }


    [Fact]
    public void VictoireSurColonne()
    {
        // Arrange
        var board = new Board();
        board.Play(0, 1, 'O');
        board.Play(1, 1, 'O');
        board.Play(2, 1, 'O');

        // Act
        bool result = board.HasWinner('O');
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void VictoirePremiereDiag()
    {
        var board = new Board();
        board.Play(0, 0, 'X');
        board.Play(1, 1, 'X');
        board.Play(2, 2, 'X');

        Assert.True(board.HasWinner('X'));
    }
    
    [Fact]
    public void PartieTermine()
    {
        var board = new Board();
        
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                board.Play(row, col, 'X');
            }
        }
        Assert.True(board.IsFull());
    }

    
    


}