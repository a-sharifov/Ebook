using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
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
                RefreshToken_Expired = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                EmailConfirmationToken = table.Column<string>(type: "text", nullable: false),
                IsEmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                Role = table.Column<string>(type: "text", nullable: false),
                Gender = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.UniqueConstraint("AK_Users_Email", x => x.Email);
            });

        migrationBuilder.CreateTable(
            name: "Carts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false)
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
            name: "Authors",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                AuthorImageId = table.Column<Guid>(type: "uuid", nullable: true),
                Pseudonym = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                AuthorDescription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                BookId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Authors", x => x.Id);
                table.UniqueConstraint("AK_Authors_Pseudonym", x => x.Pseudonym);
            });

        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                BookDescription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                PageCount = table.Column<int>(type: "integer", nullable: false),
                Price_Currency = table.Column<string>(type: "text", nullable: false),
                Price_Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                ISBN = table.Column<string>(type: "text", nullable: false),
                QuantityBook = table.Column<int>(type: "integer", nullable: false),
                SoldUnits = table.Column<int>(type: "integer", nullable: false),
                AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                GenreId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Books", x => x.Id);
                table.UniqueConstraint("AK_Books_ISBN", x => x.ISBN);
                table.ForeignKey(
                    name: "FK_Books_Authors_AuthorId",
                    column: x => x.AuthorId,
                    principalTable: "Authors",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "CartItem",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CartId = table.Column<Guid>(type: "uuid", nullable: false),
                BookId = table.Column<Guid>(type: "uuid", nullable: false),
                CartItemQuantity = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CartItem", x => x.Id);
                table.ForeignKey(
                    name: "FK_CartItem_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CartItem_Carts_CartId",
                    column: x => x.CartId,
                    principalTable: "Carts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Genres",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                GenreName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                BookId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Genres", x => x.Id);
                table.UniqueConstraint("AK_Genres_GenreName", x => x.GenreName);
                table.ForeignKey(
                    name: "FK_Genres_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Image",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                ImageUrl = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                BookId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Image", x => x.Id);
                table.ForeignKey(
                    name: "FK_Image_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Wish",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                BookId = table.Column<Guid>(type: "uuid", nullable: false),
                UserId1 = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Wish", x => x.Id);
                table.ForeignKey(
                    name: "FK_Wish_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Wish_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Wish_Users_UserId1",
                    column: x => x.UserId1,
                    principalTable: "Users",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Authors_AuthorImageId",
            table: "Authors",
            column: "AuthorImageId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Authors_BookId",
            table: "Authors",
            column: "BookId");

        migrationBuilder.CreateIndex(
            name: "IX_Books_AuthorId",
            table: "Books",
            column: "AuthorId");

        migrationBuilder.CreateIndex(
            name: "IX_Books_GenreId",
            table: "Books",
            column: "GenreId");

        migrationBuilder.CreateIndex(
            name: "IX_CartItem_BookId",
            table: "CartItem",
            column: "BookId");

        migrationBuilder.CreateIndex(
            name: "IX_CartItem_CartId",
            table: "CartItem",
            column: "CartId");

        migrationBuilder.CreateIndex(
            name: "IX_Carts_UserId",
            table: "Carts",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Genres_BookId",
            table: "Genres",
            column: "BookId");

        migrationBuilder.CreateIndex(
            name: "IX_Image_BookId",
            table: "Image",
            column: "BookId");

        migrationBuilder.CreateIndex(
            name: "IX_Wish_BookId",
            table: "Wish",
            column: "BookId");

        migrationBuilder.CreateIndex(
            name: "IX_Wish_UserId",
            table: "Wish",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Wish_UserId1",
            table: "Wish",
            column: "UserId1");

        migrationBuilder.AddForeignKey(
            name: "FK_Authors_Books_BookId",
            table: "Authors",
            column: "BookId",
            principalTable: "Books",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Authors_Image_AuthorImageId",
            table: "Authors",
            column: "AuthorImageId",
            principalTable: "Image",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Books_Genres_GenreId",
            table: "Books",
            column: "GenreId",
            principalTable: "Genres",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Authors_Books_BookId",
            table: "Authors");

        migrationBuilder.DropForeignKey(
            name: "FK_Genres_Books_BookId",
            table: "Genres");

        migrationBuilder.DropForeignKey(
            name: "FK_Image_Books_BookId",
            table: "Image");

        migrationBuilder.DropTable(
            name: "CartItem");

        migrationBuilder.DropTable(
            name: "Languages");

        migrationBuilder.DropTable(
            name: "Wish");

        migrationBuilder.DropTable(
            name: "Carts");

        migrationBuilder.DropTable(
            name: "Users");

        migrationBuilder.DropTable(
            name: "Books");

        migrationBuilder.DropTable(
            name: "Authors");

        migrationBuilder.DropTable(
            name: "Genres");

        migrationBuilder.DropTable(
            name: "Image");
    }
}
