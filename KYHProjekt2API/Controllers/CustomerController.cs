using KYHProjekt2API.Data;
using KYHProjekt2API.DTO.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
        public IActionResult Index()
        {
            return Ok(_context.Customers.Where(customer=> customer.IsActive == true).Select(e => new CustomerDTO()
            {
                Id = e.Id,
                Name = e.Name
            }).ToList());
        }

        [Route("{id:int}")]
        public IActionResult GetOne(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound("Kund kunde inte hittas.");
            if (!customer.IsActive) return NotFound("Kund kunde inte hittas");
            
            var returnItem = new CustomerDTO()
            {
                Id = customer.Id,
                Name = customer.Name,
            };
            
            return Ok(returnItem);
        }

        [Route("{id:int}/projects")]
        public IActionResult GetRegistrationsForCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound("Kund kunde inte hittas");
            if (!customer.IsActive) return NotFound("Kund kunde inte hittas");
            
            _context.Entry(customer)
                .Collection(e => e.Projects)
                .Load();
            
            _context.Entry(customer)
                .Collection(e=> e.Projects)
                .Query().Include(e=>e.TimeRegistrations)
                .Load();

            var projectList = customer.Projects.Select((proj) => new ProjectDTO()
            {
                Id = proj.Id,
                Name = proj.Name,
                Registration = proj.TimeRegistrations.Select(reg => new TimeRegDTO()
                {
                    Id = reg.Id,
                    Description = reg.Description,
                    EventEnd = reg.EventEnd,
                    EventStart = reg.EventStart,
                }).ToList()
            }).ToList();

            return Ok(projectList);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Uppdatera(int id, UpdateCustomerDTO inputCustomer)
        {
            var customer = _context.Customers.Find(id);
            if(customer== null) return NotFound("Kund kunde inte hittas.");
            if (!customer.IsActive) return NotFound("Kund kunde inte hittas");
                
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

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return BadRequest("Kund kunde inte hittas.");
            
            _context.Entry(customer).Collection(e => e.Projects).Load();
            _context.Entry(customer).Collection(e => e.Projects)
                .Query()
                .Include(e => e.TimeRegistrations)
                .Load();
            customer.IsActive = false;
            customer.Projects.ForEach(proj => proj.IsActive = false);
            customer.Projects.ForEach(proj => proj.TimeRegistrations.ForEach(tr => tr.IsActive = false));
            _context.SaveChanges();
            return Ok();
        }
    }