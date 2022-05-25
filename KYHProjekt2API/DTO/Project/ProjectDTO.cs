using KYHProjekt2API.DTO.Customer;

namespace KYHProjekt2API.DTO.Project;

public class ProjectDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CustomerDTO Customer { get; set; }
    public List<TimeRegDTO>? Registrations { get; set; }
}