using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.Triggers;

public sealed class CartItemTriggers : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
                      CREATE OR REPLACE FUNCTION trg_decrease_book_quantity()
                      RETURNS TRIGGER AS $$
                      BEGIN
                          UPDATE public."Books"
                          SET "Quantity" = "Quantity" - NEW."Quantity"
                          WHERE "Id" = NEW."BookId";
                          RETURN NEW;
                      END;
                      $$ LANGUAGE plpgsql;
                      """);

        migrationBuilder.Sql("""
                      CREATE TRIGGER trg_before_insert_decrease_book_quantity
                      BEFORE INSERT ON public."CartItems"
                      FOR EACH ROW
                      EXECUTE FUNCTION trg_decrease_book_quantity();
                      """);

        migrationBuilder.Sql("""
                      CREATE OR REPLACE FUNCTION trg_increase_book_quantity()
                      RETURNS TRIGGER AS $$
                      BEGIN
                          UPDATE public."Books"
                          SET "Quantity" = "Quantity" + OLD."Quantity"
                          WHERE "Id" = OLD."BookId";
                          RETURN OLD;
                      END;
                      $$ LANGUAGE plpgsql;
                      """);

        migrationBuilder.Sql("""
                      CREATE TRIGGER trg_before_delete_increase_book_quantity
                      BEFORE DELETE ON public."CartItems"
                      FOR EACH ROW
                      EXECUTE FUNCTION trg_increase_book_quantity();
                      """);

        migrationBuilder.Sql("""
                      CREATE OR REPLACE FUNCTION trg_adjust_book_quantity()
                      RETURNS TRIGGER AS $$
                      BEGIN
                          UPDATE public."Books"
                          SET "Quantity" = ("Quantity" + OLD."Quantity") - NEW."Quantity"
                          WHERE "Id" = NEW."BookId";
                          RETURN NEW;
                      END;
                      $$ LANGUAGE plpgsql;
                      """);

        migrationBuilder.Sql("""
                      CREATE TRIGGER trg_before_update_adjust_book_quantity
                      BEFORE UPDATE ON public."CartItems"
                      FOR EACH ROW
                      EXECUTE FUNCTION trg_adjust_book_quantity();
                      """);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS trg_before_insert_decrease_book_quantity ON public.""CartItems"";");
        migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS trg_decrease_book_quantity();");

        migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS trg_before_delete_increase_book_quantity ON public.""CartItems"";");
        migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS trg_increase_book_quantity();");

        migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS trg_before_update_adjust_book_quantity ON public.""CartItems"";");
        migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS trg_adjust_book_quantity();");
    }
}
