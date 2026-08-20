using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Commands.ProductCommands
{
    public class RemoveProductCommand
    {
        public RemoveProductCommand(Guid productID)
        {
            ProductID = productID;
        }

        public Guid ProductID { get; set; }
    }
}
