namespace WordMix.Migrator.Migrations;

using FluentMigrator;

[Migration(202505141323)]
public class Migration202505141323_Create_Words_Table : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("words")
              .WithColumn("id").AsInt64().PrimaryKey().Identity()
              .WithColumn("value").AsString(128).NotNullable();
    }
}