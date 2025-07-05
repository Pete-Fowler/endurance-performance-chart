export interface IFitnessData {
    Date: string;
    Fatigue: number;
    Fitness: number;
    Form: number;
    Activity?: IActivity;
}

export interface IActivity {
    Type: string; 
    Duration: number; 
    Distance: number; 
    Load: number; 
    Intensity: number; 
    AvgHeartRate: number; 
    AvgPace: number; 
    Name: string; 
    Time: string; 
}