using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Windows.Forms;
using mshtml;
using System.Threading;

namespace SymbolExtractorEtoro_Test
{
    public class Program
    {
        static bool IsReady = false;


        [STAThread]
        public static void Main(string[] args)
        {
            string url = "https://www.etoro.com/discover/markets/stocks/industry/technology?sort=SymbolFull";

            // c# get rendered html


            WebBrowser wb = new WebBrowser();
            wb.AllowNavigation = true;
            wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
            wb.Navigate(url);
            do
            {
                Application.DoEvents();
                Thread.Sleep(1000);
            } while (!IsReady);



            var dd = wb.Document.DomDocument as IHTMLDocument2;
            string val = dd.body.parentElement.outerHTML;
            string s = null;
        }

        private static void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            IsReady = true;
        }

    }
}
