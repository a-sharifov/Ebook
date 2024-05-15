using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Intialize_DB : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Authors",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Pseudonym = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Authors", x => x.Id);
                table.UniqueConstraint("AK_Authors_Pseudonym", x => x.Pseudonym);
            });

        migrationBuilder.CreateTable(
            name: "Genres",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Genres", x => x.Id);
                table.UniqueConstraint("AK_Genres_Name", x => x.Name);
            });

        migrationBuilder.CreateTable(
            name: "Images",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Bucket = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Images", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Languages",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Code = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Languages", x => x.Id);
                table.UniqueConstraint("AK_Languages_Code", x => x.Code);
            });

        migrationBuilder.CreateTable(
            name: "OutboxMessageConsumers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OutboxMessageConsumers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "OutboxMessages",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                Type = table.Column<string>(type: "text", nullable: false),
                Message = table.Column<string>(type: "text", nullable: false),
                ProcessedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                Error = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OutboxMessages", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                PasswordHash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                PasswordSalt = table.Column<string>(type: "text", nullable: false),
                RefreshToken_Token = table.Column<string>(type: "text", nullable: true),
                RefreshToken_ExpiredTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                EmailConfirmationToken = table.Column<string>(type: "text", nullable: true),
                ResetPasswordToken = table.Column<string>(type: "text", nullable: true),
                IsEmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                Role = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.UniqueConstraint("AK_Users_Email", x => x.Email);
            });

        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                PageCount = table.Column<int>(type: "integer", nullable: false),
                Price_Value = table.Column<decimal>(type: "numeric", nullable: false),
                LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                Quantity = table.Column<int>(type: "integer", nullable: false),
                SoldUnits = table.Column<int>(type: "integer", nullable: false),
                AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                PosterId = table.Column<Guid>(type: "uuid", nullable: false),
                GenreId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Books", x => x.Id);
                table.ForeignKey(
                    name: "FK_Books_Authors_AuthorId",
                    column: x => x.AuthorId,
                    principalTable: "Authors",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Books_Genres_GenreId",
                    column: x => x.GenreId,
                    principalTable: "Genres",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Books_Images_PosterId",
                    column: x => x.PosterId,
                    principalTable: "Images",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Books_Languages_LanguageId",
                    column: x => x.LanguageId,
                    principalTable: "Languages",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Carts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                ExpirationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Carts", x => x.Id);
                table.ForeignKey(
                    name: "FK_Carts_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Wishes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Wishes", x => x.Id);
                table.ForeignKey(
                    name: "FK_Wishes_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "CartItems",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CartId = table.Column<Guid>(type: "uuid", nullable: false),
                BookId = table.Column<Guid>(type: "uuid", nullable: false),
                Quantity = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CartItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_CartItems_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CartItems_Carts_CartId",
                    column: x => x.CartId,
                    principalTable: "Carts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "WishItems",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                BookId = table.Column<Guid>(type: "uuid", nullable: false),
                WishId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WishItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_WishItems_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_WishItems_Wishes_WishId",
                    column: x => x.WishId,
                    principalTable: "Wishes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Books_AuthorId",
            table: "Books",
            column: "AuthorId");

        migrationBuilder.CreateIndex(
            name: "IX_Books_GenreId",
            table: "Books",
            column: "GenreId");

        migrationBuilder.CreateIndex(
            name: "IX_Books_LanguageId",
            table: "Books",
            column: "LanguageId");

        migrationBuilder.CreateIndex(
            name: "IX_Books_PosterId",
            table: "Books",
            column: "PosterId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_CartItems_BookId",
            table: "CartItems",
            column: "BookId");

        migrationBuilder.CreateIndex(
            name: "IX_CartItems_CartId",
            table: "CartItems",
            column: "CartId");

        migrationBuilder.CreateIndex(
            name: "IX_Carts_UserId",
            table: "Carts",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Wishes_UserId",
            table: "Wishes",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_WishItems_BookId",
            table: "WishItems",
            column: "BookId");

        migrationBuilder.CreateIndex(
            name: "IX_WishItems_WishId",
            table: "WishItems",
            column: "WishId");

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

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CartItems");

        migrationBuilder.DropTable(
            name: "OutboxMessageConsumers");

        migrationBuilder.DropTable(
            name: "OutboxMessages");

        migrationBuilder.DropTable(
            name: "WishItems");

        migrationBuilder.DropTable(
            name: "Carts");

        migrationBuilder.DropTable(
            name: "Books");

        migrationBuilder.DropTable(
            name: "Wishes");

        migrationBuilder.DropTable(
            name: "Authors");

        migrationBuilder.DropTable(
            name: "Genres");

        migrationBuilder.DropTable(
            name: "Images");

        migrationBuilder.DropTable(
            name: "Languages");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
