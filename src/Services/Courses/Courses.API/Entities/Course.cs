using MongoDB.Bson.Serialization.Attributes;

namespace Courses.API.Entities
{
    public class Course
    {

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
       
        public string TeacherName { get; set; }
        public bool IsMandatory { get; set; }
       
        public int LessonsCount { get; set; }

        public List<Lesson> Lessons { get; set; }
    }

    public class Lesson
    {
        public string Title { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
    }

}
