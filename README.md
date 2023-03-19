<h1>Scheduling Algorithm with Optimization using the Hungarian Algorithm</h1>
    <p>The purpose of this project is to provide a fair employee schedule based on their preferences using the Hungarian Algorithm for optimization. The problem this algorithm aims to solve is that it can be difficult for managers to consider all employee preferences when creating a schedule. Often, employees may request certain days off or have priorities regarding shifts, which can be difficult for managers to incorporate into the schedule. This algorithm takes all employee requests and preferences as input and outputs the best possible schedule considering all preferences.</p>
    <h2>Simulator</h2>
<p>The simulator can be found in the <code>Program.cs</code> file. The program generates requests with the simulator and creates a text file containing those requests. The <code>EmploysRequestsMatrix.txt</code> file shows the requests matrix of the simulator. For each employee, there are seven rows, with each row representing a day, and each column representing a shift. The value of each cell in the matrix corresponds to an employee's priority for that shift. A "want" value is 1, "prefer not to" value is 2, and "can't" value is <code>int.MaxValue</code>.</p>

<h2>Greedy-Dynamic Programming Algorithm</h2>
<p>As part of the pre-processing step, a Greedy-Dynamic Programming Algorithm is used to generate a large number of valid matrices where each matrix represents a schedule that is correct in terms of employees not working in the morning after a night shift. The complexity of this algorithm is O(2^n), so a probability condition is set to stop the algorithm and make it useful. The resulting matrices are fed into the Hungarian Algorithm to find the optimal schedule, which handles the constraint that employees cannot work two shifts in the same day.</p>

<h2>Hungarian Algorithm</h2>
<p>The Hungarian Algorithm is used to find the optimal schedule among the valid matrices generated by the Greedy-Dynamic Programming Algorithm. The algorithm takes the requests matrix as input and outputs a schedule matrix that meets all constraints. The constraint of an employee not working two shifts in the same day is handled by the algorithm. The resulting schedule is the best possible schedule that can be created, considering all employee requests and preferences.</p>

<h2>Usage</h2>
<p>The <code>NumOfWorkers</code> static variable determines how the algorithm will work with a different number of employees in the organization. The program prints the current state of the algorithm while it is running, and in the end, it prints the best schedule with visualization. It is recommended to check the <code>EmploysRequestsMatrix.txt</code> file to ensure it represents the employee requests matrix correctly.</p>
<h2>Backend</h2>
<p>This scheduling algorithm has been implemented in a backend using ASP.NET. This implementation allows the algorithm to be easily integrated into other applications and websites, providing a seamless and efficient scheduling solution for organizations of all sizes.</p>
<p>The ASP.NET backend was developed with a focus on flexibility and scalability. It provides a robust and reliable scheduling solution that can be easily adapted to meet the unique needs of any organization. The backend is designed to handle a large number of requests efficiently and in real-time, ensuring that schedules are always up-to-date and accurate.</p>
<p>This implementation has been extensively tested and optimized to ensure that it provides the best possible performance and reliability. It is a powerful tool for any organization that needs to manage complex employee schedules and is looking for a reliable and efficient solution.</p>
