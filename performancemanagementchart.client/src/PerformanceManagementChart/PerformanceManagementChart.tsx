import {
    LineChart,
    Line,
    XAxis,
    YAxis,
    CartesianGrid,
    ResponsiveContainer,
    ComposedChart,
    Area,
    ReferenceArea,
} from "recharts";
import { Col, Container, Row } from "reactstrap";
import { format, parseISO } from "date-fns";

import { FullScreenSpinner } from "./Components/FullScreenSpinner";
import { colors } from "./Colors";
import styles from "./PerformanceManagementChart.module.css";
import { FormLegend } from "./Components/FormLegend/FormLegend";
import CurrentFitnessLegend from "./Components/CurrentFitness/CurrentFitnessLegend";
import { usePerformanceManagementChart } from "./usePerformanceManagementChart";
import { CustomTooltip } from "./Components/CustomTooltip/CustomTooltip";

export default function PerformanceManagementChart() {
    const {
        activities,
        isLoading,
        error,
        getMonthlyTicks,
        toolTipState,
        handleMouseMove,
        handleMouseLeave,
    } = usePerformanceManagementChart();

    const { yellow, blue, gray, green, red, purple } = colors;

    if (error) {
        return (
            <Container className={styles.chartContainer}>
                <Row className="mt-5">
                    <h1 className="text-center">
                        Performance Management Chart
                    </h1>
                </Row>
                <Row>
                    <Col xs="12" className="text-center">
                        <p className="text-danger">{error}</p>
                    </Col>
                </Row>
            </Container>
        );
    }

    return (
        <>
            {isLoading && <FullScreenSpinner />}
            <Container size="lg" className={styles.chartContainer}>
                {/* Header */}
                <Row className="mt-5">
                    <h1 className="text-center">
                        Performance Management Chart
                    </h1>
                </Row>
                <Row>
                    {/* Creates vertical space for custom tooltip above chart */}
                    <Col xs="12">
                        <div style={{ height: "40px" }}>
                            {toolTipState.active && (
                                <CustomTooltip
                                    active={toolTipState.active}
                                    payload={toolTipState.payload}
                                    label={toolTipState.label}
                                    coordinate={toolTipState.coordinate}
                                />
                            )}
                        </div>
                    </Col>
                </Row>

                {/* Fitness / Fatigue Chart */}
                <Row>
                    <Col xs="11">
                        <ResponsiveContainer width="100%" height={400}>
                            <ComposedChart
                                data={activities}
                                syncId="pmc"
                                onMouseMove={handleMouseMove}
                                onMouseLeave={handleMouseLeave}
                            >
                                <CartesianGrid strokeDasharray="3 3" />
                                <YAxis />
                                {/* <Legend /> */}
                                <Line
                                    type="basis"
                                    dataKey="fatigue"
                                    stroke={purple}
                                    dot={false}
                                />
                                <Area
                                    type="basis"
                                    dataKey="fitness"
                                    stroke={blue}
                                    fill="#D9EEF9"
                                    dot={false}
                                />
                            </ComposedChart>
                        </ResponsiveContainer>
                    </Col>
                    <Col xs="1" className="ps-0">
                        <CurrentFitnessLegend
                            currentFitness={activities[activities.length - 1]}
                        />
                    </Col>
                </Row>

                {/* Form */}
                <Row>
                    <Col>
                        <ResponsiveContainer
                            width="100%"
                            height={250}
                            data-testid="form-chart"
                        >
                            <LineChart
                                data={activities}
                                syncId="pmc"
                                onMouseMove={handleMouseMove}
                                onMouseLeave={handleMouseLeave}
                            >
                                <CartesianGrid strokeDasharray="3 3" />
                                <XAxis
                                    dataKey="date"
                                    ticks={getMonthlyTicks()}
                                    tick={({ x, y, payload }) => (
                                        <text
                                            x={x}
                                            y={y + 10}
                                            fontSize={12}
                                            fontWeight="bold"
                                            textAnchor="middle"
                                            fill="#222"
                                        >
                                            {format(
                                                parseISO(payload.value),
                                                "MMM"
                                            )}
                                        </text>
                                    )}
                                />
                                <YAxis
                                    domain={[-40, 30]}
                                    ticks={[-30, -10, 5, 20]}
                                />
                                {/* <Tooltip /> */}
                                {/* Transition (yellow) */}
                                <ReferenceArea
                                    y1={20}
                                    y2={30}
                                    fill={yellow}
                                    fillOpacity={0.2}
                                />
                                {/* Fresh (blue) */}
                                <ReferenceArea
                                    y1={5}
                                    y2={20}
                                    fill={blue}
                                    fillOpacity={0.2}
                                />
                                {/* Neutral (gray) */}
                                <ReferenceArea
                                    y1={-10}
                                    y2={5}
                                    fill={gray}
                                    fillOpacity={0.2}
                                />{" "}
                                {/* Optimal (Green) */}
                                <ReferenceArea
                                    y1={-30}
                                    y2={-10}
                                    fill={green}
                                    fillOpacity={0.2}
                                />{" "}
                                {/* High Risk (Red) */}
                                <ReferenceArea
                                    y1={-40}
                                    y2={-30}
                                    fill={red}
                                    fillOpacity={0.2}
                                />{" "}
                                <Line
                                    dataKey="form"
                                    type="basis"
                                    stroke="black"
                                    dot={false}
                                    isAnimationActive={false}
                                />
                            </LineChart>
                        </ResponsiveContainer>
                    </Col>
                    <Col xs="1">
                        <FormLegend />
                    </Col>
                </Row>
            </Container>
        </>
    );
}
