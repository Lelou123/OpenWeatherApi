import React, { useState } from 'react';
import { GeolocationApiFetch, GeolocationSearchResult } from '../../Api/GeolocationApi';
import { WeeklyWeatherApiFetch, WeeklyWeatherData } from '../../Api/WeeklyWeatherApi';
import { CurrentWeatherApiFetch, CurrentWeatherResponse } from '../../Api/CurrentWeatherApi';
import './SearchBar.css'


interface SearchComponentProps {        
    setWeatherResponseData: (value: WeeklyWeatherData[]) => void;
    setDailyResponseData: (value: CurrentWeatherResponse | null) => void;
}

const SearchComponent: React.FC<SearchComponentProps> = ({        
    setWeatherResponseData,
    setDailyResponseData,
}) => {
    const [searchTerm, setSearchTerm] = useState<string>('');
    const [searchResults, setSearchResults] = useState<GeolocationSearchResult[]>([]);    

    const callGeolocationApi = async () => {
        try {
            const response = await GeolocationApiFetch(searchTerm);
            if (response !== null) {
                setSearchResults(response);
            }
        } catch (error) {
            console.error('Error fetching geolocation data', error);
        }
    };

    const apisFetch = async (selectedValueParsed: GeolocationSearchResult | null) => {        
        if (selectedValueParsed !== null) {
            
            try {
                const dailyWeather = await CurrentWeatherApiFetch(selectedValueParsed);
                if (dailyWeather !== null) {
                    setDailyResponseData(dailyWeather);
                }
                const weeklyWeather = await WeeklyWeatherApiFetch(selectedValueParsed);
                if (weeklyWeather !== null) {
                    setWeatherResponseData(weeklyWeather);
                }
            } catch (error) {
                console.error('Error fetching weather data', error);
            }
        }
    };

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const newSearchTerm: string = event.target.value;
        setSearchTerm(newSearchTerm);
    };

    const handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        
        const selectedValueParsed = JSON.parse(event.target.value);        
        apisFetch(selectedValueParsed);

    };

    const handleButtonClick = () => {
        callGeolocationApi();
    };


    return (
        <div className='searchComponent'>
            <div className='searchContainer'>
                <input type="text" value={searchTerm} onChange={handleInputChange} placeholder="Search your city weather" className="searchBar" />
                <button onClick={handleButtonClick} className="searchButton">Search City</button>
            </div>

            <select onChange={handleSelectChange} className="selectComponent">
                
                {searchResults.length > 0 ? <option>Selecione uma opção</option> : <></>}
                                
                {searchResults.map((result, index) => (
                    <option key={index} value={JSON.stringify(result)}>
                        {`${result.name}, ${result.state}, ${result.country}`}
                    </option>
                ))}
            </select>
        
        </div>
    );

};

export default SearchComponent;
