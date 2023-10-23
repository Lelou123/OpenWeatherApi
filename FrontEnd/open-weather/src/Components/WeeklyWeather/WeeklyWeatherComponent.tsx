import React from 'react';
import { format} from 'date-fns';
import { WeeklyWeatherData } from '../../Api/WeeklyWeatherApi'
import Accordion from './Accordion';


interface WeatherAccordionProps {
    weeklyWeatherValue: WeeklyWeatherData[];
}


const WeatherAccordion: React.FC<WeatherAccordionProps> = ({ weeklyWeatherValue }) => {

    weeklyWeatherValue.sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime());
    
    const dataPorDia: Map<string, WeeklyWeatherData[]> = new Map();
    weeklyWeatherValue.forEach((item) => {
        const dia = item.date.split("T")[0];
        if (!dataPorDia.has(dia)) {
            dataPorDia.set(dia, []);
        }
        dataPorDia.get(dia)?.push(item);
    });
    
    const resultadoFinal: WeeklyWeatherData[] = [];
    const horarioAtual: Date = new Date(); 
    dataPorDia.forEach((value, key) => {
        const resultadoMaisProximo = value.reduce((prev, current) =>
            Math.abs(new Date(current.date).getTime() - horarioAtual.getTime()) < Math.abs(new Date(prev.date).getTime() - horarioAtual.getTime()) ? current : prev
        );
        resultadoFinal.push(resultadoMaisProximo);
    });

    
    resultadoFinal.sort((a, b) => Math.abs(new Date(a.date).getTime() - horarioAtual.getTime()) - Math.abs(new Date(b.date).getTime() - horarioAtual.getTime()));

    return (
        <div className="accordion-container">
            {resultadoFinal.map((data, index) => {
                const date = new Date(data.date);
                const formattedDate = format(date, "EEEE, dd MMMM - HH:mm");

                return (
                    <Accordion key={index} img={data.weatherIcon} title={formattedDate} tempMin={data.temperatureMin} tempMax={data.temperatureMax}>
                        <div className="accordion-content">
                            <div className="accordion-column">
                                <p>{`Feels Like: ${data.feelsLike}°C`}</p>
                                <p>{`Pressure: ${data.pressure} hPa`}</p>
                                <p>{`Clouds: ${data.cloudsAll}%`}</p>
                                <p>{`Humidity: ${data.humidity}%`}</p>
                            </div>
                            <div className="accordion-column">
                                <p>{`Wind Speed: ${data.windSpeed} m/s`}</p>
                                <p>{`Visibility: ${data.visibility / 1000}km`}</p>
                                <p>{`Ground Level: ${data.groundLevel}m`}</p>
                                <p>{`Sea Level: ${data.seaLevel}m`}</p>
                            </div>
                        </div>
                    </Accordion>
                );
            })}
        </div>
    );
};

export default WeatherAccordion;
