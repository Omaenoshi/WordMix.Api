namespace WordMix.Migrator.Migrations;

using FluentMigrator;

[Migration(202505151546)]
public class Migration202505151546_Create_Games_Table : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("games")
              .WithColumn("id").AsInt64().PrimaryKey().Identity()
              .WithColumn("player_id").AsInt64().NotNullable().ForeignKey("players", "id")
              .WithColumn("word").AsString(128).NotNullable().ForeignKey("words", "id")
              .WithColumn("shuffled_word").AsString(128).NotNullable()
              .WithColumn("answer_options").AsString(int.MaxValue).NotNullable(); // JSON массив строк
    }
}