using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Average_Grades
{
    class Program
    {
        class Student
        {
            public string Name { get; set; }
            public double[] Grades { get; set; }
            public double Average
            {
                get
                {
                    return Grades.Average();
                }
            }
        }
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            
            List<Student> students = new List<Student>();

            while (n -- > 0)
            {
                var line = Console.ReadLine().Split(' ');
                string name = line[0];
                var grades = line.Skip(1).Select(double.Parse).ToArray(); 

                Student student = new Student();
                student.Name = name;
                student.Grades = grades;

                students.Add(student);
            }
            students = students
                .Where(s => s.Average >= 5.00)
                .OrderBy(s => s.Name)
                .ThenByDescending(s => s.Average).ToList();

            foreach (var s in students)
            {
                Console.WriteLine("{0} -> {1:F2}", s.Name, s.Average);
            }
        }
    }
}
