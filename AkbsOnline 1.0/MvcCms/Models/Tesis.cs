using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcCms.Models
{
    public class Tesis
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string TesisAdi { get; set; }

        public string Adres { get; set; }

        public string Telefon { get; set; }
    }
}