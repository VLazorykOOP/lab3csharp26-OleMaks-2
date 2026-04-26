using System;
using System.Linq;

namespace Lab3
{
//1
    class Triangle
    {
        protected int a, b, c;
        protected int color;

        public Triangle(int a, int b, int c, int color)
        {
            this.color = color;
            
            if (IsValid(a, b, c))
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }
            else
            {
                Console.WriteLine($"Помилка: Трикутник зі сторонами {a}, {b}, {c} не існує. Встановлено 1, 1, 1.");
                this.a = 1;
                this.b = 1;
                this.c = 1;
            }
        }

        private bool IsValid(int sideA, int sideB, int sideC)
        {
            return (sideA + sideB > sideC) && (sideA + sideC > sideB) && (sideB + sideC > sideA);
        }

        public int A
        {
            get { return a; }
            set { if (IsValid(value, b, c)) a = value; else Console.WriteLine("Помилка: Некоректна сторона A."); }
        }

        public int B
        {
            get { return b; }
            set { if (IsValid(a, value, c)) b = value; else Console.WriteLine("Помилка: Некоректна сторона B."); }
        }

        public int C
        {
            get { return c; }
            set { if (IsValid(a, b, value)) c = value; else Console.WriteLine("Помилка: Некоректна сторона C."); }
        }

        public int Color
        {
            get { return color; }
        }
        public void PrintSides()
        {
            Console.WriteLine($"Трикутник (Колір: {color}): a = {a}, b = {b}, c = {c}");
        }

        public int Perimeter()
        {
            return a + b + c;
        }

        public double Area()
        {
            double p = Perimeter() / 2.0;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
    }




     class Engine
    {
        public double Power { get; set; } 

        public Engine(double power)
        {
            Power = power;
        }

        public virtual void Show()
        {
            Console.WriteLine($"[Engine] Базовий двигун. Потужність: {Power} к.с.");
        }
    }

    // Похідний клас 1
    class InternalCombustionEngine : Engine 
    {
        public int Cylinders { get; set; }

        public InternalCombustionEngine(double power, int cylinders) : base(power)
        {
            Cylinders = cylinders;
        }

        public override void Show()
        {
            Console.WriteLine($"[InternalCombustionEngine] ДВЗ. Потужність: {Power} к.с., Кількість циліндрів: {Cylinders}");
        }
    }


    class DieselEngine : InternalCombustionEngine 
    {
        public bool HasTurbo { get; set; }

        public DieselEngine(double power, int cylinders, bool hasTurbo) : base(power, cylinders)
        {
            HasTurbo = hasTurbo;
        }

        public override void Show()
        {
            string turboStatus = HasTurbo ? "Так" : "Ні";
            Console.WriteLine($"[DieselEngine] Дизель. Потужність: {Power} к.с., Циліндри: {Cylinders}, Наявність турбіни: {turboStatus}");
        }
    }

    
    class JetEngine : Engine 
    {
        public double Thrust { get; set; } // Тяга

        public JetEngine(double power, double thrust) : base(power)
        {
            Thrust = thrust;
        }

        public override void Show()
        {
            Console.WriteLine($"[JetEngine] Реактивний двигун. Потужність: {Power} к.с., Тяга: {Thrust} кН");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ЗАВДАННЯ 1 (Трикутники)");
            
            Triangle[] triangles = new Triangle[]
            {
                new Triangle(3, 4, 5, 2),  // Колір 2
                new Triangle(5, 5, 5, 1),  // Колір 1
                new Triangle(6, 8, 10, 3), // Колір 3
                new Triangle(7, 24, 25, 1) // Колір 1
            };


            Console.WriteLine("\n Впорядковані за кольором");
            var byColor = triangles.OrderBy(t => t.Color);
            foreach (var t in byColor)
            {
                t.PrintSides();
                Console.WriteLine($"Колір: {t.Color}");
            }

            Console.WriteLine("\nВпорядковані за площею");
            var byArea = triangles.OrderBy(t => t.Area());
            foreach (var t in byArea)
            {
                t.PrintSides();
                Console.WriteLine($"Площа: {t.Area():F2}");
            }

            Console.WriteLine("\nВпорядковані за периметром");
            var byPerimeter = triangles.OrderBy(t => t.Perimeter());
            foreach (var t in byPerimeter)
            {
                t.PrintSides();
                Console.WriteLine($"Периметр: {t.Perimeter()}");
            }


            Console.WriteLine("\n\nЗАВДАННЯ 2 (Двигуни)");

            Engine[] engines = new Engine[]
            {
                new DieselEngine(150, 4, true),
                new Engine(50),
                new JetEngine(50000, 120),
                new InternalCombustionEngine(100, 4),
                new DieselEngine(300, 6, false)
            };

            Console.WriteLine("\nМасив двигунів, впорядкований за типом класу");
            var sortedEngines = engines.OrderBy(e => e.GetType().Name);
            
            foreach (var engine in sortedEngines)
            {
                engine.Show();
            }
        }
    }
}