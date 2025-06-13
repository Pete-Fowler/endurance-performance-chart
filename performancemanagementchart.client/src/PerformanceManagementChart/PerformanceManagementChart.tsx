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
import { Col, Container, Row } from "reactstrap";

import { colors } from "./Colors";
import styles from "./PerformanceManagementChart.module.css";
import { FormLegend } from "./Components/FormLegend/FormLegend";
import CurrentFitness from "./Components/CurrentFitness/CurrentFitness";

// Placeholder data: dates and three metrics (ATL, CTL, TSB)
const data = [
    { date: "2024-06-01", fatigue: 80, fitness: 70, form: 10 },
    { date: "2024-06-02", fatigue: 85, fitness: 72, form: 13 },
    { date: "2024-06-03", fatigue: 78, fitness: 74, form: 4 },
    { date: "2024-06-04", fatigue: 90, fitness: 76, form: 14 },
    { date: "2024-06-05", fatigue: 88, fitness: 78, form: 10 },
    { date: "2024-06-06", fatigue: 82, fitness: 80, form: 2 },
];

export default function PerformanceManagementChart() {
    const { yellow, blue, gray, green, red, purple, black } = colors;

    return (
        <Container size="lg">
            <Row className="mt-5"><h1 className="text-center">Performance Management Chart</h1></Row>
            <Row>
                <Col xs="11">
                    <ResponsiveContainer
                        minWidth="380px"
                        width="100%"
                        height={400}
                    >
                        <ComposedChart data={data} syncId={"pmc"}>
                            <CartesianGrid strokeDasharray="3 3" />
                            <YAxis />
                            <Tooltip />
                            {/* <Legend /> */}
                            <Line
                                type="monotone"
                                dataKey="fatigue"
                                stroke={purple}
                                dot={false}
                            />
                            <Area
                                type="monotone"
                                dataKey="fitness"
                                stroke={blue}
                                fill="#D9EEF9"
                                dot={false}
                            />
                        </ComposedChart>
                    </ResponsiveContainer>
                </Col>
                <Col xs="1" className="ps-0"><CurrentFitness currentFitness={data[data.length-1]}/></Col>
            </Row>

            {/* Form */}

            <Row>
                <Col>
                    <ResponsiveContainer width="100%" height={250}>
                        <LineChart data={data} syncId="pmc">
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
                                dataKey="form"
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
    );
}

