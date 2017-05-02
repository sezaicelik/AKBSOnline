using MvcCms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcCms.Data
{
    public interface IRecordRepository
    {
        int CountPublished { get; }
        Record Get(int id);
        void Edit(int id, Record updatedItem);
        void Create(Record model);
        void Delete(int id);
        Task<IEnumerable<Record>> GetAllAsync();
        Task<IEnumerable<Record>> GetRecordsByAuthorAsync(string authorId);
        Task<IEnumerable<Record>> GetPublishedRecordsAsync();
        Task<IEnumerable<Record>> GetRecordsByTagAsync(string tagId);
        Task<IEnumerable<Record>> GetPageAsync(int pageNumber, int pageSize);
        //IEnumerable<SelectListItem> GetGender();
    }
}
