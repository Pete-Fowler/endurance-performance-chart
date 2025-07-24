import type { TooltipProps } from "recharts";
import type {
    ValueType,
    NameType,
} from "recharts/types/component/DefaultTooltipContent";
import { format, parseISO} from "date-fns";

import styles from "./CustomTooltip.module.css";

export const CustomTooltip = ({
    active,
    payload,
    label,
}: TooltipProps<ValueType, NameType>) => {
    const isVisible = active && payload?.length;
    const data = payload && payload.length > 0 ? payload[0].payload : undefined;

    return (
        <aside
            className={styles.customTooltip}
            style={{ visibility: isVisible ? "visible" : "hidden" }}
        >
            {isVisible && (
                <section className={styles.tooltipContent}>
                    <div
                        className={styles.date}
                    >{format(parseISO(data.date), "EEE MMM d")}</div>
                    <div className={styles.fitness}>{data?.fitness}</div>
                    <div className={styles.fatigue}>{data?.fatigue}</div>
                    <div className={styles.form}>{data?.form}</div>
                </section>
            )}
        </aside>
    );
};

// payload[i].payload is json data
// payload[0] is the first object in the array - fitness
// payload[1] is the second object in the array - fatigue

/*
{
    "stroke": "#63c",
    "dataKey": "fatigue",
    "name": "fatigue",
    "color": "#63c",
    "value": 27,
    "payload": {
        "date": "2025-04-13T00:00:00Z",
        "fatigue": 27,
        "fitness": 21,
        "form": -6,
        "threshholdPace": 8.33,
        "activity": {
            "type": "Run",
            "duration": 7536,
            "distance": 10.01,
            "load": 104,
            "intensity": 0,
            "avgHeartRate": 121,
            "avgPace": 4.93,
            "gradeAdjustedPace": 5.87,
            "name": "Jefferson County Running",
            "time": "2025-04-13T16:56:18Z"
        }
    },
    "hide": false
}
    */
