using BERTTokenizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BERTTokenizers;
using Microsoft.ML.Data;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace StarWars.Core.Core
{
    public class StableDiffusionCore
    {
        public async Task<string> SendPromptAsync(string customPrompt)
        {
            var apiKey = "YLSfzsWAEVGoyRYN7XV41Z1OBR2aEJVj4B5byIEjg3SDbOR6oYMxtdCOnWJa//";

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://stablediffusionapi.com/api/v3/dreambooth");

                // Setting the JSON data
                var data = new
                {
                    key = apiKey,
                    model_id = "midjourney",
                    prompt = customPrompt,
                    negative_prompt = "",
                    width = "512",
                    height = "512",
                    samples = "1",
                    num_inference_steps = "30",
                    seed = "",
                    guidance_scale = 7.5,
                    webhook = "",
                    track_id = ""
                };

                // JSON serializing
                var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                // Adding JSON prompt to Request
                request.Content = jsonContent;

                // Requesting then getting Respond
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Reading the Response and getting Photo URL from it
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                var photoUrl = responseObject.output[0].Value;

                return photoUrl;
            }
            catch (Exception ex)
            {                
                return "https://upload.wikimedia.org/wikipedia/commons/6/6c/Star_Wars_Logo.svg";
            }
        }
    }
}
