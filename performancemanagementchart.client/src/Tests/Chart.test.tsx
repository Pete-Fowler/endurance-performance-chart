import "@testing-library/jest-dom/vitest";
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, expect, test } from "vitest";
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

