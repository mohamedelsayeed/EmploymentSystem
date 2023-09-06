using EmploymentSystem.Domain.Common;

namespace EmploymentSystem.Domain.Entities;

public class Application : BaseEntity
{
    public int ApplicantId { get; set; }
    public int VacancyId { get; set; }
    public DateTime ApplicationDate { get; set; }

    public Vacancy Vacancy { get; set; }
}
