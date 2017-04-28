using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcCms.Models
{
    public class Record
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Sıra No")]
        [Required]
        public int SiraNo { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "TC Kimlik Numarası")]
        [Required]
        public string TCKimlikNo { get; set; }

        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Please enter student name.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Adi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Soyad")]
        public string Soyadi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Baba Adı")]
        public string BabaAdi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Ana Adı")]
        public string AnaAdi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Doğum Yeri")]
        public string DogumYeri { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Doğum Tarihi")]
        public DateTime DogumTarihi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Uyruk")]
        public string Uyrugu { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Kimlik Belgesi Türü")]
        public string KimlikBelgesiTuru { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Kimlik Seri No")]
        public string KimlikSeriNo { get; set; }

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

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Cinsiyet")]
        public string Cinsiyet { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Medeni Hal")]
        public string MedeniHali { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Meslek")]
        public string Isi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "İkametgah Adresi")]
        public string IkametAdresi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Mail")]
        public string Mail { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Geliş Tarihi")]
        public DateTime GelisTarihi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Ayrılış Tarihi")]
        public DateTime? AyrilisTarihi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Oda")]
        public string VerilenOdaNo { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Araç Plakası")]
        public string AracPlakaNo { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual CmsUser Author { get; set; }

        public int TesisId { get; set; }

        [ForeignKey("TesisId")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Tesis Id")]
        public virtual Tesis Tesis { get; set; }

    }
}