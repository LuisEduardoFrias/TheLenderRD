using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheLenderRD.Domain.Dto;
using TheLenderRD.Domain.Services;

namespace TheLenderRD.Presentation.Controllers
{
    public class CalcularController : Controller
    {
        private readonly IConfiguration _configuration;

        public CalcularController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET: CalcularController
        [HttpGet]
        public async Task<ActionResult> Cuotas()
        {
            ViewBag.Months = await ConsumeApi.ConsumeApi
                .GetInstance(_configuration)
                .CallApiGETAsync<MonthDto>("api/month");

            return View();
        }

        // POST: CalcularController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cuotas(QueryDto query)
        {
            var Rates = await ConsumeApi.ConsumeApi
                .GetInstance(_configuration)
                .CallApiGETAsync<AgeRageDto>("api/agerate");

            var age = Calculations.CalculateAge(query.DateOfBirth);

            var higher = Rates.Where(x => x.Age > age).ToList();
            var less = Rates.Where(x => x.Age <= age).ToList();

            ViewBag.Months = await ConsumeApi.ConsumeApi
                    .GetInstance(_configuration)
                    .CallApiGETAsync<MonthDto>("api/month");

            if (higher.Count != 0 && less.Count != 0)
            {
                query.ConsultationDate = DateTime.Now;

                var request = await ConsumeApi.ConsumeApi
                   .GetInstance(_configuration).CallApiPOSTAsync<QueryDto>("api/Calculation", query);

                if (request == "InternalServerError")
                {
                    ViewBag.Error = "Error en el servidor.";

                    return View(query);
                }
                else if (request == "NotFound")
                {
                    ViewBag.Error = "El recurso que esta solicitando no se a encontrado.";

                    return View(query);
                }
                else if (request == "BadRequest")
                {
                    ViewBag.Error = "Peticion incorrecta.";

                    return View();
                }
                else if (request == "NoContent")
                {
                    ViewBag.Successful = "Cálculo exitoso";

                    return View();
                }
            }

            if (higher.Count == 0)
                ViewBag.Error = "Favor pasar por una de nuestras sucursales para evaluar su caso.";
            else if (less.Count == 0)
                ViewBag.Error = "Lo sentimos aun no cuenta con la edad para solicitar este producto";

            return View(query);
        }
    }
}
