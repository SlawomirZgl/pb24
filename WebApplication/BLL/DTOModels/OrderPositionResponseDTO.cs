using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class OrderPositionResponseDTO
    {
        public int Id { get; init; }
        public int? OrderId { get; init; }
        public int Amount { get; init; }
        public double Price { get; init; }

    }
}
