using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electronic_Circuit_Editor
{
    class Electronics
    {
        public double CircuitOm { get; set; }
        public String electronicsName { get; set; }
        public Electronics() { }
        public Electronics(string electronicsName) { this.electronicsName = electronicsName; }
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
        public Resistor(string electronicsName) { this.electronicsName = electronicsName; }
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
