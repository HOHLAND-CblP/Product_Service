﻿using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace hohland_cblp.ShopBackend.Infrastructure.Migrations;

[Migration(20231119103000, "Add ProductV1 Type")]
public class AddProductV1Type : Migration
{
    
    public override void Up()
    {
        const string sql = """
                DO $$
                    BEGIN
                        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'product_v1') THEN
                            CREATE TYPE product_v1 as
                            (
                                  id                  bigint
                                , task_id             bigint
                                , parent_task_id      bigint
                                , number              text
                                , title               text
                                , description         text
                                , status              integer
                                , assigned_to_user_id bigint
                                , user_id             bigint
                                , at                  timestamp with time zone
                            );
                        END IF;
                    END
                $$;
                """;

        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = """
                            DO $$
                                BEGIN
                                    DROP TYPE IF EXISTS product_v1;
                                END
                            $$;
                            """;

        Execute.Sql(sql);
    }

    
}