using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Models;

namespace WebApi.Tests
{
    public static class FakeValues
    {
        public static string Surveys
        {
            get
            {
                var value = new StringBuilder();
                value.AppendLine("5");
                value.AppendLine("M,21,Human Resources Management,3");
                value.AppendLine("F,20,Systems Engineering,2");
                value.AppendLine("M,20,Manufacturing Engineering,3");
                value.AppendLine("M,18,Electrical Engineering,4");
                value.AppendLine("F,25,Construction Engineering,4");
                return value.ToString();
            }
        }

        public static string Cases
        {
            get
            {
                var value = new StringBuilder();
                value.AppendLine("Case #1: NONE");
                value.AppendLine("Case #2: Morgan Martinez Moore");
                value.AppendLine("Case #3: Alfie Hernandez Diaz");
                value.AppendLine("Case #4: Mohammad Green Morales,Oliver Carter Rivera");
                value.AppendLine("Case #5: Ellie Brown Reed,Laura Stewart Foster,Nicole Peterson Torres");
                return value.ToString();
            }
        }

        public static string Students
        {
            get
            {
                var value = new StringBuilder();
                value.AppendLine("Morgan Martinez Moore,F,20,Systems Engineering,2");
                value.AppendLine("Alfie Hernandez Diaz,M,20,Manufacturing Engineering,3");
                value.AppendLine("Mohammad Green Morales,M,18,Electrical Engineering,4");
                value.AppendLine("Oliver Carter Rivera,M,18,Electrical Engineering,4");
                value.AppendLine("Ellie Brown Reed,F,25,Construction Engineering,4");
                value.AppendLine("Laura Stewart Foster,F,25,Construction Engineering,4");
                value.AppendLine("Nicole Peterson Torres,F,25,Construction Engineering,4");
                return value.ToString();
            }
        }

        public static IQueryable<Student> StudentsList
        {
            get
            {
                return new List<Student>()
                {
                    new Student {Name="Morgan Martinez Moore",
                                Gender = "F",
                                Age= 20,
                                Study="Systems Engineering",
                                AcademicYear= 2 },
                    new Student {Name="Alfie Hernandez Diaz",
                                Gender="M",
                                Age=20,
                                Study="Manufacturing Engineering",
                                AcademicYear= 3 },
                    new Student {Name="Mohammad Green Morales",
                                Gender="M",
                                Age=18,
                                Study="Electrical Engineering",
                                AcademicYear= 4 },
                    new Student {Name="Oliver Carter Rivera",
                                Gender="M",
                                Age=18,
                                Study= "Electrical Engineering",
                                AcademicYear= 4 },
                    new Student {Name="Ellie Brown Reed",
                                Gender= "F",
                                Age= 25,
                                Study="Construction Engineering",
                                AcademicYear= 4 },
                    new Student {Name="Laura Stewart Foster",
                                Gender= "F",
                                Age= 25,
                                Study= "Construction Engineering",
                                AcademicYear= 4 },
                    new Student {Name="Nicole Peterson Torres",
                                Gender= "F",
                                Age= 25,
                                Study= "Construction Engineering",
                                AcademicYear= 4 }
            }.AsQueryable();
            }
        }
    }
}