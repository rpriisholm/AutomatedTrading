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


#pragma warning disable IDE0017 // Simplify object initialization
            WebBrowser wb = new WebBrowser();
#pragma warning restore IDE0017 // Simplify object initialization
            wb.AllowNavigation = true;
            wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WB_DocumentCompleted);
            wb.Navigate(url);
            do
            {
                Application.DoEvents();
                Thread.Sleep(1000);
            } while (!IsReady);



            var dd = wb.Document.DomDocument as IHTMLDocument2;
            string val = dd.body.parentElement.outerHTML;
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            string s = null;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
        }

        private static void WB_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            IsReady = true;
        }

    }
}
