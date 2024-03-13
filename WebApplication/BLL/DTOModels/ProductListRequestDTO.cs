using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class ProductListRequestDTO
    {
        public string? orderBy { get; init; }
        public bool? orderAscending { get; init; }
        public bool? getNotActive { get; init; }
        public string? filterByName { get; init; }
        public string? filterByGroupName { get; init; }
        public string? filterByGroupId { get; init; }
    }
}
