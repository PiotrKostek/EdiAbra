using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cdn_api;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace EdiAbra
{
    class CdnApiBl
    {
        int idSesji;
        string connectionString;
        public XLLoginInfo_20152 loginInfo { get; private set; }
        public XLPolaczenieInfo_20152 polaczenieInfo { get; private set; }

        public CdnApiBl()
        {
 
        }

        public int zaloguj()
        {
            polaczenieInfo = new XLPolaczenieInfo_20152();
            polaczenieInfo.Wersja = 20152;
            loginInfo = new XLLoginInfo_20152();
            loginInfo.ProgramID = "Program EDIAbra";
            loginInfo.Wersja = 20152;
            loginInfo.Winieta = -1;
            idSesji = 0;
            int wynik=cdn_api.cdn_api.XLLogin(loginInfo, ref idSesji);
            if(wynik==0)
            {
                wynik=cdn_api.cdn_api.XLPolaczenie(polaczenieInfo);
                connectionString = "SERVER = " + polaczenieInfo.Serwer + "; DATABASE = " + polaczenieInfo.Baza + "; TRUSTED_CONNECTION = No; UID = ComarchCDNXLADO; PWD = xT#h#VLDiT#xTbF53e+5TKa>fc1SnvbC=9afV><cI#-U=272eL;;Application Name = Comarch ERP XL:1:969:0:ADMIN:1"; 
            }
            return wynik;
        }

        public int wyloguj()
        {
            int wynik=cdn_api.cdn_api.XLLogout(idSesji);
            return wynik;

        }

        public int przepiszNumeryPozycjiNaWz(DokumentRecadvEdiAbra dokumentEdi) // przepisujemy do atrybutu "numer linii u klienta" na pozycjach WZ numery pozycji z potwierdzenia
        {
            int wynik = 0;
            wynik = 1; 
            return wynik;
        }

        
        public int wystawFaktureDoWz(DokumentRecadvEdiAbra dokumentEdi )
        {
            return 1;

        }

        result procesujDokumentEdi(DokumentRecadvEdiAbra dokumentEdi)
        {
            if (uzupelnijDaneDokumentuEdi(dokumentEdi) == result.OK)
            {
                uzupelnijDaneLiniiWz(dokumentEdi);
                return result.OK;
            }
            else return result.ERROR;
        }

        result uzupelnijDaneDokumentuEdi(DokumentRecadvEdiAbra dokumentEdi)
        {
            if (idSesji > 0)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = connectionString; // "SERVER=192.168.0.111;DATABASE=CDNXL_FMWOJCIK;TRUSTED_CONNECTION=No;UID=ComarchCDNXLADO;PWD=xT#h#VLDiT#xTbF53e+5TKa>fc1SnvbC=9afV><cI#-U=272eL;;Application Name = Comarch ERP XL:1:969:0:ADMIN:1";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT TrN_GIDTyp,TrN_GIDNumer,TrN_GidFirma  FROM cdn.TraNag where TrN_TrNNumer =" + dokumentEdi.numerWz + " and TrN_TrNRok =" + dokumentEdi.rokWz + " and TrN_TrNSeria ='" + dokumentEdi.seriaWz + "' and TrN_GIDTyp = 2001 ";

                DataTable tbDokumentWz = new DataTable();

                try
                {
                    con.Open();
                    //               SqlDataReader reader = cmd.ExecuteReader();
                    //tbLinieWz.Load(reader);
                    SqlDataReader reader = cmd.ExecuteReader();
                    tbDokumentWz.Load(reader);
                    if(tbDokumentWz.Rows.Count==1)
                    {
                        dokumentEdi.GidFirma =Convert.ToInt32(tbDokumentWz.Rows[0]["TrN_GidFirma"]);
                        dokumentEdi.GidTyp = Convert.ToInt32(tbDokumentWz.Rows[0]["TrN_GIDTyp"]);
                        dokumentEdi.GidNumer = Convert.ToInt32(tbDokumentWz.Rows[0]["TrN_GIDNumer"]);
                        return result.OK;
                    }
                        else return result.ERROR;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Użytkownik niezalogowany do CDN", "Błąd");
                    return result.ERROR;
                }
                
            }
            else return result.BRAK_SESJI;
        }
        result uzupelnijDaneLiniiWz (DokumentRecadvEdiAbra dokumentEdi) // Wyciągnięcie listy linii WZtki CDNowej o numerze z dokumentu EDI
        {
            //List<LiniaWzCdn> listaLinii= new List<LiniaWzCdn>();

            if (idSesji > 0)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = connectionString; // "SERVER=192.168.0.111;DATABASE=CDNXL_FMWOJCIK;TRUSTED_CONNECTION=No;UID=ComarchCDNXLADO;PWD=xT#h#VLDiT#xTbF53e+5TKa>fc1SnvbC=9afV><cI#-U=272eL;;Application Name = Comarch ERP XL:1:969:0:ADMIN:1";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT TrE_GIDTyp,TrE_GIDNumer,TrE_GIDLp,Twr_Ean,TrE_Ilosc  FROM cdn.TraNag tra inner join cdn.TraElem elem on tra.TrN_GIDNumer = elem.TrE_GIDNumer and tra.TrN_GIDTyp = elem.TrE_GIDTyp left join cdn.TwrKarty twr on elem.TrE_TwrNumer=twr.Twr_GIDNumer "
                    + "where tra.TrN_TrNNumer =" + dokumentEdi.numerWz + " and TrN_TrNRok =" + dokumentEdi.rokWz+" and TrN_TrNSeria ='"+ dokumentEdi.seriaWz +"' and TrN_GIDTyp = 2001 ";

                DataTable tbLinieWz = new DataTable();

                con.Open();
                //               SqlDataReader reader = cmd.ExecuteReader();
                //tbLinieWz.Load(reader);
                SqlDataReader reader = cmd.ExecuteReader();
                tbLinieWz.Load(reader);
                
                    if (tbLinieWz.Rows.Count>0 & dokumentEdi.indeksy.Count>0 & (tbLinieWz.Rows.Count == dokumentEdi.indeksy.Count )) // Jeśli ilość indeksów w dokumencie EDI i dokumencie CDN zgodne i większe niż 0
                    {
                        foreach(LiniaIndeksu wierszIndeksu  in dokumentEdi.indeksy)
                    {
                        //var liniaEdi = dokumentEdi.indeksy.SingleOrDefault(ean=>(ean row["Twr_Ean"])
                        wierszIndeksu.GidTyp = Convert.ToInt32(row["TrE_GIDTyp"]);
                        wierszIndeksu.GidNumer = Convert.ToInt32(row["TrE_GIDNumer"]);
                        wierszIndeksu.GidLp = Convert.ToInt32(row["TrE_GIDLp"]);
                        wierszIndeksu.EanCdn = row["Twr_Ean"].ToString();
                        liniaWzCdn.
                        listaLinii.Add(liniaWzCdn);
                    }
 
                    }
                con.Close();
                
            }
            if (listaLinii.Count > 0) return listaLinii;
            else return null;
        }

        public void dopasujLinieEdiDoCdn(List<LiniaWzCdn> linieCdn, List<LiniaIndeksu> linieEdi)
        {
            if(linieCdn.Count==linieEdi.Count)
            {
                return 
            }
            else return 
        }
        public int dodajAtrybut()
        {
            return 0;
        }

    }

    
    public class LiniaWzCdn
    {
        public int GidTyp;
        public int GidNumer;
        public int GidLp;
        public string EanCdn;

    }
   
}
