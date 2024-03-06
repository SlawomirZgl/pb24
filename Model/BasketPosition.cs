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
    public class BasketPosition : IEntityTypeConfiguration<BasketPosition>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Amount { get; set; }

        public void Configure(EntityTypeBuilder<BasketPosition> builder)
        {
            builder.ToTable("BasketPositions");
            builder.HasKey(x => x.Id);

            builder.HasOne(bp => bp.Product)
                .WithMany(p => p.BasketPositions)
                .HasForeignKey(bp => bp.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bp => bp.User)
                .WithMany(u => u.BasketPositions)
                .HasForeignKey(bp => bp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
