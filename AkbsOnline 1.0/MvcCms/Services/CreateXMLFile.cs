using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using MvcCms.Models;
using System.Web;

namespace MvcCms.Controllers
{
    public class CreateXMLFile
    {
        string konaklamaNodeValue = "Konaklama";
        string staticUserNodeName = "Kişi";
        string[] konaklamaAttributes = { "TesisKodu", "Tarih", "GonderenProgram", "GonderenProgramVersiyon" };
        string[] konaklamaValues = { "34275", "2017-04-10 23:58:18", "AKBS Online", "v1.0" };
        string[] nod2Attributes = { "SiraNo", "TCKimlikNo", "Adi", "Soyadi", "BabaAdi", "AnaAdi", "DogumYeri", "DogumTarihi", "Uyrugu", "KimlikBelgesiTuru", "KimlikSeriNo", "NufusaKayitliOlduguIl", "NufusaKayitliOlduguIlce", "NufusaKayitliOlduguMahalle", "NufusCilt", "NufusAileSira", "NufusSiraNo", "Cinsiyet", "MedeniHali", "Isi", "IkametAdresi", "GelisTarihi", "AyrilisTarihi", "VerilenOdaNo", "AracPlakaNo" };

        XmlDocument xmlDoc;
        XmlNode userNode, konaklama;
        XmlAttribute attribute;
        string XMLDocumentString;


        public void createXMLFile(IEnumerable<Record> models)
        {
            createXMLFromStaticData(models);

            string MD5Code = createMD5HashCode(XMLDocumentString);

            addMD5AndDeclarationToXMLFile2(XMLDocumentString, MD5Code);

            //xml in adını oluştur
            string XmlFileName = XMLFileName();
            //xmlDoc.Save(XmlFileName + ".xml");
            //saveFile(XmlFileName);
            string a = XMLDocumentString;

            //DisplayDownloadDialog(XmlFileName);
        }

        protected void DisplayDownloadDialog(string fileName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", "" + fileName + ".xml"));

            HttpContext.Current.Response.ContentType = "application/octet-stream";

            xmlDoc.Save(HttpContext.Current.Response.OutputStream);
            //HttpContext.Current.Response.Write(HttpContext.Current.Response.OutputStream);
            HttpContext.Current.Response.End();
        }
        public void saveFile(string fileName)
        {
            //Response.Clear();
            //Response.AppendHeader("Content-Disposition", "filename="+fileName+".xml");
            //Response.AppendHeader("Content-Length", byteArray.Length.ToString());
            //Response.ContentType = "application/octet-stream";
            //Response.BinaryWrite(byteArray);
            //xmlWriter.Close();
        }

        // kimlikbildirim_2017-04-10.xml formatındaki dosya adı
        string XMLFileName()
        {
            string Day, Month;

            int day = DateTime.Now.Day;
            if (day < 10)
            {
                string day0 = day.ToString();
                Day = "0" + day0;
            }
            else
            {
                Day = day.ToString();
            }


            int month = DateTime.Now.Month;
            if (month < 10)
            {
                string month0 = month.ToString();
                string month1 = "0" + month0;
                Month = month1;
            }
            else
            {
                Month = month.ToString();
            }

            int year = DateTime.Now.Year;

            return "kimlikbildirim_" + year + "_" + Month + "_" + Day;
        }


