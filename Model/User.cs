using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public enum UserType
    {
        Admin,
        Casual
    }

    public class User : IEntityTypeConfiguration<User>
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public bool IsActive { get; set; }
        public int? GroupId { get; set; }
        public UserGroup? Group { get; set; }
        public ICollection<BasketPosition> BasketPositions { get; set; }
        public ICollection<Order> Orders { get; set; }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.HasOne(u => u.Group)
                .WithMany(g => g.Users)
                .HasForeignKey(u=>u.GroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
