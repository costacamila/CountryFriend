using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountryFriend.Repository.Mapping
{
    public class StateMap : IEntityTypeConfiguration<Domain.State.State>
    {
        public void Configure(EntityTypeBuilder<Domain.State.State> builder)
        {
            builder.ToTable("State");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.URL).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Filename).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(25);
            builder.Property(x => x.CountryName).IsRequired().HasMaxLength(25);
            builder.HasOne<Domain.Country.Country>(x => x.Country);
        }
    }
}
