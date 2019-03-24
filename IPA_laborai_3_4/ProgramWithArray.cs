using System;
using System.Collections.Generic;
using System.Linq;

namespace IPA_laborai_3_4
{
    public class ProgramWithArray
    {
        public static void MainWithArray(string[] args)
        {
            List<Student> students = new List<Student>();

            bool continueInput = true;
            while (continueInput)
            {
                Console.WriteLine("/-/-/-/");
                Console.WriteLine("Ar norite ivesti studento duomenis? Y/N");
                continueInput = Console.ReadLine().ToLower().Equals("y");

                if (continueInput)
                {
                    Student stud = GetStudentData();
                    students.Add(stud);
                }
            }

            if (students.Count() > 0)
            {
                StudentsTable(students);
            }
        }

        public static Student GetStudentData()
        {
            string name;
            string surname;
            string input;

            int testResult = 0, counter = 0;

            double avgHWResult = 0, avgResult = 0;

            bool continueInput = true;
            bool isAvgSelected;
            bool generateNumbers = false;

            int[] homeWorkResults = new int[counter];
            Random random = new Random();

            /* Vardo ivedimas */
            Console.WriteLine("*-*-*-*");
            while (true)
            {
                Console.Write("Iveskite studento varda: ");
                name = Console.ReadLine();
                if (name.Length != 0) break;
            }

            /* Pavardes ivedimas */
            Console.WriteLine("*-*-*-*");
            while (true)
            {
                Console.Write("Iveskite studento pavarde: ");
                surname = Console.ReadLine();
                if (surname.Length != 0) break;
            }

            /* Namu darbu rezultatu ivedimas */
            Console.WriteLine("*-*-*-*");
            Console.WriteLine("Ar sugeneruoti balus? Y/N");
            input = Console.ReadLine();
            if (input.ToLower().Equals("y"))
            {
                generateNumbers = true;
            }
            else
            {
                Console.WriteLine("Iveskite namu darbu rezultatus (1-10): ");
            }

            while (continueInput)
            {
                while (true)
                {
                    int hWVal;
                    counter++;
                    Array.Resize<int>(ref homeWorkResults, counter);

                    Console.WriteLine(".......");

                    if (generateNumbers)
                    {
                        homeWorkResults[counter] = random.Next(0, 11);
                        Console.Write("Sugeneruotas rezultatas: {0}\n", homeWorkResults[counter]);
                        break;
                    }

                    Console.Write("Iveskite {0} namu darbo pazymi: ", counter);

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
                        homeWorkResults[counter] = hWVal;
                        break;
                    }
                }

                Console.WriteLine("-------");
                Console.WriteLine("Ar norite testi namu darbu ivedima? Y/N");
                continueInput = Console.ReadLine().ToLower().Equals("y");
            }

            /* Egzamino rezultato ivedimas */
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

            /* Vidurkio skaiciavimas */
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

            if (isAvgSelected)
            {
                avgHWResult = homeWorkResults.Average();
            }
            else
            {
                var ys = homeWorkResults.OrderBy(x => x).ToList();
                double mid = (ys.Count() - 1) / 2.0;
                avgHWResult = (ys[(int) (mid)] + ys[(int) (mid + 0.5)]) / 2;
            }


            avgResult = 0.3 * avgHWResult + 0.7 * testResult;

            Student stud = new Student(name, surname, avgResult, isAvgSelected);
            return stud;
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