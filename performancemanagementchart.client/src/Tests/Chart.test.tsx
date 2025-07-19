import "@testing-library/jest-dom/vitest";
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, expect, test, vi } from "vitest";
import App from "../App";
import PerformanceManagementChart from "../PerformanceManagementChart/PerformanceManagementChart";

global.ResizeObserver = class {
    observe() {}
    unobserve() {}
    disconnect() {}
};

afterEach(() => {
    cleanup();
});

test("Renders fitness chart", () => {
    render(<PerformanceManagementChart />);
    expect(screen.getByText(/form:/i)).toBeInTheDocument(); 
    expect(screen.getByText(/fitness/i)).toBeInTheDocument();
    expect(
        screen.getByText(/fatigue/i)
    ).toBeInTheDocument();
});

test("Renders form chart", () => {
    render(<PerformanceManagementChart />);

    expect(screen.getByText(/transition/i)).toBeInTheDocument();
    expect(screen.getByText(/fresh/i)).toBeInTheDocument();
    expect(screen.getByText(/neutral/i)).toBeInTheDocument();
    expect(screen.getByText(/optimal/i)).toBeInTheDocument();
    expect(screen.getByText(/high risk/i)).toBeInTheDocument();
});

test("Fetches back end API on page load", async () => {
    const fetchMock = vi.spyOn(global, "fetch").mockResolvedValue({
        ok: true,
        json: async () => [
            {
                id: 1,
                date: "2023-10-01",
                activityType: "Running",
                duration: 30,
                distance: 5,
                caloriesBurned: 300,
            },
        ],
    } as Response);

    render(<App />);
    
    expect(fetchMock).toHaveBeenCalledWith("/api/fitness-chart");
    expect(fetchMock).toHaveBeenCalledTimes(1);
});
