using FluentMigrator;

namespace Agro.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_UNIDADE_EXPLORACAO, "Criação de tabela para registro de Unidade de Exploração")]

    public class Version0000002 : VersionBase
    {
        public override void Up()
        {
            CreateTable("UnidadesExploracoes")
                        .WithColumn("QuantidadeAnimais").AsInt32().NotNullable()
                        .WithColumn("CodigoPropriedade").AsInt32().NotNullable()
                        .WithColumn("EspecieId").AsInt32().NotNullable().ForeignKey("FK_UnidadesExploracoes_Especie", "Especies", "Id");
        }
    }
}
