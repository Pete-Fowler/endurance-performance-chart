A demo of an endurance performance management chart in the style of TrainingPeaks with TypeScript, React, Recharts, C# .NET, Jest, React Testing Library, xUnit, and Moq. Business logic was built with test-driven development.

<img width="1381" height="824" alt="image" src="https://github.com/user-attachments/assets/3456db81-00e2-4ca0-b7f9-b3453b953807" />

The back end fetches my running data from Intervals ICU and performs its own calculations of intensity factor, training stress/load, fitness, and fatigue. The app uses training stress based on pace, since neither power nor a normalized power metric was available. It then sends that data to the front-end, where it is displayed on charts using the Recharts library with custom tooltips. It uses Intervals ICU since neither Garmin nor TrainingPeaks allows API access without business approval.

The app was deployed on Render with Docker, and a build/test run by GitHub Actions.

Improvements that could be made include:
- Improve the front-end design/layout/color
- Add front-end functionality such as date filters instead of defaulting to the past 6 months, activity type, etc.
- Display more detailed activity data
- Make sure accessibility concerns are met
- Better handle varying screen sizes
- Handle multiple activity types beyond running and their edge cases (for example, virtual cycling with no distance or pace data from the activity data provider)
- Benchmark the calculations/data transformation and optimize. For example, I focused on individual units of behavior while doing TDD with the business logic, and wrote 3 methods with nice separation of concerns: GetLoad, AddNonActivityDays, and GetFormFitnessFatigue. They each do one thing, but together iterate the activity collection a total of 3 times. This probably doesn't matter since the collection is small, but I don't love the idea of making 3 passes instead of doing it all at once. The collection would have activities for at most 6 months or a year, and even at 2 activities every day, it would still be less than 1000 and probably only a few hundred. 
