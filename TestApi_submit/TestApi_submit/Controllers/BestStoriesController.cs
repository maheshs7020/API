
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TestApi_submit.Model;
using Microsoft.Extensions.Caching.Memory;
namespace TestApi_submit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestStoriesController : Controller
    {

        private readonly IHttpClientFactory _clientFactory;

        public BestStoriesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<List<BestStory>>> GetBestStories(Int32 n)
        {
            
            var client = _clientFactory.CreateClient();
            var bestStoryIds = await GetBestStoryIdsAsync(client, n);
            var bestStories = await GetBestStoriesDetailsAsync(client, bestStoryIds);

            return bestStories;
        }

        private async Task<List<Int32>> GetBestStoryIdsAsync(HttpClient client, Int32 n)
        {
            var response = await client.GetStringAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
            //var bestStoryIds = JsonConvert.DeserializeObject<List<Int32>>(response);
            List<Int32> bestStoryIds = new List<Int32>();
            foreach (Int32 obj in response)
            {

                bestStoryIds.Add(Convert.ToInt32(obj));
            }
            return bestStoryIds.Take(n).ToList();
        }

        private async Task<List<BestStory>> GetBestStoriesDetailsAsync(HttpClient client, List<Int32> storyIds)
        {
            var stories = new List<BestStory>();
            foreach (var id in storyIds)
            {
                var response = await client.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
                dynamic storyDetails = JsonConvert.DeserializeObject(response);

                //foreach (var obj in storyDetails)
                //{
                stories.Add(
                   new BestStory
                   {
                       Title = storyDetails.by,
                       CommentCount = storyDetails.descendants,
                       PostedBy = storyDetails.id,
                       Score = storyDetails.score,
                       Time = storyDetails.time,
                       Uri = storyDetails.url
                      
                   });

                //}
            }

            
            return stories.OrderByDescending(s => s.Score).ToList();
        }



    }
}
