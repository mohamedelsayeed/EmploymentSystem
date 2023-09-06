using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Domain.DTOs
{
    public class ApplicationDto
    {
        public Guid ApplicantId { get; set; }
        public int VacancyId { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
