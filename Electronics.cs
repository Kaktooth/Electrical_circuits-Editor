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
        public List<Electronics> childList = new List<Electronics>();
        public Color Color { get; set; }
        readonly private Pen pen = new Pen(Color.Black,1f);
        public Point startPoint = new Point();
        public Point endPoint = new Point();
        public bool isParallel { get; set; }
        private Point[] points { get; set; }
        public static double CircuitResistance { get; set; }
        public String electronicsName { get; set; }
        public Electronics() { }
        public Electronics(string electronicsName) { this.electronicsName = electronicsName; }
        public void ResetResistance()
        {
            CircuitResistance = 0;
        }
        public double GetCircuitResistance()
        {
            return CircuitResistance;
        }
        public void SetCircuitResistance(double res)
        {
            CircuitResistance = res;
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

        public double Resistance { get; set; }
        public Resistor() { }
        public Resistor(double Resistance, string electronicsName) { this.Resistance = Resistance; this.electronicsName = electronicsName; }
        public Resistor(double Resistance) { this.Resistance = Resistance; }
        public double GetResistance()
        {
            return Resistance;
        }
        public override string ToString()
        {
            return Resistance.ToString();
        }
        public override void Action()
        {
            switch (currentConection)
            {
                case Conections.Series:
                    CircuitResistance += Resistance;
                    break;
                case Conections.Parallel:
                    CircuitResistance += 1 / Resistance;
                    break;
            }
        }

    }

}
