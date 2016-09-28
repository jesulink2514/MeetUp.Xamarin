using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MeetUp.Models;
using Newtonsoft.Json;

namespace MeetUp.Infrastructure
{
    public class EventService
    {
        private readonly string _apikey;
        public const string BaseUrl = "https://api.meetup.com/2/";

        private readonly HttpClient _client;
        public EventService(string apikey)
        {
            _apikey = apikey;
            _client = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl),
                Timeout = TimeSpan.FromSeconds(10)
            };
        }

        protected async Task<T> GetAsync<T>(string url)
        {
            var response = await _client.GetAsync(url);
            if(response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            return default(T);
        }

        public async Task<EventResult> GetEvents(string group)
        {
            return await GetAsync<EventResult>($"events?key={_apikey}&group_urlname={group}&text_format=plain");
        }
    }
}
