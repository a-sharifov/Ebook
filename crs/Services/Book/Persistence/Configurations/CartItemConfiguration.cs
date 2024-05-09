﻿using Domain.CartAggregate.Entities;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Xml.Linq;

namespace Persistence.Configurations;

internal sealed class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable(x => x.HasTrigger("SomeTrigger"));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
            cartItemId => cartItemId.Value,
            value => new CartItemId(value))
            .IsRequired();

        builder.Property(x => x.CartId)
            .HasConversion(
            bookId => bookId.Value,
            value => new CartId(value))
            .IsRequired();

        builder.HasOne(x => x.Book)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasConversion(
            cartItemQuantity => cartItemQuantity.Value,
            value => CartItemQuantity.Create(value).Value)
            .IsRequired();
    }
}
