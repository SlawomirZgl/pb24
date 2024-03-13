using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    internal class BasketAddRequestDTO
    {
        public int ProductId { get; init; }
        public int UserId { get; init; }
        public int Amount { get; init; }
    }
}
