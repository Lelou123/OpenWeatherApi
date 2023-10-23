import React from 'react';
import { Accordion, AccordionSummary, AccordionDetails, Typography } from '@mui/material';

import { format, parseISO, isAfter, isSameDay, differenceInMinutes, startOfToday } from 'date-fns';
import {WeeklyWeatherData} from '../../Api/WeeklyWeatherApi'



const styles = {
    container: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        width: '80%',
        margin: '0 auto',
    } as React.CSSProperties,
    accordion: {
        width: '100%',
        marginBottom: '15px',
        borderRadius: '20px',
    } as React.CSSProperties,
    accordionSummary: {
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        background: '#f3f3f3',
        borderRadius: '20px 20px 0 0',
        padding: '10px',
    } as React.CSSProperties,
    accordionDetails: {
        width: '90%',
        display: 'flex',
        justifyContent: 'space-around',
        alignItems: 'center',
        padding: '10px',
        borderRadius: '0 0 20px 20px'
    } as React.CSSProperties,
    column: {
        width: '18%'
    } as React.CSSProperties,
};

interface WeatherAccordionProps {
    weeklyWeatherValue:WeeklyWeatherData[];
}

const WeatherAccordion: React.FC<WeatherAccordionProps> = ({weeklyWeatherValue}) => {        

    const currentDate = new Date();
    const today = startOfToday();
    const filteredData: { [key: string]: WeeklyWeatherData } = {};

    weeklyWeatherValue.forEach(data => {
        const dataDate = parseISO(data.date);
        const isTodayOrFuture = isAfter(dataDate, today) || isSameDay(dataDate, today);
        const dataDateDiff = differenceInMinutes(dataDate, currentDate);
        const currentDataDiff = filteredData[dataDate.toDateString()] ? differenceInMinutes(parseISO(filteredData[dataDate.toDateString()].date), currentDate) : null;

        if (isTodayOrFuture && (!currentDataDiff || dataDateDiff < currentDataDiff)) {
            filteredData[dataDate.toDateString()] = data;
        }
    });

    const renderedData = Object.values(filteredData);


    return (
        <div style={styles.container}>
            {renderedData.map((data, index) => {
                const date = new Date(data.date);
                const formattedDate = format(date, "EEEE, dd MMMM - HH:mm");

                return (
                    <Accordion key={index} style={styles.accordion}>
                        <AccordionSummary style={styles.accordionSummary}>
                            <div style={{ display: 'flex', alignItems: 'center' }}>
                                <img src={`https://openweathermap.org/img/wn/${data.weatherIcon}.png`} alt="Weather Icon" style={{ marginRight: '10px', width: '50px', height: '50px' }} />
                                <Typography>{formattedDate}</Typography>
                            </div>
                            <Typography style={{ flex: 1, textAlign: 'center' }}>{data.weatherDescription}</Typography>
                            <div style={{ flex: 1, textAlign: 'center' }}>
                                <Typography>{`Max: ${data.temperatureMax}°C`}</Typography>
                                <Typography>{`Min: ${data.temperatureMin}°C`}</Typography>
                            </div>
                        </AccordionSummary>
                        <AccordionDetails style={styles.accordionDetails}>
                            <div style={styles.column}>
                                <Typography>{`Feels Like: ${data.feelsLike}°C`}</Typography>
                                <Typography>{`Pressure: ${data.pressure} hPa`}</Typography>
                                <Typography>{`Clouds: ${data.cloudsAll}%`}</Typography>
                                <Typography>{`Humidity: ${data.humidity}%`}</Typography>
                            </div>
                            <div style={styles.column}>
                                <Typography>{`Wind Speed: ${data.windSpeed} m/s`}</Typography>
                                <Typography>{`Visibility: ${data.visibility / 1000}km`}</Typography>
                                <Typography>{`Ground Level: ${data.groundLevel}m`}</Typography>
                                <Typography>{`Sea Level: ${data.seaLevel}m`}</Typography>
                            </div>
                        </AccordionDetails>
                    </Accordion>
                );
            })}
        </div>
    );
};

export default WeatherAccordion;
