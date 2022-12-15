using System;
using System.Collections.Generic;

namespace lab11
{
    class Program
    {
        static void Main()
        {
            // Декоратор
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");

            Potion potion1 = new HealingPotion();
            potion1 = new SpeedPotion(potion1); // Зелье лечения с баффом скорости
            Console.WriteLine("Название: {0}", potion1.Name);
            Console.WriteLine("Цена: {0}", potion1.GetCost());

            Potion potion2 = new HealingPotion();
            potion2 = new InvisibilityPotion(potion2);// Зелье лечения с баффом невидимости
            Console.WriteLine("Название: {0}", potion2.Name);
            Console.WriteLine("Цена: {0}", potion2.GetCost());

            Potion potion3 = new ManaPotion();
            potion3 = new SpeedPotion(potion3);
            potion3 = new InvisibilityPotion(potion3);// Зелье маны с баффом скорости и невидимости
            Console.WriteLine("Название: {0}", potion3.Name);
            Console.WriteLine("Цена: {0}", potion3.GetCost());
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            //Компановщик
            Component unit = new UnitBranch("Юниты");
            Component meleeUnit = new UnitBranch("Юнит ближнего боя");
            Component swordman = new UnitType("Мечник");
            Component assasin = new UnitType("Ассасин");
            meleeUnit.Add(swordman);
            meleeUnit.Add(assasin);
            unit.Add(meleeUnit);
            Component rangeUnit = new UnitBranch("Юнит дальнего боя");
            Component archer = new UnitType("Лучник");
            Component mage = new UnitType("Маг");
            rangeUnit.Add(archer);
            rangeUnit.Add(mage);
            unit.Add(rangeUnit);
            unit.Print();
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            //Фасад
            Sketching sketch = new Sketching();
            Coloring animation = new Coloring();
            CLR clr = new CLR();
            Drawing draw = new Drawing(sketch, animation, clr);
            Artist artist = new Artist();
            artist.drawPic(draw);
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.ReadLine();
        }
    }

    // Декоратор----------------------------------------------------------------------------------------------------------

    abstract class Potion
    {
        public Potion(string n)
        {
            this.Name = n;
        }
        public string Name { get; protected set; }
        public abstract int GetCost();
    }

    class HealingPotion : Potion
    {
        public HealingPotion() : base("Зелье лечения")
        { }
        public override int GetCost()
        {
            return 20;
        }
    }
    class ManaPotion : Potion
    {
        public ManaPotion()
            : base("Зелье маны")
        { }
        public override int GetCost()
        {
            return 15;
        }
    }

    abstract class PotionDecorator : Potion
    {
        protected Potion potion;
        public PotionDecorator(string n, Potion potion) : base(n)
        {
            this.potion = potion;
        }
    }

    class SpeedPotion : PotionDecorator
    {
        public SpeedPotion(Potion p)
            : base(p.Name + ", с баффом скорости", p)
        { }

        public override int GetCost()
        {
            return potion.GetCost() + 6;
        }
    }

    class InvisibilityPotion : PotionDecorator
    {
        public InvisibilityPotion(Potion p)
            : base(p.Name + ", с баффом невидимости", p)
        { }

        public override int GetCost()
        {
            return potion.GetCost() + 9;
        }
    }
    //Компановщик----------------------------------------------------------------------------------------------------------

    abstract class Component
    {
        protected string name;
        public Component(string name)
        {
            this.name = name;
        }
        public virtual void Add(Component component) { }
        public virtual void Remove(Component component) { }
        public virtual void Print()
        {
            Console.WriteLine(name);
        }
    }
    class UnitType : Component  //тип юнита
    {
        public UnitType(string name) : base(name) { }
    }
    class UnitBranch : Component    //род войск
    {
        private List<Component> components = new List<Component>();
        public UnitBranch(string name) : base(name) { }
        public override void Add(Component component) { components.Add(component); }
        public override void Remove(Component component) { components.Remove(component); }
        public override void Print()
        {
            Console.WriteLine("Раздел: " + name);
            Console.WriteLine("Подразделы: ");
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Print();
            }
        }
    }

    //Фасад----------------------------------------------------------------------------------------------------------
    public class Sketching
    {
        public void SketchCreate()
        {
            Console.WriteLine("Создание наброска");
        }

        public void Сontouring()
        {
            Console.WriteLine("Контуринг");
        }
    }

    public class Coloring
    {
        public void ColorFilling()
        {
            Console.WriteLine("Добавление цвета, работа с цветом");
        }

        public void Shadows()
        {
            Console.WriteLine("Добавление теней, работа с тенями");
        }
    }

    public class CLR
    {
        public void Save()
        {
            Console.WriteLine("Сохранение файла");
        }
        public void Finish()
        {
            Console.WriteLine("Завершение работы приложения");
        }
    }

    public class Drawing
    {
        Sketching sketching;
        Coloring coloring;
        CLR clr;
        public Drawing(Sketching sch, Coloring cl, CLR c)
        {
            sketching = sch;
            coloring = cl;
            clr = c;
        }

        public void Start()
        {
            sketching.SketchCreate();
            sketching.Сontouring();
            coloring.ColorFilling();
            coloring.Shadows();

        }
        public void Stop()
        {
            clr.Save();
            clr.Finish();
        }
    }
    public class Artist
    {
        public void drawPic(Drawing blender)
        {
            blender.Start();
            blender.Stop();
        }
    }
}

