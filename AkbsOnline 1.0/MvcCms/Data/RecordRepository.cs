using MvcCms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace MvcCms.Data
{
    public class RecordRepository : IRecordRepository
    {
        public int CountPublished
        {
            get
            {
                using (var db = new CmsContext())
                {
                    return 1; //db.Records.Count(p => p.Published < DateTime.Now);
                }
            }
        }

        public Record Get(int id)
        {
            using (var db = new CmsContext())
            {
                return db.Records.Include("Author")
                    .SingleOrDefault(Record => Record.Id == id);
            }
        }

        public void Edit(int id, Models.Record updatedItem)
        {
            //using (var db = new CmsContext())
            //{
            //    var Record = db.Records.SingleOrDefault(p => p.Id == id);

            //    if (Record == null)
            //    {
            //        throw new KeyNotFoundException("A Record with the id of " 
            //            + id + " does not exist in the data store.");
            //    }

            //    Record.Id = updatedItem.Id;
            //    Record.Title = updatedItem.Title;
            //    Record.Content = updatedItem.Content;
            //    Record.Published = updatedItem.Published;
            //    Record.Tags = updatedItem.Tags;

            //    db.SaveChanges();
            //}
        }

        public void Create(Record model)
        {
            using (var db = new CmsContext())
            {
                var Record = db.Records.SingleOrDefault(p => p.Id == model.Id);

                if (Record != null)
                {
                    throw new ArgumentException("A Record with the id of " + model.Id + " already exists.");
                }

                db.Records.Add(model);
                db.SaveChanges();
            }
        }

        public async Task<IEnumerable<Record>> GetAllAsync()
        {
            using (var db = new CmsContext())
            {
                return await db.Records.Include("Author")
                    .OrderByDescending(Record => Record.SiraNo).ToArrayAsync();
            }
        }

        //public async Task<IEnumerable<Record>> GetRecordsByAuthorAsync(string authorId)
        //{
        //    using (var db = new CmsContext())
        //    {
        //        return await db.Records.Include("Author")
        //            .Where(p => p. == authorId)
        //            .OrderByDescending(Record => Record.Created).ToArrayAsync();
        //    }
        //}

        public void Delete(int id)
        {
            using (var db = new CmsContext())
            {
                var Record = db.Records.SingleOrDefault(p => p.Id == id);

                if (Record == null)
                {
                    throw new KeyNotFoundException("The Record with the id of " + id + " does not exist");
                }

                db.Records.Remove(Record);
                db.SaveChanges();
            }
        }

        public Task<IEnumerable<Record>> GetRecordsByAuthorAsync(string authorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Record>> GetPublishedRecordsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Record>> GetRecordsByTagAsync(string tagId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Record>> GetPageAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<Record>> GetPublishedRecordsAsync()
        //{
        //    using (var db = new CmsContext())
        //    {
        //        return await db.Records
        //            .Include("Author")
        //            .Where(p => p.Published < DateTime.Now)
        //            .OrderByDescending(p => p.Published)
        //            .ToArrayAsync();
        //    }
        //}

        //public async Task<IEnumerable<Record>> GetRecordsByTagAsync(string tagId)
        //{
        //    using (var db = new CmsContext())
        //    {
        //        var Records = await db.Records
        //            .Include("Author")
        //            .Where(Record => Record.CombinedTags.Contains(tagId))
        //            .ToListAsync();

        //        return Records.Where(Record =>
        //            Record.Tags.Contains(tagId, StringComparer.CurrentCultureIgnoreCase))
        //            .ToList();
        //    }
        //}

        //public async Task<IEnumerable<Record>> GetPageAsync(int pageNumber, int pageSize)
        //{
        //    using (var db = new CmsContext())
        //    {
        //        var skip = (pageNumber - 1) * pageSize;

        //        return await db.Records.Where(p => p.Published < DateTime.Now)
        //                       .Include("Author")
        //                       .OrderByDescending(p => p.Published)
        //                       .Skip(skip)
        //                       .Take(pageSize)
        //                       .ToArrayAsync();
        //    }
        //}

    }
}