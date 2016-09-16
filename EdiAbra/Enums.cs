using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiAbra
{
    public enum StatusLiniiDokumentEdi
    {
        Nieprzetworzona,
        LiniaOK,
        BrakwCdn,
        BrakEan,
        RoznaIloscwCdn
    }

    public enum StatusDokumentEdi
    {
        Nieprzetworzony,
        DokumentOK,
        BrakwCdn,
        Bledny
    }

    public enum result
    {
        OK,
        ERROR,
        BRAK_SESJI,
        BRAK_DOKUMENTOW
    }
   
}
