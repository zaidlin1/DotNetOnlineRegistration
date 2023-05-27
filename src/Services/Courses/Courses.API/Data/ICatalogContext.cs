using Courses.API.Entities;
using MongoDB.Driver;

namespace Courses.API.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Course> Courses { get; }
    }
}
