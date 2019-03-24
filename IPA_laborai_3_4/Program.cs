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
            
        }
    }
}