import type { IActivityDto } from "../../Interfaces";
import styles from "./CurrentFitnessLegend.module.css";
import { colors } from "../../Colors";
import { format, parseISO } from "date-fns";

interface IProps {
    currentFitness: IActivityDto;
}

export default function CurrentFitnessLegend({currentFitness}: IProps) 
{
    return (
        <aside className={styles.fitnessBox}>
            {/* date
             */}
            <div className={styles.date}>
                {currentFitness?.date
                    ? format(
                          parseISO(currentFitness.date.slice(0, 10)),
                          "EEE MMM d"
                      )
                    : "No date"}
            </div>
            <div style={{ color: colors.blue, fontWeight: "bold" }}>
                Fitness: {currentFitness?.fitness}
            </div>
            <div style={{ color: colors.purple, fontWeight: "bold" }}>
                Fatigue: {currentFitness?.fatigue}
            </div>
            <div style={{ color: colors.gray, fontWeight: "bold" }}>
                Form: {currentFitness?.form}
            </div>
        </aside>
    );
}