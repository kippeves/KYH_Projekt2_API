using KYHProjekt2API.Data;
using KYHProjekt2API.DTO.Customer;
using KYHProjekt2API.DTO.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDTO = KYHProjekt2API.DTO.Project.ProjectDTO;

namespace KYHProjekt2API.Controllers;

    [Route("project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_context.Projects.Include(e=>e.TimeRegistrations).Select(e => new ProjectDTO()
            {
                Id = e.Id,
                Name = e.Name,
                Customer = new CustomerDTO()
                {
                    Id = e.Customer.Id,
                    Name = e.Customer.Name,
                },
                Registrations = e.TimeRegistrations.Select(reg =>
                new TimeRegDTO(){
                    Id = reg.Id,
                    Description = reg.Description,
                    EventStart = reg.EventStart,
                    EventEnd = reg.EventEnd
                }).ToList()
            }).ToList());
        }

        [Route("{id:int}")]
        public IActionResult GetOne(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null) return NoContent();
            _context.Entry(project).Reference(e=>e.Customer).Load();
            var returnItem = new ProjectDTO()
            {
                Id = project.Id,
                Name = project.Name,
                Customer = new CustomerDTO()
                {
                    Id = project.Customer.Id,
                    Name = project.Customer.Name
                }
            };
            return Ok(returnItem);
        }

        [Route("{id:int}/timereg")]
        public IActionResult GetTimeRegForProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null) return NotFound();
            _context.Entry(project).Collection(e => e.TimeRegistrations).Load();

            var filteredTimeRegList = project.TimeRegistrations.Select((timereg) => new TimeRegDTO(){
                Id = timereg.Id,
                Description = timereg.Description,
                EventStart = timereg.EventStart,
                EventEnd = timereg.EventEnd
            }).ToList();
            
            return Ok(filteredTimeRegList);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Uppdatera(int id, UpdateProjectDTO inputProject)
        {
            var project = _context.Projects.Find(id);
            var customer = _context.Customers.Find(inputProject.CustomerId);
            if ((project == null) || (customer == null)) return NotFound();
            project.Name = inputProject.Name;
            project.Customer = customer;
            _context.SaveChanges();
            return NoContent();
        }


        [HttpPost]
        public IActionResult Skapa(CreateProjectDTO project)
        {
            var customer = _context.Customers.Find(project.CustomerId);
            if (customer == null)
            {
                return NoContent();
            }
            _context.Entry(customer).Collection(e=>e.Projects).Load();
            var newProject = new Project()
            {
                Name = project.Name,
            };
            
            customer.Projects.Add(newProject);
            _context.SaveChanges();

            var madeProject = new ProjectDTO()
            {
                Id = newProject.Id,
                Name = newProject.Name,
                Customer = new CustomerDTO()
                {
                    Id = newProject.Customer.Id,
                    Name = newProject.Customer.Name
                }
            };
            return CreatedAtAction(nameof(GetOne), new {id = newProject.Id}, madeProject);
        }
    }