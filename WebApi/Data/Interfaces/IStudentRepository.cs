using System.Linq;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IStudentRepository
    {
        IQueryable<Student> GetAll();
    }
}