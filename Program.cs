using System;
using System.Collections;
namespace Star
{
    public class StudentCard
    {
        public int N { get; set; }
        public string Series { get; set; }
        public override string ToString() { return $"{Series,2}-{N:d6}"; }
    }
    public class Student : IComparable, ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }
        public StudentCard Card { get; set; }
        public object Clone()
        {
            //return new Student {
            //    LastName=this.LastName, 
            //};
            Student st = (Student)this.MemberwiseClone();
            st.Card = new StudentCard
            {
                Series = this.Card.Series,
                N = this.Card.N
            };
            return st;
        }
        public int CompareTo(object obj)
        {
            return LastName.CompareTo((obj as Student)?.LastName);
        }
        public override string ToString()
        {
            return $"| {LastName,-15}| {FirstName,-15}| {Date.ToShortDateString()} |" +
                $" {Card} |";
        }
    }
    class Group : IEnumerable
    {
        Student[] students = {
            new Student {
                LastName = "Ivanov",
                FirstName = "Ivan",
                Date = new DateTime(2002,11,23),
                Card = new StudentCard { Series="AA", N=35 }
            },
            new Student {
                LastName = "Petrov",
                FirstName = "Petro",
                Date = new DateTime(2001,05,23),
                Card = new StudentCard { Series="BA", N=135 }
            },
            new Student {
                LastName = "Stepanenko",
                FirstName = "Stepan",
                Date = new DateTime(2002,08,12),
                Card = new StudentCard { Series="AK", N=75 }
            },
            new Student {
                LastName = "Kavun",
                FirstName = "Inna",
                Date = new DateTime(2000,2,10),
                Card = new StudentCard { Series="AA", N=25 }
            }
        };
        public IEnumerator GetEnumerator()
        {
            return students.GetEnumerator();
        }
        //public void Sort() {
        //    Array.Sort(students);
        //}
        public void Sort(IComparer cmp = null)
        {
            Array.Sort(students, cmp);
        }
    }
    class SortByFirstName : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is Student st1 && y is Student st2)
                return String.Compare(st1.FirstName, st2.FirstName);
            throw new Exception("Not a Student");
        }
    }
    class SortByDateBirthday : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is Student st1 && y is Student st2)
                return DateTime.Compare(st1.Date, st2.Date);
            throw new Exception("Not a Student");
        }
    }

    class Program
    {
        public static void Test1()
        {
            Student Ivan = new Student
            {
                LastName = "Ivanov",
                FirstName = "Ivan",
                Date = new DateTime(2002, 11, 23),
                Card = new StudentCard { Series = "AA", N = 35 }
            };
            Console.WriteLine(Ivan);

        }
        public static void Test2()
        {
            Group PE911 = new Group();
            Console.WriteLine(new string('-', 70));
            foreach (var stud in PE911) Console.WriteLine(stud);
            Console.WriteLine(new string('-', 70));
            PE911.Sort();
            foreach (var stud in PE911) Console.WriteLine(stud);
            Console.WriteLine(new string('-', 70));
            PE911.Sort(new SortByFirstName());
            foreach (var stud in PE911) Console.WriteLine(stud);
            Console.WriteLine(new string('-', 70));

            PE911.Sort(new SortByDateBirthday());
            foreach (var stud in PE911) Console.WriteLine(stud);
            Console.WriteLine(new string('-', 70));
        }
        public static void Test3()
        {
            Student Ivan = new Student
            {
                LastName = "Ivanov",
                FirstName = "Ivan",
                Date = new DateTime(2002, 11, 23),
                Card = new StudentCard { Series = "AA", N = 35 }
            };
            //Student IvanCopy = Ivan;
            Student IvanCopy = (Student)Ivan.Clone();
            IvanCopy.FirstName = "WWWW";
            IvanCopy.Card.N = 123321;
            Console.WriteLine(Ivan);
            Console.WriteLine(IvanCopy);
        }
        static void Main(string[] args)
        {
            //Test1();
            //Test2();
            Test3();
        }
    }
}