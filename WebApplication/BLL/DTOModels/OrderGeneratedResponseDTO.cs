using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class OrderGeneratedResponseDTO
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public ICollection<OrderPositionResponseDTO> OrderPositions { get; init; }
    }
}
