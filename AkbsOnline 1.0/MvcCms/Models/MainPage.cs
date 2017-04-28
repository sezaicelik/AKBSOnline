using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcCms.Models
{
    public class MainPage
    {
       public Record Record { get; set; }

        public IEnumerable<Record> RecordList { get; set; }
    }
}