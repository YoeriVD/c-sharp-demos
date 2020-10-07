using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using linq_app.entities;

namespace linq_app
{
    public class CarCsvReader : IDisposable

    {
        private readonly StreamReader _stream;

        public CarCsvReader(string fileName = "cars.csv")
        {
            _stream = new StreamReader(fileName);
        }

        public IEnumerable<Car> GetCars()
        {
            return GetLines()
                .Skip(1)
                .Select(ToCar);
        }

        private Car ToCar(string carString)
        {
            var carProps = carString.Split(";");
            return new Car()
            {
                Manufacturer = carProps[0],
                Model = carProps[1],
                Year = int.Parse(carProps[2]),
                Usage = int.Parse(carProps[3])
            };
        }

        private int _count = 0;
        private IEnumerable<string> GetLines()
        {
            const string skippedMessage = "(skipped, headerrow)";
            while (!_stream.EndOfStream)
            {
                Console.WriteLine($"Reading car number {++_count} {(_count == 1 ? skippedMessage : string.Empty)}");
                yield return _stream.ReadLine();
            }

            _stream.BaseStream.Position = 0;
        }


        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}