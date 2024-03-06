using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class Product : IEntityTypeConfiguration<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }
        public int? GroupId { get; set; }
        public ProductGroup? Group { get; set; }
        public ICollection<BasketPosition>? BasketPositions { get; set; }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
           
            builder.HasOne(p=>p.Group)
                .WithMany(g=>g.Products)
                .HasForeignKey(p=>p.GroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
