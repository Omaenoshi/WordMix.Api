namespace WordMix.Migrator.Migrations;

using FluentMigrator;

[Migration(202505132315)]
public class Migration202505132315_Create_Users_Table : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("users").WithDescription("Пользователи")
              .WithColumn("id").AsInt64().PrimaryKey().Identity()
              .WithColumn("email").AsString().NotNullable().Unique().WithColumnDescription("Электронная почта")
              .WithColumn("password_hash").AsCustom("bytea").NotNullable().WithColumnDescription("Хеш пароля")
              .WithColumn("password_salt").AsCustom("bytea").NotNullable().WithColumnDescription("Соль для хеширования пароля")
              .WithColumn("is_verified").AsBoolean().NotNullable().WithColumnDescription("Подтверждена ли почта")
              .WithColumn("verification_token").AsString().NotNullable().WithColumnDescription("Токен подтверждения")
              .WithColumn("created_at").AsDateTimeOffset().NotNullable().WithColumnDescription("Время создания")
              .WithColumn("updated_at").AsDateTimeOffset().Nullable().WithColumnDescription("Время обновления");
    }
}