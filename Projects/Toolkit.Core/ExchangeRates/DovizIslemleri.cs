using System;
using System.Xml;
using System.Xml.Linq;

namespace LOGI.Framework.Toolkit.Core.ExchangeRates
{
    public class DovizIslemleri
    {
        private static string Formatla(string deger, int basamakSayisi)
        {
            if (!OndalikKontrol(deger, basamakSayisi))
            {
                return OndalikAyarla(deger, basamakSayisi);
            }
            else
            {
                return deger;
            }
        }
        private static bool OndalikKontrol(string deger, int ondalik)
        {
            for (int i = 0; i < deger.Length; i++)
            {
                if (deger[i] == '.')
                {
                    if (deger.Length - (i + 1) != ondalik)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private static string OndalikAyarla(string deger, int basamak)
        {
            for (int i = 0; i < deger.Length; i++)
            {
                if (deger[i] == '.')
                {
                    int ondalik = deger.Length - (i + 1);

                    if (ondalik < basamak)
                    {
                        for (int f = 0; f < basamak - ondalik; f++)
                        {
                            deger = deger.Insert(deger.Length, "0");
                        }
                        return deger;
                    }
                    else
                    {
                        deger = deger.Remove((i + 1) + basamak);
                        return deger;
                    }
                }
            }
            return deger;
        }
        private static string KurXmlGetir(string XmlPath, string Kod, string Tur)
        {
            try
            {
                XmlTextReader rdr = new XmlTextReader(XmlPath);
                XmlDocument MyXml = new XmlDocument();
                MyXml.Load(rdr);
                XmlNodeList MyNode = MyXml.SelectNodes("/Tarih_Date/Currency[@Kod ='" + Kod + "']/" + Tur);
                string sonuc = MyNode.Item(0).InnerXml.ToString();
                sonuc = sonuc.Replace('.', ',');
                sonuc = Formatla(sonuc, 4);
                return sonuc;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public DovizIslemleri()
        {
        }

        public static string KurDegerAl(DateTime date, ExchangeRateCurrencyCodes code, ExchangeOperations islem)
        {
            string tur = islem.ToString();
            string dovizCode = code.ToString();

            string KurDeger = String.Empty;
            //string YilAy = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString()
            for (int i = 0; i > -29; i--)
            {
                try
                {
                    DateTime tarih = date.AddDays(i);
                    string Yil = tarih.Year.ToString();
                    string Ay = tarih.Month > 9 ? tarih.Month.ToString() : "0" + tarih.Month.ToString();
                    string Gun = tarih.Day > 9 ? tarih.Day.ToString() : "0" + tarih.Day.ToString();
                    string newPath = "http://www.tcmb.gov.tr/kurlar/" + Yil + Ay + "/" + Gun + Ay + Yil + ".xml";

                    KurDeger = KurXmlGetir(newPath, dovizCode, tur);
                    if (!String.IsNullOrEmpty(KurDeger))
                    {
                        break;
                    }
                }
                catch (Exception)
                {

                }
            }
            return KurDeger;
        }

        public static XDocument TumKurlar(DateTime date)
        {
            XDocument retXDocument = new XDocument();
            XmlDocument MyXml = null;
            //string YilAy = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString()
            for (int i = 0; i > -29; i--)
            {
                try
                {
                    DateTime tarih = date.AddDays(i);
                    string Yil = tarih.Year.ToString();
                    string Ay = tarih.Month > 9 ? tarih.Month.ToString() : "0" + tarih.Month.ToString();
                    string Gun = tarih.Day > 9 ? tarih.Day.ToString() : "0" + tarih.Day.ToString();
                    string newPath = "http://www.tcmb.gov.tr/kurlar/" + Yil + Ay + "/" + Gun + Ay + Yil + ".xml";

                    XmlTextReader rdr = new XmlTextReader(newPath);
                    MyXml = new XmlDocument();
                    MyXml.Load(rdr);

                    break;
                }
                catch (Exception)
                {
                }
            }
            if (MyXml != null)
            {
                retXDocument = XDocument.Parse(MyXml.OuterXml);
            }
            return retXDocument;
        }
    }
}
