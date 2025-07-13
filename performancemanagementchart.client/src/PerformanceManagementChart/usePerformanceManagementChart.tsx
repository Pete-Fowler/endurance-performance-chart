import { useEffect, useState } from "react";
import type { IActivityDto } from "./Interfaces";

export default function usePerformanceManagementChart() {
    const [activities, setActivities] = useState<IActivityDto[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        fetchActivities();

        async function fetchActivities() {
            setIsLoading(true);
            setError(null);

            try {
                const response = await fetch("/api/activities");
                const json = await response.json();

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

    return {
        activities,
        isLoading,
        error,
    };
}
