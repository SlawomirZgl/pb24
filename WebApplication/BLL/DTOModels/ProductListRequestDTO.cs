using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class ProductListRequestDTO
    {
        string? sortBy { get; init; }
        bool? sortAscending { get; init; }
        bool? getNotActive { get; init; }
        string? filterByName { get; init; }
        string? filterByGroupName { get;init; }
        string? filterByGroupId { get; init; }
    }
}
