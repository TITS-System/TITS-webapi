using System;
using Infrastructure;

namespace Seeder
{
    public static class Program
    {
        private static TitsDbContext GetContext()
        {
            return new();
        }

        static void Main(string[] args)
        {
            new SeedData(GetContext()).Seed();
            Console.WriteLine("Seeded successfully");
        }
    }
}