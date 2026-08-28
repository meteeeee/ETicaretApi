using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.MediatorDesignPattern.Results.ReviewResults
{
    public class getReviewByIdQueryResult
    {
        public Guid ReviewID { get; set; }
        public Guid ProductID { get; set; }
        public Guid UserID { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
