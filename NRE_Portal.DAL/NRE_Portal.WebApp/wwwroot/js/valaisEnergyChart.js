import { createChart, fetchData } from './components/chartComponent.js';

/**
 * Transforms JSON data to Chart.js format
 * @param {object} jsonData - Raw API data
 * @returns {object} Formatted data for Chart.js
 */
function transformValaisData(jsonData) {
    const productionData = jsonData["Production d'El"] || jsonData.productionDEl || jsonData;
    
    return {
        labels: productionData.map(d => d.year),
        production: productionData.map(d => d.All)
    };
}

/**
 * Displays a LINE chart of total Valais production
 * @param {string} canvasId - Canvas ID
 * @param {string} apiUrl - API URL
 */
export async function renderValaisTotalProduction(canvasId, apiUrl) {
    try {
        // 1. Fetch data
        const rawData = await fetchData(apiUrl);
        
        // 2. Transform data
        const { labels, production } = transformValaisData(rawData);
        
        // 3. LINE chart configuration
        const chartConfig = {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Total Production (GWh)',
                    data: production,
                    fill: true,
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 3,
                    tension: 0.4,
                    pointRadius: 5,
                    pointBackgroundColor: 'rgba(54, 162, 235, 1)',
                    pointBorderColor: '#fff',
                    pointHoverRadius: 7,
                    pointHoverBackgroundColor: 'rgba(54, 162, 235, 1)',
                    pointHoverBorderColor: '#fff'
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        position: 'top',
                        labels: {
                            font: { size: 14 }
                        }
                    },
                    title: {
                        display: true,
                        text: 'Total Energy Production - Valais',
                        font: { size: 18 }
                    },
                    tooltip: {
                        callbacks: {
                            label: function(context) {
                                return `Production: ${context.parsed.y.toFixed(2)} GWh`;
                            }
                        }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Production (GWh)',
                            font: { size: 14 }
                        },
                        ticks: {
                            callback: function(value) {
                                return value.toLocaleString() + ' GWh';
                            }
                        }
                    },
                    x: {
                        title: {
                            display: true,
                            text: 'Year',
                            font: { size: 14 }
                        }
                    }
                }
            }
        };
        
        // 4. Create chart
        const chart = createChart(canvasId, chartConfig);
        
        if (chart) {
            console.log('Line chart created successfully');
        }
        
    } catch (error) {
        console.error('Error rendering Valais chart:', error);
        
        // Display error on page
        const canvas = document.getElementById(canvasId);
        if (canvas) {
            const parent = canvas.parentElement;
            parent.innerHTML = `
                <div class="alert alert-danger" role="alert">
                    <strong>Error!</strong> Unable to load chart.
                    <br><small>Details: ${error.message}</small>
                    <br><small>Check that the API is running on: ${apiUrl}</small>
                </div>
            `;
        }
    }
}

/**
 * Displays a stacked chart by energy source
 * @param {string} canvasId - Canvas ID
 * @param {string} apiUrl - API URL
 */
export async function renderValaisProductionBySource(canvasId, apiUrl) {
    try {
        const rawData = await fetchData(apiUrl);
        const productionData = rawData["Production d'El"] || rawData;
        
        const chartConfig = {
            type: 'bar',
            data: {
                labels: productionData.map(d => d.year),
                datasets: [
                    {
                        label: 'Hydroelectric',
                        data: productionData.map(d => d.hydro),
                        backgroundColor: 'rgba(54, 162, 235, 0.8)',
                    },
                    {
                        label: 'Small Hydro',
                        data: productionData.map(d => d.hydroSmall),
                        backgroundColor: 'rgba(153, 102, 255, 0.8)',
                    },
                    {
                        label: 'Photovoltaic',
                        data: productionData.map(d => d.PV),
                        backgroundColor: 'rgba(255, 206, 86, 0.8)',
                    },
                    {
                        label: 'Biogas',
                        data: productionData.map(d => d.biogaz),
                        backgroundColor: 'rgba(75, 192, 192, 0.8)',
                    },
                    {
                        label: 'Wind',
                        data: productionData.map(d => d.Windturbine),
                        backgroundColor: 'rgba(255, 159, 64, 0.8)',
                    },
                    {
                        label: 'Renewable Thermal',
                        data: productionData.map(d => d.thermaRen),
                        backgroundColor: 'rgba(255, 99, 132, 0.8)',
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { position: 'top' },
                    title: {
                        display: true,
                        text: 'Energy Production by Source - Valais',
                        font: { size: 18 }
                    }
                },
                scales: {
                    x: {
                        stacked: true,
                        title: { display: true, text: 'Year' }
                    },
                    y: {
                        stacked: true,
                        beginAtZero: true,
                        title: { display: true, text: 'Production (GWh)' }
                    }
                }
            }
        };
        
        createChart(canvasId, chartConfig);
        
    } catch (error) {
        console.error('Error rendering chart by source:', error);
    }
}

/**
 * Displays only renewable energies (NRE)
 * @param {string} canvasId - Canvas ID
 * @param {string} apiUrl - API URL
 */
export async function renderValaisNEROnly(canvasId, apiUrl) {
    try {
        const rawData = await fetchData(apiUrl);
        const productionData = rawData["Production d'El"] || rawData;
        
        // Calculate only PV + Biogas + Wind + Small Hydro
        const nerData = productionData.map(d => 
            d.PV + d.biogaz + d.Windturbine + d.hydroSmall + d.thermaRen
        );
        
        const chartConfig = {
            type: 'line',
            data: {
                labels: productionData.map(d => d.year),
                datasets: [{
                    label: 'New Renewable Energy (NRE)',
                    data: nerData,
                    fill: true,
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    tension: 0.4,
                    pointRadius: 5,
                    pointHoverRadius: 7
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { position: 'top' },
                    title: {
                        display: true,
                        text: 'Evolution of New Renewable Energies',
                        font: { size: 18 }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        title: { display: true, text: 'Production (GWh)' }
                    },
                    x: {
                        title: { display: true, text: 'Year' }
                    }
                }
            }
        };
        
        createChart(canvasId, chartConfig);
        
    } catch (error) {
        console.error('Error rendering NRE chart:', error);
    }
}