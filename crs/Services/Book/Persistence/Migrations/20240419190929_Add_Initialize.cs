using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Add_Initialize : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
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
                Role = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.UniqueConstraint("AK_Users_Email", x => x.Email);
            });

        migrationBuilder.CreateTable(
            name: "Authors",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Pseudonym = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                ImageId = table.Column<Guid>(type: "uuid", nullable: true),
                Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Authors", x => x.Id);
                table.UniqueConstraint("AK_Authors_Pseudonym", x => x.Pseudonym);
                table.ForeignKey(
                    name: "FK_Authors_Images_ImageId",
                    column: x => x.ImageId,
                    principalTable: "Images",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
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
            name: "Wish",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Wish", x => x.Id);
                table.ForeignKey(
                    name: "FK_Wish_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                PageCount = table.Column<int>(type: "integer", nullable: false),
                Price_Currency = table.Column<string>(type: "text", nullable: false),
                Price_Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                ISBN = table.Column<string>(type: "text", nullable: false),
                Quantity = table.Column<int>(type: "integer", nullable: false),
                SoldUnits = table.Column<int>(type: "integer", nullable: false),
                AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                PosterId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
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
            name: "CartItem",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CartId = table.Column<Guid>(type: "uuid", nullable: false),
                BookId = table.Column<Guid>(type: "uuid", nullable: false),
                Quantity = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CartItem", x => x.Id);
                table.ForeignKey(
                    name: "FK_CartItem_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
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
                Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                BookId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Genres", x => x.Id);
                table.UniqueConstraint("AK_Genres_Name", x => x.Name);
                table.ForeignKey(
                    name: "FK_Genres_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Wishes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                BookId = table.Column<Guid>(type: "uuid", nullable: false),
                WishId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Wishes", x => x.Id);
                table.ForeignKey(
                    name: "FK_Wishes_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Wishes_Wish_WishId",
                    column: x => x.WishId,
                    principalTable: "Wish",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Authors_ImageId",
            table: "Authors",
            column: "ImageId",
            unique: true);

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
            name: "IX_Wish_UserId",
            table: "Wish",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Wishes_BookId",
            table: "Wishes",
            column: "BookId");

        migrationBuilder.CreateIndex(
            name: "IX_Wishes_WishId",
            table: "Wishes",
            column: "WishId");

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
            name: "FK_Authors_Images_ImageId",
            table: "Authors");

        migrationBuilder.DropForeignKey(
            name: "FK_Books_Images_PosterId",
            table: "Books");

        migrationBuilder.DropForeignKey(
            name: "FK_Books_Authors_AuthorId",
            table: "Books");

        migrationBuilder.DropForeignKey(
            name: "FK_Books_Genres_GenreId",
            table: "Books");

        migrationBuilder.DropTable(
            name: "CartItem");

        migrationBuilder.DropTable(
            name: "Wishes");

        migrationBuilder.DropTable(
            name: "Carts");

        migrationBuilder.DropTable(
            name: "Wish");

        migrationBuilder.DropTable(
            name: "Users");

        migrationBuilder.DropTable(
            name: "Images");

        migrationBuilder.DropTable(
            name: "Authors");

        migrationBuilder.DropTable(
            name: "Genres");

        migrationBuilder.DropTable(
            name: "Books");

        migrationBuilder.DropTable(
            name: "Languages");
    }
}
