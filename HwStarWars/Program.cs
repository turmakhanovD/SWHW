using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace HwStarWars
{
    class Program
    {
        static void Main(string[] args)
        {

            var url = "https://swapi.co/api/people/";
            People people = new People();


            Console.WriteLine("Enter Id of people: ");

            int id = 1;
            int.TryParse(Console.ReadLine(), out id);
            url += id;


            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {

                var result = streamReader.ReadToEnd();

                people = JsonConvert.DeserializeObject<People>(result);
                people.Id = id;
                Console.WriteLine(result.ToString());


            }

            XmlSerializer formatter = new XmlSerializer(typeof(People));

                XmlTextReader reader = new XmlTextReader("file.xml");
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // Узел является элементом.
                       if(reader.Name == "Id")
                            Console.WriteLine(reader.Name);
                        break;
                    case XmlNodeType.Text: // Вывести текст в каждом элементе.
                        if (reader.Name == "Id")
                            Console.WriteLine( reader.GetAttribute(1));
                        break;
                    case XmlNodeType.EndElement: // Вывести конец элемента.
                        Console.Write("</" + reader.Name);
                        Console.WriteLine(">");
                        break;
                }
            }
            reader.Close();
            using (FileStream fs = new FileStream("file.xml", FileMode.OpenOrCreate))
            {

                  //  formatter.Serialize(fs, people);


                
                Console.ReadLine();
            }
        }
    }
}
