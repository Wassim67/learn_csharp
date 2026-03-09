using System;
using FluentMigrator.Runner;
using HelloWorld;
using HelloWorld.Infrastructure.Migrations;
using HelloWorld.Infrastructure.Persistence;
using HelloWorld.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

try
{
    const string connectionString = "Server=localhost;Port=3306;Database=tictactoe;User=root;Password=root;";

    // Create DbContext options
    var dbContextOptions = new DbContextOptionsBuilder<TicTacToeDbContext>()
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .Options;
    using var gameDbContext = new TicTacToeDbContext(dbContextOptions);

    // Setup FluentMigrator services
    var services = new ServiceCollection()
        .AddFluentMigratorCore()
        .ConfigureRunner(rb => rb
            .AddMySql8()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(typeof(CreateGameSessionsTable).Assembly).For.Migrations())
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        .BuildServiceProvider(false);

    using (var scope = services.CreateScope())
    {
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    var persistence = new GamePersistence(gameDbContext);
    var stats = await persistence.GetStatisticsAsync();
    int totalVictoiresHumainBot = stats.VictoiresHumain + stats.VictoiresBot;
    string ratio = totalVictoiresHumainBot == 0
        ? "Aucune partie humain/bot terminee"
        : string.Format("{0}:{1}", stats.VictoiresHumain, stats.VictoiresBot);

    Console.WriteLine("Nombre de parties jouees: " + stats.PartiesJouees);
    Console.WriteLine("Victoires humain: " + stats.VictoiresHumain + " | Victoires bot: " + stats.VictoiresBot);
    Console.WriteLine("Ratio victoires humain/bot: " + ratio);
    Console.WriteLine();
    Console.WriteLine("Voulez-vous jouer contre un robot ? (O/N)");

    bool modeRobot = Console.ReadLine()?.Trim().ToUpper() == "O";
    var inProgressGame = await persistence.GetLastInProgressGameAsync(modeRobot);

    IGame game;

    if (inProgressGame is not null)
    {
        Console.WriteLine("Une partie en cours existe. La reprendre ? (O/N)");
        bool reprendre = Console.ReadLine()?.Trim().ToUpper() == "O";

        if (reprendre)
        {
            var board = new Board();
            board.LoadFromState(inProgressGame.BoardState);

            var playerO = new HumanPlayer('O');
            IPlayer playerX = modeRobot ? new BotPlayer('X') : new HumanPlayer('X');

            game = new Game(
                board,
                playerO,
                playerX,
                display: true,
                modeBot: modeRobot,
                persistence: persistence,
                gameId: inProgressGame.Id,
                currentPlayerSymbol: inProgressGame.CurrentPlayerSymbol);
        }
        else
        {
            game = new Game(modeRobot, persistence);
        }
    }
    else
    {
        game = new Game(modeRobot, persistence);
    }

    await game.Lancer();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex.Message);
    Console.ResetColor();
}
