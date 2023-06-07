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
            var apiKey = "muGWQYwn2hLauXg3bIJAUjDr5THfL5r60i9f4KPQMvqh8QTyxRpDJIbjzD9c";

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://stablediffusionapi.com/api/v3/dreambooth");

            // JSON veri oluşturma
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

            // JSON içeriğini hazırlama
            var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            // İstek ayarlarını güncelleme
            request.Content = jsonContent;

            // İsteği gönderme ve yanıtı alma
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // Yanıtı okuma
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
            var photoUrl = responseObject.output[0].Value;

            return photoUrl;
        }
    }
}
