using FluentMigrator;

namespace Agro.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_INSERT_DATA_SAIDAS_ANIMAIS, "Inserção de dados na tabela saidas animais")]

    public class Version0000004 : VersionBase
    {
        public override void Up()
        {
            Insert.IntoTable("Especies").Row(new { NomeEspecie = "Suíno" });
            Insert.IntoTable("Especies").Row(new { NomeEspecie = "Bovino" });
            Insert.IntoTable("Especies").Row(new { NomeEspecie = "Equino" });
            Insert.IntoTable("Especies").Row(new { NomeEspecie = "Ovino" });        }
    }
}
