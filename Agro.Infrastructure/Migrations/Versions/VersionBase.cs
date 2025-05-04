using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace Agro.Infrastructure.Migrations.Versions
{
    public abstract class VersionBase : ForwardOnlyMigration
    {
        protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
        {
            return Create.Table(table)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity();                
        }
    }
}
