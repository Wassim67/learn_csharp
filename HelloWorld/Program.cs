using System;
using HelloWorld;

try
{
    var game = new Game();
    game.Run();
}
catch (Exception ex){
    Console.WriteLine(ex.Message, ConsoleColor.Red);
}
