using System.ComponentModel.DataAnnotations;

namespace KYHProjekt2API.DTO.TimeRegistration;

public class UpdateTimeRegDTO
{
    [Required(ErrorMessage = "Ett kund krävs")]
    public int CustomerID { get; set; }

    [Required(ErrorMessage = "Ett projekt krävs")]
    public int ProjectID { get; set; }

    [Required(ErrorMessage = "Det krävs en starttid")]
    public string EventStart { get; set; }

    [Required(ErrorMessage = "Det krävs en sluttid")]
    public string EventEnd { get; set; }

    public string Description { get; set; }
}