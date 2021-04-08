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

        public void Join(int addLength, Point thisLocation, Point thisSize, Point anotherLocation, Point anotherSize, Electricity child, PictureBox pictureBox)
        {
            this.child = child;
            startPoint.X = thisLocation.X + thisSize.X;

            if (thisSize.Y % 2 == 0)
            {
                startPoint.Y = thisLocation.Y + (thisSize.Y / 2);
            }
            else
            {
                startPoint.Y = thisLocation.Y + ((thisSize.Y + 1) / 2);
            }
            Point point2 = new Point(startPoint.X + addLength, startPoint.Y);
            Point point3 = new Point(anotherLocation.X + addLength, anotherLocation.Y);

            using (var g = Graphics.FromImage(pictureBox.Image))
            {
                g.DrawLine(pen, startPoint, point2);
                g.DrawLine(pen, point2, point3);
                g.DrawLine(pen, point3, anotherLocation);
                //foreach (var point in points)
                //{
                //    g.DrawRectangle(pen, startPoint.X, startPoint.Y, 1, 1);
                //}
                pictureBox.Refresh();
            }
        }
        public void RecFunc(Point point,Electronics anotherElectronics, int counter)
        {
           
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
