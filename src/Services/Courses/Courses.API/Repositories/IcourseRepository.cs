using Courses.API.Entities;

namespace Courses.API.Repositories
{
    public interface IcourseRepository
    {
        Task<IEnumerable<Course>> GetCourses();
        Task<Course> GetCourse(string id);
        Task<IEnumerable<Course>> GetCourseByName(string name);

        Task CreateCourse(Course course);
        Task<bool> UpdateCourse(Course course);
        Task<bool> DeleteCourse(string id);

        Task<bool> CheckCourses(IEnumerable<Course> courses);

        Task<string> CheckIntersectCourses(IEnumerable<Course> courses);
    }
}
