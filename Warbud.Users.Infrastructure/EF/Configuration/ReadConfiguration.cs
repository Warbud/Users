using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warbud.Users.Infrastructure.EF.Models;

namespace Warbud.Users.Infrastructure.EF.Configuration
{
    internal sealed class ReadConfiguration : IEntityTypeConfiguration<UserReadModel>,
        IEntityTypeConfiguration<WarbudAppReadModel>,
        IEntityTypeConfiguration<WarbudClaimReadModel>
    {
        // public void Configure(EntityTypeBuilder<PackingListReadModel> builder)
        // {
        //     builder
        //         .Property(pl => pl.Localization)
        //         .HasConversion(l => l.ToString(), l => LocalizationReadModel.Create(l));
        //
        //     builder
        //         .HasMany(pl => pl.Items)
        //         .WithOne(pi => pi.PackingList);
        // }

        public void Configure(EntityTypeBuilder<UserReadModel> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            
        }

        public void Configure(EntityTypeBuilder<WarbudAppReadModel> builder)
        {
            builder.ToTable("Apps");
            builder.HasKey(x => x.Id);
        }

        public void Configure(EntityTypeBuilder<WarbudClaimReadModel> builder)
        {
            builder.ToTable("Claims");
            builder
                .HasKey(x => new
                {
                    x.UserId,
                    x.AppId,
                    x.ProjectId
                });
        }
    }
}