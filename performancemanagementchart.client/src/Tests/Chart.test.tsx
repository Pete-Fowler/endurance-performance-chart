import "@testing-library/jest-dom/vitest";
import { render, screen } from "@testing-library/react";
import { expect, test } from "vitest";
import App from "../App";
import PerformanceManagementChart from "../PerformanceManagementChart/PerformanceManagementChart";

global.ResizeObserver = class {
    observe() {}
    unobserve() {}
    disconnect() {}
};

test("renders chart", () => {
    render(<PerformanceManagementChart />);
    expect(
        screen.getByText(/fatigue/i)
    ).toBeInTheDocument();
});
