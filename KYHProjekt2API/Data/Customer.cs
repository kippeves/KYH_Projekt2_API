using System.ComponentModel.DataAnnotations;

namespace KYHProjekt2API.Data;

public class Customer
{
    public int Id { get; set; }

    [Required] [MaxLength(100)] public string Name { get; set; }
    public List<Project> Projects { get; set; } = new();
    public List<TimeRegistration> TimeRegistrations { get; set; } = new();
    public bool IsActive { get; set; } = true;
}