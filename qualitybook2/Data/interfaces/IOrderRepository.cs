using qualitybook2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Data.interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
