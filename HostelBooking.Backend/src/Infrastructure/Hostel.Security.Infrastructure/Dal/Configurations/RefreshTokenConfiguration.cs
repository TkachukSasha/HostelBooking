using Hostel.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostel.Security.Infrastructure.Dal.Configurations
{

    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.RefreshTokenId);

            builder.Property(x => x.Token)
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(x => x.RefreshTokens)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
