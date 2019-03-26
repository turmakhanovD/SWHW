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

            using (FileStream fs = new FileStream("file.xml", FileMode.OpenOrCreate))
            {
                  
                if(people.Id != id)
                    formatter.Serialize(fs, people);
                else
                    Console.WriteLine($"{people.Id}.{people.Name}");

                
                Console.ReadLine();
            }
        }
    }
}
