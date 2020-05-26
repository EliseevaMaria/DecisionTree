using System;

namespace DecisionTree
{
    public enum Brand
    {
        Audi, BMW, Ford, Honda, Nissan, Renault, Skoda
    }
    public enum BodyType
    {
        SUV, Truck, Sedan, Van, Coupe, Wagon, Hatchback
    }
    public class Car
    {
        public int Id { set; get; } // 0
        public Brand Brand { set; get; } // 1
        public BodyType BodyType { set; get; } // 2

        int _year;
        public static int MinYear = 2000;
        public static int MaxYear = 2017;
        public int Year // 3
        {
            set
            {
                _year = value;
                if (value < MinYear)
                    _year = MinYear;
                if (value > MaxYear)
                    _year = MaxYear;
            }
            get
            {
                return _year;
            }
        }

        public bool IsCool { set; get; } // 4

        public object this[int i]
        {
            set
            {
                switch (i)
                {
                    case 0:
                        Id = (int)value;
                        break;
                    case 1:
                        Brand = (Brand)value;
                        break;
                    case 2:
                        BodyType = (BodyType)value;
                        break;
                    case 3:
                        Year = (int)value;
                        break;
                    case 4:
                        IsCool = (bool)value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            get
            {
                switch (i)
                {
                    case 0:
                        return Id;
                    case 1:
                        return Brand;
                    case 2:
                        return BodyType;
                    case 3:
                        return Year;
                    case 4:
                        return IsCool;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }
}
