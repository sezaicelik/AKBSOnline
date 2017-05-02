using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCms.Models
{
    public class Record
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Sıra No")]
        [Required]
        public int SiraNo { get; set; }


        //Kisisel bilgiler
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Bu alan doldurulmalıdır")]
        [StringLength(80, ErrorMessage = "Bu alan 80 harften uzun olmamalıdır")]
        public string Adi { get; set; }

        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Bu alan doldurulmalıdır")]
        [StringLength(80, ErrorMessage = "Bu alan 80 harften uzun olmamalıdır")]
        public string Soyadi { get; set; }

        //[Display(Name = "Cinsiyet")]
        //public IEnumerable<SelectListItem> Cinsiyet { get; set; }

        [Display(Name = "Cinsiyet")]
        //public int MyColorId { get; set; }
        public int SelectedCinsiyetId { get; set; }
        public IEnumerable<SelectListItem> Cinsiyet { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime DogumTarihi { get; set; }

        [Display(Name = "Geliş Tarihi")]
        public DateTime GelisTarihi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "İkametgah Adresi")]
        public string IkametAdresi { get; set; }

        [Display(Name = "Uyruk")]
        public string Uyrugu { get; set; }

        [Display(Name = "TC Kimlik Numarası")]
        [Required]
        public string TCKimlikNo { get; set; }

        [Display(Name = "Ana Adı")]
        public string AnaAdi { get; set; }

        [Display(Name = "Baba Adı")]
        public string BabaAdi { get; set; }

        [Display(Name = "Kimlik Belgesi Türü")]
        public string KimlikBelgesiTuru { get; set; }

        [Display(Name = "Kimlik Seri No")]
        public string KimlikSeriNo { get; set; }


        //Konaklama Bilgileri
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Ayrılış Tarihi")]
        public DateTime? AyrilisTarihi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Verilen Oda Numarasi")]
        public string VerilenOdaNo { get; set; }


        //Iletisim bilgileri
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Meslek")]
        public string Isi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Mail")]
        public string Mail { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Araç Plakası")]
        public string AracPlakaNo { get; set; }


        //Kimlik Bilgileri
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Doğum Yeri")]
        public string DogumYeri { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Medeni Hal")]
        public string MedeniHali { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Nüfusa Kayıtlı Olduğu İl")]
        public string NufusaKayitliOlduguIl { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Nüfusa Kayıtlı Olduğu İlçe")]
        public string NufusaKayitliOlduguIlce { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Nüfusa Kayıtlı Olduğu Mahalle")]
        public string NufusaKayitliOlduguMahalle { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Nüfus Cilt")]
        public string NufusCilt { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Nüfus Aile Sıra")]
        public string NufusAileSira { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Nüfus Sıra No")]
        public string NufusSiraNo { get; set; }


        /// <summary>
        /// Bagli tablolar
        /// </summary>
        public string AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual CmsUser Author { get; set; }


        public int TesisId { get; set; }
        [ForeignKey("TesisId")]
        [Display(Name = "Tesis Numarasi")]
        public virtual Tesis Tesis { get; set; }

    }
}