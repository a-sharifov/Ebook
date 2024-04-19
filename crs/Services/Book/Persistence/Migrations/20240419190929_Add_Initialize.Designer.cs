﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.DbContexts;

#nullable disable

namespace Persistence.Migrations;

[DbContext(typeof(BookDbContext))]
[Migration("20240419190929_Add_Initialize")]
partial class Add_Initialize
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.3")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("Domain.AuthorAggregate.Author", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnType("character varying(1000)");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)");

                b.Property<Guid?>("ImageId")
                    .HasColumnType("uuid");

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)");

                b.Property<string>("Pseudonym")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)");

                b.HasKey("Id");

                b.HasAlternateKey("Pseudonym");

                b.HasIndex("ImageId")
                    .IsUnique();

                b.ToTable("Authors");
            });

        modelBuilder.Entity("Domain.BookAggregate.Book", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<Guid>("AuthorId")
                    .HasColumnType("uuid");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnType("character varying(1000)");

                b.Property<Guid?>("GenreId")
                    .HasColumnType("uuid");

                b.Property<string>("ISBN")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid>("LanguageId")
                    .HasColumnType("uuid");

                b.Property<int>("PageCount")
                    .HasColumnType("integer");

                b.Property<Guid>("PosterId")
                    .HasColumnType("uuid");

                b.Property<int>("Quantity")
                    .HasColumnType("integer");

                b.Property<int>("SoldUnits")
                    .HasColumnType("integer");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.HasKey("Id");

                b.HasAlternateKey("ISBN");

                b.HasIndex("AuthorId");

                b.HasIndex("GenreId");

                b.HasIndex("LanguageId");

                b.HasIndex("PosterId")
                    .IsUnique();

                b.ToTable("Books");
            });

        modelBuilder.Entity("Domain.CartAggregate.Cart", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<Guid>("UserId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.HasIndex("UserId")
                    .IsUnique();

                b.ToTable("Carts");
            });

        modelBuilder.Entity("Domain.CartAggregate.Entities.CartItem", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<Guid>("BookId")
                    .HasColumnType("uuid");

                b.Property<Guid>("CartId")
                    .HasColumnType("uuid");

                b.Property<int>("Quantity")
                    .HasColumnType("integer");

                b.HasKey("Id");

                b.HasIndex("BookId");

                b.HasIndex("CartId");

                b.ToTable("CartItem");
            });

        modelBuilder.Entity("Domain.GenreAggregate.Genre", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<Guid?>("BookId")
                    .HasColumnType("uuid");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)");

                b.HasKey("Id");

                b.HasAlternateKey("Name");

                b.HasIndex("BookId");

                b.ToTable("Genres");
            });

        modelBuilder.Entity("Domain.LanguageAggregate.Language", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<string>("Code")
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnType("character varying(35)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.HasKey("Id");

                b.HasAlternateKey("Code");

                b.ToTable("Languages");
            });

        modelBuilder.Entity("Domain.SharedKernel.Entities.Image", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<string>("Bucket")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnType("character varying(500)");

                b.Property<string>("Url")
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnType("character varying(500)");

                b.HasKey("Id");

                b.ToTable("Images");
            });

        modelBuilder.Entity("Domain.UserAggregate.User", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.Property<string>("EmailConfirmationToken")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)");

                b.Property<bool>("IsEmailConfirmed")
                    .HasColumnType("boolean");

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)");

                b.Property<string>("PasswordHash")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.Property<string>("PasswordSalt")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("Role")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasAlternateKey("Email");

                b.ToTable("Users");
            });

        modelBuilder.Entity("Domain.WishAggregate.Entities.WishItem", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<Guid>("BookId")
                    .HasColumnType("uuid");

                b.Property<Guid>("WishId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.HasIndex("BookId");

                b.HasIndex("WishId");

                b.ToTable("Wishes");
            });

        modelBuilder.Entity("Domain.WishAggregate.Wish", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<Guid>("UserId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.HasIndex("UserId")
                    .IsUnique();

                b.ToTable("Wish");
            });

        modelBuilder.Entity("Domain.AuthorAggregate.Author", b =>
            {
                b.HasOne("Domain.SharedKernel.Entities.Image", "Image")
                    .WithOne()
                    .HasForeignKey("Domain.AuthorAggregate.Author", "ImageId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.Navigation("Image");
            });

        modelBuilder.Entity("Domain.BookAggregate.Book", b =>
            {
                b.HasOne("Domain.AuthorAggregate.Author", "Author")
                    .WithMany("Books")
                    .HasForeignKey("AuthorId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("Domain.GenreAggregate.Genre", null)
                    .WithMany("Books")
                    .HasForeignKey("GenreId")
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne("Domain.LanguageAggregate.Language", "Language")
                    .WithMany()
                    .HasForeignKey("LanguageId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Domain.SharedKernel.Entities.Image", "Poster")
                    .WithOne()
                    .HasForeignKey("Domain.BookAggregate.Book", "PosterId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.OwnsOne("Domain.SharedKernel.ValueObjects.Money", "Price", b1 =>
                    {
                        b1.Property<Guid>("BookId")
                            .HasColumnType("uuid");

                        b1.Property<decimal>("Amount")
                            .HasColumnType("decimal(18,2)");

                        b1.Property<string>("Currency")
                            .IsRequired()
                            .HasColumnType("text");

                        b1.HasKey("BookId");

                        b1.ToTable("Books");

                        b1.WithOwner()
                            .HasForeignKey("BookId");
                    });

                b.Navigation("Author");

                b.Navigation("Language");

                b.Navigation("Poster");

                b.Navigation("Price")
                    .IsRequired();
            });

        modelBuilder.Entity("Domain.CartAggregate.Cart", b =>
            {
                b.HasOne("Domain.UserAggregate.User", null)
                    .WithOne("Cart")
                    .HasForeignKey("Domain.CartAggregate.Cart", "UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("Domain.CartAggregate.Entities.CartItem", b =>
            {
                b.HasOne("Domain.BookAggregate.Book", "Book")
                    .WithMany()
                    .HasForeignKey("BookId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("Domain.CartAggregate.Cart", null)
                    .WithMany("Items")
                    .HasForeignKey("CartId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Book");
            });

        modelBuilder.Entity("Domain.GenreAggregate.Genre", b =>
            {
                b.HasOne("Domain.BookAggregate.Book", null)
                    .WithMany("Genres")
                    .HasForeignKey("BookId")
                    .OnDelete(DeleteBehavior.NoAction);
            });

        modelBuilder.Entity("Domain.UserAggregate.User", b =>
            {
                b.OwnsOne("Domain.UserAggregate.ValueObjects.RefreshToken", "RefreshToken", b1 =>
                    {
                        b1.Property<Guid>("UserId")
                            .HasColumnType("uuid");

                        b1.Property<DateTime>("Expired")
                            .HasColumnType("timestamp with time zone");

                        b1.Property<string>("Token")
                            .IsRequired()
                            .HasColumnType("text");

                        b1.HasKey("UserId");

                        b1.ToTable("Users");

                        b1.WithOwner()
                            .HasForeignKey("UserId");
                    });

                b.Navigation("RefreshToken");
            });

        modelBuilder.Entity("Domain.WishAggregate.Entities.WishItem", b =>
            {
                b.HasOne("Domain.BookAggregate.Book", "Book")
                    .WithMany()
                    .HasForeignKey("BookId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("Domain.WishAggregate.Wish", null)
                    .WithMany("Items")
                    .HasForeignKey("WishId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Book");
            });

        modelBuilder.Entity("Domain.WishAggregate.Wish", b =>
            {
                b.HasOne("Domain.UserAggregate.User", null)
                    .WithOne("Wish")
                    .HasForeignKey("Domain.WishAggregate.Wish", "UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("Domain.AuthorAggregate.Author", b =>
            {
                b.Navigation("Books");
            });

        modelBuilder.Entity("Domain.BookAggregate.Book", b =>
            {
                b.Navigation("Genres");
            });

        modelBuilder.Entity("Domain.CartAggregate.Cart", b =>
            {
                b.Navigation("Items");
            });

        modelBuilder.Entity("Domain.GenreAggregate.Genre", b =>
            {
                b.Navigation("Books");
            });

        modelBuilder.Entity("Domain.UserAggregate.User", b =>
            {
                b.Navigation("Cart")
                    .IsRequired();

                b.Navigation("Wish")
                    .IsRequired();
            });

        modelBuilder.Entity("Domain.WishAggregate.Wish", b =>
            {
                b.Navigation("Items");
            });
#pragma warning restore 612, 618
    }
}