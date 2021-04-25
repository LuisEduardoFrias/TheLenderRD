using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheLenderRD.Domain.Dto;
using TheLenderRD.Domain.Services;
using TheLenderRD.Persistence.Repository;
using System.Net;

namespace TheLenderRD.WebApi.Controllers
{
    [Route("api/calculation")]
    [ApiController]
    public class CalculationController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> Quota([FromBody] QueryDto query)
        {
            var rates = await RepositoryAgeRate.GetInstance().Get();
            var months = await RepositoryMonth.GetInstance().Get();

            var rate = rates.FirstOrDefault(x => x.Age == Calculations.CalculateAge(query.DateOfBirth));

            if(rate == null || months == null)
            {
                return NotFound();
            }

            var month = months.FirstOrDefault(x => x.Value == query.LoanMonths);

            var AccountValue = Calculations.QuotaCalculation(query.LoanAmount, rate.Rate, month.Value);

            var resul = await RepositoryLog.GetInstance().Create(new Domain.Entitys.Log 
            {
                ConsultationDate = query.ConsultationDate,
                Edad = Calculations.CalculateAge(query.DateOfBirth),
                Amount = query.LoanAmount,
                AccountValue = AccountValue,
                QueryIp = GetIp(),
                MonthId = query.LoanMonths
            });

            if (resul)
                return NoContent();
            else
                return NotFound();

        }


        private string GetIp()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    return ip.ToString();
                }
            }

            return "000.000.000.000";
        }

    }
}
