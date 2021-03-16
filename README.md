# AsynchronousTraining

Simulate a randomly countoff game
1. Total count is from argument
2. Simulate parallelly countoff (depends in CPU cores) but only one can report
3. Each countoff report has 0-1s random delay
4. First reporter say: “Let’s start”, all next reporter say number from previous reporter with format “Report after {number}”, last one say one more sentence: “and I am the last”
5. Use async and await function
6. DO NOT use Random class
