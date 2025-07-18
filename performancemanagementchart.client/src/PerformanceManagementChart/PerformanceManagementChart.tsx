import {
    LineChart,
    Line,
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    ResponsiveContainer,
    ComposedChart,
    Area,
    ReferenceArea,
} from "recharts";
import { Col, Container, Row, Spinner } from "reactstrap";

import { colors } from "./Colors";
import styles from "./PerformanceManagementChart.module.css";
import { FormLegend } from "./Components/FormLegend/FormLegend";
import CurrentFitnessLegend from "./Components/CurrentFitness/CurrentFitnessLegend";
import type { IActivityDto } from "./Interfaces";
import { usePerformanceManagementChart } from "./usePerformanceManagementChart";

export default function PerformanceManagementChart() {
    const { activities, isLoading, error } = usePerformanceManagementChart();

    const { yellow, blue, gray, green, red, purple } = colors;

    return (
        <>
            {isLoading && <Spinner />}
            <Container size="lg">
                <Row className="mt-5">
                    <h1 className="text-center">Performance Management Chart</h1>
                </Row>
                <Row>
                <Col xs="11">
                    <ResponsiveContainer
                        minWidth="380px"
                        width="100%"
                        height={400}
                        data-testid="fatigue-fitness-chart"
                    >
                        <ComposedChart data={activities} syncId="pmc">
                            <CartesianGrid strokeDasharray="3 3" />
                            <YAxis />
                            <Tooltip />
                            {/* <Legend /> */}
                            <Line
                                type="monotone"
                                dataKey="Fatigue"
                                stroke={purple}
                                dot={false}
                            />
                            <Area
                                type="monotone"
                                dataKey="Fitness"
                                stroke={blue}
                                fill="#D9EEF9"
                                dot={false}
                            />
                        </ComposedChart>
                    </ResponsiveContainer>
                </Col>
                <Col xs="1" className="ps-0">
                    <CurrentFitnessLegend currentFitness={activities[activities.length - 1]} />
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
                        <LineChart data={activities} syncId="pmc">
                            <CartesianGrid strokeDasharray="3 3" />
                            <XAxis dataKey="date" />
                            <YAxis
                                domain={[-40, 30]}
                                ticks={[-30, -10, 5, 20]}
                            />
                            <Tooltip />
                            {/* Transition (yellow) */}
                            <ReferenceArea
                                y1={20}
                                y2={30}
                                fill={yellow}
                                fillOpacity={0.1}
                            />
                            {/* Fresh (blue) */}
                            <ReferenceArea
                                y1={5}
                                y2={20}
                                fill={blue}
                                fillOpacity={0.1}
                            />
                            {/* Neutral (gray) */}
                            <ReferenceArea
                                y1={-10}
                                y2={5}
                                fill={gray}
                                fillOpacity={0.1}
                            />{" "}
                            {/* Optimal (Green) */}
                            <ReferenceArea
                                y1={-30}
                                y2={-10}
                                fill={green}
                                fillOpacity={0.1}
                            />{" "}
                            {/* High Risk (Red) */}
                            <ReferenceArea
                                y1={-40}
                                y2={-30}
                                fill={red}
                                fillOpacity={0.1}
                            />{" "}
                            <Line
                                dataKey="Form"
                                type="monotone"
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

