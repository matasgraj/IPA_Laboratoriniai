namespace IPA_laborai_3_4
{
    public class Student
    {
        public string Name;
        public string Surname;
        public double AvgResult;
        public double MedianResult;
        public bool IsInputFromFile;
        public bool IsAvgSelected;

        public Student(string vName, string vSurname, double vAvgResult, double vMedianResult, bool vIsInputFromFile,
            bool vIsAvgSelected)
        {
            Name = vName;
            Surname = vSurname;
            AvgResult = vAvgResult;
            MedianResult = vMedianResult;
            IsInputFromFile = vIsInputFromFile;
            IsAvgSelected = vIsAvgSelected;
        }
    }
}