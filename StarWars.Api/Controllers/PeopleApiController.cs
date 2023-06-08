using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using StarWars.Core.Entities;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using StarWars.Core.Core;
using StarWars.Core.Entities.Enums;
using System.Threading.Tasks;

namespace StarWars.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleApiController : ControllerBase
    {
        private readonly StarWarsApiCore swapiCore;
        private readonly StableDiffusionCore stableDiffusionCore;

        public PeopleApiController()
        {
            swapiCore = new StarWarsApiCore();
            stableDiffusionCore = new StableDiffusionCore();
        }

        [HttpGet]
        [Route("AllPeople")]
        public IActionResult GetAll()
        {
            EntityResults<People> allPeople = swapiCore.GetAllPeople();


            for (int i = 0; i < 10; i++)
            {
                int k = allPeople.results[i].homeworld.Trim().Length - 2;
                string l = allPeople.results[i].homeworld.Trim()[k].ToString();

                if (!(Convert.ToInt32(l) ==0))
                {
                allPeople.results[i].homeworld =
                    swapiCore.GetPlanet(l).name;
                }
                else
                {
                    allPeople.results[i].homeworld = string.Empty;
                }
            }

            List<People> people = allPeople.results;

            return Ok(people);
        }

        [HttpGet, Route("Detail")]
        public async Task<IActionResult> GetAsync(string id)
        {
            People people = swapiCore.GetPeople(id);
            Planet planet = swapiCore.GetPlanet(id);
            people.homeworld = planet.name;

            var prompt = $"Name: {people.name}\n" +
             $"Height: {people.height}\n" +
             $"Gender: {people.gender}\n" +
             $"Eye Color: {people.eye_color}\n" +
             $"Mass: {people.mass}\n" +
             $"Specie: {string.Join(", ", people.species)}\n" +
             $"Skin Color: {people.skin_color}\n";

            var photo = await stableDiffusionCore.SendPromptAsync(prompt);
            people.photo = photo;

            return Ok(people);
        }

        [HttpGet, Route("{gender}")]
        public IActionResult GetByGender(Gender gender)
        {
            EntityResults<People> allPeople = swapiCore.GetAllPeople();

            List<People> peopleByGender = allPeople.results.Where(g => g.gender == gender.ToString()).ToList();

            return Ok(peopleByGender);
        }
    }
}