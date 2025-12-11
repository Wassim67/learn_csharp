using System;
using HelloWorld;

try
{
    IGame game = new Game();
    game.Lancer();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex.Message);
    Console.ResetColor();
}