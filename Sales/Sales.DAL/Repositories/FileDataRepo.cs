using Sales.Entities.Models;
using System.Data.Entity;

namespace Sales.DAL.Repositories
{
    public class FileDataRepo : BaseRepo<FileData>, IFileDataRepo
    {
        public FileDataRepo(DbContext context) : base(context)
        { }
    }
}
