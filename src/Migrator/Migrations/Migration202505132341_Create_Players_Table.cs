namespace WordMix.Migrator.Migrations;

using FluentMigrator;

[Migration(202505132341)]
public class Migration202505132341_Create_Players_Table : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("players").WithDescription("Игроки")
              .WithColumn("id").AsInt64().PrimaryKey().Identity()
              .WithColumn("username").AsString().NotNullable().Unique().WithColumnDescription("Имя пользователя")
              .WithColumn("experience").AsInt32().NotNullable().WithColumnDescription("Опыт")
              .WithColumn("balance").AsInt32().NotNullable().WithColumnDescription("Баланс")
              .WithColumn("avatar_url").AsString().Nullable().WithColumnDescription("Ссылка на аватар")
              .WithColumn("user_id").AsInt64().NotNullable().ForeignKey("users", "id").WithColumnDescription("ИД пользователя");
    }
}