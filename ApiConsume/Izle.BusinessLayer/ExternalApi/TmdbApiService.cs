using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Izle.BusinessLayer.ExternalApi
{
    public class TmdbApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "f9c7f28b048a3da62021161d03c0c5ba";

        public TmdbApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // 🔹 1. Popüler filmleri Türkçe + İngilizce çeker ve birleştirir
        public async Task<JArray?> GetPopularMoviesAsync(int page = 1)
        {
            // 🇹🇷 Türkçe API isteği
            var trUrl = $"https://api.themoviedb.org/3/movie/popular?api_key={ApiKey}&language=tr-TR&page={page}";
            var trResponse = await _httpClient.GetAsync(trUrl);
            if (!trResponse.IsSuccessStatusCode) return null;

            var trJson = JObject.Parse(await trResponse.Content.ReadAsStringAsync());
            var trResults = (JArray?)trJson["results"];

            // 🇬🇧 İngilizce API isteği
            var enUrl = $"https://api.themoviedb.org/3/movie/popular?api_key={ApiKey}&language=en-US&page={page}";
            var enResponse = await _httpClient.GetAsync(enUrl);
            if (!enResponse.IsSuccessStatusCode) return null;

            var enJson = JObject.Parse(await enResponse.Content.ReadAsStringAsync());
            var enResults = (JArray?)enJson["results"];

            // 🔄 Verileri birleştir
            var mergedResults = new JArray();

            if (trResults != null && enResults != null)
            {
                for (int i = 0; i < trResults.Count && i < enResults.Count; i++)
                {
                    var trMovie = (JObject)trResults[i];
                    var enMovie = (JObject)enResults[i];

                    var merged = new JObject
                    {
                        ["id"] = trMovie["id"],
                        ["title_tr"] = trMovie["title"],
                        ["title_en"] = enMovie["title"],
                        ["overview_tr"] = trMovie["overview"],
                        ["overview_en"] = enMovie["overview"],
                        ["poster_path"] = trMovie["poster_path"],
                        ["vote_average"] = trMovie["vote_average"],
                        ["release_date"] = trMovie["release_date"],
                        ["genre_ids"] = trMovie["genre_ids"] // 🎯 kategori id'leri artık burada
                    };

                    mergedResults.Add(merged);
                }
            }

            return mergedResults;
        }

        // 🔹 2. Kategori listesini hem Türkçe hem İngilizce çeker
        public async Task<JArray?> GetGenresAsync()
        {
            // 🇹🇷 Türkçe türler
            var trUrl = $"https://api.themoviedb.org/3/genre/movie/list?api_key={ApiKey}&language=tr-TR";
            var trResponse = await _httpClient.GetAsync(trUrl);
            if (!trResponse.IsSuccessStatusCode) return null;

            var trJson = JObject.Parse(await trResponse.Content.ReadAsStringAsync());
            var trGenres = (JArray?)trJson["genres"];

            // 🇬🇧 İngilizce türler
            var enUrl = $"https://api.themoviedb.org/3/genre/movie/list?api_key={ApiKey}&language=en-US";
            var enResponse = await _httpClient.GetAsync(enUrl);
            if (!enResponse.IsSuccessStatusCode) return null;

            var enJson = JObject.Parse(await enResponse.Content.ReadAsStringAsync());
            var enGenres = (JArray?)enJson["genres"];

            // 🔄 Eşleştir
            var mergedGenres = new JArray();
            if (trGenres != null && enGenres != null)
            {
                foreach (var trGenre in trGenres)
                {
                    var enGenre = enGenres.FirstOrDefault(g => (int)g["id"] == (int)trGenre["id"]);
                    if (enGenre != null)
                    {
                        mergedGenres.Add(new JObject
                        {
                            ["id"] = trGenre["id"],
                            ["name_tr"] = trGenre["name"],
                            ["name_en"] = enGenre["name"]
                        });
                    }
                }
            }

            return mergedGenres;
        }
    }
}
