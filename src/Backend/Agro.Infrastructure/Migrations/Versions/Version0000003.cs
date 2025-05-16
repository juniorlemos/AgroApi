using Agro.Domain.Entities;
using FluentMigrator;

namespace Agro.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_SAIDAS_ANIMAIS, "Criação de tabela para registro de saida de animais")]
    public class Version0000003 : VersionBase
    {
        public override void Up()
        {
            CreateTable("SaidasAnimais")
             .WithColumn("DataSaida").AsDateTime().NotNullable()
             .WithColumn("QuantidadeAnimais").AsInt32().NotNullable()
             .WithColumn("CodigoUEPOrigem").AsInt32().NotNullable().ForeignKey("FK_saidaAnimaisOrigem", "UnidadesExploracoes", "Id")
             .WithColumn("CodigoUEPSaida").AsInt32().NotNullable().ForeignKey("FK_saidaAnimaisSaida", "UnidadesExploracoes", "Id");
        }
    }

 }

