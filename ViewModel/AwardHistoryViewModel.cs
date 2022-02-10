using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.ViewModel
{
    public class AwardHistoryViewModel
    {
        public long AwardId { get; set; }
        public string AwardName { get; set; }

        public int Point { get; set; }
        public DateTime Date { get; set; }
        public int Point { get; set; }
        public string Presenter { get; set; }

        public bool Status { get; set; }
    }
}
