using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using Weather.Domain.Dtos.OpenWeatherDtos;
using Weather.Domain.Dtos.Requests;
using Weather.Domain.Interfaces.Services.ExternalServices;

namespace Weather.Infra.Services
{
    public class WeatherApiService : IWeatherApiService
    {
        private readonly IConfiguration _configuration;

        public WeatherApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<WeatherOpenWeatherMapDto> GetWeatherData(WeatherGetDto weatherGet)
        {

            var response = await ExecuteApi("http://api.openweathermap.org/data/2.5/weather", weatherGet);

            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                return JsonConvert.DeserializeObject<WeatherOpenWeatherMapDto>(response.Content) ?? throw new Exception("Erro ao fazer a solicitação");
            }

            throw new Exception("Erro ao fazer a solicitação: Resposta inválida ou vazia.");
        }


        public async Task<ForecastOpenWeatherMapDto> GetForecastData(WeatherGetDto weatherGet)
        {

            var response = await ExecuteApi("http://api.openweathermap.org/data/2.5/forecast", weatherGet);

            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                return JsonConvert.DeserializeObject<ForecastOpenWeatherMapDto>(response.Content) ?? throw new Exception("Erro ao fazer a solicitação");
            }

            throw new Exception("Erro ao fazer a solicitação: Resposta inválida ou vazia.");
        }



        public async Task<RestResponse> ExecuteApi(string api, WeatherGetDto weatherGet)
        {
            var appSettings = _configuration.GetSection("AppSettings");
            var apiKey = appSettings["APIKEY"];

            var client = new RestClient();

            var request = new RestRequest(api, Method.Get);
            request.AddParameter("appid", apiKey);          
            request.AddParameter("lat", weatherGet.Latitude);
            request.AddParameter("lon", weatherGet.Longitude);
            

            if (!string.IsNullOrEmpty(weatherGet.Units.ToString()))
            {
                request.AddParameter("units", weatherGet.Units.ToString());
            }

            if (!string.IsNullOrEmpty(weatherGet.Lang))
            {
                request.AddParameter("lang", weatherGet.Lang);
            }


            var response = (await client.ExecuteAsync(request));
            return response;
        }


    }
}
