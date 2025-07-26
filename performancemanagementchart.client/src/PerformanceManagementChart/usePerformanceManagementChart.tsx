import { useEffect, useState } from "react";
import type { IActivityDto } from "./Interfaces";
import { format, parseISO } from "date-fns";

export function usePerformanceManagementChart() {
    const [activities, setActivities] = useState<IActivityDto[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        fetchActivities();

        async function fetchActivities() {
            setIsLoading(true);
            setError(null);

            try {
                const response = await fetch("/api/fitness-chart");
                const json = await response.json();

                console.log("/api/fitness-chart response:", json);

                if (!response.ok) {
                    const message = json?.message || json?.title;
                    setError(message || "Failed to load activities");
                    return;
                }

                setActivities(json as IActivityDto[]);

            } catch (error) {
                if (error instanceof Error) {
                    setError(error.message);
                    console.error("Fetch error:", error);
                } else {
                    setError(String(error));
                    console.error("Fetch error:", error);
                }
            } finally {
                setIsLoading(false);
            }
        }
    }, []);

    const getMonthlyTicks = () => {
        if (!activities.length) return [];
        const seenMonths = new Set();
        const ticks = [];
        for (const activity of activities) {
            const month = format(parseISO(activity.date), "yyyy-MM");
            if (!seenMonths.has(month)) {
                seenMonths.add(month);
                ticks.push(activity.date);
            }
        }
        return ticks;
    };

    return {
        activities,
        isLoading,
        error,
        getMonthlyTicks,
    };
}
