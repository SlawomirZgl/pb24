using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    internal class ProductAddRequestDTO
    {
        public string Name { get; init; }
        public double Price { get; init; }
        public int? GroupId { get; init; }
    }
}
