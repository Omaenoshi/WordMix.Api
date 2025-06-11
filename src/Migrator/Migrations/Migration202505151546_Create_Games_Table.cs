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
              .WithColumn("correct_word_id").AsInt64().NotNullable().ForeignKey("words", "id");
    }
}