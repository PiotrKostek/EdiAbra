using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EdiAbra
{
    public class DokumentRecadvEdiAbra // Dane z nagłówka dokumentu
    {
        public string nazwaPliku { get; set; }
        public XmlDocument dokumentXml { get; set; }
        public string glnOdbiorca { get; set; }
        public string glnSprzedawca { get; set; }
        public string glnMiejsceDostawy { get; set; }
        public string numerPrzesylki { get; set; }
        public DateTime dataPrzesylki { get; set; }
        public DateTime dataDostawy { get; set; }
        public string numerPelnyWz { get; set; }
        public int numerWz { get; set; }
        public int rokWz { get; set; }
        public string seriaWz { get; set; }
        public int GidTyp { get; set; }
        public int GidFirma { get; set; }
        public int GidNumer { get; set; }
        public List<LiniaIndeksu> indeksy { get; set; }
        

        public DokumentRecadvEdiAbra()
        {
            indeksy = new List<LiniaIndeksu>();
        }

        public bool  PrzeniesDoArchiwum()
            {
            try
            {
                File.Move(nazwaPliku, Properties.Settings.Default.katalogArchiw);
                return true;
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Błąd przeniesienia do archiwum");
                return false;
            }
        }


    }
    public class LiniaIndeksu
    {
        public int numerLinii { get; set; }
        public string ean { get; set; }
        public string kodOdbiorcy { get; set; }
        public decimal iloscOtrzymana { get; set; }
        public decimal iloscZamowiona { get; set; }
        public string jednostkaMiary { get; set; }
        public string numerZamowieniaOdbiorcy { get; set; }
        public int GidTyp { get; set; }
        public int GidNumer { get; set; }
        public int GidLp { get; set; }
        public decimal IloscCdn { get; set; }
        public StatusLiniiDokumentEdi StatusLinii {get;set;}
    }

}
