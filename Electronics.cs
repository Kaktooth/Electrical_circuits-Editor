using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Electronic_Circuit_Editor
{
    class Electronics
    {
        public Electronics child { get; set; }
        public Color color { get; set; }
        private Pen pen = new Pen(Color.Black,1f);
        public Point startPoint = new Point();
        public Point endPoint = new Point();
        private Point[] points { get; set; }
        public double CircuitOm { get; set; }
        public String electronicsName { get; set; }
        public Electronics() { }
        public Electronics(string electronicsName) { this.electronicsName = electronicsName; }

        public virtual void Join(Electricity child)
        {
            this.child = child;
        }

        public virtual void Action()
        {

        }

    }
    class Electricity : Electronics
    {
        public Electricity() { }
        public Electricity(string electronicsName) { this.electronicsName = electronicsName; }
        public override string ToString()
        {
            return "Electricity";
        }
      
    }
    class Resistor : Electronics
    {
        public enum Conections { Series, Parallel } //series and parallel connection
        public Conections currentConection = Conections.Series;
        public double Om { get; set; }
        public Resistor() { }
        public Resistor(double Om,string electronicsName) { this.Om = Om;  this.electronicsName = electronicsName;

        }
        public Resistor(double Om) { this.Om = Om; }
       
        public override string ToString()
        {
            return Om.ToString();
        }
        public override void Action()
        {
            switch (currentConection)
            {
                case Conections.Series:
                    CircuitOm += Om;
                    break;
                case Conections.Parallel:
                    CircuitOm += 1/Om;
                    break;

            }
        }
    }

}
