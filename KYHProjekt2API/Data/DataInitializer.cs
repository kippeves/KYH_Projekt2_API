using Microsoft.EntityFrameworkCore;

namespace KYHProjekt2API.Data;

public class DataInitializer
{
    private readonly ApplicationDbContext _context;

    public DataInitializer(ApplicationDbContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        _context.Database.Migrate();
        SeedCustomers();
    }

    private void SeedCustomers()
    {
        var date = DateTime.Now;
        if (!_context.Customers.Any(e => e.Name == "IntelliCode Inc."))
        {
            var cust = new Customer
            {
                Name = "IntelliCode Inc.",
                Projects = new List<Project>(),
                TimeRegistrations = new List<TimeRegistration>(),
                IsActive = true
            };

            var proj = new Project
            {
                Name = "Ny hemsida till företaget",
                IsActive = true
            };

            var reg = new TimeRegistration
            {
                Customer = cust,
                Project = proj,
                EventStart = date.AddYears(-2),
                EventEnd = date.AddYears(-2).AddHours(4),
                Description = "Första möte med kunden, alla var glada.",
                IsActive = true
            };

            cust.Projects.Add(proj);
            cust.TimeRegistrations.Add(reg);
            _context.Customers.Add(cust);
        }

        if (!_context.Customers.Any(e=>e.Name == "Stockholm MegaHackers"))
        {
            var cust = new Customer
            {
                Name = "Stockholm MegaHackers",
                Projects = new List<Project>(),
                TimeRegistrations = new List<TimeRegistration>(),
                IsActive = true
            };

            var proj = new Project
            {
                Name = "Fixade ny Email-tjänst",
                IsActive = true
            };

            var reg = new TimeRegistration
            {
                Customer = cust,
                Project = proj,
                EventStart = date.AddYears(-2),
                EventEnd = date.AddYears(-2).AddHours(4),
                Description = "Det ser ut att bli ett bra samarbete.",
                IsActive = true
            };

            cust.Projects.Add(proj);
            cust.TimeRegistrations.Add(reg);
            _context.Customers.Add(cust);
        }

        _context.SaveChanges();
    }
}