using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Dto.Dtos.AdminReviewDtos
{
    public class AdminResultReviewDto
    {
        public Guid ReviewID { get; set; }
        public Guid ProductID { get; set; }
        public string? ProductName { get; set; }
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
