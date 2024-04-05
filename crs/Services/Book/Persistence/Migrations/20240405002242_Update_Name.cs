using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Image_AuthorImageId",
                table: "Authors");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Genres_GenreName",
                table: "Genres");

            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Genres",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CartItemQuantity",
                table: "CartItem",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "QuantityBook",
                table: "Books",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "BookDescription",
                table: "Books",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "AuthorImageId",
                table: "Authors",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "AuthorDescription",
                table: "Authors",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_AuthorImageId",
                table: "Authors",
                newName: "IX_Authors_ImageId");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Genres",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Genres_Name",
                table: "Genres",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ImageId",
                table: "Genres",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Image_ImageId",
                table: "Authors",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Image_ImageId",
                table: "Genres",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Image_ImageId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Image_ImageId",
                table: "Genres");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Genres_Name",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_ImageId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Genres");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genres",
                newName: "GenreName");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "CartItem",
                newName: "CartItemQuantity");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Books",
                newName: "QuantityBook");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Books",
                newName: "BookDescription");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Authors",
                newName: "AuthorImageId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Authors",
                newName: "AuthorDescription");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_ImageId",
                table: "Authors",
                newName: "IX_Authors_AuthorImageId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Genres_GenreName",
                table: "Genres",
                column: "GenreName");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Image_AuthorImageId",
                table: "Authors",
                column: "AuthorImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
