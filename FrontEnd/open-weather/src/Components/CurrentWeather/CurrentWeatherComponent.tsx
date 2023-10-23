import React from 'react';
import './CurrentWeather.css'
import { CurrentWeatherResponse } from '../../Api/CurrentWeatherApi';

interface CurrentWeatherCardProps {    
    currentWeatherValue: CurrentWeatherResponse;
}

const CurrentWeatherCard: React.FC<CurrentWeatherCardProps> = ({currentWeatherValue }) => {

    if (!currentWeatherValue) {
        return <div className='current-weather-card'>Loading...</div>;
    }

    return (
        <div className="current-weather-card">
            <h2>{currentWeatherValue.location.cityName} - {currentWeatherValue.location.state} - {currentWeatherValue.location.country}</h2>
            <p>{currentWeatherValue.weatherDescription}</p>
            <p>Feels Like: {currentWeatherValue.feelsLike}°C</p>
            <p>Wind: {currentWeatherValue.windSpeed} m/s</p>
            <p>Humidity: {currentWeatherValue.humidity}%</p>
            <p>Temperature: {currentWeatherValue.temperature}°C</p>
            <img src={`https://openweathermap.org/img/wn/${currentWeatherValue.weatherIcon}.png`} alt="Weather Icon" />
        </div>
    );
};

export default CurrentWeatherCard;
