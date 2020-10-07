using System;
using System.IO;
using System.Threading.Tasks;
using linq_app.entities;

namespace linq_app
{
    public class CarCsvWriter : IAsyncDisposable
    {
        private readonly StreamWriter _stream;

        public CarCsvWriter(string fileName)
        {
            _stream = new StreamWriter(fileName, false);
            _stream.WriteLine("MANUFACTURER;MODEL;YEAR;USAGEINL");
        }

        public async Task Write(Car car)
        {
            await _stream.WriteLineAsync($"{car.Manufacturer};{car.Model};{car.Year};{car.Usage}");
        }
        public async ValueTask DisposeAsync()
        {
            await _stream.FlushAsync();
            await _stream.DisposeAsync();
        }
    }
}