/**
 * Generic component to create Chart.js charts
 * @param {string} canvasId - The HTML canvas ID
 * @param {object} chartConfig - Complete Chart.js configuration
 */
export function createChart(canvasId, chartConfig) {
    const ctx = document.getElementById(canvasId);
    if (!ctx) {
        console.error(`Canvas with id "${canvasId}" not found`);
        return null;
    }

    try {
        const chart = new Chart(ctx, chartConfig);
        console.log(' Chart created successfully');
        return chart;
    } catch (error) {
        console.error('Error creating chart:', error);
        const parent = ctx.parentElement;
        parent.innerHTML = `
            <div class="alert alert-danger" role="alert">
                <strong>Error!</strong> Unable to create chart.
                <br>Details: ${error.message}
            </div>
        `;
        return null;
    }
}

/**
 * Utility function to fetch data from an API
 * @param {string} apiUrl - API URL
 * @returns {Promise<any>} The JSON data
 */
export async function fetchData(apiUrl) {
    try {
        const response = await fetch(apiUrl);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
    } catch (error) {
        console.error(' Error loading data:', error);
        throw error;
    }
}