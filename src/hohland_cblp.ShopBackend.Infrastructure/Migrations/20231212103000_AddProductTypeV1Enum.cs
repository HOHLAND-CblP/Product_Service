using FluentMigrator;

namespace hohland_cblp.ShopBackend.Infrastructure.Migrations;

[Migration(20231212103000, "Add ProductTypeV1 Enum")]
public class AddProductTypeV1Enum : Migration
{
    public override void Up()
    {
        const string sql = """
                           DO $$
                               BEGIN
                                   IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'product_type_v1') THEN
                                       CREATE TYPE product_type_v1 as ENUM
                                       (
                                               'general'
                                             , 'household_chemicals'
                                             , 'appliances'
                                             , 'food'
                                       );
                                   END IF;
                               END
                           $$;
                           """;

        Execute.Sql(sql);
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}