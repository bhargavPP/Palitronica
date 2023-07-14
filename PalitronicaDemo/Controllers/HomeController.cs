using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PalitronicaDemo.Model;
using PalitronicaDemo.ViewModels;
using System.Text.Json;

namespace PalitronicaDemo.Controllers
{
    //[ApiController]
    //[Route("api/payment")]
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HomeController(MyDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }



        public IActionResult Index()
        {
            return View("InputForm");
        }

        [HttpGet("/Home/GetCustomers")]
        public async Task<ICollection<Customer>> GetCustomers()
        {
            var Customers = await _context.Customers.ToListAsync();

            return Customers;
        }

        [HttpGet("/Home/GetItems")]
        public async Task<ICollection<Item>> GetItems()
        {
            var items = await _context.Items.ToListAsync();

            return items;
        }

        [HttpPost]
        public async Task<IActionResult> CalculatePayment(Customer request)
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null)
                return BadRequest("Customer not found.");

            // Calculate total price and taxes
            decimal totalPrice = 0.0m;
            decimal totalTaxes = 0.0m;
            Dictionary<string, decimal> itemPrices = new Dictionary<string, decimal>();

            foreach (var item in request.Items)
            {
                // Retrieve item details from the external API based on the item ID
                var itemDetails = await GetItemTaxes(customer.zip);
                if (itemDetails == null)
                    return BadRequest($"Item with ID {item.ItemId} not found.");

                decimal itemPrice = item.Price * item.Quantity;
                decimal itemTaxes = itemPrice * Convert.ToDecimal(itemDetails.combined_rate);

                itemPrices.Add(item.ItemId, itemPrice);
                totalPrice += itemPrice;
                totalTaxes += itemTaxes;
            }

            decimal totalPriceWithTaxes = totalPrice + totalTaxes;

            // Create the payment response
            var customerResponse = new CustomerVM
            {
                CustomerName = customer.CustomerName,
                // TotalItemsPrice = itemPrices,
                TotalTaxes = totalTaxes,
                TotalPrice = totalPriceWithTaxes
            };

            return Ok(customerResponse);
        }

        private async Task<ApiResponse> GetItemTaxes(string Zip)
        {
            // Make an API request to retrieve item details based on the item ID
            // Replace the API endpoint and logic with your actual implementation
            // You can use HttpClient or any other HTTP client library

            var httpClient = _httpClientFactory.CreateClient();
            var apiUrl = $"https://api.example.com/items/{Zip}";

            var response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Parse the response to extract the item details
                // Adapt the code based on the structure of the API response
                // Deserialize the content into an ItemDetails object
                var itemDetails = JsonSerializer.Deserialize<ApiResponse>(content);
                return itemDetails;
            }

            return null;
        }

    }
}
