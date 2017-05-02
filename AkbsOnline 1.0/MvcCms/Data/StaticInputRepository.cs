using MvcCms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace MvcCms.Data
{
    public class StaticInputRepository : IStaticInputRepository
    {
        public async Task<IEnumerable<SelectListItem>> GetGender()
        {
            var collection = await GetGenderAsync();

            var list = collection.Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Value.ToString(),
                                    Text = x.Text.ToString()
                                });
            return list;
        }

        public async Task<IEnumerable<StaticInput>> GetGenderAsync()
        {
            using (var db = new CmsContext())
            {
                return await db.StaticInputs
                    .Where(p => p.DropDownId == "Gender")
                    .ToArrayAsync();

                //Task<IEnumerable<SelectListItem>> slItem = from s in  db.StaticInputs
                //                                     select new SelectListItem
                //                                     {
                //                                         Selected = s.DropDownId == "Gender",
                //                                         Text = s.Name,
                //                                         Value = s.Value
                //                                     };

                //return slItem;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetGenderb()
        {
            using (var db = new CmsContext())
            {
                var Genders = db.StaticInputs
                    .Include(p => p.Text)
                    .Where(p => p.DropDownId == "Gender")
                    .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Value.ToString(),
                                    Text = x.Text.ToString()
                                });
                return new SelectList(Genders, "Value", "Text");
            }
        }

       
    }
}