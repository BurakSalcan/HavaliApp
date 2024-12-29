using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HavaliApp
{
    public partial class HavaliApp : Form
    {
        public HavaliApp()
        {
            InitializeComponent();

        }

        private void HavaliApp_Load(object sender, EventArgs e)
        {

            #region Anlık veriler

            string yol = "https://api.openweathermap.org/data/2.5/weather?lat=39.773438&lon=30.519283&appid=23677514988f94eda5c1b79f854b61a4&mode=xml&units=metric&lang=tr";
            XDocument havadurumu = XDocument.Load(yol);
            XElement current = havadurumu.Root;

            double sicaklik = Convert.ToDouble(current.Element("temperature").Attribute("value").Value.Replace('.', ',')); //Bu şekilde mevcut derece yuvarlanıyor.
            lbl_sicaklik.Text = Convert.ToInt32(sicaklik).ToString() + "℃";

            double min = Convert.ToDouble(current.Element("temperature").Attribute("min").Value.Replace('.', ','));
            lbl_min.Text = "Min: " + Convert.ToInt32(min).ToString() + "℃";

            double max = Convert.ToDouble(current.Element("temperature").Attribute("max").Value.Replace('.', ','));
            lbl_max.Text = "Max: " + Convert.ToInt32(max).ToString() + "℃";

            string sehir = current.Element("city").Attribute("name").Value;
            lbl_sehir.Text = sehir;

            double hissedilen = Convert.ToDouble(current.Element("feels_like").Attribute("value").Value.Replace('.', ','));
            lbl_hissedilen.Text = "Hissedilen: " + Convert.ToInt32(hissedilen).ToString() + "℃";

            string hava = current.Element("weather").Attribute("value").Value;
            string ikon = current.Element("weather").Attribute("icon").Value;
            string imagePath = "https://openweathermap.org/img/wn/" + ikon + "@2x.png"; //güvenlik hatası alırsan https kısmındaki s harfini sil.
            pb_anlikHava.ImageLocation = imagePath;
            lbl_hava.Text = hava.ToUpper(new CultureInfo("tr-TR"));//Bu yöntem ile alınan veriyi büyük harflerle yazdırabiliyoruz.

            DateTime zaman = DateTime.Now;
            lbl_saat.Text = zaman.Hour.ToString("D2"); //D2 olarak yazınca veriler çift basamaklı olarak geliyormuş.
            lbl_dakika.Text = zaman.Minute.ToString("D2");

            double nem = Convert.ToDouble(current.Element("humidity").Attribute("value").Value);
            lbl_nem.Text = Convert.ToInt32(nem).ToString() + "%";

            XElement yagmur = current.Element("precipitation");

            if (yagmur != null && current.Attribute("value") != null)
            {
                double yagis = Convert.ToDouble(current.Element("precipitation").Attribute("value").Value.Replace('.', ','));
                lbl_yagis.Text = yagis.ToString() + " mm";
            }
            else
            {
                lbl_yagis.Text = "Yağış yok";
            }

            double basinc = Convert.ToDouble(current.Element("pressure").Attribute("value").Value);
            lbl_basinc.Text = Convert.ToInt32(basinc).ToString() + " hPa";

            double gorus = Convert.ToDouble(current.Element("visibility").Attribute("value").Value);
            lbl_gorus.Text = Convert.ToInt32(gorus / 1000).ToString("0") + " km"; //ToString() içerisine tırnak içerisinde yazılan "0", tam sayı olarak formatlar. Kısaca virgülden sonraki kısımları göstermez.

            #endregion

            Haftalik();
        }

        private void Haftalik()
        {
            //string path = "https://api.openweathermap.org/data/2.5/forecast/dailylat=39.773438&lon=30.519283&cnt=7&appid=23677514988f94eda5c1b79f854b61a4&mode=xml&units=metric&lang=tr";

            //XDocument havadurumu = XDocument.Load(path);
            //XElement haftalik = havadurumu.Root;


        }
    }
}
