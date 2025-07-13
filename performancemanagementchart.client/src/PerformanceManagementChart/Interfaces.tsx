export interface IActivityDto {
    Date: string;
    Fatigue: number;
    Fitness: number;
    Form: number;
    ThreshholdPace?: number;
    Activity?: IActivity;
}

export interface IActivity {
    Type: string;
    Duration: number; // in seconds
    Distance: number; // in miles
    Load: number;
    Intensity: number;
    AvgHeartRate: number;
    AvgPace: number; // in miles/hour
    GradeAdjustedPace: number; // in miles/hour
    Name: string;
    Time: string; // ISO date string
}