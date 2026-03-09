using FluentMigrator;

namespace HelloWorld.Infrastructure.Migrations;

[Migration(202603091230)]
public class CreateGameSessionsTable : Migration
{
    public override void Up()
    {
        Create.Table("game_sessions")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("ModeBot").AsBoolean().NotNullable()
            .WithColumn("BoardState").AsString(9).NotNullable()
            .WithColumn("CurrentPlayerSymbol").AsString(1).NotNullable()
            .WithColumn("WinnerSymbol").AsString(1).Nullable()
            .WithColumn("IsFinished").AsBoolean().NotNullable()
            .WithColumn("CreatedAtUtc").AsDateTime().NotNullable()
            .WithColumn("UpdatedAtUtc").AsDateTime().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("game_sessions");
    }
}
