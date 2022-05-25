using KYHProjekt2API.Data;
using KYHProjekt2API.DTO.Customer;
using Microsoft.AspNetCore.Mvc;

namespace KYHProjekt2API.Controllers;

    [Route("customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_context.Customers.Select(e => new CustomerDTO()
            {
                Id = e.Id,
                Name = e.Name
            }).ToList());
        }

        [Route("{id:int}")]
        public IActionResult GetOne(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();
            var returnItem = new CustomerDTO()
            {
                Id = customer.Id,
                Name = customer.Name,
            };
            
            return Ok(returnItem);
        }

        [Route("{id:int}/projects")]
        public IActionResult GetProjectsForCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();
            _context.Entry(customer).Collection(e => e.Projects).Load();

            var projectList = customer.Projects.Select((proj) => new ProjectDTO()
            {
                Id = proj.Id,
                Name = proj.Name,
            }).ToList();
            
            return Ok(projectList);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Uppdatera(int id, UpdateCustomerDTO inputCustomer)
        {
            var customer = _context.Customers.Find(id);
            if(customer== null) return NotFound();

            customer.Name = inputCustomer.Name;
            _context.SaveChanges();

            return NoContent();
        }


        [HttpPost]
        public IActionResult Skapa(CreateCustomerDTO customer)
        {
            var createCustomer = new Customer()
            {
                
                Name = customer.Name
                
            };
            _context.Customers.Add(createCustomer);
            _context.SaveChanges();

            var customerDto = new CustomerDTO()
            {
                Id = createCustomer.Id,
                Name = createCustomer.Name,
            };
            return CreatedAtAction(nameof(GetOne), new {id = createCustomer.Id}, customerDto);
        }
    }