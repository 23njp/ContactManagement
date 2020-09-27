using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Entity.Model
{
    public class DataTablesHelper
    {
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int Pagesize { get; set; }
        public int Skip { get; set; }
        public int TotalCount { get; set; }
        public List<Filter> Filters { get; set; }
    }
}
