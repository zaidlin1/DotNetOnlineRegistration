using Courses.API.Entities;
using MongoDB.Driver;

namespace Courses.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Course> courseCollection)
        {
            bool existProduct = courseCollection.Find(p => true).Any();
            if (!existProduct)
            {
                courseCollection.InsertManyAsync(GetPreconfiguredCourses());
            }
        }

        private static IEnumerable<Course> GetPreconfiguredCourses()
        {
            return new List<Course>()
            {
                new Course()
                {
                    Id= "6460ca98ffafa423269f808d",
                    Name="Infi Part 1",
                    TeacherName="Jon Rambo",
                    IsMandatory=true,
                    LessonsCount=23,
                    Lessons= new List<Lesson> { new Lesson { Day = "Monday", Time="10 AM", Title ="1"},
                                                new Lesson { Day = "Tuesday", Time="10 AM", Title ="2"},
                                                new Lesson { Day = "Wednesday", Time="10 AM", Title ="3"}
                    }
                },
                new Course()
                {
                     Id="6460ca98ffafa423269f808e",
                     Name= "Computers Science 1",
                     TeacherName= "Jack Knife",
                     IsMandatory= true,
                     LessonsCount=25,
                     Lessons= new List<Lesson> { new Lesson{ Day = "Monday",  Time="12 AM", Title ="1"},
                                                new Lesson { Day = "Tuesday",  Time="12 AM", Title ="2"},
                                                new Lesson { Day = "Wednesday",Time="12 AM", Title ="3"}
                     }
                },
                new Course()
                {
                     Id= "6460cb6effafa423269f808f",
                     Name= "C#",
                     TeacherName= "Ann Random",
                     IsMandatory=false,
                     LessonsCount=25,
                     Lessons= new List<Lesson> { new Lesson { Day = "Monday", Time="2 PM", Title ="1"},
                                                new Lesson { Day = "Tuesday", Time="2 PM", Title ="2"},
                                                new Lesson { Day = "Wednesday", Time="2 PM", Title ="3"}
                     }
                },
                new Course()
                {
                    Id= "6460cb6effafa423269f8090",
                    Name= "C++",
                    TeacherName= "Marie Constant",
                    IsMandatory= false,
                    LessonsCount= 25,
                    Lessons= new List<Lesson> { new Lesson { Day = "Monday", Time="4 PM", Title ="1"},
                                                new Lesson { Day = "Tuesday", Time="4 PM", Title ="2"},
                                                new Lesson { Day = "Wednesday", Time="4 PM", Title ="3"}
                     }
                },
                new Course()
                {
                    Id= "6460cc19ffafa423269f8091",
                    Name= "Data Structures",
                    TeacherName= "Ion Space",
                    IsMandatory= false,
                    LessonsCount=25,
                    Lessons= new List<Lesson> { new Lesson { Day = "Monday", Time="4 PM", Title ="1"},
                                                new Lesson { Day = "Tuesday", Time="4 PM", Title ="2"},
                                                new Lesson { Day = "Wednesday", Time="4 PM", Title ="3"}
                     }
                },
                new Course()
                {
                  Id= "6460cc19ffafa423269f8092",
                  Name="Differential equations",
                  TeacherName= "Marge Dormant",
                  IsMandatory= false,
                  LessonsCount= 25,
                  Lessons= new List<Lesson> { new Lesson { Day = "Monday", Time="4 PM", Title ="1"},
                                                new Lesson { Day = "Tuesday", Time="8 PM", Title ="2"},
                                                new Lesson { Day = "Wednesday", Time="10 PM", Title ="3"}
                     }
                },
                new Course()
                {
                  Id= "6460cc19ffafa423269f8093",
                  Name="Linear Algerbra",
                  TeacherName= "Marge Dormant",
                  IsMandatory= true,
                  LessonsCount= 25,
                  Lessons= new List<Lesson> { new Lesson { Day = "Sunday", Time="10 AM", Title ="1"},
                                                new Lesson { Day = "Thursday", Time="8 AM", Title ="2"},
                                                new Lesson { Day = "Friday", Time="10 AM", Title ="3"}
                     }
                }
            };
        }
    }
}
