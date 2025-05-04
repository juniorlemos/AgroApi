using FluentMigrator;

namespace Agro.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_ESPECIES, "Criação de tabela para registro de Especies")]

    public class Version0000001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Especies")
                .WithColumn("NomeEspecie").AsString(80).NotNullable();

        }
    }
}
