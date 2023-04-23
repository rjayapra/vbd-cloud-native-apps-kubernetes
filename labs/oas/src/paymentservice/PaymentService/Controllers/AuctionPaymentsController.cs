using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentService.Models;
using PaymentService.Services;
namespace PaymentService.Controllers
{
    [Route("api/payments/[controller]")]
    [ApiController]
    public class AuctionPaymentsController : ControllerBase
    {
        private readonly PaymentServiceContext _context;
        private readonly Logger _logger;
        private readonly KafkaService _service;
        public AuctionPaymentsController(PaymentServiceContext context, Logger logger, KafkaService service)
        {
            _context = context;
            _logger = logger;
            _service = service;
        }

        // GET: api/AuctionPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionPayment>>> GetAuctionPayment()
        {
            return await _context.AuctionPayment.ToListAsync();
        }

        // GET: api/AuctionPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionPayment>> GetAuctionPayment(int id)
        {
            var auctionPayment = await _context.AuctionPayment.FindAsync(id);

            if (auctionPayment == null)
            {
                return NotFound();
            }

            return auctionPayment;
        }

        // PUT: api/AuctionPayments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuctionPayment(int id, AuctionPayment auctionPayment)
        {
            if (id != auctionPayment.Id)
            {
                return BadRequest();
            }

            _context.Entry(auctionPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionPaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AuctionPayments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AuctionPayment>> PostAuctionPayment(AuctionPayment auctionPayment)
        {

            string ocelotRequestId = Request.Headers["OcRequestId"];
            try
            {
                auctionPayment.CorrelationId = ocelotRequestId;
                auctionPayment.PaymentDate = DateTime.Now;
                auctionPayment.PaymentStatus = 2;

                _context.AuctionPayment.Add(auctionPayment);
                await _context.SaveChangesAsync();

                _logger.LogMessage("Saved auction payment information");
                //Send to Kafka 

                _service.SendMessage(auctionPayment);

                _logger.LogMessage("Sent auction payment information to Payment topic");

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                _logger.LogMessage(ex.StackTrace);
            }

            return CreatedAtAction("GetAuctionPayment", new { id = auctionPayment.Id }, auctionPayment);
        }

        // DELETE: api/AuctionPayments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuctionPayment>> DeleteAuctionPayment(int id)
        {
            var auctionPayment = await _context.AuctionPayment.FindAsync(id);
            if (auctionPayment == null)
            {
                return NotFound();
            }

            _context.AuctionPayment.Remove(auctionPayment);
            await _context.SaveChangesAsync();

            return auctionPayment;
        }

        private bool AuctionPaymentExists(int id)
        {
            return _context.AuctionPayment.Any(e => e.Id == id);
        }
    }
}
