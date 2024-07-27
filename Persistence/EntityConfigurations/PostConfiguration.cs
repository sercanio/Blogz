using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts").HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.BlogId).HasColumnName("BlogId").IsRequired();
        builder.Property(p => p.IsPublic).HasColumnName("IsPublic").IsRequired();
        builder.Property(p => p.CoverImageURL).HasColumnName("CoverImageURL");
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(p => p.Blog)
               .WithMany(b => b.Posts)
               .HasForeignKey(p => p.BlogId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(p => !p.DeletedDate.HasValue);
    }
}
