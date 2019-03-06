using ClassLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serial
{
    [Serializable]
    public class Person
    {
        public Person()
        {

        }
        public string name { get; set; }
        public int year { get; set; }
        public string title { get; set; }
        public string phone { get; set; }
        public string city { get; set; }

        [NonSerialized]
        public string iin;
        public Person(string name, int year)
        {
            this.name = name;
            this.year = year;
        }

        public void Method1()
        {
            Console.WriteLine("Hello");
        }
    }

    public class ServicePC<T>
    {
        public List<T> listPC = new List<T>();
        public void Add(T obj)
        {
            listPC.Add(obj);
        }
        public void SerializeObj(string path)
        {            
            //string path = "listSerial.txt";
            FileInfo fl = new FileInfo(path);
            string msg = "";
            if (!fl.Exists)
                msg = "New file created";
            else
                msg = "File rewrited";

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, listPC.ToArray());
            }

            
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ServicePC<PC> service = new ServicePC<PC>();
            for (int i = 0; i < 3; i++)
            {
                service.listPC.Add(new PC("Mark"+i, "SN"+i, "Model"+i));
            }
            service.SerializeObj("listSerial.txt");            

        }

        static void Exmpl01()
        {
            Person person1 = new Person("Сет", 30);
            Person person2 = new Person("Мет", 30);
            Person[] persons = new Person[] { person1, person2 };
            Console.WriteLine("object created");

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("people.data", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, person1);
                formatter.Serialize(fs, person2);
                Console.WriteLine("Object serialized");
            }

            using (FileStream fs = new FileStream("people.data", FileMode.Open))
            {
                //Person data = (Person)formatter.Deserialize(fs);
                Person[] data2 = (Person[])formatter.Deserialize(fs);
            }
        }

        static void Exmpl02()
        {
            Person person1 = new Person("Сет", 30);
            Person person2 = new Person("Мет", 30);
            Person[] persons = new Person[] { person1, person2 };
            Console.WriteLine("object created");

            SoapFormatter formatter = new SoapFormatter();
            using (FileStream fs = new FileStream("people.soap", FileMode.OpenOrCreate))
            {
                //formatter.Serialize(fs, person1);
                formatter.Serialize(fs, persons);
                Console.WriteLine("Object serialized");
            }

            using (FileStream fs = new FileStream("people.soap", FileMode.Open))
            {
                //Person data = (Person)formatter.Deserialize(fs);
                Person[] data2 = (Person[])formatter.Deserialize(fs);
            }
        }

        static void Exmpl03()
        {
            Person person1 = new Person("Сет", 30);
            Person person2 = new Person("Мет", 30);
            Person[] persons = new Person[] { person1, person2 };
            Console.WriteLine("object created");

            XmlSerializer formatter = new XmlSerializer(typeof(Person[]));
            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            {
                //formatter.Serialize(fs, person1);
                formatter.Serialize(fs, persons);
                Console.WriteLine("Object serialized");
            }

            using (FileStream fs = new FileStream("people.xml", FileMode.Open))
            {
                //Person data = (Person)formatter.Deserialize(fs);
                Person[] data2 = (Person[])formatter.Deserialize(fs);
            }
        }

        static void Exmpl04()
        {
            Person person1 = new Person("Сет", 30);
            Person person2 = new Person("Мет", 30);
            Person[] persons = new Person[] { person1, person2 };            

            string json = JsonConvert.SerializeObject(persons);
            Console.WriteLine(json);
            Person[] data2 = JsonConvert.DeserializeObject<Person[]>(json);
            foreach (Person item in data2)
            {
                Console.WriteLine(item.name);
            }
        }

        static void Exmpl05()
        {
            FileInfo fileInfo = new FileInfo("PhoneBook.csv");
            if (!fileInfo.Exists)
            {
                using (FileStream fs=fileInfo.Create())
                {
                    using (StreamWriter sw=new StreamWriter(fs))
                    {                        
                        for (int i = 0; i < 20; i++)
                        {
                            var user = RandomUser.GenerateUser.GetUser();
                            string str = string.Format("{0};{1};{2};{3}", user.name.title, user.name.first, GetPhone(), user.location.city);
                            sw.WriteLine(str);
                        }
                    }                    
                }
            }
        }

        static string GetPhone()
        {
            Random rnd = new Random();
            string phone = string.Format("+7 {0} {1}-{2}-{3}", rnd.Next(111, 999), rnd.Next(111, 999), rnd.Next(11, 99), rnd.Next(11, 99));
            return phone;
        }

        static void Exmpl06()
        {
            List<Person> persons = new List<Person>();
            using (StreamReader sr = new StreamReader("PhoneBook.csv", Encoding.Default))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tmp = line.Split(';');
                    Person person = new Person();
                    person.title = tmp[0];
                    person.name = tmp[1];
                    person.phone = tmp[2];
                    person.city = tmp[3];
                    persons.Add(person);
                }
            }

            SoapFormatter formatter = new SoapFormatter();
            using (FileStream fs = new FileStream("PhoneBook.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, persons.ToArray());
            }
        }
    }
}
