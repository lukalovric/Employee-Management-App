using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Common
{
    public class Filter
    {
        public string SearchSurname { get; set; }
        public string SearchName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? ProjectId { get; set; }
    }

}
