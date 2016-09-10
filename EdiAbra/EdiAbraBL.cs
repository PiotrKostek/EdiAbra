using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EdiAbra
{
    public class EdiAbraBl
    
    {
        public List<string> listaPlikowEdi= new List<string>();
        public List<DokumentRecadvEdiAbra> listaDokumentyRecadvEdi = new List<DokumentRecadvEdiAbra>();

        public void wczytajListePlikow() 
        {
            listaPlikowEdi.Clear();
            try
            {
                OpenFileDialog pliki = new OpenFileDialog();
                pliki.Filter = "Pliki EDI(*.xml)|*.xml|Wszystkie pliki(*.*)|*.*";
                pliki.InitialDirectory = Properties.Settings.Default.katalogEdi;
                pliki.Multiselect = true;
                pliki.ShowDialog();
                listaPlikowEdi = pliki.FileNames.ToList();
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                MessageBox.Show("Błąd nie odnaleziono katalogu", "Błąd");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd");
            }

            
        }

        void przeniesPlikEdi(string nazwaPliku)
        {

        }
        public int zaladujPlikiEdi() //zaczytuje podany plik i zwraca jego zawartość jako string
        {
            listaDokumentyRecadvEdi.Clear();
            FabrykaDokumentRecadvEdiAbra fabrykaDokumentowRecadv = new EdiAbra.FabrykaDokumentRecadvEdiAbra();
            if (listaPlikowEdi.Count() > 0)
            {
                try
                {
                    foreach (string nazwaPliku in listaPlikowEdi)
                    {
                        DokumentRecadvEdiAbra dokument = fabrykaDokumentowRecadv.budujDokumentAbra(nazwaPliku);
                        if(dokument!=null) listaDokumentyRecadvEdi.Add(dokument);
                    }
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "Błąd");
                    
                }
            }
            return listaPlikowEdi.Count();
            
        }


 
    }
}
