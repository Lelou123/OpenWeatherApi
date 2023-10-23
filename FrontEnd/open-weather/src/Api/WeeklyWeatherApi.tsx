import axios from "axios";

import { GeolocationSearchResult } from "./GeolocationApi";

export interface WeeklyWeatherData {
    rainVolume: number;
    pop: number;
    seaLevel: number;
    groundLevel: number;
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

export const WeeklyWeatherApiFetch = async (props: GeolocationSearchResult) => {
    const apiUrl = `https://weatheropenapi.azurewebsites.net/WeeklyWeather?Latitude=${props.lat}&Longitude=${props.lon}&Units=1`;

    try {
        const response = await axios.get(apiUrl);
        
        const weatherData: WeeklyWeatherData[] = response.data.data;

        return weatherData;
    } catch (error) {
        console.error('Houve um erro ao buscar os dados:', error);
        return null;
        
    }

};