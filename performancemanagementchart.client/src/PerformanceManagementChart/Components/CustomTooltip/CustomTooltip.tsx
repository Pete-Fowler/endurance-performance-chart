import type { TooltipProps } from "recharts";
import type {
    ValueType,
    NameType,
} from "recharts/types/component/DefaultTooltipContent";
import { format, parseISO } from "date-fns";
import styles from "./CustomTooltip.module.css";
import { Col, Row } from "reactstrap";

import { Formatter } from "./Formatter";

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
        <>
            {/* Custom tooltip for date, fitness, fatigue, form at the top*/}
            <aside
                className={styles.customTooltip}
                style={{ left, visibility: isVisible ? "visible" : "hidden", position: "absolute", top: "-60px", transform: "translate(-50%, 0)", zIndex: 1000 }}
            >
                {isVisible && (
                    <section>
                        <Row className={styles.formFitnessFatigue}>

                            {/* Date */}
                            <Col className="d-flex flex-column align-items-center justify-content-center">
                                <div className={styles.topRow}>{format(parseISO(data.date), "EEE")}</div>
                                <div className={`${styles.bottomRow} ${styles.date}`}>{format(parseISO(data.date), "MMM d")}</div>
                            </Col>

                            {/* Fitness */}
                            <Col className="d-flex flex-column align-items-center justify-content-center">
                                <div className={styles.topRow}>Fitness</div>
                                <div className={`${styles.bottomRow} ${styles.fitness}`}>{data?.fitness}</div>
                            </Col>

                            {/* Fatigue */}
                            <Col className="d-flex flex-column align-items-center justify-content-center">
                                <div className={styles.topRow}>Fatigue</div>
                                <div className={`${styles.bottomRow} ${styles.fatigue}`}>{data?.fatigue}</div>
                            </Col>

                            {/* Form */}
                            <Col className="d-flex flex-column align-items-center justify-content-center">
                                <div className={styles.topRow}>Form</div>
                                <div className={`${styles.bottomRow} ${styles.form}`}>{data?.form}</div>
                            </Col>
                        </Row>
                    </section>
                )}
            </aside>

            {/* Activity section at bottom, moves horizontally with mouse */}
            {isVisible && data?.activity && (
                <aside
                    className={styles.customTooltip}
                    style={{ left, position: "absolute", top: "calc(100% + 10px)", transform: "translate(-50%, 0)", zIndex: 1000, minWidth: "400px", background: "#fff", boxShadow: "0 2px 8px rgba(0,0,0,0.1)", padding: "8px 16px", borderRadius: "6px" }}
                >
                    <Row className={styles.activityRow}>

                        {/* Duration & Distance */}
                        <Col className="d-flex flex-column align-items-center justify-content-center">
                            <div style={{ fontWeight: 600 }}>{Formatter.formatDuration(data.activity.duration)}</div>
                            <div style={{ color: '#555', fontSize: '13px' }}>{data.activity.distance?.toFixed(2)}mi</div>
                        </Col>

                        {/* Load & Intensity */}
                        <Col className="d-flex flex-column align-items-center justify-content-center">
                            <div style={{ fontWeight: 600, color: Formatter.getIntensityColor(data.activity.intensity) }}>{data.activity.load}</div>
                            <div style={{ color: Formatter.getIntensityColor(data.activity.intensity), fontSize: '13px' }}>{data.activity.intensity.toFixed(2)}</div>
                        </Col>

                        {/* Heart Rate */}
                        <Col className="d-flex flex-column align-items-center justify-content-center">
                            <div style={{ fontWeight: 600, color: '#ff4136' }}>{data.activity.avgHeartRate}bpm</div>
                        </Col>

                        {/* Pace */}
                        <Col className="d-flex flex-column align-items-center justify-content-center">
                            <div style={{ fontWeight: 600, color: '#0074d9' }}>{Formatter.formatPace(data.activity.avgPace)}</div>
                        </Col>

                        {/* Name & Time */}
                        <Col className="d-flex flex-column align-items-center justify-content-center">
                            <div style={{ fontWeight: 600 }}>{data.activity.name}</div>
                            <div style={{ color: '#555', fontSize: '13px' }}>{data.activity.time ? format(parseISO(data.activity.time), "h:mmaaa") : ""}</div>
                        </Col>
                    </Row>
                </aside>
            )}
        </>
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
