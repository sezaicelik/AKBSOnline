using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using MvcCms.Models;

namespace CleanCode
{
    public static class CreateXml
    {
        static string path = "C:\\Users\\baris\\Desktop\\KimlikBildirimSistemi\\AKBS";
        static string XMLFile = "AKBS.xml";
        static string TXTFile = "AKBS.txt";
        static string pathWithXMLFile = @"C:\Users\baris\Desktop\KimlikBildirimSistemi\AKBS\AKBS.xml";
        static string pathWithTXTFile = @"C:\Users\baris\Desktop\KimlikBildirimSistemi\AKBS\AKBS.txt";
        static string konaklamaNodeValue = "Konaklama";
        static string staticUserNodeName = "Kişi";

        static string[] konaklamaAttributes = { "TesisKodu", "Tarih", "GonderenProgram", "GonderenProgramVersiyon" };
        static string[] nod1Values = { "34275", "2017-04-10 23:58:18", "AKBS Online", "1" };
        // static string[] nod1Values = { "34275", "2017-04-10 23:58:18", "AKBS Online", "1" };
        static string[] nod2Attributes = { "SiraNo", "TCKimlikNo", "Adi", "Soyadi", "BabaAdi", "AnaAdi", "DogumYeri", "DogumTarihi", "Uyrugu", "KimlikBelgesiTuru", "KimlikSeriNo", "NufusaKayitliOlduguIl", "NufusaKayitliOlduguIlce", "NufusaKayitliOlduguMahalle", "NufusCilt", "NufusAileSira", "NufusSiraNo", "Cinsiyet", "MedeniHali", "Isi", "IkametAdresi", "GelisTarihi", "AyrilisTarihi", "VerilenOdaNo", "AracPlakaNo" };
        static string[] nod3Attributes = { "SiraNo", "TCKimlikNo", "Adi", "Soyadi", "BabaAdi", "AnaAdi", "DogumYeri", "DogumTarihi", "Uyrugu", "KimlikBelgesiTuru", "KimlikSeriNo", "NufusaKayitliOlduguIl", "NufusaKayitliOlduguIlce", "NufusaKayitliOlduguMahalle", "Cinsiyet", "Isi", "IkametAdresi", "GelisTarihi", "AyrilisTarihi", "VerilenOdaNo", "AracPlakaNo", "NufusCilt", "NufusAileSira", "NufusSiraNo", "MedeniHali" };
        static string[] values = { "1", "19814258132", "yusuf emre", "şencan", " mehmet emin", "nuran", "", "1986-06-21", "TC", "N", "", "İstanbul", "Zeytinburnu", "", "", "", "", "E", "", "", "maltepe", "2017-04-18 19:44:23", "", "100", "" };

        static XmlDocument xmlDoc, dynamicXML=null;
        static XmlNode userNode, konaklama;
        static XmlAttribute attribute;

        static void Main(string[] args)
        {


            //createXMLFromStaticData(pathWithXMLFile);

            var XMLString = readFromXMLFileToString(pathWithXMLFile);
            // Console.WriteLine(XMLString);
            //Console.WriteLine("--------------------------");
            var oneLineXMLString = StripXmlWhitespace(pathWithXMLFile);   //tek satıra çevirdik         
           // Console.WriteLine(oneLineString);

            //tek satırlık xml'den MD5 HashCode  üretiyoruz
            string MD5Code = createMD5HashCode(oneLineXMLString);
            Console.WriteLine(MD5Code);

            //XML'in ilk haline hashcode attributunu ekliyoruz
            addMD5AndDeclarationToXMLFile(pathWithXMLFile, MD5Code);
        }

