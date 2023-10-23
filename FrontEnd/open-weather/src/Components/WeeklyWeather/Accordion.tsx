import React, { useState } from 'react';
import './Accordion.css';

interface AccordionProps {
    title: string;
    tempMin: number;
    tempMax: number;
    img: string;
    children: React.ReactNode;
}

const Accordion: React.FC<AccordionProps> = ({ img, title, children, tempMin, tempMax }) => {
    const [isExpanded, setIsExpanded] = useState(false);

    const toggleAccordion = () => {
        setIsExpanded(!isExpanded);
    };

    return (
        <div className="accordion">
            <div className="accordion-title" onClick={toggleAccordion}>

                <div>
                    <img src={`https://openweathermap.org/img/wn/${img}.png`} alt="Weather Icon"  />
                </div>
                <div>
                    {title}
                </div>
                <div className='accordion-temps'>
                    <p>Min {tempMin}°C</p>

                    <p>Max {tempMax}°C</p>

                </div>
            </div>
            {isExpanded && (
                <div className="accordion-content">
                    {children}
                </div>
            )}
        </div>
    );
};

export default Accordion;
