import axios from "axios";
const apiKey = process.env.REACT_APP_API_KEY;


export interface GeolocationSearchResult {
    name: string | null;
    local_names: string[] | null;
    lat: string;
    lon: string;
    country: string | null;
    state: string | null;
}


export const GeolocationApiFetch = async (input: string) => {

    const apiUrl =`https://api.openweathermap.org/geo/1.0/direct?q=${input}&limit=5&appid=${apiKey}`;
    
    try {

        const response = await axios.get<GeolocationSearchResult[]>(apiUrl);
    
        return response.data;

    } catch (error) {
        console.error('Houve um erro ao buscar os dados:', error);
        return null;
    }
}