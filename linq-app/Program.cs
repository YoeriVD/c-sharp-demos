using System.Linq;
using System.Threading.Tasks;

namespace linq_app
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // await GenerateCarCsv();
            // IntroToLinqAndDeferredExecution.Run();
            // SyntaxExamples.Run();
            AdvancedLinq.Run();
        }


        private static async Task GenerateCarCsv()
        {
            await using var carWriter = new CarCsvWriter("cars.csv");
            foreach (var car in 
                Generator
                .InfiniteListOfCars()
                .Take(100000)
                )
            {
                await carWriter.Write(car);
            }
        }
    }
}