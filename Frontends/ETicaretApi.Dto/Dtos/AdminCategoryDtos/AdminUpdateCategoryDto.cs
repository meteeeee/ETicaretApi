using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Dto.Dtos.AdminCategoryDtos
{
    public class AdminUpdateCategoryDto
    {
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
