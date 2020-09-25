using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountryFriend.Repository.Mapping
{
    public class CountryMap : IEntityTypeConfiguration<Domain.Country.Country>
    {
        public void Configure(EntityTypeBuilder<Domain.Country.Country> builder)
        {
            builder.ToTable("Country");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.URL).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Filename).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(25);
            builder.HasMany<Domain.State.State>(x => x.States).WithOne(x => x.Country);
        }
    }
}
