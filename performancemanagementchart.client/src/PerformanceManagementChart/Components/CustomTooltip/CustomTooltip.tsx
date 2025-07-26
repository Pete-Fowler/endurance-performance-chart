import type { TooltipProps } from "recharts";
import type {
    ValueType,
    NameType,
} from "recharts/types/component/DefaultTooltipContent";
import { format, parseISO} from "date-fns";

import styles from "./CustomTooltip.module.css";
import { Col, Row } from "reactstrap";

export const CustomTooltip = ({
    active,
    payload,
    label,
    coordinate
}: TooltipProps<ValueType, NameType>) => {
    const isVisible = active && payload?.length;
    const data = payload && payload.length > 0 ? payload[0].payload : undefined;

    const left = coordinate ? `${coordinate.x}px` : "50%";

    return (
        <aside
            className={styles.customTooltip}
            style={{ left, visibility: isVisible ? "visible" : "hidden" }}
        >
            {isVisible && (
                <section>
                    <Row className={styles.formFitnessFatigue}>
                        <Col className="text-center">
                            <div className={styles.topRow}>
                                {format(parseISO(data.date), "EEE")}
                            </div>
                            <div className={`${styles.bottomRow} ${styles.date}`}>
                                {format(parseISO(data.date), "MMM d")}
                            </div>
                        </Col>
                        <Col className="text-center">
                            <div className={styles.topRow}>Fitness</div>
                            <div className={`${styles.bottomRow} ${styles.fitness}`}>{data?.fitness}</div>
                        </Col>
                        <Col className="text-center">
                            <div className={styles.topRow}>Fatigue</div>
                            <div className={`${styles.bottomRow} ${styles.fatigue}`}>{data?.fatigue}</div>
                        </Col>
                        <Col className="text-center">
                            <div className={styles.topRow}>Form</div>
                            <div className={`${styles.bottomRow} ${styles.form}`}>{data?.form}</div>
                        </Col>
                    </Row>
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
