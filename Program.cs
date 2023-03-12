using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Scheduling
{
    class Program
    {
        static List<int[,]> conformingToTheConsraintList = new List<int[,]>();
        static int NumOfWorkers = 5;
        static int Size = 7* NumOfWorkers;
        static int Prop = 1;
        static int Counter_Prop = 1;
        static void Main(string[] args)
        {
            one_woker_per_shift();
            return;
        }
        public static void one_woker_per_shift()
        {
            Random rand = new Random();
            int[,] EmploysRequestsMatrix = BulidJobRequestsSimulator();
            Console.WriteLine("-------------------Creating Employ Requsets matrix from request simulator----------------------------");
            Console.WriteLine("-------------------Text file in the folder explorer as'EmploysRequestsMatrix.txt'--------------------");

            String jason = printMatrix(EmploysRequestsMatrix);
            Console.SetOut(Console.Out);
            Console.WriteLine("--------------------Creating matrices that Conforming to the constraint 'no morning after night'-----");

            Create_Conforming_to_the_constraint_matrices(EmploysRequestsMatrix, 0, 2);
            Console.WriteLine("--------------------Recursion stop- no more creating new matrices-------------------------------------");
            Console.WriteLine("\n\n\n\n");
            Console.WriteLine("--------------------Runnig the hungarian algorithm on all the 'Conforming to the constraint matrices'-");
            Console.WriteLine("\n\n\n\n");
            
            try
            {
                int[] chossen_matrix = RunHungarianOnAllMatrices();
                ShowWeeklySchedule(chossen_matrix);
            }
            catch (InvalidOperationException e){
                Console.WriteLine("---------------The algorithm didn't found a schedule for the requests-----------------------------");

            }

        }
        public static int[] RunHungarianOnAllMatrices()
        {
            int[] chossen_matrix = new int[Size];
            int chossen_matrix_val = int.MaxValue;
            int listPos_counter = -1;
            int listPos = 0;
            int possibleSolutions = 0;
            foreach (var matrix in conformingToTheConsraintList)
            {
                listPos_counter++;
                var algorithm = new HungarianAlgorithm(matrix);
                var result = algorithm.Run();
                int res = HungarianAlgorithm.printArraySum(matrix, result);
                if (int.MaxValue != res)
                {
                    possibleSolutions++;
                }
                if (chossen_matrix_val > res)
                {
                    listPos = listPos_counter;
                    chossen_matrix_val = res;
                    chossen_matrix = result;
                }

            }
            Console.WriteLine("\n\n\n");
            
            Console.WriteLine("-----------------------------------------------chossen matrix---------------------------------------------------");
            HungarianAlgorithm.printArray(chossen_matrix);
            Console.WriteLine("Numbers of employs:           " + NumOfWorkers);
            Console.WriteLine("Total created matrices:       " + conformingToTheConsraintList.Count);
            Console.WriteLine("Possible scheduling:          " + possibleSolutions);
            Console.WriteLine("Choosen matrix value:         " + chossen_matrix_val);
            Console.WriteLine("Best score matrix possible:   " + 21);
            Console.WriteLine("Choosen matrix number :       " + listPos);
            if (chossen_matrix_val == int.MaxValue)
            {
                throw new InvalidOperationException("The algorithm didn't found a schedule for the requests");
            }
            return chossen_matrix;
        }
        public static int[,] BulidJobRequestsSimulator()
        {
            Random rand = new Random();
            int index = 0;
            int[,] MainMatrix = new int[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    MainMatrix[i, j] = int.MaxValue;
                    if (j > 20)
                        MainMatrix[i, j] = 0;
                }
            }


            for (int i = 0; i < Size; i++)
            {

                for (int j = 0; j < 21; j++)
                {

                    if (index == j)
                    {
                        for (int counter = 0; counter < 3; counter++)
                        {
                            int action = rand.Next(0, 5);
                            switch (action)
                            {
                                case 0:
                                    MainMatrix[i, j + counter] = 1;
                                    break;
                                case 1:
                                    MainMatrix[i, j + counter] = 2;
                                    break;
                                case 2:
                                    MainMatrix[i, j + counter] = int.MaxValue;
                                    break;
                                case 3:
                                    MainMatrix[i, j + counter] = 1;
                                    break;
                                case 4:
                                    MainMatrix[i, j + counter] = 2;
                                    break;
                            }
                        }
                        j += 2;
                    }


                }
                index += 3;
                if (index > 18)
                    index = 0;
            }
            return MainMatrix;
        }


        public static void ShowWeeklySchedule(int [] chossen_matrix)
        {
            Console.WriteLine("\n\n-------Visualisation of the optimal schedule-------------\n\n");
           



            String[] workers = new String[Size];
            String[] m = new String[7];
            String[] e = new String[7];
            String[] n = new String[7];
            int worker_count = 7;
            int worker_index = 1;
            String days = "Sunday".PadRight(15) + "Monday".PadRight(15) + "Tuesday".PadRight(15)
                + "Wednesday".PadRight(15) + "Thursday".PadRight(15) + "Friday".PadRight(15) + "Saturday".PadRight(15);
          
            Console.WriteLine(days);
            

            for (int i = 0; i < chossen_matrix.Length; i++)
            {
                if (i >= worker_count)
                {
                    worker_count += 7;
                    worker_index++;
                }
                if (chossen_matrix[i] < 21)
                {

                    if (chossen_matrix[i] % 3 == 0)
                    {
                        m[chossen_matrix[i] / 3] = "Worker" + worker_index;

                    }
                    if (chossen_matrix[i] % 3 == 1)
                    {
                        e[chossen_matrix[i] / 3] = "Worker" + worker_index;

                    }
                    if (chossen_matrix[i] % 3 == 2)
                    {
                        n[chossen_matrix[i] / 3] = "Worker" + worker_index;

                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("-----------------------------------------Morning-----------------------------------------");
            Console.WriteLine();

            PrintWeeklySchedule(m);
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------Eavning-------------------------------------------");
            Console.WriteLine();

            PrintWeeklySchedule(e);
            Console.WriteLine();
            Console.WriteLine("------------------------------------------Night---------------------------------------------");
            Console.WriteLine();

            PrintWeeklySchedule(n);


        }
        public static void PrintWeeklySchedule(string[] arr)
        {
            for (int i = 0; i < 7; i++)
            {

                Console.Write(arr[i].PadRight(15));

            }
        }
        public static void Create_Conforming_to_the_constraint_matrices(int[,] m, int ii, int jj)
        {

            for (int i = ii; i < Size - 1; i++)
            {
                if ((i + 1) % 7 == 0)
                    continue;
                if (m[i, (i * 3 + 2) % 21] != int.MaxValue && m[i + 1, (i * 3 + 2) % 21 + 1] != int.MaxValue)
                {
                    int[,] a = m.Clone() as int[,];
                    int[,] b = m.Clone() as int[,];
                    a[i, (i * 3 + 2) % 21] = int.MaxValue;
                    b[i + 1, (i * 3 + 2) % 21 + 1] = int.MaxValue;
                    Random rand = new Random();
                    int action = rand.Next(1, Prop);
                    if (action == 1)
                    {
                        Counter_Prop++;
                        if (Counter_Prop % 100000 == 0)
                        {
                            Prop++;
                        }

                        Create_Conforming_to_the_constraint_matrices(a, i + 1, (i * 3 + 2) % 21 + 3);
                        Create_Conforming_to_the_constraint_matrices(b, i + 1, (i * 3 + 2) % 21 + 3);

                    }
                    return;
                }

            }
            conformingToTheConsraintList.Add(m);
        }
        public static String printMatrix(int[,] matrix)
        {
            int worker_counter=1;
            String jason = "";
            // Get the parent directory of the current directory (one level up)
            string parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

            // Get the parent directory of the parent directory (two levels up)
            string grandParentDirectory = Directory.GetParent(parentDirectory).FullName;
            // Combine the directory and file name to get the full path to the file
            string filePath = Path.Combine(grandParentDirectory, "EmploysRequestsMatrix.txt");

            // Write to the file using a using block to ensure proper disposal
            using (StreamWriter writer = new StreamWriter(filePath))
            {

                writer.WriteLine("\n\n\n");
                writer.WriteLine("------------------------------Main employs requests matrix-----------------------");
                writer.WriteLine("\n\n");
                jason += "{\n";
                for (int i = 0; i < Size; i++)
                {


                    jason +="Worker"+worker_counter+ "-Day" + (i % 7 + 1) + ":[";
                    jason += "[";
                    for (int j = 0; j < 21; j++)
                    {

                        jason += matrix[i, j].ToString() + " ";

                    }

                    jason += "]\n";

                    if ((i + 1) % 7==0)
                    {
                        worker_counter++;

                    }
                }
                jason += "}";
                writer.WriteLine("\n" + jason);
            }
            
                 
            return jason;

        }
        public sealed class HungarianAlgorithm
        {
            private readonly int[,] _costMatrix;
            private int _inf;
            private int _n; //number of elements
            private int[] _lx; //labels for workers
            private int[] _ly; //labels for jobs 
            private bool[] _s;
            private bool[] _t;
            private int[] _matchX; //vertex matched with x
            private int[] _matchY; //vertex matched with y
            private int _maxMatch;
            private int[] _slack;
            private int[] _slackx;
            private int[] _prev; //memorizing paths

            /// <summary>
            /// 
            /// </summary>
            /// <param name="costMatrix"></param>
            public HungarianAlgorithm(int[,] costMatrix)
            {
                _costMatrix = costMatrix;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public int[] Run()
            {
                _n = _costMatrix.GetLength(0);

                _lx = new int[_n];
                _ly = new int[_n];
                _s = new bool[_n];
                _t = new bool[_n];
                _matchX = new int[_n];
                _matchY = new int[_n];
                _slack = new int[_n];
                _slackx = new int[_n];
                _prev = new int[_n];
                _inf = int.MaxValue;


                InitMatches();

                if (_n != _costMatrix.GetLength(1))
                    return null;

                InitLbls();

                _maxMatch = 0;

                InitialMatching();

                var q = new Queue<int>();

                #region augment

                while (_maxMatch != _n)
                {
                    q.Clear();

                    InitSt();
                    //Array.Clear(S,0,n);
                    //Array.Clear(T, 0, n);


                    //parameters for keeping the position of root node and two other nodes
                    var root = 0;
                    int x;
                    var y = 0;

                    //find root of the tree
                    for (x = 0; x < _n; x++)
                    {
                        if (_matchX[x] != -1) continue;
                        q.Enqueue(x);
                        root = x;
                        _prev[x] = -2;

                        _s[x] = true;
                        break;
                    }

                    //init slack
                    for (var i = 0; i < _n; i++)
                    {
                        _slack[i] = _costMatrix[root, i] - _lx[root] - _ly[i];
                        _slackx[i] = root;
                    }

                    //finding augmenting path
                    while (true)
                    {
                        while (q.Count != 0)
                        {
                            x = q.Dequeue();
                            var lxx = _lx[x];
                            for (y = 0; y < _n; y++)
                            {
                                if (_costMatrix[x, y] != lxx + _ly[y] || _t[y]) continue;
                                if (_matchY[y] == -1) break; //augmenting path found!
                                _t[y] = true;
                                q.Enqueue(_matchY[y]);

                                AddToTree(_matchY[y], x);
                            }
                            if (y < _n) break; //augmenting path found!
                        }
                        if (y < _n) break; //augmenting path found!
                        UpdateLabels(); //augmenting path not found, update labels

                        for (y = 0; y < _n; y++)
                        {
                            //in this cycle we add edges that were added to the equality graph as a
                            //result of improving the labeling, we add edge (slackx[y], y) to the tree if
                            //and only if !T[y] &&  slack[y] == 0, also with this edge we add another one
                            //(y, yx[y]) or augment the matching, if y was exposed

                            if (_t[y] || _slack[y] != 0) continue;
                            if (_matchY[y] == -1) //found exposed vertex-augmenting path exists
                            {
                                x = _slackx[y];
                                break;
                            }
                            _t[y] = true;
                            if (_s[_matchY[y]]) continue;
                            q.Enqueue(_matchY[y]);
                            AddToTree(_matchY[y], _slackx[y]);
                        }
                        if (y < _n) break;
                    }

                    _maxMatch++;

                    //inverse edges along the augmenting path
                    int ty;
                    for (int cx = x, cy = y; cx != -2; cx = _prev[cx], cy = ty)
                    {
                        ty = _matchX[cx];
                        _matchY[cy] = cx;
                        _matchX[cx] = cy;
                    }
                }

                #endregion

                return _matchX;
            }

            private void InitMatches()
            {
                for (var i = 0; i < _n; i++)
                {
                    _matchX[i] = -1;
                    _matchY[i] = -1;
                }
            }

            private void InitSt()
            {
                for (var i = 0; i < _n; i++)
                {
                    _s[i] = false;
                    _t[i] = false;
                }
            }

            private void InitLbls()
            {
                for (var i = 0; i < _n; i++)
                {
                    var minRow = _costMatrix[i, 0];
                    for (var j = 0; j < _n; j++)
                    {
                        if (_costMatrix[i, j] < minRow) minRow = _costMatrix[i, j];
                        if (minRow == 0) break;
                    }
                    _lx[i] = minRow;
                }
                for (var j = 0; j < _n; j++)
                {
                    var minColumn = _costMatrix[0, j] - _lx[0];
                    for (var i = 0; i < _n; i++)
                    {
                        if (_costMatrix[i, j] - _lx[i] < minColumn) minColumn = _costMatrix[i, j] - _lx[i];
                        if (minColumn == 0) break;
                    }
                    _ly[j] = minColumn;
                }
            }

            private void UpdateLabels()
            {
                var delta = _inf;
                for (var i = 0; i < _n; i++)
                    if (!_t[i])
                        if (delta > _slack[i])
                            delta = _slack[i];
                for (var i = 0; i < _n; i++)
                {
                    if (_s[i])
                        _lx[i] = _lx[i] + delta;
                    if (_t[i])
                        _ly[i] = _ly[i] - delta;
                    else _slack[i] = _slack[i] - delta;
                }
            }

            private void AddToTree(int x, int prevx)
            {
                //x-current vertex, prevx-vertex from x before x in the alternating path,
                //so we are adding edges (prevx, matchX[x]), (matchX[x],x)

                _s[x] = true; //adding x to S
                _prev[x] = prevx;

                var lxx = _lx[x];
                //updateing slack
                for (var y = 0; y < _n; y++)
                {
                    if (_costMatrix[x, y] - lxx - _ly[y] >= _slack[y]) continue;
                    _slack[y] = _costMatrix[x, y] - lxx - _ly[y];
                    _slackx[y] = x;
                }
            }

            private void InitialMatching()
            {
                for (var x = 0; x < _n; x++)
                {
                    for (var y = 0; y < _n; y++)
                    {
                        if (_costMatrix[x, y] != _lx[x] + _ly[y] || _matchY[y] != -1) continue;
                        _matchX[x] = y;
                        _matchY[y] = x;
                        _maxMatch++;
                        break;
                    }
                }
            }
            public static void printArray(int[] array)
            {
                Console.WriteLine("Array:");
                var size = array.Length;
                for (int i = 0; i < size; i++)
                {
                    if (i % 7 == 0)
                        Console.WriteLine();
                    Console.Write("{0,5:0}", array[i]);
                }
                Console.WriteLine();
            }

            public static int printArraySum(int[,] matrix, int[] array)
            {
                int sum = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    sum += matrix[i, array[i]];
                    if (matrix[i, array[i]] == int.MaxValue)
                    {
                        return int.MaxValue;
                    }
                }
                return sum;
            }
        }

    }
}
