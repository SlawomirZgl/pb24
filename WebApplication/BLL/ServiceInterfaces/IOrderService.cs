using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    internal interface IOrderService
    {

        OrderGeneratedResponseDTO generateOrder(int userId);

        bool PayForOrder(double amount);
    }
}
