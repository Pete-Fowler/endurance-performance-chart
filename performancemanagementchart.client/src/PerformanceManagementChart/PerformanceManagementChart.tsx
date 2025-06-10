import {
    LineChart,
    Line,
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    Legend,
    ResponsiveContainer,
} from "recharts";

// Placeholder data: dates and three metrics (ATL, CTL, TSB)
const data = [
    { date: "2024-06-01", ATL: 80, CTL: 70, TSB: 10 },
    { date: "2024-06-02", ATL: 85, CTL: 72, TSB: 13 },
    { date: "2024-06-03", ATL: 78, CTL: 74, TSB: 4 },
    { date: "2024-06-04", ATL: 90, CTL: 76, TSB: 14 },
    { date: "2024-06-05", ATL: 88, CTL: 78, TSB: 10 },
    { date: "2024-06-06", ATL: 82, CTL: 80, TSB: 2 },
];

export default function PerformanceManagementChart() {
    return (
        <div style={{ width: "100%", height: 350 }}>
            <ResponsiveContainer>
                <LineChart data={data}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="date" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Line
                        type="monotone"
                        dataKey="ATL"
                        stroke="#ff7300"
                        dot={false}
                    />
                    <Line
                        type="monotone"
                        dataKey="CTL"
                        stroke="#387908"
                        dot={false}
                    />
                    <Line
                        type="monotone"
                        dataKey="TSB"
                        stroke="#8884d8"
                        dot={false}
                    />
                </LineChart>
            </ResponsiveContainer>
        </div>
    );
}
