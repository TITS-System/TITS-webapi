using System;
using System.Threading.Tasks;
using Infrastructure;

namespace Seeder
{
    public static class Program
    {
        private static TitsDbContext GetContext()
        {
            return new();
        }

        static async Task Main(string[] args)
        {
            await new SeedData().Seed();
            Console.WriteLine("Seeded successfully");
        }
    }
}