        //static datadan xml oluşturma
        public static void createXMLFromStaticData(IEnumerable<Record>  models)
        {
            // Fill <Konaklama> </Konaklama> node with static data            
            konaklama = xmlDoc.CreateElement(konaklamaNodeValue);
            for (int k = 0; k < konaklamaAttributes.Length; k++)
            {
                attribute = xmlDoc.CreateAttribute(konaklamaAttributes[k]);
                attribute.Value = nod1Values[k];
                konaklama.Attributes.Append(attribute);
            }  

            xmlDoc.AppendChild(konaklama);

            //<Kisi> </Kisi>
            foreach (var model in models)
            {
                // create <Kisi> node
                userNode = xmlDoc.CreateElement(staticUserNodeName);

                // get Attributes and set value in <Kisi> node
                XmlAttribute SiraNo = xmlDoc.CreateAttribute(nod2Attributes[0]);
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
                Adi.Value = model.Adi.ToString();
                userNode.Attributes.Append(Adi);

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


            ////< xml file in <Kişi ... /> nodları
            //for (int j = 0; j < 2; j++)
            //{
            //    userNode = xmlDoc.CreateElement(staticUserNodeName);

            //    for (int i = 0; i < nod2Attributes.Length; i++)
            //    {
            //        attribute = xmlDoc.CreateAttribute(nod3Attributes[i]);
            //        attribute.Value = values[i];
            //        userNode.Attributes.Append(attribute);
            //    }

                

            //    konaklama.AppendChild(userNode);
            //}

            // xmlDoc.Save("C:\\Users\\bgul\\Desktop\\AKBS\\baris.xml");
            xmlDoc.Save("");
           // string fullSavePath = HttpContext.Current.Server.MapPath(string.Format("~/App_Data/Platypus{0}.csv", dbContextAsInt));

        }

        public static void GetNode2Attributes()
        { 
            var SiraNo = xmlDoc.CreateAttribute(nod2Attributes[0]);                
            var TCKimlikNo = xmlDoc.CreateAttribute(nod2Attributes[1]);
            var Adi = xmlDoc.CreateAttribute(nod2Attributes[2]);
            var Soyadi = xmlDoc.CreateAttribute(nod2Attributes[3]);
            var BabaAdi = xmlDoc.CreateAttribute(nod2Attributes[4]);
            var AnaAdi = xmlDoc.CreateAttribute(nod2Attributes[5]);
            var DogumYeri = xmlDoc.CreateAttribute(nod2Attributes[6]);
            var DogumTarihi = xmlDoc.CreateAttribute(nod2Attributes[7]);
            var Uyrugu = xmlDoc.CreateAttribute(nod2Attributes[8]);
            var KimlikBelgesiTuru = xmlDoc.CreateAttribute(nod2Attributes[9]);
            var KimlikSeriNo = xmlDoc.CreateAttribute(nod2Attributes[10]);
            var NufusaKayitliOlduguIl = xmlDoc.CreateAttribute(nod2Attributes[11]);
            var NufusaKayitliOlduguIlce = xmlDoc.CreateAttribute(nod2Attributes[12]);
            var NufusaKayitliOlduguMahalle = xmlDoc.CreateAttribute(nod2Attributes[13]);
            var NufusCilt = xmlDoc.CreateAttribute(nod2Attributes[14]);
            var NufusAileSira = xmlDoc.CreateAttribute(nod2Attributes[15]);
            var NufusSiraNo = xmlDoc.CreateAttribute(nod2Attributes[16]);
            var Cinsiyet = xmlDoc.CreateAttribute(nod2Attributes[17]);
            var MedeniHali = xmlDoc.CreateAttribute(nod2Attributes[18]);
            var Isi = xmlDoc.CreateAttribute(nod2Attributes[19]);
            var IkametAdresi = xmlDoc.CreateAttribute(nod2Attributes[20]);
            var GelisTarihi = xmlDoc.CreateAttribute(nod2Attributes[21]);
            var AyrilisTarihi = xmlDoc.CreateAttribute(nod2Attributes[22]);
            var VerilenOdaNo = xmlDoc.CreateAttribute(nod2Attributes[23]);
            var AracPlakaNo = xmlDoc.CreateAttribute(nod2Attributes[24]);  
        }
        //md5 hashcode üretme
        public static string createMD5HashCode(string OneLineXmlString)
        {

            Encoding encoding = Encoding.GetEncoding("ISO-8859-9"); //Or any other Encoding
            //string Something = File.ReadAllText(path + "\\xmlTekTirnak.txt");
            // string Something = "<Konaklama TesisKodu=\"34275\" Tarih=\"2017 - 04 - 10 23:58:18\" GonderenProgram=\"Basit_Otel\" GonderenProgramVersiyon =\"9.0\" ><Kisi SiraNo=\"1\" TCKimlikNo =\"10888665192\" Adi =\"GÖKÇE\" Soyadi =\"AKBULUT\" BabaAdi =\"AKAY\" AnaAdi =\"EMİNE\" DogumYeri =\"\" DogumTarihi =\"1997 - 06 - 12\" Uyrugu =\"TC\" KimlikBelgesiTuru =\"N\" KimlikSeriNo =\"890734\" NufusaKayitliOlduguIl =\"ANTALYA\" NufusaKayitliOlduguIlce =\"KORKUTELİ\" NufusaKayitliOlduguMahalle =\"\" NufusCilt =\"\" NufusAileSira =\"\" NufusSiraNo =\"\" Cinsiyet =\"K\" MedeniHali =\"\" Isi=\"\" IkametAdresi=\"ALTINKUM MAH.438 SOKPARLAZ APT.KAT.1   KONYA ALTI/ ANTALYA BATMAN\" GelisTarihi =\"2017 - 03 - 30 16:36:46\" AyrilisTarihi =\"\" VerilenOdaNo =\"1 / A\" AracPlakaNo =\"\" />";
            // step 1, calculate MD5 hash from input
            string Something = "Latin alfabesinde bulunmayan karakterleri ifade eder. Türkçe karakter sorunu ise bilgisayar ortamında Türk alfabesinde özgü bazı karakterlerin çıkardığı sorunu tanımlar. Latin alfabesinde bulunmayan bu karakterler ç, ı, ü, ğ, ö, ş, İ, Ğ, Ü, Ö, Ş, Ç harfleridir";



            MD5 md5 = System.Security.Cryptography.MD5.Create();

            //byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Something);
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

        public static void addMD5AndDeclarationToXMLFile(string xmlFile, string MD5HashCode)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);

            XmlDeclaration xmldecl, hash;
            xmldecl = doc.CreateXmlDeclaration("1.0", null, null);
            // hash = doc.DocumentElement(
            xmldecl.Encoding = "ISO-8859-9";
            xmldecl.Standalone = "yes";

            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmldecl, root);

