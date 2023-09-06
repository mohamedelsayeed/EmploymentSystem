using EmploymentSystem.Domain.Common;

namespace EmploymentSystem.Domain.Entities;

public class Vacancy : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int MaxApplications { get; set; }
    public int CurrentApplications { get; set; }
    public bool IsClosed { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int EmployerId { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsArchived { get; set; }

    public User Employer { get; set; }
    public ICollection<Application> Applications { get; set; }
}
