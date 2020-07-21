// С страницы http://finance.i.ua/ (сохранить файл как html страницу).
// Выполнить экспорт курса доллара по всем банкам.
// Из input файла забрать только необходимую информацию о названии банка,
// курсе покупки и продажи и записать ее в output.xml файл

using System;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace Cs17_1_t01
{
    class Program
    {
        static void ExportRates()
        {
            using (StreamReader reader = new StreamReader("finance.html"))
            {
                string line;
                Regex regex = new Regex(@"<table id=""latest_currency_container"" class=""table table-data"">.+<\/table>");
                while ((line = reader.ReadLine()) != null)
                {
                    MatchCollection matches = regex.Matches(line);
                    if (matches.Count > 0)
                    {
                        line = matches[0].Value;
                        break;
                    }
                }
                    
                using (var writer = new XmlTextWriter("output.xml", Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartDocument();
                    {
                        writer.WriteDocType("banks", null, null, null);
                        writer.WriteStartElement("banks");
                        {

                            try
                            {
                                XmlDocument doc = new XmlDocument();
                                doc.LoadXml(line);

                                XmlElement xRoot = doc.DocumentElement;
                                foreach (XmlNode xnode in xRoot)
                                {
                                    if (xnode.Name == "tbody" && xnode.Attributes[0].Value == "bank_rates_usd")
                                        foreach (XmlNode childnode in xnode.ChildNodes)
                                        {
                                            writer.WriteStartElement("bank");
                                            {
                                                writer.WriteStartElement("name");
                                                {
                                                    writer.WriteString(childnode.ChildNodes[0].InnerText);
                                                }
                                                writer.WriteEndElement();
                                                writer.WriteStartElement("buy_rate");
                                                {
                                                    writer.WriteString(childnode.ChildNodes[1].InnerText);
                                                }
                                                writer.WriteEndElement();
                                                writer.WriteStartElement("sell_rate");
                                                {
                                                    writer.WriteString(childnode.ChildNodes[2].InnerText);
                                                }
                                                writer.WriteEndElement();
                                            }
                                            writer.WriteEndElement();
                                        }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndDocument();
                    Console.WriteLine("\nOutput File Saved Successfully\n");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            
            ExportRates();
        }
    }
}