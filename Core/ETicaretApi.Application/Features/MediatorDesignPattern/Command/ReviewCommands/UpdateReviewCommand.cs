using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Command.ReviewCommands
{
    public class UpdateReviewCommand : IRequest
    {
        public Guid ReviewID { get; set; }
        public Guid ProductID { get; set; }
        public Guid UserID { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
