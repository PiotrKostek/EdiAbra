using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EdiAbra
{
    public partial class Form1 : Form
    {
        EdiAbraBl ediAbraBl; 
        public Form1()
        {
            InitializeComponent();
            ediAbraBl = new EdiAbraBl();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ediAbraBl.wczytajListePlikow();
            
            listBoxPlikiEdi.DataSource = ediAbraBl.listaPlikowEdi;
            
        }

        private void buttonGenerujDokumenty_Click(object sender, EventArgs e)
        {
            ediAbraBl.zaladujPlikiEdi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DokumentRecadvEdiAbra dok = new EdiAbra.DokumentRecadvEdiAbra();
            CdnApiBl api = new CdnApiBl();
            api.zaloguj();
            api.PobierzListeLiniiWz(ediAbraBl.listaDokumentyRecadvEdi[0]);
            api.wyloguj();
        }
    }
}
