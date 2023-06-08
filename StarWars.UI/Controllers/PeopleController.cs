using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StarWars.Core.Entities;
using static System.Reflection.Metadata.BlobBuilder;
using System.Net.Http.Json;
using StarWars.Core.Core;

namespace StarWars.UI.Controllers
{
    public class PeopleController : Controller
    {
        string baseAddress = "https://localhost:44311/api/";


        [Route("people")]
        public async Task<IActionResult> GetAll()
        {
            List<People> people = new List<People>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var response = await client.GetAsync("PeopleApi/AllPeople");


                if (response.IsSuccessStatusCode)
                {
                    var readData = await response.Content.ReadFromJsonAsync<List<People>>();

                    people = readData;

                    return View(people);
                }
                return View();
            }
        }

        [Route("people/detail/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            People person = new People();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var response = await client.GetAsync($"PeopleApi/Detail?id={id}");


                if (response.IsSuccessStatusCode)
                {
                    var readData = await response.Content.ReadFromJsonAsync<People>();

                    person = readData;

                    return View(person);
                }
                return View();
            }
        }

        [Route("people/{gender}")]
        public async Task<IActionResult> GetByGender(string gender)
        {
            List<People> people = new List<People>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var response = await client.GetAsync($"PeopleApi/{gender}");


                if (response.IsSuccessStatusCode)
                {
                    var readData = await response.Content.ReadFromJsonAsync<List<People>>();

                    people = readData;

                    return View(people);
                }
                return View();
            }
        }
    }
}