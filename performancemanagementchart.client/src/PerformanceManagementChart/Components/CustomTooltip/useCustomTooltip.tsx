export function useCustomTooltip(coordinate: { x: number }) {
    const chartElement = document.querySelector(".recharts-responsive-container");
    const tooltipWidth = 425; 
    
    // Coord x is relative to the chart container
    let tooltipX = coordinate.x;

    if (chartElement) {
        const chartRect = chartElement.getBoundingClientRect();
        const chartLeft = chartRect.left;
        const chartRight = chartRect.right;

        const absoluteX = chartLeft + coordinate.x;

        // Extra 50 makes left boundary in line with chart, not the container
        // to which x is relative to which is slightly to the left
        const minPosition = chartLeft + tooltipWidth / 2 + 50;
        const maxPosition = chartRight - tooltipWidth / 2;

        tooltipX = Math.max(minPosition, Math.min(maxPosition, absoluteX));
    }
    return {
        tooltipX,
    };
}