import type { TooltipProps } from "recharts";
import type {
    ValueType,
    NameType,
} from "recharts/types/component/DefaultTooltipContent";
import { format, parseISO } from "date-fns";
import styles from "./CustomTooltip.module.css";
import { Col, Row } from "reactstrap";

import { Formatter } from "./Formatter";
import { useCustomTooltip } from "./useCustomTooltip";

interface CustomTooltipProps extends TooltipProps<ValueType, NameType> {
    coordinate?: { x: number; y: number };
}

export const CustomTooltip = ({
    payload,
    coordinate,
}: CustomTooltipProps) => {
    const { tooltipX } = useCustomTooltip(coordinate ?? { x: 0, y: 0 });
    const data = payload && payload.length > 0 ? payload[0].payload : undefined;

    if (!data || !coordinate) return null;


    return (
        <>
            {/* Custom tooltip for date, fitness, fatigue, form at the top*/}
            <aside
                className={styles.customTooltip}
                style={{ left: `${tooltipX}px` }}
            >
                {data && (
                    <section>
                        <Row className={styles.formFitnessFatigue}>
                            {/* Date */}
                            <Col className={styles.contentCenteredCol}>
                                <div className={styles.topRow}>
                                    {format(
                                        parseISO(data.date.split("T")[0]),
                                        "EEE"
                                    )}
                                </div>
                                <div
                                    className={`${styles.bottomRow} ${styles.date}`}
                                >
                                    {format(
                                        parseISO(data.date.split("T")[0]),
                                        "MMM d"
                                    )}
                                </div>
                            </Col>

                            {/* Fitness */}
                            <Col className={styles.contentCenteredCol}>
                                <div className={styles.topRow}>Fitness</div>
                                <div
                                    className={`${styles.bottomRow} ${styles.fitness}`}
                                >
                                    {data?.fitness}
                                </div>
                            </Col>

                            {/* Fatigue */}
                            <Col className={styles.contentCenteredCol}>
                                <div className={styles.topRow}>Fatigue</div>
                                <div
                                    className={`${styles.bottomRow} ${styles.fatigue}`}
                                >
                                    {data?.fatigue}
                                </div>
                            </Col>

                            {/* Form */}
                            <Col className={styles.contentCenteredCol}>
                                <div className={styles.topRow}>Form</div>
                                <div
                                    className={`${styles.bottomRow} ${styles.form}`}
                                >
                                    {data?.form}
                                </div>
                            </Col>
                        </Row>
                    </section>
                )}
            </aside>

            {/* Activity section at bottom, moves horizontally with mouse */}
            {data?.activity && (
                <>
                    {/* Floating date above activityTooltip */}
                    <div
                        className={styles.floatingDate}
                        style={{ left: `${tooltipX}px` }}
                    >
                        {data.activity.time
                            ? format(parseISO(data.activity.time), "EEE MMM d")
                            : format(parseISO(data.date), "EEE MMM d")}
                    </div>

                    {/* Activity tooltip */}
                    <aside
                        className={`${styles.activityTooltip}`}
                        style={{ left: `${tooltipX}px` }}
                    >
                        <Row className={styles.activityRow}>
                            {/* Duration & Distance */}
                            <Col className={styles.contentCenteredCol}>
                                <div className={styles.activityDuration}>
                                    {Formatter.formatDuration(
                                        data.activity.duration
                                    )}
                                </div>
                                <div className={styles.activityDistance}>
                                    {data.activity.distance?.toFixed(2)}mi
                                </div>
                            </Col>

                            {/* Load & Intensity */}
                            <Col className={styles.contentCenteredCol}>
                                <div
                                    className={styles.activityLoad}
                                    style={{
                                        color: Formatter.getLoadColor(
                                            data.activity.load
                                        ),
                                    }}
                                >
                                    Load {data.activity.load}
                                </div>
                                <div
                                    className={styles.activityIntensityFactor}
                                    style={{
                                        color: Formatter.getIntensityColor(
                                            data.activity.intensity
                                        ),
                                    }}
                                >
                                    {data.activity.intensity.toFixed(2)}%
                                </div>
                            </Col>

                            {/* Heart Rate */}
                            <Col className={styles.contentCenteredCol}>
                                <div className={styles.activityHeartRate}>
                                    {data.activity.avgHeartRate}bpm
                                </div>
                            </Col>

                            {/* Pace */}
                            <Col className={styles.contentCenteredCol}>
                                <div className={styles.activityPace}>
                                    {Formatter.formatPace(
                                        data.activity.avgPace
                                    )}
                                </div>
                            </Col>

                            {/* Name & Time */}
                            <Col className={styles.contentCenteredCol}>
                                <div className={styles.activityName}>
                                    {data.activity.name}
                                </div>
                                <div className={styles.activityTime}>
                                    {data.activity.time
                                        ? format(
                                              parseISO(data.activity.time),
                                              "h:mmaaa"
                                          )
                                        : ""}
                                </div>
                            </Col>
                        </Row>
                    </aside>
                </>
            )}
        </>
    );
};
