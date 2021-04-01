using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Electronic_Circuit_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
            
        }

        bool isDragged = false;
        Point ptOffset;
        
        private void DragMouseDown(object sender, MouseEventArgs e)
        {
            Button b = ((Button)sender);
            if (e.Button == MouseButtons.Left)
            {
                isDragged = true;
                Point ptStartPosition = b.PointToScreen(new Point(e.X, e.Y));
                
                ptOffset = new Point();
                ptOffset.X = b.Location.X - ptStartPosition.X;
                ptOffset.Y = b.Location.Y - ptStartPosition.Y;
            }
            else
            {
                isDragged = false;
            }
           
        }
        
        private void DragMouseMove(object sender, MouseEventArgs e)
        {
            Button b = ((Button)sender);
            if (isDragged)
            {
                Point newPoint = b.PointToScreen(new Point(e.X, e.Y));
                newPoint.Offset(ptOffset);
                b.Location = newPoint;
            }
           
        }

        private void DragMouseUp(object sender, MouseEventArgs e)
        {
            Button b = ((Button)sender);
            isDragged = false;
            if (b.Name.Contains("Resistor"))
            {
                string name = b.Name.Replace("Resistor","");
                Control x = (TextBox)Controls.Find(name + "X", true)[0];
                Control y = (TextBox)Controls.Find(name + "Y", true)[0];

                x.Text = b.Location.X.ToString();
                y.Text = b.Location.Y.ToString();
            }
           
        }

        //private void button2_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        isDragged = true;
        //        Point ptStartPosition = electricity.PointToScreen(new Point(e.X, e.Y));

        //        ptOffset = new Point();
        //        ptOffset.X = electricity.Location.X - ptStartPosition.X;
        //        ptOffset.Y = electricity.Location.Y - ptStartPosition.Y;
        //    }
        //    else
        //    {
        //        isDragged = false;
        //    }
        //}
        //private void button2_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (isDragged)
        //    {
        //        Point newPoint = electricity.PointToScreen(new Point(e.X, e.Y));
        //        newPoint.Offset(ptOffset);
        //        electricity.Location = newPoint;
        //    }

        //}
        private void ResistorLocationCheck(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            try {
                
                string name = ((TextBox)sender).Name;
                name = name.Substring(0, name.Length - 1);
             
                //MessageBox.Show(name);
                Control b = pictureBox1.Controls[name+"Resistor"];
                
                Control x = (TextBox)Controls.Find(name + "X", true)[0];
                Control y = (TextBox)Controls.Find(name + "Y", true)[0];
                


                b.Location = new Point(Convert.ToInt32(x.Text), Convert.ToInt32(y.Text));
                
              //  MessageBox.Show($"{b.Location}, {x.Text}, {b.Name}");
            }
            catch(Exception E)
            {
                MessageBox.Show(E.ToString());
            }

        }
        private void addResistor_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Controls[constructorText.Text+"Resistor"] == null && constructorText.Text != "")
            {
                Panel panel = new Panel();
                Label label1 = new Label();
                Label label2 = new Label();
                Label label3 = new Label();
                Label labelName = new Label();
                TextBox X = new TextBox();
                TextBox Y = new TextBox();
                Button resistor = new Button();


                try
                {

                    resistor.Name = constructorText.Text + "Resistor";
                    resistor.Location = new Point(ClientSize.Width / 2, ClientSize.Width / 2);
                    resistor.Image = Electronic_Circuit_Editor.Properties.Resources.resistor;
                    resistor.Text = "";
                    resistor.AutoSize = true;
                    resistor.MouseDown += DragMouseDown;
                    resistor.MouseUp += DragMouseUp;
                    resistor.MouseMove += DragMouseMove;
                    resistor.Cursor = Cursors.Cross;
                    resistor.FlatStyle = FlatStyle.Flat;
                    resistor.BackColor = Color.White;
                    resistor.ForeColor = Color.White;
                    resistor.UseVisualStyleBackColor = true;
                    pictureBox1.Controls.Add(resistor);

                    panel.Name = constructorText.Text + "Panel";
                    panel.AutoSize = true;
                    panel.Controls.Add(labelName);
                    panel.Controls.Add(label1);
                    panel.Controls.Add(label2);
                    panel.Controls.Add(label3);
                    panel.Controls.Add(X);
                    panel.Controls.Add(Y);

                    labelName.Name = constructorText.Text + "NameLabel";
                    labelName.Text = "Name: " + constructorText.Text;
                    labelName.AutoSize = true;
                    

                    label1.Name = constructorText.Text + "Label1";
                    label1.Text = "Location: ";
                    label1.AutoSize = true;


                    X.Name = constructorText.Text + "X";
                    Y.Name = constructorText.Text + "Y";
                    label1.Location = new Point(labelName.Location.X , labelName.Location.Y + 20);
                    X.Location = new Point(label1.Location.X + label1.Text.Length + 10, label1.Location.Y + 20);
                    Y.Location = new Point(label1.Location.X + label1.Text.Length + 10, label1.Location.Y + 40);
                    X.Text = resistor.Location.X.ToString();
                    Y.Text = resistor.Location.Y.ToString();
                    X.KeyDown += ResistorLocationCheck;
                    Y.KeyDown += ResistorLocationCheck;
                    //MessageBox.Show($"{X.Name}, {Y.Name}");
                    // + label1.Text.Length + 10

                    label2.Location = new Point(label1.Location.X, label1.Location.Y + 60);
                    label2.Name = constructorText.Text + "Label2";
                    label2.Text = "Reverse Resistor: ";
                    label2.AutoSize = true;

                    label3.Location = new Point(label2.Location.X, label2.Location.Y + 60);
                    label3.Name = constructorText.Text + "Label3";
                    label3.Text = "Join Line: ";
                    label3.AutoSize = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


                flowLayoutPanel1.Controls.Add(panel);
            }
        }
    }
}
