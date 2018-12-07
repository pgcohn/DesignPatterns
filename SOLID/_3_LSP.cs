using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    using static System.Console;

    // In this demo the Liskov Substitution Principle is demonstrated. This principle
    // says that objects can be represented by their base classes without having their
    // behavior changed.

    // using a classic example
    public class Rectangle
    {
        //public int Width { get; set; }
        //public int Height { get; set; }

        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        //public new int Width
        //{
        //  set { base.Width = base.Height = value; }
        //}

        //public new int Height
        //{ 
        //  set { base.Width = base.Height = value; }
        //}

        public override int Width // nasty side effects
        {
            set { base.Width = base.Height = value; }
        }

        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }

    public class _3_LSP
    {
        static public int Area(Rectangle r) => r.Width * r.Height;

        public static void Run()
        {
            Rectangle rc = new Rectangle(2, 3);
            WriteLine($"{rc} has area {Area(rc)}");

            // should be able to substitute a base type for a subtype
            /*Square*/
            Rectangle sq = new Square();
            sq.Width = 4;
            WriteLine($"{sq} has area {Area(sq)}");
        }
    }
}
