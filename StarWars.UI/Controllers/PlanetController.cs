using Microsoft.AspNetCore.Mvc;
using StarWars.Core.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace StarWars.UI.Controllers
{
    public class PlanetsController : Controller
    {
        string baseAddress = "https://localhost:44311/api/";

        [Route("planets")]
        public async Task<IActionResult> GetAll()
        {
            List<Planet> planets = new List<Planet>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var response = await client.GetAsync("PlanetApi/AllPlanets");


                if (response.IsSuccessStatusCode)
                {
                    var readData = await response.Content.ReadFromJsonAsync<List<Planet>>();

                    planets = readData;

                    return View(planets);
                }
                return View();
            }
        }
        public async Task<IActionResult> Get(int id)
        {
            return View();
        }


    }
}