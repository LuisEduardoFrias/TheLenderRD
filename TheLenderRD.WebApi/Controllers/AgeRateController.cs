using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLenderRD.Domain.Dto;
using TheLenderRD.Persistence.Repository;

namespace TheLenderRD.WebApi.Controllers
{
    [Route("api/agerate")]
    [ApiController]
    public class AgeRateController
    {
        [HttpGet]
        public async Task<ActionResult<List<AgeRageDto>>> Get() => await RepositoryAgeRate.GetInstance().Get();
    }
}
