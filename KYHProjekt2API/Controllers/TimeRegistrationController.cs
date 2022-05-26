using KYHProjekt2API.Data;
using KYHProjekt2API.DTO.Customer;
using KYHProjekt2API.DTO.TimeRegistration;
using Microsoft.AspNetCore.Mvc;
using ProjectDTO = KYHProjekt2API.DTO.TimeRegistration.ProjectDTO;
using TimeRegDTO = KYHProjekt2API.DTO.TimeRegistration.TimeRegDTO;

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

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Uppdatera(int id, UpdateTimeRegDTO updateTimeRegDto)
        {
            var timereg = _context.TimeRegistrations.Find(id);
            if(timereg== null) return NotFound();

            var errors = new List<string>();
            var customer = _context.Customers.Find(updateTimeRegDto.CustomerID);
            var project = _context.Projects.Find(updateTimeRegDto.ProjectID);

            if(customer == null)
                errors.Add("Kund kunde ej hittas.");

            if(project == null)
                errors.Add("Projekt kunde ej hittas.");
            
            if(!DateTime.TryParse(updateTimeRegDto.EventStart, out var eventStart))
                errors.Add("Startdaturm är ej rätt formatterat.");
            
            if(!DateTime.TryParse(updateTimeRegDto.EventEnd, out var eventEnd))
                errors.Add("Slutdatum är ej rätt formatterat");

            timereg.Customer = customer;
            timereg.Project = project;

            if (errors.Any())
                return BadRequest(errors);
            
            timereg.Description = updateTimeRegDto.Description;
            timereg.EventStart = eventStart;
            timereg.EventEnd = eventEnd;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public IActionResult Skapa(CreateTimeRegDTO timereg)
        {
            var errors = new List<string>();
            
            var customer = _context.Customers.Find(timereg.CustomerID);
            var project = _context.Projects.Find(timereg.ProjectID);

            if (customer == null)
            {
                errors.Add("Kund kunde ej hittas.");
            }

            if(project == null)
                errors.Add("Projekt kunde ej hittas.");

            var start = DateTime.TryParse(timereg.EventStart, out var eventStart);
            var end = DateTime.TryParse(timereg.EventEnd, out var eventEnd);
            
            if (start && end) {
                if (eventStart > eventEnd)
                {
                    errors.Add("Slutdatum kan inte ligga före startdatum");
                }
            }
            
            if(!start)
                errors.Add("Startdaturm är ej rätt formatterat.");
            if(!end)
                errors.Add("Slutdatum är ej rätt formatterat");
            
            if(customer != null)
                if (project != null)
                {
                    _context.Entry(customer).Collection(e => e.Projects).Load();
                    if (!customer.Projects.Contains(project))
                    {
                        errors.Add("Projekt tillhör inte den valda kunden");
                    }
                }

            if(errors.Any())
                return BadRequest(errors);
            
            var inputTimeReg = new TimeRegistration()
            {
                Customer = customer,
                Project = project,
                Description = timereg.Description,
                EventStart = eventStart,
                EventEnd = eventEnd
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
        
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var registration = _context.TimeRegistrations.Find(id);
            if (registration == null) return NotFound("Registrering kunde inte hittas");
            registration.IsActive = false;
            _context.SaveChanges();
            return Ok();
        }
    }