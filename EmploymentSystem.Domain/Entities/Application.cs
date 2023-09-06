using EmploymentSystem.Domain.Common;

namespace EmploymentSystem.Domain.Entities;

public class Application : BaseEntity
{
    public Guid ApplicantId { get; set; }
    public int VacancyId { get; set; }
    public DateTime ApplicationDate { get; set; }
}
