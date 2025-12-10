// Saisie dans une colonne grille de 3*3 
// Pouvoir saisir plusieurs coups dans la console

void PrintTableau(char[][] plateau)
{
    for (int i = 0; i < 3; i++)
    {
        Console.WriteLine($"{plateau[i][0]} {plateau[i][1]} {plateau[i][2]}");
    }
}

int nombreValideColonneEtLigne(string message)
{
    int valeur;
    
    while (true)
    {
        Console.Write(message);
        
        if (int.TryParse(Console.ReadLine(), out valeur) && valeur >= 1 && valeur <= 3)
        {
            return valeur; 
        }

        Console.WriteLine("erreur mauvais nombre : entre 1 et 3 svp");
    }
}

char[][] plateau =
{
    new char[] { '.', '.', '.' },
    new char[] { '.', '.', '.' },
    new char[] { '.', '.', '.' }
};

// commencer par O
char joueur = 'O';

for (int tour = 1; tour <= 9; tour++)
{
    Console.WriteLine();
    PrintTableau(plateau);
    Console.WriteLine($"au tour du {joueur}");

    int ligne;
    int colonne;

    while (true)
    {
        ligne = nombreValideColonneEtLigne("Saisir la ligne (1-3) : ");
        colonne = nombreValideColonneEtLigne("Saisir la colonne (1-3) : ");

        if (plateau[ligne - 1][colonne - 1] == '.')
        {
            break;
        }

        Console.WriteLine("case déjà prise");
    }

    // placer le coup
    plateau[ligne - 1][colonne - 1] = joueur;

    // changer de joueur pour le prochain tour
    joueur = (joueur == 'O') ? 'X' : 'O';
}

Console.WriteLine();
Console.WriteLine("tableau rempli");
PrintTableau(plateau);