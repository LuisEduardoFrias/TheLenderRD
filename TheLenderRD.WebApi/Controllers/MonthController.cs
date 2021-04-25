using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLenderRD.Domain.Dto;
using TheLenderRD.Persistence.Repository;

namespace TheLenderRD.WebApi.Controllers
{
    [Route("api/month")]
    [ApiController]
    public class MonthController
    {
        [HttpGet]
        public async Task<ActionResult<List<MonthDto>>> Get() => await RepositoryMonth.GetInstance().Get();

    }
}
