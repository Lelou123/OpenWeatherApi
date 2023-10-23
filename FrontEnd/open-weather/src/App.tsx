import React, { useState, useEffect } from 'react';
import SearchComponent from './Components/SearchBar/SerchComponent';
import { GeolocationSearchResult } from './Api/GeolocationApi';
import CurrentWeatherCard from './Components/CurrentWeather/CurrentWeatherComponent';
import WeatherAccordion from './Components/WeeklyWeather/WeeklyWeatherComponent';
import { WeeklyWeatherApiFetch, WeeklyWeatherData } from './Api/WeeklyWeatherApi';
import { CurrentWeatherApiFetch, CurrentWeatherResponse } from './Api/CurrentWeatherApi';


function App() {
  const [selectedValue, setSelectedValue] = useState<GeolocationSearchResult | null>(null);
  const [weeklyWeather, setWeeklyWeather] = useState<WeeklyWeatherData[]>([]);
  const [dailyWeather, setDailyWeather] = useState<CurrentWeatherResponse | null>(null);


  useEffect(() => {
    const handlePermission = () => {
      if (!selectedValue) {
        navigator.geolocation.getCurrentPosition(
          (position) => {
            const { latitude, longitude } = position.coords;
            const geolocationResult: GeolocationSearchResult = {
              name: null,
              local_names: null,
              lat: latitude.toString(),
              lon: longitude.toString(),
              country: null,
              state: null,
            };
            setSelectedValue(geolocationResult);
          },
          (error) => {
            console.error('Error getting location', error);
          }
        );
      }
    };
    handlePermission();
  }, []);



  useEffect(() => {
    const apisFetch = async () => {
      try {
        if (selectedValue) {
          const dailyWeather = await CurrentWeatherApiFetch(selectedValue);
          if (dailyWeather !== null) {
            setDailyWeather(dailyWeather);
          }

          const weeklyWeather = await WeeklyWeatherApiFetch(selectedValue);

          if (weeklyWeather !== null) {
            setWeeklyWeather(weeklyWeather);
          }
        }
      } catch (error) {
        console.error('Ocorreu um erro:', error);
      }
    };
    apisFetch();
  }, [selectedValue]);




  return (
    <div className="App">

      <SearchComponent setWeatherResponseData={setWeeklyWeather} setDailyResponseData={setDailyWeather} />

      {selectedValue && dailyWeather &&
        <CurrentWeatherCard currentWeatherValue={dailyWeather} />}

      {selectedValue && weeklyWeather && (
        <WeatherAccordion weeklyWeatherValue={weeklyWeather} />
      )}

    </div>
  );
}

export default App;
