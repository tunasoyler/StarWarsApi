using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StarWars.Core.Entities;
using System.Net.Http;
using System;
using System.Collections.Generic;
using StarWars.Core.Core;
using StarWars.Core.Entities.Enums;
using System.Linq;

namespace StarWars.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetApiController : ControllerBase
    {
        private readonly StarWarsApiCore swapiCore;

        public PlanetApiController()
        {
            swapiCore = new StarWarsApiCore();
        }

        [HttpGet, Route("AllPlanets")]
        public IActionResult GetAll()
        {
            EntityResults<Planet> allPlanets = swapiCore.GetAllPlanets();

            List<Planet> planet = allPlanets.results;

            return Ok(planet);
        }

        [HttpGet, Route("Detail")]
        public IActionResult Get(string id)
        {
            Planet planet = swapiCore.GetPlanet(id);

            return Ok(planet);
        }

        [HttpGet, Route("{climate}")]
        public IActionResult GetByClimate(Climate climate)
        {
            EntityResults<Planet> allPlanets = swapiCore.GetAllPlanets();

            List<Planet> planetByClimate = allPlanets.results.Where(g => g.climate == climate.ToString()).ToList();

            return Ok(planetByClimate);
        }


    }
}