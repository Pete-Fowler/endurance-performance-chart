export interface IActivityDto {
    date: string;
    fatigue: number;
    fitness: number;
    form: number;
    threshholdPace?: number;
    activity?: IActivity;
}

export interface IActivity {
    type: string;
    duration: number; // in seconds
    distance: number; // in miles
    load: number;
    intensity: number;
    avgHeartRate: number;
    avgPace: number; // in miles/hour
    gradeAdjustedPace: number; // in miles/hour
    name: string;
    time: string; // ISO date string
}