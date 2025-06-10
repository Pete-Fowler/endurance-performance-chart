import "@testing-library/jest-dom/vitest";
import { render, screen } from "@testing-library/react";
import { expect, test } from "vitest";
import App from "../App";


test("renders app", () => {
    render(<App />);
    expect(screen.getByText(/your app/i)).toBeInTheDocument();
});
  
