using FluentMigrator;

namespace hohland_cblp.ShopBackend.Infrastructure.Migrations;

[Migration(20231114143000, "Initial migration")]
public class InitSchema : Migration
{
    public override void Up()
    {
        const string sql = """
                            CREATE TABLE products (
                                product_id  bigint PRIMARY KEY,
                                name        varchar NOT NULL,
                                price       numeric NOT NULL,
                                
                            )
                           """;
        
        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = """
                            DROP TABLE products;
                           """;
        
        Execute.Sql(sql);
    }
}