using System;
using System.Collections.Generic;
using System.Linq;

namespace IPA_laborai_3_4
{
    public class Student
    {
        public string Name;
        public string Surname;
        public double Result;
        public bool isAvgSelected;

        public Student(string vName, string vSurname, double vResult, bool vIsAvgSelected)
        {
            Name = vName;
            Surname = vSurname;
            Result = vResult;
            isAvgSelected = vIsAvgSelected;
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            string dataInput = "";
            string[] fileInput;

            List<Student> students = new List<Student>();

            bool continueInput = true;

            Console.WriteLine("Iveskite savo pasirinkima:");
            Console.WriteLine("1 - duomenu ivedimas ranka;");
            Console.WriteLine("2 - duomenu ivedimas is failo;");

            while (true)
            {
                dataInput = Console.ReadLine();

                if (dataInput.Equals("1"))
                {
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

                    break;
                }

                if (dataInput.Equals("2"))
                {
                    // TODO: exception handling
                    fileInput = System.IO.File.ReadAllLines(
                        @"C:\Users\Matas\RiderProjects\IPA_laborai_3_4\kursiokai.txt");
                    foreach (var line in fileInput)
                    {
                        Student stud = GetStudentData(true, line);
                        students.Add(stud);
                    }

                    break;
                }

                Console.WriteLine("Galite rinkits tik 1 arba 2, pakartokite!");
            }

            if (students.Count() > 0)
            {
                StudentsTable(students);
            }
        }

        public static Student GetStudentData(bool isInputFromFile, string line)
        {
            string name;
            string surname;
            string input;
            string[] inputLine = line.Split(' ');

            int testResult = 0;

            double avgHWResult = 0, avgResult = 0;

            bool continueInput = true;
            bool isAvgSelected;
            bool generateNumbers = false;

            List<int> homeWorkResults = new List<int>();
            Random random = new Random();

            /* Vardo ivedimas */
            name = isInputFromFile ? inputLine[0] : GetStudentName();


            /* Pavardes ivedimas */
            surname = isInputFromFile ? inputLine[1] : GetStudentSurname();

            /* Namu darbu rezultatu ivedimas */
            if (isInputFromFile)
            {
                for (int i = 2; i < inputLine.Length; i++)
                {
                    // TODO: exception handling
                    homeWorkResults.Add(int.Parse(inputLine[i]));
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
            // TODO: exception handling
            testResult = isInputFromFile
                ? int.Parse(inputLine[inputLine.Length-1])
                : GetStudentTestResult(generateNumbers);

            /* Vidurkio skaiciavimas */
            if (isInputFromFile)
            {
                isAvgSelected = true;
            }
            else
            {
            isAvgSelected = GetStudentChoiceOfAvg();
            avgHWResult = isAvgSelected
                ? GetStudentAvgHWResult(homeWorkResults)
                : GetStudentMedianHWResult(homeWorkResults);
            }

            avgResult = 0.3 * avgHWResult + 0.7 * testResult;

            Student stud = new Student(name, surname, avgResult, isAvgSelected);
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
            string input;

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
                                          (tableAvg.Length - stud.Result.ToString().Length - 3) + 2;
                Console.WriteLine("{0}{1}{2}{3}",
                    FormatSpaces(stud.Name, ' ', columnNameOffset),
                    FormatSpaces(stud.Surname, ' ', columnSurnameOffset),
                    stud.isAvgSelected
                        ? $"{stud.Result:F2}"
                        : FormatSpaces("", ' ', defaultOffset + tempS.Length + tableMed.Length),
                    !stud.isAvgSelected ? $"{stud.Result:F2}" : FormatSpaces("", ' ', tableMed.Length));
            }
        }

        static string FormatSpaces(string w, char c, int n) // Counting needed spaces
        {
            return w + new String(c, n);
        }
    }
}