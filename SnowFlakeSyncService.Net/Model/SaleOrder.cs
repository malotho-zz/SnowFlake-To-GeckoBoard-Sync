using System;

namespace SnowFlakeSyncService.Net.Model
{
    public class SaleOrder
    {
        public long ORDERID { get; set; }
        public DateTimeOffset ORDERDATE { get; set; }
        public long USERID { get; set; }
        public long BRANDID { get; set; }
        public long CAMPAIGNID { get; set; }
        public long DIALERAGENTID { get; set; }
        public string COMMENTS { get; set; }
        public string BASKETREFERENCE { get; set; }
        public DateTimeOffset BILLINGDATE { get; set; }
    }
}
