using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IPA_laborai_3_4
{
    internal class Program
    {
        public static string[] LIST = new[]
        {
            "LIST",
            "QUEUE",
            "LINKEDLIST"
        };

        public static string[] PATHS = new[]
        {
            @"C:\\Users\\Matas\\RiderProjects\\IPA_laborai_3_4\\10students_generated.txt",
            @"C:\\Users\\Matas\\RiderProjects\\IPA_laborai_3_4\\100students_generated.txt",
            @"C:\\Users\\Matas\\RiderProjects\\IPA_laborai_3_4\\1000students_generated.txt",
            @"C:\\Users\\Matas\\RiderProjects\\IPA_laborai_3_4\\10000students_generated.txt",
            @"C:\\Users\\Matas\\RiderProjects\\IPA_laborai_3_4\\100000students_generated.txt"
        };

        public static void Main(string[] args)
        {
            string dataInput = "";

            Console.WriteLine("Iveskite savo pasirinkima:");
            Console.WriteLine("1 - duomenu ivedimas ranka;");
            Console.WriteLine("2 - duomenu ivedimas is failo;");
            Console.WriteLine("3 - failu generavimas;");
            Console.WriteLine("4 - sugeneruotu failu efektyvumo tyrimas;");
            Console.WriteLine("5 - skirtingu konteineriu testavimas");

            while (true)
            {
                dataInput = Console.ReadLine();

                if (dataInput.Equals("1"))
                {
                    InputByConsole();
                    break;
                }

                if (dataInput.Equals("2"))
                {
                    InputByFile();
                    break;
                }

                if (dataInput.Equals("3"))
                {
                    FileGenerator();
                    break;
                }

                if (dataInput.Equals("4"))
                {
                    FileGenerator();
                    GroupToFiles(@"C:\\Users\\Matas\\RiderProjects\\IPA_laborai_3_4\\100000students_generated.txt");
                    break;
                }

                if (dataInput.Equals("5"))
                {
                    foreach (var list in LIST)
                    {
                        Console.WriteLine(list + " rusiavimas");
                        PerformanceTesting(list);
                    }
                }

                Console.WriteLine("Galite rinkits 1-4, pakartokite!");
            }
        }

        public static void PerformanceTesting(string list = "LIST")
        {
            Stopwatch watch;

            foreach (var path in PATHS)
            {
                watch = Stopwatch.StartNew();
                GroupToFiles(path, false, list);
                watch.Stop();
                long elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine(path + " ||| uzima: " + TimeSpan.FromMilliseconds(elapsedMs).TotalSeconds + "s");

                Process proc = Process.GetCurrentProcess();
                Console.WriteLine("Panaudota baitu atminties: " + proc.PrivateMemorySize64);
            }
        }

        public static void InputByConsole()
        {
            bool continueInput = true;
            List<Student> students = new List<Student>();

            while (continueInput)
            {
                Console.WriteLine("/-/-/-/");
                Console.WriteLine("Ar norite ivesti studento duomenis? Y/N");
                continueInput = Console.ReadLine().ToLower().Equals("y");

                if (continueInput)
                {
                    Student stud = GetStudentData(false, null);
                    students.Add(stud);
                }
            }

            StudentSort(students);
        }

        public static void InputByFile()
        {
            string[] fileInput;
            List<Student> students = new List<Student>();

            try
            {
                fileInput = System.IO.File.ReadAllLines(
                    @"C:\Users\Matas\RiderProjects\IPA_laborai_3_4\kursiokai.txt");
                foreach (var line in fileInput)
                {
                    Student stud = GetStudentData(true, line);
                    students.Add(stud);
                }

                StudentSort(students);
            }
            catch
            {
                Console.WriteLine("!!!!!!!!!");
                Console.WriteLine("Ivyko klaida bandant ikelti faila, rezultatus iveskite per konsole");
                Console.WriteLine("!!!!!!!!!");
                InputByConsole();
            }
        }

        public static void StudentSort(List<Student> students)
        {
            List<Student> sortedStudents = new List<Student>();
            if (students.Any())
            {
                sortedStudents = students.OrderBy(o => o.Name).ToList();
                StudentsTable(sortedStudents);
            }
        }

        public static Student GetStudentData(bool isInputFromFile, string line)
        {
            string name;
            string surname;
            string input;
            string[] inputLine = {""};

            int testResult = 0;

            double avgHWResult = 0, medianResult = 0, avgResult = 0;

            bool continueInput = true;
            bool isAvgSelected = true;
            bool generateNumbers = false;

            List<int> homeWorkResults = new List<int>();

            if (isInputFromFile && line != null)
            {
                inputLine = line.Split(' ');
            }

            /* Vardo ivedimas */
            name = isInputFromFile ? inputLine[0] : GetStudentName();


            /* Pavardes ivedimas */
            surname = isInputFromFile ? inputLine[1] : GetStudentSurname();

            /* Namu darbu rezultatu ivedimas */
            if (isInputFromFile)
            {
                for (int i = 2; i < inputLine.Length - 1; i++)
                {
                    try
                    {
                        homeWorkResults.Add(int.Parse(inputLine[i]));
                    }
                    catch
                    {
                        Console.WriteLine("{0} {1} {2} namu darbo rezultatas netinkamai ivestas!", name, surname,
                            i - 1);
                        Console.WriteLine("Programa baigia darba.");
                        Environment.Exit(1);
                    }
                }
            }
            else
            {
                Console.WriteLine("*-*-*-*");
                Console.WriteLine("Ar sugeneruoti balus? Y/N");

                input = Console.ReadLine();
                generateNumbers = input.ToLower().Equals("y");

                homeWorkResults = GetStudentHomeWorkSum(generateNumbers);
            }


            /* Egzamino rezultato ivedimas */
            if (isInputFromFile)
            {
                try
                {
                    testResult = int.Parse(inputLine[inputLine.Length - 1]);
                }
                catch
                {
                    Console.WriteLine("{0} {1} egzamino rezultatas netinkamai ivestas!", name, surname);
                    Console.WriteLine("Programa baigia darba.");
                    Environment.Exit(1);
                }
            }
            else
            {
                testResult = GetStudentTestResult(generateNumbers);
            }

            /* Vidurkio skaiciavimas */
            if (isInputFromFile)
            {
                avgResult = 0.3 * GetStudentAvgHWResult(homeWorkResults) + 0.7 * testResult;
                medianResult = 0.3 * GetStudentMedianHWResult(homeWorkResults) + 0.7 * testResult;
            }
            else
            {
                isAvgSelected = GetStudentChoiceOfAvg();
                if (isAvgSelected)
                {
                    avgResult = 0.3 * GetStudentAvgHWResult(homeWorkResults) + 0.7 * testResult;
                }
                else
                {
                    medianResult = 0.3 * GetStudentMedianHWResult(homeWorkResults) + 0.7 * testResult;
                }
            }

            Student stud = new Student(name, surname, avgResult, medianResult, isInputFromFile, isAvgSelected);
            return stud;
        }

        public static string GetStudentName()
        {
            string name;
            Console.WriteLine("*-*-*-*");
            while (true)
            {
                Console.Write("Iveskite studento varda: ");
                name = Console.ReadLine();
                if (name.Length != 0) break;
            }

            return name;
        }

        public static string GetStudentSurname()
        {
            string surname;
            Console.WriteLine("*-*-*-*");
            while (true)
            {
                Console.Write("Iveskite studento pavarde: ");
                surname = Console.ReadLine();
                if (surname.Length != 0) break;
            }

            return surname;
        }

        public static List<int> GetStudentHomeWorkSum(bool generateNumbers)
        {
            bool continueInput = true;

            List<int> homeWorkResults = new List<int>();
            Random random = new Random();


            Console.WriteLine("*-*-*-*");
            if (!generateNumbers)
            {
                Console.WriteLine("Iveskite namu darbu rezultatus (1-10): ");
            }

            while (continueInput)
            {
                while (true)
                {
                    int hWVal;
                    Console.WriteLine(".......");

                    if (generateNumbers)
                    {
                        homeWorkResults.Add(random.Next(0, 11));
                        Console.Write("Sugeneruotas rezultatas: {0}\n", homeWorkResults[homeWorkResults.Count - 1]);
                        break;
                    }

                    Console.Write("Iveskite {0} namu darbo pazymi: ", homeWorkResults.Count() + 1);

                    if (!int.TryParse(Console.ReadLine(), out hWVal))
                    {
                        Console.WriteLine("Turite ivesti skaiciu!");
                    }
                    else if (hWVal < 0 || hWVal > 10)
                    {
                        Console.WriteLine("Galimi reziai 1-10, pakartokite!");
                    }
                    else
                    {
                        homeWorkResults.Add(hWVal);
                        break;
                    }
                }

                Console.WriteLine("-------");
                Console.WriteLine("Ar norite testi namu darbu ivedima? Y/N");
                continueInput = Console.ReadLine().ToLower().Equals("y");
            }

            return homeWorkResults;
        }

        public static int GetStudentTestResult(bool generateNumbers)
        {
            int testResult;
            Random random = new Random();

            Console.WriteLine("*-*-*-*");
            if (!generateNumbers)
            {
                Console.Write("Egzamino pazymis: ");

                if (!int.TryParse(Console.ReadLine(), out testResult))
                {
                    Console.WriteLine("Turite ivesti skaiciu!");
                }
                else if (testResult < 0 || testResult > 10)
                {
                    Console.WriteLine("Galimi reziai 1-10, pakartokite!");
                }
            }
            else
            {
                testResult = random.Next(0, 11);
                Console.Write("Sugeneruotas egzamino rezultatas: {0}\n", testResult);
            }

            return testResult;
        }

        public static bool GetStudentChoiceOfAvg()
        {
            string input;
            bool isAvgSelected;

            Console.WriteLine("*-*-*-*");
            while (true)
            {
                Console.WriteLine("Skaiciavimui naudoti vidurki ar mediana? V/M ");
                input = Console.ReadLine();
                if (input.ToLower().Equals("v"))
                {
                    isAvgSelected = true;
                    break;
                }

                if (input.ToLower().Equals("m"))
                {
                    isAvgSelected = false;
                    break;
                }

                Console.WriteLine("Negalimas simbolis, pakartokite!");
            }

            return isAvgSelected;
        }

        public static double GetStudentAvgHWResult(List<int> homeWorkResults)
        {
            return homeWorkResults.Average();
        }

        public static double GetStudentMedianHWResult(List<int> homeWorkResults)
        {
            double medianHWResult;

            var ys = homeWorkResults.OrderBy(x => x).ToList();
            double mid = (ys.Count() - 1) / 2.0;
            medianHWResult = (ys[(int) (mid)] + ys[(int) (mid + 0.5)]) / 2;

            return medianHWResult;
        }

        public static void StudentsTable(List<Student> students)
        {
            string tableName = "Vardas";
            string tableSurname = "Pavarde";
            string tableAvg = "Galutinis (Vid.)";
            string tableMed = "Galutinis (Med.)";
            string tempS = "/";
            int defaultOffset = 6;
            int columnVardasLenght = 6; // For longest name
            int columnPavardeLength = 7; // For longest surname

            foreach (Student stud in students)
            {
                if (stud.Name.Length > columnVardasLenght) columnVardasLenght = stud.Name.Length;
                if (stud.Surname.Length > columnPavardeLength) columnPavardeLength = stud.Surname.Length;
            }

            /* Column names */
            Console.WriteLine("{0}{1}{2}{3}{4}",
                FormatSpaces(tableName, ' ', Math.Abs(columnVardasLenght - tableName.Length) + defaultOffset),
                FormatSpaces(tableSurname, ' ', Math.Abs(columnPavardeLength - tableSurname.Length) + defaultOffset),
                FormatSpaces(tableAvg, ' ', defaultOffset / 2),
                FormatSpaces(tempS, ' ', defaultOffset / 2),
                tableMed);

            Console.WriteLine(FormatSpaces("", '-',
                columnVardasLenght + columnPavardeLength + 3 * defaultOffset + tableAvg.Length + tableMed.Length +
                tempS.Length));

            /* Results */
            foreach (Student stud in students)
            {
                int columnNameOffset = columnVardasLenght - stud.Name.Length + defaultOffset;
                int columnSurnameOffset = columnPavardeLength - stud.Surname.Length + defaultOffset +
                                          (tableAvg.Length - stud.AvgResult.ToString().Length - 3) + 2;
                Console.Write("{0}{1}",
                    FormatSpaces(stud.Name, ' ', columnNameOffset),
                    FormatSpaces(stud.Surname, ' ', columnSurnameOffset));

                if (stud.IsInputFromFile)
                {
                    string columnAvgResultOffset = "   " + stud.AvgResult;
                    Console.WriteLine("{0:F2} {1} {2:F2}", stud.AvgResult, FormatSpaces("", ' ',
                            defaultOffset + tempS.Length + tableMed.Length - columnAvgResultOffset.Length),
                        stud.MedianResult);
                }
                else
                {
                    Console.WriteLine("{0} {1}", stud.IsAvgSelected
                            ? $"{stud.AvgResult:F2}"
                            : FormatSpaces("", ' ', defaultOffset + tempS.Length + tableMed.Length),
                        !stud.IsAvgSelected ? $"{stud.AvgResult:F2}" : FormatSpaces("", ' ', tableMed.Length));
                }
            }
        }

        static string FormatSpaces(string w, char c, int n) // Counting needed spaces
        {
            return w + new String(c, n);
        }

        public static void FileGenerator()
        {
            const string filePath = @"C:\\Users\\Matas\\RiderProjects\\IPA_laborai_3_4\\";
            int entityCount = 1;

            Random rnd = new Random();

            try
            {
                // Generating with 10, 100, 1000, 10000, 100000 entities files
                for (int i = 0; i < 5; i++)
                {
                    entityCount *= 10;
                    string newFileName = filePath + entityCount + "students_generated.txt";
                    StringBuilder entityBuilder = new StringBuilder();

                    if (File.Exists(newFileName))
                    {
                        File.Delete(newFileName);
                    }

                    for (int j = 1; j <= entityCount; j++)
                    {
                        entityBuilder.Append("Vardenis" + j + " Pavardenis" + j + " "
                                             + rnd.Next(1, 11) + " "
                                             + rnd.Next(1, 11) + " "
                                             + rnd.Next(1, 11) + " "
                                             + rnd.Next(1, 11) + " "
                                             + rnd.Next(1, 11) + " "
                                             + rnd.Next(1, 11) + "\n"
                        );
                    }

                    // Creating new file and populating with entities
                    using (FileStream fs = File.Create(newFileName))
                    {
                        Byte[] content = new UTF8Encoding(true).GetBytes(entityBuilder.ToString());
                        fs.Write(content, 0, content.Length);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failo sukurimas negalimas");
                Console.WriteLine("Programa baigia darba");
                Environment.Exit(1);
            }
        }

        public static void GroupToFiles(string path, bool output = true, string list = "LIST")
        {
            string[] dataSet;
            string dumbPath = @"C:\\Users\\Matas\\RiderProjects\\IPA_laborai_3_4\\dumbStudents.txt";
            string smartPath = @"C:\\Users\\Matas\\RiderProjects\\IPA_laborai_3_4\\smartStudents.txt";

            List<Student> dumbList = new List<Student>();
            List<Student> smartList = new List<Student>();

            Queue<Student> dumbQueue = new Queue<Student>();
            Queue<Student> smartQueue = new Queue<Student>();

            LinkedList<Student> dumbLinkedList = new LinkedList<Student>();
            LinkedList<Student> smartLinkedList = new LinkedList<Student>();

            IEnumerable<Student> dumb;
            IEnumerable<Student> smart;

            if (!LIST.Contains(list))
            {
                Environment.Exit(1);
            }

            try
            {
                dataSet = File.ReadAllLines(path);

                foreach (var line in dataSet)
                {
                    var student = GetStudentData(true, line);

                    switch (list)
                    {
                        case "LIST":
                        {
                            smartList.Add(student);
                            break;
                        }
                        case "LINKEDLIST":
                        {
                            smartLinkedList.AddLast(student);

                            break;
                        }
                        case "QUEUE":
                        {
                            smartQueue.Enqueue(student);
                            break;
                        }
                        default:
                            smartList.Add(student);

                            break;
                    }
                }

                /*      */
                foreach (var line in dataSet)
                {
                    var student = GetStudentData(true, line);

                    switch (list)
                    {
                        case "LIST":
                        {
                            if (student.AvgResult >= 5)
                            {
                                smartList.Add(student);
                            }
                            else
                            {
                                dumbList.Add(student);
                            }

                            break;
                        }
                        case "LINKEDLIST":
                        {
                            if (student.AvgResult >= 5)
                            {
                                smartLinkedList.AddLast(student);
                            }
                            else
                            {
                                dumbLinkedList.AddLast(student);
                            }

                            break;
                        }
                        case "QUEUE":
                        {
                            if (student.AvgResult >= 5)
                            {
                                smartQueue.Enqueue(student);
                            }
                            else
                            {
                                dumbQueue.Enqueue(student);
                            }

                            break;
                        }
                        default:
                            if (student.AvgResult >= 5)
                            {
                                smartList.Add(student);
                            }
                            else
                            {
                                dumbList.Add(student);
                            }

                            break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failas nerastas");
                Environment.Exit(1);
            }

            switch (list)
            {
                case "LIST":
                {
                    smart = smartList;
                    dumb = dumbList;
                    break;
                }
                case "LINKEDLIST":
                {
                    smart = smartLinkedList;
                    dumb = dumbLinkedList;
                    break;
                }
                case "QUEUE":
                {
                    smart = smartQueue;
                    dumb = dumbQueue;
                    break;
                }
                default:
                {
                    smart = smartList;
                    dumb = dumbList;
                    break;
                }
            }

            try
            {
                if (File.Exists(dumbPath) || File.Exists(smartPath))
                {
                    File.Delete(dumbPath);
                    File.Delete(smartPath);
                }

                StringBuilder stringToFile = new StringBuilder();
                foreach (var student in dumb)
                {
                    // Constructing student string from object
                    string stud = student.Name + " " + student.Surname + " " + student.AvgResult + " " +
                                  student.MedianResult + "\n";
                    stringToFile.Append(stud);
                }

                using (FileStream fs = File.Create(dumbPath))
                {
                    Byte[] content = new UTF8Encoding(true).GetBytes(stringToFile.ToString());
                    fs.Write(content, 0, content.Length);
                }

                stringToFile.Clear();
                foreach (var student in smart)
                {
                    string stud = student.Name + " " + student.Surname + " " + student.AvgResult + " " +
                                  student.MedianResult + "\n";
                    stringToFile.Append(stud);
                }

                using (FileStream fs = File.Create(smartPath))
                {
                    Byte[] content = new UTF8Encoding(true).GetBytes(stringToFile.ToString());
                    fs.Write(content, 0, content.Length);
                }
            }
            catch
            {
                Console.WriteLine("Failai neirasyti");
            }
        }
    }
}