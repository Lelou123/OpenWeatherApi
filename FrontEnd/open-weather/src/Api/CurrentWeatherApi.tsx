import axios from "axios";
import { GeolocationSearchResult } from "./GeolocationApi";


export interface CurrentWeatherResponse {
    date: string;
    temperature: number;
    temperatureMin: number;
    temperatureMax: number;
    feelsLike: number;
    humidity: number;
    pressure: number;
    weatherId: number;
    weatherMain: string;
    weatherDescription: string;
    weatherIcon: string;
    windSpeed: number;
    visibility: number;
    cloudsAll: number;
    location: {
        latitude: number;
        longitude: number;
        cityName: string;
        cityId: number;
        country: string;
    };
}


export const CurrentWeatherApiFetch = async (selectedValue: GeolocationSearchResult) => {

    const { lat, lon } = selectedValue;
    const apiUrl = `https://weatheropenapi.azurewebsites.net/CurrentWeather?Latitude=${lat}&Longitude=${lon}1&Units=1`;

    try {
        const response = await axios.get(apiUrl);
        const weatherData: CurrentWeatherResponse = response.data.data;
        return weatherData;
    } catch (error) {
        console.log(error);
        return null;        
    }

};