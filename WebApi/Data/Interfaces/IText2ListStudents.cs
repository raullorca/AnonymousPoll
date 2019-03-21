using System.Linq;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IText2ListStudents
    {
        string LoadFile(string path);

        IQueryable<Student> ToStudents(string value);
    }
}