        //static datadan xml oluşturma
        public void createXMLFromStaticData(IEnumerable<Record> models)
        {
            Record record = models.FirstOrDefault();
            // Fill <Konaklama> </Konaklama> node with static data      
            xmlDoc = new XmlDocument();
            konaklama = xmlDoc.CreateElement(konaklamaNodeValue);

            XmlAttribute TesisKodu = xmlDoc.CreateAttribute(konaklamaAttributes[0]);
            TesisKodu.Value = record.TesisId.ToString();
            // konaklama = xmlDoc.CreateElement(TesisKodu.Value.ToString());
            konaklama.Attributes.Append(TesisKodu);

            XmlAttribute Tarih = xmlDoc.CreateAttribute(konaklamaAttributes[1]);
            Tarih.Value = DateTime.Now.ToString();
            konaklama.Attributes.Append(Tarih);

            XmlAttribute GonderenProgram = xmlDoc.CreateAttribute(konaklamaAttributes[2]);
            GonderenProgram.Value = konaklamaValues[2].ToString();
            konaklama.Attributes.Append(GonderenProgram);

            XmlAttribute GonderenProgramVersiyon = xmlDoc.CreateAttribute(konaklamaAttributes[3]);
            GonderenProgramVersiyon.Value = konaklamaValues[3].ToString();
            konaklama.Attributes.Append(GonderenProgramVersiyon);

            xmlDoc.AppendChild(konaklama);

            //<Kisi> </Kisi>
            foreach (var model in models)
            {
                // create <Kisi> node
                userNode = xmlDoc.CreateElement(staticUserNodeName);

                // get Attributes and set value in <Kisi> node
                XmlAttribute SiraNo = xmlDoc.CreateAttribute(nod2Attributes[0]);
                //if (model.SiraNo == null)
                //    model.SiraNo = 0;
                SiraNo.Value = model.SiraNo.ToString();
                userNode.Attributes.Append(SiraNo);

                XmlAttribute TCKimlikNo = xmlDoc.CreateAttribute(nod2Attributes[1]);
                TCKimlikNo.Value = model.TCKimlikNo.ToString();
                userNode.Attributes.Append(TCKimlikNo);

                XmlAttribute Adi = xmlDoc.CreateAttribute(nod2Attributes[2]);
                Adi.Value = model.Adi.ToString();
                userNode.Attributes.Append(Adi);

                XmlAttribute Soyadi = xmlDoc.CreateAttribute(nod2Attributes[3]);
                Soyadi.Value = model.Soyadi.ToString();
                userNode.Attributes.Append(Soyadi);

                XmlAttribute BabaAdi = xmlDoc.CreateAttribute(nod2Attributes[4]);
                BabaAdi.Value = model.BabaAdi.ToString();
                userNode.Attributes.Append(BabaAdi);

                XmlAttribute AnaAdi = xmlDoc.CreateAttribute(nod2Attributes[5]);
                AnaAdi.Value = model.AnaAdi.ToString();
                userNode.Attributes.Append(AnaAdi);

                XmlAttribute DogumYeri = xmlDoc.CreateAttribute(nod2Attributes[6]);
                DogumYeri.Value = model.DogumYeri.ToString();
                userNode.Attributes.Append(DogumYeri);

                XmlAttribute DogumTarihi = xmlDoc.CreateAttribute(nod2Attributes[7]);
                DogumTarihi.Value = model.DogumTarihi.ToString();
                userNode.Attributes.Append(DogumTarihi);

                XmlAttribute Uyrugu = xmlDoc.CreateAttribute(nod2Attributes[8]);
                Uyrugu.Value = model.Uyrugu.ToString();
                userNode.Attributes.Append(Uyrugu);

                XmlAttribute KimlikBelgesiTuru = xmlDoc.CreateAttribute(nod2Attributes[9]);
                KimlikBelgesiTuru.Value = model.KimlikBelgesiTuru.ToString();
                userNode.Attributes.Append(KimlikBelgesiTuru);

                XmlAttribute KimlikSeriNo = xmlDoc.CreateAttribute(nod2Attributes[10]);
                KimlikSeriNo.Value = model.KimlikSeriNo.ToString();
                userNode.Attributes.Append(KimlikSeriNo);

                XmlAttribute NufusaKayitliOlduguIl = xmlDoc.CreateAttribute(nod2Attributes[11]);
                NufusaKayitliOlduguIl.Value = model.NufusaKayitliOlduguIl.ToString();
                userNode.Attributes.Append(NufusaKayitliOlduguIl);

                XmlAttribute NufusaKayitliOlduguIlce = xmlDoc.CreateAttribute(nod2Attributes[12]);
                NufusaKayitliOlduguIlce.Value = model.NufusaKayitliOlduguIlce.ToString();
                userNode.Attributes.Append(NufusaKayitliOlduguIlce);

                XmlAttribute NufusaKayitliOlduguMahalle = xmlDoc.CreateAttribute(nod2Attributes[13]);
                NufusaKayitliOlduguMahalle.Value = model.NufusaKayitliOlduguMahalle.ToString();
                userNode.Attributes.Append(NufusaKayitliOlduguMahalle);

                XmlAttribute NufusCilt = xmlDoc.CreateAttribute(nod2Attributes[14]);
                NufusCilt.Value = model.NufusCilt.ToString();
                userNode.Attributes.Append(NufusCilt);

                XmlAttribute NufusAileSira = xmlDoc.CreateAttribute(nod2Attributes[15]);
                NufusAileSira.Value = model.NufusAileSira.ToString();
                userNode.Attributes.Append(NufusAileSira);

                XmlAttribute NufusSiraNo = xmlDoc.CreateAttribute(nod2Attributes[16]);
                NufusSiraNo.Value = model.NufusSiraNo.ToString();
                userNode.Attributes.Append(NufusSiraNo);

                XmlAttribute Cinsiyet = xmlDoc.CreateAttribute(nod2Attributes[17]);
                Cinsiyet.Value = model.Cinsiyet.ToString();
                userNode.Attributes.Append(Cinsiyet);

                XmlAttribute MedeniHali = xmlDoc.CreateAttribute(nod2Attributes[18]);
                MedeniHali.Value = model.MedeniHali.ToString();
                userNode.Attributes.Append(MedeniHali);

                XmlAttribute Isi = xmlDoc.CreateAttribute(nod2Attributes[19]);
                Isi.Value = model.Isi.ToString();
                userNode.Attributes.Append(Isi);

                XmlAttribute IkametAdresi = xmlDoc.CreateAttribute(nod2Attributes[20]);
                IkametAdresi.Value = model.IkametAdresi.ToString();
                userNode.Attributes.Append(IkametAdresi);

                XmlAttribute GelisTarihi = xmlDoc.CreateAttribute(nod2Attributes[21]);
                GelisTarihi.Value = model.GelisTarihi.ToString();
                userNode.Attributes.Append(GelisTarihi);

                XmlAttribute AyrilisTarihi = xmlDoc.CreateAttribute(nod2Attributes[22]);
                AyrilisTarihi.Value = model.AyrilisTarihi.ToString();
                userNode.Attributes.Append(AyrilisTarihi);

                XmlAttribute VerilenOdaNo = xmlDoc.CreateAttribute(nod2Attributes[23]);
                VerilenOdaNo.Value = model.VerilenOdaNo.ToString();
                userNode.Attributes.Append(VerilenOdaNo);

                XmlAttribute AracPlakaNo = xmlDoc.CreateAttribute(nod2Attributes[24]);
                AracPlakaNo.Value = model.AracPlakaNo.ToString();
                userNode.Attributes.Append(AracPlakaNo);

                // add <Kisi> node to <Konaklama> node
                konaklama.AppendChild(userNode);
            }

            // assign all XML content to a string variable
            XMLDocumentString = xmlDoc.OuterXml;
            Console.WriteLine(XMLDocumentString);
        }

