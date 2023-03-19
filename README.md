##Employee Scheduling Algorithm in C#
This program is an implementation of a scheduling algorithm in C# that helps managers to create fair schedules for their employees. The algorithm takes employee requests and priorities as input and produces an optimized schedule using the Hungarian algorithm. The program includes constraints such that an employee cannot take two shifts in the same day, and cannot work in morning shift after a night shift.

How to Use
To use the program, follow these steps:

Open the EmploysRequestsMatrix.txt file to view the employee requests matrix used by the program.

Set the NumOfWorkers variable to the number of employees in the organization.

Run the program. The console prints show the state of the algorithm, and in the end, it displays the best schedule with visualization.

Details
The employee requests matrix is stored in the EmploysRequestsMatrix.txt file. Each employee has 7 rows, each row describes a day, and each column describes a shift. The values in the matrix represent the employee's priority for a particular shift, where "want" is 1, "prefer not to" is 2, and "can't" is int.max.

The "greedy-dynamic programming" algorithm generates a list of valid matrices using a complexity of big O of two in the power of n. To prevent the algorithm from becoming too complex, the program includes a probability condition to stop it.

The program then activates the Hungarian algorithm on all matrices in the list, and the matrix with the lowest score is the best schedule that the manager can create considering the employees' requests.

The program outputs the best schedule, including the shifts and the employees assigned to each shift.

Conclusion
This scheduling algorithm offers a practical solution to the challenges managers face when creating schedules. By using the Hungarian algorithm and implementing constraints such that employees cannot take two shifts in the same day or work in the morning shift after a night shift, the program can optimize schedules while ensuring they are fair and efficient. Managers can use this program to create schedules that meet their business needs while accommodating their employees' preferences.
