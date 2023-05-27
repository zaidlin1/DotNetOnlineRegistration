using Courses.API.Data;
using Courses.API.Entities;
using MongoDB.Driver;

namespace Courses.API.Repositories
{
    public class CourseRepository : IcourseRepository
    {
        const int MIN_NOT_MANDATORY_COURSES = 75;

        private readonly ICatalogContext _context;
        public CourseRepository(ICatalogContext context)
        {
            _context = context;
        }
        public async Task CreateCourse(Course course)
        {
            await _context.Courses.InsertOneAsync(course);
        }

        public async Task<bool> DeleteCourse(string id)
        {
            FilterDefinition<Course> filter = Builders<Course>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Courses
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<Course> GetCourse(string id)
        {
            return await _context
                            .Courses
                            .Find(p => p.Id == id)
                            .FirstOrDefaultAsync();
        }



        public async Task<IEnumerable<Course>> GetCourseByName(string name)
        {
            FilterDefinition<Course> filter = Builders<Course>.Filter.Eq(p => p.Name, name);

            return await _context
                           .Courses
                           .Find(filter)
                           .ToListAsync();

        }
        /// <summary>
        /// Get list of all courses
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await _context
                          .Courses
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<bool> UpdateCourse(Course course)
        {
            var updateResult = await _context
                                       .Courses
                                       .ReplaceOneAsync(filter: g => g.Id == course.Id, replacement: course);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// check if there is intersection in time in taken courses
        /// </summary>
        /// <param name="courses"></param>
        /// <returns></returns>
        public async Task<string> CheckIntersectCourses(IEnumerable<Course> courses)
        {
            List<Course> exepCourses = new List<Course>();
            foreach (var course in courses)
            {
                exepCourses.Add(course);
                var courseDays = course.Lessons.Select(d => d.Day).ToList();
                foreach (var ec in courses.Except(exepCourses))
                {
                    var anotherCourseDays = ec.Lessons.Select(d => d.Day).ToList();
                    var commonDays = courseDays.Intersect(anotherCourseDays);

                    foreach (var day in commonDays)
                    {
                        var courseTime = course.Lessons.Select(d => d.Time).ToList();
                        var anotherCourseTime = ec.Lessons.Select(d => d.Time).ToList();

                        var commonTimes = courseTime.Intersect(anotherCourseTime);
                        if (commonTimes.Count<string>() > 0)
                        {
                            string commonTimeMessage = string.Empty;

                            foreach (var item in commonTimes)
                            {
                                commonTimeMessage += item + ",";
                            }
                            commonTimeMessage += commonTimeMessage.Substring(0, commonTimeMessage.Length - 1); // remove last coma

                            return $"{ec.Name} and {course.Name} have intersection on {day} on {commonTimeMessage}";

                        }
                    }
                }
            }

            return "";
        }
        /// <summary>
        /// check if all madnatory courses are taken and there is quota on non-mandatory courses
        /// </summary>
        /// <param name="courses"></param>
        /// <returns></returns>
        public async Task<bool> CheckCourses(IEnumerable<Course> courses)
        {
            var allCoursesAsync = await GetCourses();
            var allCourses = allCoursesAsync.AsQueryable().Where(x => x.IsMandatory == true).Select(x => x.Id).ToList();
            var checkCourses = courses.AsQueryable().Where(x => x.IsMandatory == true).Select(x => x.Id).ToList();
            var result = allCourses.Except(checkCourses);

            if (result.Count<string>() == 0) // includes all mandatory courses
            {


                // check non mandatory courses quota
                var checkNotMandatoryCourses = courses.AsQueryable().Where(x => x.IsMandatory == false).Select(x => x.LessonsCount).ToList();
                int sum = checkNotMandatoryCourses.Sum();

                return (sum >= MIN_NOT_MANDATORY_COURSES);

            }

            return false;

        }
    }
}