        //create MD5HashCode
        public string createMD5HashCode(string OneLineXmlString)
        {
            Encoding encoding = Encoding.GetEncoding("ISO-8859-9"); //generate utf-8 encoding 

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(OneLineXmlString);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            string x = sb.ToString();
            return sb.ToString();
        }

        //Add MD5HashCode to XML Document 
        public void addMD5AndDeclarationToXMLFile(string xmlFile, string MD5HashCode)
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xmlFile);
            // insert declaration and version
            XmlDeclaration xmldecl;
            xmldecl = doc.CreateXmlDeclaration("1.0", null, null);
            xmldecl.Encoding = "ISO-8859-9";
            xmldecl.Standalone = "";
            //xmldecl.Standalone = "yes";

            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmldecl, root);

            var hashCode = doc.CreateProcessingInstruction("hash", "" + MD5HashCode);
            doc.InsertBefore(hashCode, root);

            // assign all XML content to string variable
            XMLDocumentString = doc.OuterXml;
            //xmlDoc.doc(HttpContext.Current.Response.OutputStream);
            XMLDocumentString = doc.OuterXml;
        }
        public void addMD5AndDeclarationToXMLFile2(string xmlFile, string MD5HashCode)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile);

            XmlDeclaration xmldecl;
            xmldecl = doc.CreateXmlDeclaration("1.0", "ISO-8859-9", null);
            xmldecl.Encoding = "ISO-8859-9";
            xmldecl.Standalone = "";
            //xmldecl.Standalone = "yes";

            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmldecl, root);

            var hashCode = doc.CreateProcessingInstruction("hash", "" + MD5HashCode);
            doc.InsertBefore(hashCode, root);

            //XMLDocumentString = doc.OuterXml;

            string XmlFileName = XMLFileName();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", 
                string.Format("attachment; filename={0}", "" + XmlFileName + ".xml"));
            HttpContext.Current.Response.AddHeader("Content-Length", doc.OuterXml.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream"; 
            //HttpContext.Current.Response.Charset = "ISO-8859-9";
            var isoEncoding = Encoding.GetEncoding("ISO-8859-9");
            HttpContext.Current.Response.ContentEncoding = isoEncoding;
            
            HttpContext.Current.Response.Write(doc.OuterXml);
            HttpContext.Current.Response.End();

        }

    }



}
