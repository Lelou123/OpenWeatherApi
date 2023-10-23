import React, { useState, ChangeEvent, useEffect, Dispatch, SetStateAction } from 'react';
import { TextField, Autocomplete } from '@mui/material';
import './SearchBar.css'
import { GeolocationApiFetch, GeolocationSearchResult } from '../../Api/GeolocationApi';
import { WeeklyWeatherApiFetch, WeeklyWeatherData } from '../../Api/WeeklyWeatherApi'
import { CurrentWeatherResponse, CurrentWeatherApiFetch } from '../../Api/CurrentWeatherApi';


interface SearchComponentProps {
    setSelectedValue: Dispatch<SetStateAction<GeolocationSearchResult | null>>;
    selectedValue: GeolocationSearchResult | null;
    setWeatherResponseData: Dispatch<React.SetStateAction<WeeklyWeatherData[]>>;
    setDailyResponseData: Dispatch<React.SetStateAction<CurrentWeatherResponse | null>>;
}

const SearchComponent: React.FC<SearchComponentProps> = ({ setSelectedValue, selectedValue, setWeatherResponseData, setDailyResponseData }) => {

    const apiKey = process.env.REACT_APP_API_KEY;

    const [searchTerm, setSearchTerm] = useState<string>('');
    const [searchResults, setSearchResults] = useState<GeolocationSearchResult[]>([]);


    useEffect(() => {
        const delayDebounceFn = setTimeout(async () => {
            
            if (searchTerm) {

                const response = await GeolocationApiFetch(searchTerm);

                if (response !== null) {
                    setSearchResults(response);
                }
            }

        }, 2000);

        return () => clearTimeout(delayDebounceFn);
    }, [searchTerm, apiKey]);



    const apisFetch = async () => {
        if (selectedValue) {

            const dailyWeather = await CurrentWeatherApiFetch(selectedValue);
            if (dailyWeather !== null) {
                setDailyResponseData(dailyWeather);
            }

            const weeklyWeather = await WeeklyWeatherApiFetch(selectedValue);

            if (weeklyWeather !== null) {
                setWeatherResponseData(weeklyWeather);
            }

        }
    }

    


    
    
    const handleSearchChange = (event: ChangeEvent<HTMLInputElement>) => {
        
        const newSearchTerm: string = event.target.value;
        
        setSearchTerm(newSearchTerm);

    };



    return (
        <div className='searchComponent' >
            <Autocomplete
                id="search"
                options={searchResults}
                getOptionLabel={(option) => `${option.name}, ${option.state}, ${option.country}`}
                onChange={(event, value) => setSelectedValue(value)}
                renderInput={(params) => <TextField {...params} label="Buscar" variant="outlined" onChange={handleSearchChange} />}
                onSelect={apisFetch}
            />
        </div>
    );
};


export default SearchComponent;