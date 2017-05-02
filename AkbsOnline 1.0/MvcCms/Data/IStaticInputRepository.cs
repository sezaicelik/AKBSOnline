using MvcCms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcCms.Data
{
    public interface IStaticInputRepository
    {
        Task<IEnumerable<StaticInput>> GetGenderAsync();
        Task<IEnumerable<SelectListItem>> GetGender();
    }
}
