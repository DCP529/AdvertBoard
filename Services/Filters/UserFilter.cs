using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Filters
{
    public class UserFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
