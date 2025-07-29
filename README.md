A minimal demo of an endurance performance management chart in the style of TrainingPeaks with TypeScript, React, Recharts, C# .NET, Jest, React Testing Library, xUnit, and Moq. Business logic was built with test-driven development. The demo app fetches some training data and performs its own calculations of intensity factor, training stress / load, fitness, and fatigue.

<img width="1381" height="824" alt="image" src="https://github.com/user-attachments/assets/f2f6583c-8c45-4729-bbac-5dd4afb3215f" />

The app uses training stress based on pace, since neither power nor a normalized power metric was available in the running data from the endurance activity API.

Some of the more important improvements that could be made beyond the scope of this demo:
- Improve the front end design / layout / color
- Add front end functionality such as date filters instead of defaulting to the past 6 months, activity type, etc.
- Spend time improving accessibility and varying screen size concerns
- Handle multiple activity types beyond running and edge cases (for example, virtual cycling with no distance or pace data from the activity data provider)
- Implement a login and handle multiple users and different activity data providers rather than defaulting to my own data
- Optimize calculations with things like normalized power and if available. For example, using a default threshhold pace since the data provider did not track this from Garmin led to some unrealistic intensity factor calculations
