using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warbud.Users.Domain.Entities;
using Warbud.Users.Domain.ValueObjects;

namespace Warbud.Users.Infrastructure.EF.Configuration
{
    internal sealed class WriteConfiguration : IEntityTypeConfiguration<User>, IEntityTypeConfiguration<WarbudClaim>, IEntityTypeConfiguration<WarbudApp>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(pl => pl.Id);
            builder.HasIndex(x => x.Email).IsUnique();
            
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Role).IsRequired();
            
            builder
                .Property(x => x.Id)
                .HasConversion(x => x.Value, x => new UserId(x));
            builder
                .Property(x => x.Email)
                .HasConversion(x => x.Value, x => new Email(x));
            builder
                .Property(x => x.FirstName)
                .HasConversion(x => x.Value, x => new UserName(x));
            builder
                .Property(x => x.LastName)
                .HasConversion(x => x.Value, x => new UserName(x));
            builder
                .Property(x => x.Password)
                .HasConversion(x => x.Value, x => new Password(x));
            
            
            builder.ToTable("Users");
        }

        public void Configure(EntityTypeBuilder<WarbudClaim> builder)
        {
            builder
                .HasKey(x => new
                {
                    x.UserId,
                    x.AppId,
                    x.ProjectId
                });
            
            builder
                .Property(x => x.UserId)
                .HasConversion(x => x.Value, x => new UserId(x));
            
            builder.Property(x => x.Value).IsRequired()
                .HasMaxLength(50);
            builder.ToTable("Claims");
        }
        
        public void Configure(EntityTypeBuilder<WarbudApp> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();;
            builder.Property(x => x.AppName).IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.ModuleName).IsRequired()
                .HasMaxLength(100);
            builder.ToTable("Apps");
        }
    }
}