            var hashCode = doc.CreateProcessingInstruction("hash", "=" + MD5HashCode);
            doc.InsertBefore(hashCode, root);

            doc.Save(xmlFile);

            // Display the modified XML document 
            Console.WriteLine(doc.OuterXml);
        }


        public static string readFromXMLFileToString(string xmlFile)
        {
            // string file = "C:\\Users\\bgul\\Desktop\\AKBS\\original.xml";
            string file = xmlFile;
            var xDocument = XDocument.Load(file);
            string xml = xDocument.ToString();
            //  Console.WriteLine(xml);

            string code = xml.GetHashCode().ToString();

            return xml;
        }

        //xml i tek satıra çevirme
        public static string StripXmlWhitespace(string xmlFile)
        {
            string file = xmlFile;
            var xDocument = XDocument.Load(file);
            string xml = xDocument.ToString();
            Regex Parser = new Regex(@">\s*<");
            xml = Parser.Replace(xml, "><");

            return xml.Trim();
        }

        public static void writeToTxtFile(string path)
        {
            Encoding encoding = Encoding.GetEncoding("ISO-8859-9"); //Or any other Encoding
            string Something = File.ReadAllText(path + "\\original.xml", encoding);

            //write to a file

            using (StreamWriter writer = new StreamWriter(path + "\\Test.txt", true, encoding))
            {
                writer.WriteLine(Something);
            }
        }


    }
}
