using HtmlAgilityPack;
using System;
using System.IO;

namespace GuatemalaBankCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // Scraping do site da Superintendência de Bancos da Guatemala
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.sib.gob.gt/buscador-instituciones/");
            HtmlNodeCollection rows = doc.DocumentNode.SelectNodes("//table[@id='tablaDatos']/tbody/tr");

            // Construção do script SQL de inserção
            string insertScript = "";
            foreach (HtmlNode row in rows)
            {
                HtmlNodeCollection cells = row.SelectNodes("td");
                if (cells != null && cells.Count == 3 && cells[1].InnerText.Trim() == "BANCO DE GUATEMALA")
                {
                    string bankCode = cells[0].InnerText.Trim();
                    string bankName = cells[1].InnerText.Trim();
                    insertScript += "INSERT INTO GuatemalaBanks (BankCode, BankName) VALUES ('" + bankCode + "', '" + bankName + "');\n";
                    break;
                }
            }

            // Gravação do script SQL em um arquivo
            string scriptFilePath = "insert_script.sql";
            File.WriteAllText(scriptFilePath, insertScript);
        }
    }
}
