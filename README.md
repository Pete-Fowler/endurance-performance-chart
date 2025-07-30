A demo of an endurance performance management chart in the style of TrainingPeaks with TypeScript, React, Recharts, C# .NET, Jest, React Testing Library, xUnit, and Moq. Business logic was built with test-driven development.

<img width="1381" height="824" alt="image" src="https://github.com/user-attachments/assets/3456db81-00e2-4ca0-b7f9-b3453b953807" />

The back end fetches my running data from Intervals ICU and performs its own calculations of intensity factor, training stress/load, fitness, and fatigue. The app uses training stress based on pace, since neither power nor a normalized power metric was available in the running data from the endurance activity API. It then sends that data to the front-end, where it is displayed on charts using the Recharts library with custom tooltips. It uses Intervals ICU since neither Garmin nor TrainingPeaks allows API access without business approval.

The app was deployed on a free Render tier with Docker, and a build/test run by GitHub Actions. Due to Render's free tier hosting, it can have some spin up time on a Render loading page when the instance is put to sleep.

Improvements that could be made:
- Improve the front-end design/layout/color
- Add front-end functionality such as date filters instead of defaulting to the past 6 months, activity type, etc.
- Ensure high accessibility compliance and varying screen size concerns. 
- Handle multiple activity types beyond running and their edge cases (for example, virtual cycling with no distance or pace data from the activity data provider)

A fully developed app beyond the scope of this demo would require many things, such as:
- Implement a login and handle multiple users and different activity data providers rather than defaulting to my own data on Intervals ICU
- Optimize calculations with things like normalized power and threshold power if available. For example, the app uses a default threshold pace since the data provider did not track this from Garmin led to some unrealistic intensity factor calculations
