using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Facet.Combinatorics;

namespace NameGenerator
{
    class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {

            Console.Write("Enter the folder path to save the file: ");
            string folder = Console.ReadLine();
            Console.WriteLine("The generated file will be generated with number unique name");

            StartTheProcess:
            int file = 0;
            Console.WriteLine("The longer you input, the longer the list");
            Console.Write(
                "Enter seed words with space (for example 'tran van tuan dep trai nguyen le an phuong xau xi'): ");
            string fullName = Console.ReadLine();
            
            string[] input = fullName.Split(' ');
            ObservableCollection<string> result = new ObservableCollection<string>();
            
            for (int i = 2;i<=input.Length;i++)
            {

                


                Variations<string> variations = new Variations<string>(input, i);
                foreach (IList<string> c in variations)
                {
                    string temp = "";

                    bool notAvaiName = false;

                    //Write first name
                    for (int index = 0; index < c.Count; index++)
                    {
                        string s = c[index];
                        int n;
                        bool isNumeric = int.TryParse(s, out n);

                        if (index == 0 && isNumeric)
                        {
                            notAvaiName = true;
                        }

                        if (!isNumeric && temp.Split('|').Count() < 3)
                        {
                            temp = temp + s + "|";
                        }
                    }

                    temp = temp + c.Aggregate("", (current, s) => current + s);
                    if (!notAvaiName)
                    {
                        result.Add(temp);
                        Console.WriteLine(temp);
                    }
                }
            }

            System.IO.File.WriteAllLines(folder + "\\result" + file + ".txt", result);
            file++;

            Console.WriteLine("You can continue generating another list with numberical file name");

            goto StartTheProcess;

            //Combinations<char> combinations = new Combinations<char>(inputSet, 3);
            //string cformat = "Combinations of {{A B C D}} choose 3: size = {0}";
            //Console.WriteLine(String.Format(cformat, combinations.Count));
            //foreach (IList<char> c in combinations)
            //{
            //    Console.WriteLine(String.Format("{{{0} {1} {2}}}", c[0], c[1], c[2]));
            //}

            //Variations<char> variations = new Variations<char>(inputSet, 2);
            //string vformat = "Variations of {{A B C D}} choose 2: size = {0}";
            //Console.WriteLine(String.Format(vformat, variations.Count));
            //foreach (IList<char> v in variations)
            //{
            //    Console.WriteLine(String.Format("{{{0} {1}}}", v[0], v[1]));
            //}
        }

        public static string Object2Xml<T>(T value, string fileName)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));

                var stringWriter = new StringWriter();
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
                {
                    Indent = true,
                    Encoding = Encoding.UTF8
                };

                using (var writer = XmlWriter.Create(stringWriter, xmlWriterSettings))
                {
                    xmlserializer.Serialize(writer, value);
                    File.WriteAllText(@"E:\Data\Work\45 - TourGuide\Data\" + fileName + ".xml", stringWriter.ToString());
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }
    }
}
