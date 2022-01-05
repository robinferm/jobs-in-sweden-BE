using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JIS_BE.Models
{
    public class JISDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CollectionName { get; set; } = null!;
        public string StatisticsCollection { get; set; }
        public string SearchHistoryCollection { get; set; }
    }
}
