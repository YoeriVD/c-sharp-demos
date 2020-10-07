using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;
using linq_app;

namespace linq_to_x.xml
{
    internal class XmlProgram
    {
        public static void RunProgram()
        {
            // CreateXmlFile();
            CreateXmlFileWithLinq();
            QueryXml();
        }

        private static void CreateXmlFile()
        {
            var cars = Generator.ListOfCars(100);
            var document = new XDocument();
            var carsElement = new XElement("Cars");
            foreach (var record in cars)
            {
                var manufacturer = new XAttribute("Manufacturer", record.Manufacturer);
                var model = new XAttribute("Model", record.Model);
                var usage = new XAttribute("Usage", record.Usage);

                var car = new XElement("Car");
                car.Add(manufacturer);
                car.Add(model);
                car.Add(usage);

                carsElement.Add(car);
            }

            document.Add(carsElement);
            document.Save("cars.xml", SaveOptions.None);
        }

        private static void CreateXmlFileWithLinq()
        {
            var cars = Generator.ListOfCars(1000);
            var document = new XDocument();
            var carsElement = new XElement("Cars",
                cars.Select(car => new XElement("Car",
                    new XAttribute("Manufacturer", car.Manufacturer),
                    new XAttribute("Model", car.Model),
                    new XAttribute("Usage", car.Usage)
                ))
            );
            document.Add(carsElement);
            document.Save("cars.xml", SaveOptions.None);
        }

        private static void CreateXmlFileWithLinqCs()
        {
            var cars = Generator.ListOfCars(100);
            var document = new XDocument();
            var carsElement = new XElement("Cars",
                from car in cars
                select new XElement("Car",
                    new XAttribute("Manufacturer", car.Manufacturer),
                    new XAttribute("Model", car.Model),
                    new XAttribute("Usage", car.Usage)
                )
            );
            document.Add(carsElement);
            document.Save("cars.xml", SaveOptions.None);
        }
        
        private static void QueryXml()
        {
            var document = XDocument.Load("cars.xml");
            var result =
                from element in document.Element("Cars")?.Elements("Car") ?? Enumerable.Empty<XElement>()
                where element.Attribute("Manufacturer")?.Value == "Volvo"
                select element.Attribute("Model")?.Value;
            foreach (var model in result)
            {
                Console.WriteLine(model);
            }

        }
        
    }
}