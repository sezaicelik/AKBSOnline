using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcCms.Models
{
    public class StaticInput
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string DropDownId { get; set; }

        public string Value { get; set; }

        public string Text { get; set; }
    }

    public class StaticInput2
    {
        public int GenderId { get; set; }
        public string Name { get; set; }
    }



    public enum Gender
    {
        Erkek,
        Kiz
    }
}