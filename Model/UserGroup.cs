using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class UserGroup : IEntityTypeConfiguration<UserGroup>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User>? Users { get; set; }

        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("UserGroups");
            builder.HasKey(x => x.Id); 
        }
    }
}
