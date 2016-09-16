using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EdiAbra
{
    public class FabrykaDokumentRecadvEdiAbra
    {
        XmlDocument dokumentXml;
        DokumentRecadvEdiAbra dokumentAbra;

        public FabrykaDokumentRecadvEdiAbra()
        {

        }

        public DokumentRecadvEdiAbra budujDokumentAbra (string nazwaPliku)
        {
            dokumentAbra = new DokumentRecadvEdiAbra();
            dokumentAbra.nazwaPliku = nazwaPliku;
            if (!wczytajDokumentXml(nazwaPliku)) return null;
            if (!parsujXml()) return null;
            return dokumentAbra;
        }

        bool wczytajDokumentXml(string nazwaPliku)
        {
            dokumentXml = new XmlDocument();
            try
            {
                dokumentXml.Load(nazwaPliku);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd");
                return false;
            }
        }

        bool parsujXml()
        {

            try // całość objęta blokiem try - jakikolwiek bład powoduje przerwanie konwersji 
            {
                // parsowanie nagłówka dokumentu
                XmlNode wezelNaglowkowy = dokumentXml.SelectSingleNode("/Document-ReceivingAdvice/ReceivingAdvice-Header");
                XmlNode wezelKontrahenci = dokumentXml.SelectSingleNode("/Document-ReceivingAdvice/ReceivingAdvice-Parties");
                dokumentAbra.numerPrzesylki = wezelNaglowkowy.SelectSingleNode("ReceivingAdviceNumber").InnerText;
                dokumentAbra.dataPrzesylki = Convert.ToDateTime(wezelNaglowkowy.SelectSingleNode("ReceivingAdviceDate").InnerText);
                dokumentAbra.dataDostawy = Convert.ToDateTime(wezelNaglowkowy.SelectSingleNode("GoodsReceiptDate").InnerText);
                dokumentAbra.numerPelnyWz = wezelNaglowkowy.SelectSingleNode("DespatchNumber").InnerText;
                dokumentAbra.glnOdbiorca = wezelKontrahenci.SelectSingleNode("Buyer/ILN").InnerText;
                dokumentAbra.glnSprzedawca = wezelKontrahenci.SelectSingleNode("Seller/ILN").InnerText;
                dokumentAbra.glnMiejsceDostawy = wezelKontrahenci.SelectSingleNode("DeliveryPoint/ILN").InnerText;

                string[] elementyNumeruWz = dokumentAbra.numerPelnyWz.Split(new char[] { '/' },3,StringSplitOptions.RemoveEmptyEntries);
                dokumentAbra.numerWz = Convert.ToInt32(elementyNumeruWz[0]);
                dokumentAbra.rokWz = Convert.ToInt32(elementyNumeruWz[1]);
                dokumentAbra.seriaWz = elementyNumeruWz[2];
                // parsowanie linii dokumentu
                XmlNodeList linie = dokumentXml.SelectNodes("/Document-ReceivingAdvice/ReceivingAdvice-Lines/Line");
                //            XmlNodeList linie = dokumentXml.GetElementsByTagName("Line");
                foreach (XmlNode linia in linie)
                {
                    LiniaIndeksu liniaIndeksu = new LiniaIndeksu();
                    liniaIndeksu.numerLinii = Convert.ToInt32(linia.SelectSingleNode("Line-Item/LineNumber").InnerText);
                    liniaIndeksu.ean = linia.SelectSingleNode("Line-Item/EAN").InnerText;
                    liniaIndeksu.kodOdbiorcy = linia.SelectSingleNode("Line-Item/BuyerItemCode").InnerText;
                    liniaIndeksu.iloscOtrzymana = Convert.ToDecimal(linia.SelectSingleNode("Line-Item/QuantityReceived").InnerText.Replace('.', ','));
                    liniaIndeksu.iloscZamowiona = Convert.ToDecimal(linia.SelectSingleNode("Line-Item/OrderedQuantity").InnerText.Replace('.', ','));
                    liniaIndeksu.jednostkaMiary = linia.SelectSingleNode("Line-Item/UnitOfMeasure").InnerText;
                    dokumentAbra.indeksy.Add(liniaIndeksu);
                }

                return true;
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message +" "+dokumentAbra.nazwaPliku, "Błąd importu pliku");
                dokumentAbra.statusDokumentu = StatusDokumentEdi.Bledny;
                return false;
            }
        }


    }
 
}
