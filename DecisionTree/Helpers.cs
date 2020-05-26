using System;
using System.Collections.Generic;
using System.IO;

namespace DecisionTree
{
    public static class Helpers
    {
        static Random r = new Random();
        public static Car[] ReadIntoCarArray()
        {
            List<Car> cars = new List<Car>();
            using (StreamReader sr = new StreamReader("carArray.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] splitedLine = line.Split(new char[] { ',' });
                    cars.Add(new Car() {
                        Id = Convert.ToInt32(splitedLine[0]),
                        Brand = (Brand)Convert.ToInt32(splitedLine[1]),
                        BodyType = (BodyType)Convert.ToInt32(splitedLine[2]),
                        Year = Convert.ToInt32(splitedLine[3]),
                        IsCool = Convert.ToBoolean(splitedLine[4])
                    });
                }
            }
            return cars.ToArray();
        }

        public static void WriteCarArrayToFile(Car[] carArray)
        {
            using (StreamWriter sw = new StreamWriter("carArray.txt", false))
            {
                foreach (Car car in carArray)
                {
                    sw.WriteLine(String.Format("{0},{1},{2},{3},{4}", car[0], (int)car[1], (int)car[2], car[3], car[4]));
                }
            }
        }

        public static Car[] FillCarArray(int length)
        {
            List<Car> cars = new List<Car>();
            int bodyType, brand, year;
            bool cool;
            for (int i = 1; i < length + 1; i++)
            {
                bodyType = r.Next(7);
                brand = r.Next(7);
                year = r.Next(18);
                cool = (bodyType + brand + year) % 2 == 0;
                cars.Add(new Car() { Id = i, Brand = (Brand)bodyType, BodyType = (BodyType)brand, Year = year + 2000, IsCool = cool });
            }
            return cars.ToArray();
        }
    }
}
