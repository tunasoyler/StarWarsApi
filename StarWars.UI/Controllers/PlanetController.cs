using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using StarWars.UI.Models;

namespace StarWars.UI.Controllers
{
    public class PlanetController : Controller
    {
        string baseAddress = "https://localhost:44444/api/";

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
        [Route("planet/detail/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Planet planet = new Planet();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var response = await client.GetAsync($"PlanetApi/Detail?id={id}");


                if (response.IsSuccessStatusCode)
                {
                    var readData = await response.Content.ReadFromJsonAsync<Planet>();

                    planet = readData;

                    return View(planet);
                }
                return View();
            }
        }


    }
}