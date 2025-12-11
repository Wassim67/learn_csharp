using System;
using HelloWorld;

try
{
    Console.WriteLine("Voulez-vous jouer contre un robot ? (O/N)");
    bool modeRobot = Console.ReadLine()?.Trim().ToUpper() == "O";

    IGame game = new Game(modeRobot);
    game.Lancer();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex.Message);
    Console.ResetColor();
}