using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notificationlistener.dapr.Models
{
    public class NotificationMessage
    {
        public int Id { get; set; }
        public string CreditCardNo { get; set; }
        public string Name { get; set; }
        public int IdAuction { get; set; }
        public string BidUser { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string CorrelationId { get; set; }
    }

}
