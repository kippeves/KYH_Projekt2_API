using KYHProjekt2API.DTO.Customer;

namespace KYHProjekt2API.DTO.TimeRegistration;

public class TimeRegDTO
{
    public int Id { get; set; }
    public CustomerDTO Customer { get; set; }
    public ProjectDTO Project { get; set; }
    public DateTime? EventStart { get; set; }
    public DateTime? EventEnd { get; set; }
    public string Description { get; set; }
}