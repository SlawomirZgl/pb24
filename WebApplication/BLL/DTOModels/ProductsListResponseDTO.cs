using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    internal class ProductsListResponseDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public double Price { get; init; }
        public bool IsActive { get; init; }
        public int? GroupId { get; init; }
        public string? GroupName { get; init; }
    }
}
