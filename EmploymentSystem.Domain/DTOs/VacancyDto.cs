namespace EmploymentSystem.Domain.DTOs;

public class VacancyDto
{
    public int? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int MaxApplications { get; set; }
    public int CurrentApplications { get; set; }
    public bool IsClosed { get; set; }
    public DateTime ExpiryDate { get; set; }
    public Guid EmployerId { get; set; }
}
