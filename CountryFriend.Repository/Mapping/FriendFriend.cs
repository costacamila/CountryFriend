using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountryFriend.Repository.Mapping
{
    public class FriendFriendMap : IEntityTypeConfiguration<Domain.Friend.FriendFriend>
    {
        public void Configure(EntityTypeBuilder<Domain.Friend.FriendFriend> builder)
        {
            builder.ToTable("FriendFriend");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.URL).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Filename).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(75);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Birthday).IsRequired();
            builder.Property(x => x.Country).IsRequired().HasMaxLength(25);
            builder.Property(x => x.State).IsRequired().HasMaxLength(25);
            builder.Property(x => x.FriendName).IsRequired().HasMaxLength(50);
            builder.HasOne<Domain.Friend.Friend>(x => x.Friend);
        }
    }
}
