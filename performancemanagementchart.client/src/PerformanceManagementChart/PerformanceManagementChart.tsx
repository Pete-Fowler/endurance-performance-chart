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

import { colors } from "./Colors";

// Placeholder data: dates and three metrics (ATL, CTL, TSB)
const data = [
    { date: "2024-06-01", Fatigue: 80, Fitness: 70, Form: 10 },
    { date: "2024-06-02", Fatigue: 85, Fitness: 72, Form: 13 },
    { date: "2024-06-03", Fatigue: 78, Fitness: 74, Form: 4 },
    { date: "2024-06-04", Fatigue: 90, Fitness: 76, Form: 14 },
    { date: "2024-06-05", Fatigue: 88, Fitness: 78, Form: 10 },
    { date: "2024-06-06", Fatigue: 82, Fitness: 80, Form: 2 },
];

export default function PerformanceManagementChart() {
    const { yellow, blue, gray, green, red, purple, black } = colors;

    return (
        <div>
            <ResponsiveContainer width="100%" height={400}>
                <ComposedChart data={data} syncId={"pmc"}>
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

            {/* Form */}
            <ResponsiveContainer width="100%" height={250}>
                <LineChart data={data} syncId="pmc">
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="date" />
                    <YAxis domain={[-40, 30]} ticks={[-30, -10, 5, 20]} />
                    <Tooltip />
                    {/* Transition (yellow) */}
                    <ReferenceArea
                        y1={20}
                        y2={30}
                        fill={yellow}
                        fillOpacity={.1}
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
                        stroke="black"
                        dot={false}
                        isAnimationActive={false}
                    />
                </LineChart>
            </ResponsiveContainer>
            <FormLegend />
        </div>
    );
}


function FormLegend() {
    // Order: yellow, blue, gray, green, red (top to bottom)
    const zones = [
        { label: "Transition (≥ 20)", color: "#ffe066" }, // yellow
        { label: "Fresh (> 5)", color: "#74c0fc" }, // blue
        { label: "Neutral (-10 to 5)", color: "#adb5bd" }, // gray
        { label: "Green Zone (-10 to -30)", color: "#51cf66" }, // green
        { label: "High Risk (≤ -30)", color: "#fa5252" }, // red
    ];
    return (
        <div style={{ marginLeft: 24 }}>
            <h4>Form Zones</h4>
            <ul style={{ listStyle: "none", padding: 0, margin: 0 }}>
                {zones.map((zone) => (
                    <li
                        key={zone.label}
                        style={{
                            display: "flex",
                            alignItems: "center",
                            marginBottom: 8,
                        }}
                    >
                        <span
                            style={{
                                display: "inline-block",
                                width: 16,
                                height: 16,
                                background: zone.color,
                                borderRadius: 4,
                                marginRight: 8,
                                border: "1px solid #ccc",
                            }}
                        />
                        <span>{zone.label}</span>
                    </li>
                ))}
            </ul>
        </div>
    );
}