import {
    LineChart,
    Line,
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    Legend,
    ResponsiveContainer,
    ComposedChart,
    Area,
} from "recharts";

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
    return (
        <div >
            <ResponsiveContainer width="100%" height={400}>
                <ComposedChart data={data}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="date" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Line
                        type="monotone"
                        dataKey="Fatigue"
                        stroke="#63c"
                        dot={false}
                    />
                    <Area
                        type="monotone"
                        dataKey="Fitness"
                        stroke="#34ace4"
                        dot={false}
                    />
                </ComposedChart>
            </ResponsiveContainer>

                {/* Form */}
            <ResponsiveContainer width="100%" height={250}>
                <LineChart data={data} syncId="pmc">
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="date" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Line
                        type="monotone"
                        dataKey="Form"
                        stroke="#999"
                        dot={false}
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