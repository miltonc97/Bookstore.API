using Bookstore.API.Models;
using Microsoft.Extensions.Options;
using Supabase;

namespace Bookstore.API.Services
{

    public class SupabaseService
    {
        public readonly Client _client;

        public SupabaseService(IConfiguration config)
        {
            var url = config["Supabase:Url"];
            var apiKey = config["Supabase:ApiKey"];

            _client = new Client(url!, apiKey!);
            _client.InitializeAsync().Wait();
        }
    }
}
