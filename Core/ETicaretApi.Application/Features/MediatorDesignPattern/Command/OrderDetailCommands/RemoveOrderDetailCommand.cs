using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Command.OrderDetailCommands
{
    public class RemoveOrderDetailCommand : IRequest
    {
        public Guid OrderDetailID { get; set; }

        public RemoveOrderDetailCommand(Guid orderDetailID)
        {
            OrderDetailID = orderDetailID;
        }
    }
}
