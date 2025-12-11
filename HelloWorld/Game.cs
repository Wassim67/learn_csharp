namespace HelloWorld;

public class Game
{
    private Board _board = new Board();
    private char _currentPlayer = 'O'; // on commence par O

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Tic Tac Toe | WASSIM");
            Console.WriteLine();
            _board.Print();
            Console.WriteLine();
            Console.WriteLine($"Au tour du joueur {_currentPlayer}");

            int ligne;
            int colonne;

            // saisie de luser entre O ET X
            while (true)
            {
                ligne = LireNombreEntre1Et3("Saisir la ligne (1-3) : ") - 1;
                colonne = LireNombreEntre1Et3("Saisir la colonne (1-3) : ") - 1;

                if (_board.IsEmpty(ligne, colonne))
                {
                    break;
                }

                Console.WriteLine("case déjà pris !");
            }

            // jouer le tour
            _board.Play(ligne, colonne, _currentPlayer);

            // victoire ?
            if (_board.HasWinner(_currentPlayer))
            {
                Console.Clear();
                _board.Print();
                Console.WriteLine();
                Console.WriteLine($"{_currentPlayer} a gg la partie");
                return;
            }

            // match nul ?
            if (_board.IsFull())
            {
                Console.Clear();
                _board.Print();
                Console.WriteLine();
                Console.WriteLine("match nul");
                return;
            }

            // Alterner entre o et X
            _currentPlayer = (_currentPlayer == 'O') ? 'X' : 'O';
        }
    }

    private int LireNombreEntre1Et3(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int valeur) && valeur >= 1 && valeur <= 3)
            {
                return valeur;
            }

            Console.WriteLine("erreur : stp saisie un nombre entre 1 et 3.");
        }
    }
}
