import { useEffect, useState } from "react";
import type { IActivityDto } from "./Interfaces";
import { format, parseISO } from "date-fns";
import type {
    Payload,
    ValueType,
    NameType,
} from "recharts/types/component/DefaultTooltipContent";



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

        const firstActivity = activities[0];
        const firstDate = parseISO(firstActivity.date);
        const firstMonth = format(firstDate, "yyyy-MM");

        const seenMonths = new Set();
        const ticks = [];

        for (const activity of activities) {
            const month = format(parseISO(activity.date), "yyyy-MM");
            if (!seenMonths.has(month)) {
                // Skip first month if it starts after the 5th to avoid really
                // narrow spacing
                if (month === firstMonth && firstDate.getDate() > 5) {
                    seenMonths.add(month);
                    continue;
                }

                seenMonths.add(month);
                ticks.push(activity.date);
            }
        }

        return ticks;
    };

    const [toolTipState, setToolTipState] = useState<{
        active: boolean;
        payload?: Payload<ValueType, NameType>[];
        label?: string;
        coordinate?: { x: number; y: number };
    }>({ active: false });

    const handleMouseMove = (data: any) => {
        if (data?.activePayload) {
            setToolTipState({
                active: true,
                payload: data.activePayload,
                label: data.activeLabel,
                coordinate: data.activeCoordinate,
            });
        }
    };

    const handleMouseLeave = () => {
        setToolTipState({ active: false });
    };

    return {
        activities,
        isLoading,
        error,
        getMonthlyTicks,
        toolTipState,
        handleMouseMove,
        handleMouseLeave,
    };
}
