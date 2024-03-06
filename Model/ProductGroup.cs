using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class ProductGroup : IEntityTypeConfiguration<ProductGroup>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public ProductGroup? Parent { get; set; }
        public ICollection<Product>? Products { get; set; }
        public ICollection<ProductGroup>? childGroups { get; set; }
        public void Configure(EntityTypeBuilder<ProductGroup> builder)
        {
            builder.ToTable("ProductGroups");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Parent)
                .WithMany(x => x.childGroups)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.SetNull);
            
        }
    }
}
