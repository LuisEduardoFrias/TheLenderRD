
using System;

namespace TheLenderRD.Domain.Dto
{
    public class QueryDto : Error
    {
        public DateTime ConsultationDate { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal LoanAmount { get; set; }

        public int LoanMonths { get; set; }
    }
}
