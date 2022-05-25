using KYHProjekt2API.Data;
using KYHProjekt2API.DTO.Customer;
using KYHProjekt2API.DTO.TimeRegistration;
using Microsoft.AspNetCore.Mvc;
using static System.DateTime;
using ProjectDTO = KYHProjekt2API.DTO.TimeRegistration.ProjectDTO;

namespace KYHProjekt2API.Controllers;

    [Route("/timereg")]
    [ApiController]
    public class TimeRegistrationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TimeRegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_context.TimeRegistrations.Select(e => new TimeRegDTO()
            {
                Id = e.Id,
                Customer = new CustomerDTO()
                {
                    Id = e.Customer.Id,
                    Name = e.Customer.Name
                },
                Project = new ProjectDTO()
                {
                    Id = e.Project.Id,
                    Name = e.Project.Name
                },
                EventStart = e.EventStart,
                EventEnd = e.EventEnd,
                Description = e.Description,
            }).ToList());
        }

        [Route("customer/{id:int}")]
        public IActionResult FilterPerCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();
            _context.Entry(customer).Collection((e => e.TimeRegistrations));

            var filteredTimeRegistrations = customer.TimeRegistrations.Select(timeRegistration => new TimeRegDTO()
            {
                Id = timeRegistration.Id,
                Description = timeRegistration.Description,
                EventStart = timeRegistration.EventStart,
                EventEnd = timeRegistration.EventEnd,
                Project = new ProjectDTO()
                {
                    Id = timeRegistration.Project.Id,
                    Name = timeRegistration.Project.Name
                }
            }).ToList();
            
            return Ok(filteredTimeRegistrations);
        }
        
        [Route("{id:int}")]
        public IActionResult GetOne(int id)
        {
            var timereg = _context.TimeRegistrations.Find(id);
            if (timereg == null) return NotFound();

            var returnItem = new TimeRegDTO()
            {
                Id = timereg.Id,
                Customer = new CustomerDTO()
                {
                    Id = timereg.Customer.Id,
                    Name = timereg.Customer.Name
                },
                Project = new ProjectDTO()
                {
                    Id = timereg.Project.Id,
                    Name = timereg.Project.Name
                },
                EventStart = timereg.EventStart,
                EventEnd = timereg.EventEnd,
                Description = timereg.Description
            };
            
            return Ok(returnItem);
        }


        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Uppdatera(int id, UpdateTimeRegDTO updateTimeRegDto)
        {
            var timereg = _context.TimeRegistrations.Find(id);
            if(timereg== null) return NotFound();
            
            timereg.Customer = _context.Customers.Find(updateTimeRegDto.CustomerID);
            timereg.Project = _context.Projects.Find(updateTimeRegDto.ProjectID);
            timereg.Description = updateTimeRegDto.Description;
            timereg.EventStart = updateTimeRegDto.EventStart;
            timereg.EventEnd = updateTimeRegDto.EventEnd;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public IActionResult Skapa(CreateTimeRegDTO timereg)
        {
            var inputTimeReg = new TimeRegistration()
            {
                Customer = _context.Customers.Find(timereg.CustomerID),
                Project = _context.Projects.Find(timereg.ProjectID),
                Description = timereg.Description,
                EventStart = Parse(timereg.EventStart),
                EventEnd = Parse(timereg.EventEnd)
            };
            _context.TimeRegistrations.Add(inputTimeReg);
            _context.SaveChanges();

            var createdTimeReg = new TimeRegDTO()
            {
                Id = inputTimeReg.Id,
                Customer = new CustomerDTO()
                {
                    Id = inputTimeReg.Customer.Id,
                    Name = inputTimeReg.Customer.Name
                },
                Project = new ProjectDTO()
                {
                    Id = inputTimeReg.Project.Id,
                    Name = inputTimeReg.Project.Name
                },
                Description = inputTimeReg.Description,
                EventStart = inputTimeReg.EventStart,
                EventEnd = inputTimeReg.EventEnd
            };
            return CreatedAtAction(nameof(GetOne), new {id = inputTimeReg.Id}, createdTimeReg);
        }
    }