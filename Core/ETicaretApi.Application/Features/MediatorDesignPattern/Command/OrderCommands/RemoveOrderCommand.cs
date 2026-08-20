using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Command.OrderCommands
{
    public class RemoveOrderCommand : IRequest
    {
        public Guid OrderID { get; set; }
        public RemoveOrderCommand(Guid orderID)
        {
            OrderID = orderID;
        }


    }
}
