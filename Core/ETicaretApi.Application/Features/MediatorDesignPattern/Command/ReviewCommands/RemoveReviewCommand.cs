using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Command.ReviewCommands
{
    public class RemoveReviewCommand : IRequest
    {
        public Guid Id { get; set; }

        public RemoveReviewCommand(Guid id)
        {
            Id = id;
        }
    }
}
