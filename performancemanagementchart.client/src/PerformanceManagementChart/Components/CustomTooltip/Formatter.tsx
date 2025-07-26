export class Formatter {
    static formatDuration(seconds: number) {
        const hours = Math.floor(seconds / 3600);
        const minutes = Math.floor((seconds % 3600) / 60);
        const secondsLeft = Math.floor(seconds % 60);
        return `${hours > 0 ? hours + "h" : ""}${minutes > 0 ? minutes + "m" : hours > 0 ? "0m" : ""}${
            secondsLeft > 0 ? secondsLeft + "s" : ""
        }`;
    }

    static formatPace(pace: number) {
        // pace in min/mi
        const min = Math.floor(pace);
        const sec = Math.round((pace - min) * 60);
        return `${min}:${sec.toString().padStart(2, "0")}/mi`;
    }
    
    static getIntensityColor(intensity: number) {
        if (intensity < 0.7) return "#2ecc40"; // green
        if (intensity < 0.85) return "#ffdc00"; // yellow
        if (intensity < 1.0) return "#ff851b"; // orange
        return "#ff4136"; // red
    }
}
