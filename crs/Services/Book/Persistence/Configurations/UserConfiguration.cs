using Domain.UserAggregate.ValueObjects;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Contracts.Enumerations;
using Domain.UserAggregate.Ids;
using Domain.SharedKernel.ValueObjects;
using Domain.CartAggregate;
using Domain.UserAggregate.Entities;

namespace Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasAlternateKey(x => x.Email);

        builder.Property(x => x.Id).HasConversion(
            userId => userId.Value,
            value => new UserId(value)).IsRequired();

        builder.Property(x => x.Email).HasConversion(
            email => email.Value,
            value => Email.Create(value).Value)
            .HasMaxLength(Email.MaxLength)
            .IsRequired();

        builder.Property(x => x.PasswordHash).HasConversion(
            password => password.Value,
            value => PasswordHash.Create(value).Value)
            .HasMaxLength(PasswordHash.MaxLength)
            .IsRequired();

        builder.Property(x => x.PasswordSalt).HasConversion(
            password => password.Value,
            value => PasswordSalt.Create(value).Value)
            .IsRequired();

        builder.Property(x => x.FirstName).HasConversion(
            firstName => firstName.Value,
            value => FirstName.Create(value).Value)
            .HasMaxLength(FirstName.MaxLength)
            .IsRequired();

        builder.Property(x => x.LastName).HasConversion(
            lastName => lastName.Value,
            value => LastName.Create(value).Value)
            .HasMaxLength(LastName.MaxLength)
            .IsRequired();

        builder.OwnsOne(x => x.RefreshToken, refreshTokenBuilder =>
        {
            refreshTokenBuilder.Property(x => x.Token)
            .IsRequired();

            refreshTokenBuilder.Property(x => x.Expired)
            .IsRequired();
        });

        builder.Property(x => x.EmailConfirmationToken).HasConversion(
            token => token.Value,
            value => EmailConfirmationToken.Create(value).Value)
            .IsRequired();

        builder.Property(x => x.IsEmailConfirmed).IsRequired();

        builder.Property(x => x.Role).IsRequired();

        builder.Property(x => x.Role).HasConversion(
           gender => gender.Name,
           name => Role.FromName(name)
           ).IsRequired();

        builder.Property(x => x.Gender).HasConversion(
            gender => gender.Name,
            name => Gender.FromName(name)
            ).IsRequired();

        //для того чтоб сделать каскадное удаление cart при удалении пользователя с помощью CartId:
        builder.HasOne(x => x.Cart)
            .WithOne()
            .HasForeignKey<Cart>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Wishes)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}