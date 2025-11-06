using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Linkify.Domain.Entities;

namespace Linkify.Infrastructure.Configurations;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasKey(l => l.Id);

        builder.HasOne(l => l.User)
            .WithMany(u => u.Likes)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        // Prevent duplicate likes
        builder.HasIndex(l => new { l.UserId, l.PostId })
            .IsUnique();
    }
}