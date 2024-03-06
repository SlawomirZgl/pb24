using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class OrderPosition : IEntityTypeConfiguration<OrderPosition>
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        public void Configure(EntityTypeBuilder<OrderPosition> builder)
        {
            builder.ToTable("OrderPositions");
            builder.HasKey(x => x.Id);

            builder.HasOne(op => op.Order)
                .WithMany(o=> o.OrderPositions)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
