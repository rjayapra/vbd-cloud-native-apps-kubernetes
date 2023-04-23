using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PaymentService.Models
{
    public class PaymentServiceContext : DbContext
    {
        public PaymentServiceContext (DbContextOptions<PaymentServiceContext> options)
            : base(options)
        {
        }

        public DbSet<PaymentService.Models.AuctionPayment> AuctionPayment { get; set; }
    }
}
