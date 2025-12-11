namespace HelloWorld;

public class Board
{
    private const int TAILLE = 3;      // plateau 3x3
    private char[][] _cells;

    public Board()
    {
        _cells = new char[TAILLE][];
        for (int i = 0; i < TAILLE; i++)
        {
            _cells[i] = new char[TAILLE];
            for (int j = 0; j < TAILLE; j++)
            {
                _cells[i][j] = '.';   // le . chez moi c'est le vide
            }
        }
    }

    public void Print()
    {
        for (int i = 0; i < TAILLE; i++)
        {
            for (int j = 0; j < TAILLE; j++)
            {
                Console.Write(_cells[i][j] + " ");
            }
            Console.WriteLine();
        }
    }

    public bool IsEmpty(int row, int col)
    {
        return _cells[row][col] == '.';
    }

    public void Play(int row, int col, char player)
    {
        _cells[row][col] = player;
    }

    public bool HasWinner(char player)
    {
        // vérif si une des lignes contient uniquement le symbole du joueur
        for (int i = 0; i < TAILLE; i++)
        {
            if (_cells[i].All(c => c == player))
            {
                return true;
            }
        }

        // vérif si une des colonnes contient uniquement le symbole du joueur
        for (int j = 0; j < TAILLE; j++)
        {
            char[] colonne = new char[TAILLE];
            for (int i = 0; i < TAILLE; i++)
            {
                colonne[i] = _cells[i][j];
            }

            if (colonne.All(c => c == player))
            {
                return true;
            }
        }

        // premiere diago de victoire (0,0) (1,1) (2,2)
        char[] diag1 = new char[TAILLE];
        for (int i = 0; i < TAILLE; i++)
        {
            diag1[i] = _cells[i][i];
        }
        if (diag1.All(c => c == player))
        {
            return true;
        }

        // deuxieme diago de victoir (0,2) (1,1) (2,0)
        char[] diag2 = new char[TAILLE];
        for (int i = 0; i < TAILLE; i++)
        {
            diag2[i] = _cells[i][TAILLE - 1 - i];
        }
        if (diag2.All(c => c == player))
        {
            return true;
        }

        return false;
    }

    public bool IsFull()
    {
        // fin dela partie
        return _cells.All(row => row.All(c => c != '.'));
    }
}
