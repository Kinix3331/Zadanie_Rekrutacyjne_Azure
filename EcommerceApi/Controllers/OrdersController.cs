using EcommerceApi.Data;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders (Pobieranie wszystkich zamówień wraz z produktami)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            // .Include(o => o.Products) sprawia, że Entity Framework "dociąga" relację
            return await _context.Orders.Include(o => o.Products).ToListAsync();
        }

        // GET: api/Orders/5 (Pobieranie jednego zamówienia)
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // Tworzymy pomocniczą klasę (DTO), żeby klient API musiał wysłać tylko ID produktów
        public class CreateOrderRequest
        {
            public List<int> ProductIds { get; set; } = new();
        }

        // POST: api/Orders (Tworzenie nowego zamówienia i przypisywanie produktów)
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] CreateOrderRequest request)
        {
            var newOrder = new Order
            {
                OrderDate = DateTime.UtcNow
            };

            // Wyciągamy z bazy tylko te produkty, których ID przyszły w żądaniu
            var products = await _context.Products
                .Where(p => request.ProductIds.Contains(p.Id))
                .ToListAsync();

            // Przypisujemy produkty do nowego zamówienia
            newOrder.Products.AddRange(products);

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
        }

        // PUT: api/Orders/5 (Aktualizacja zamówienia - zmiana listy produktów)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, [FromBody] CreateOrderRequest request)
        {
            // 1. Dociągamy zamówienie wraz z obecnymi produktami
            var order = await _context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            // 2. Pobieramy nowe produkty na podstawie przesłanych ID
            var newProducts = await _context.Products
                .Where(p => request.ProductIds.Contains(p.Id))
                .ToListAsync();

            // 3. Czyścimy starą listę i przypisujemy nową
            order.Products.Clear();
            order.Products.AddRange(newProducts);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Orders/5 (Usuwanie zamówienia)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}