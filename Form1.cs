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
            constructorText.Text = "electricity";
            object sender = null;
            MouseEventArgs ms = new MouseEventArgs(MouseButtons,1,1,1,1);
            addResistor_Click(sender,ms);
            constructorText.Text = "";
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            
        }
        Electricity electricity = new Electricity();
        List<Resistor> resistors = new List<Resistor>();
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
            else if (b.Name.Contains("electricity"))
            {
                string name = b.Name;
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
            if (e.KeyCode == Keys.Enter)
                try
                {
                    Control b = new Control();
                    string name = ((TextBox)sender).Name;
                    name = name.Substring(0, name.Length - 1);

                    //MessageBox.Show(name);
                    if (name == "electricity")
                    {
                        b = pictureBox1.Controls[name];
                    }
                    else if (pictureBox1.Controls[name + "Resistor"] != null)
                    {
                        b = pictureBox1.Controls[name + "Resistor"];
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }

                    Control x = (TextBox)Controls.Find(name + "X", true)[0];
                    Control y = (TextBox)Controls.Find(name + "Y", true)[0];
                    b.Location = new Point(Convert.ToInt32(x.Text), Convert.ToInt32(y.Text));
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
                }
        }
        private void JoinElectronics(object sender, EventArgs e)
        {
            try
            {
                Control thisControl = new Control();
                Control b = new Control();
               
                string name = ((Button)sender).Name;
                name = name.Replace("Join","");
                MessageBox.Show(name);
                Control TextBox = (TextBox)Controls.Find(name + "joinTextBox", true)[0];
                int addLength = 5;
                Pen pen = Pens.Black;
                if (name == "electricity")
                {

                    thisControl = pictureBox1.Controls[name];
                }
                else if (pictureBox1.Controls[name + "Resistor"] != null)
                {
                    thisControl = pictureBox1.Controls[name + "Resistor"];
                }
                else
                {
                    MessageBox.Show("Error");
                }

                if (TextBox.Text == "electricity")
                {
                    
                    b = pictureBox1.Controls[TextBox.Text];
                }
                else if (pictureBox1.Controls[TextBox.Text + "Resistor"] != null)
                {
                    b = pictureBox1.Controls[TextBox.Text + "Resistor"];
                }
                else
                {
                    MessageBox.Show("Error");
                }
                Point startPoint = new Point();
                Point endPoint = new Point();
                // this.child = child;
                startPoint.X = thisControl.Location.X + thisControl.Size.Width;
                endPoint.X = b.Location.X - addLength;
                if (thisControl.Size.Height % 2 == 0)
                {
                    startPoint.Y = thisControl.Location.Y + (thisControl.Size.Height / 2);

                }
                else
                {
                    startPoint.Y = thisControl.Location.Y + ((thisControl.Size.Height + 1) / 2);
                }

                endPoint.Y = b.Location.Y + (b.Size.Height / 2);
                Point point2 = new Point(startPoint.X + addLength, startPoint.Y);
                Point point3 = new Point(endPoint.X, endPoint.Y);

                using (var g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.DrawLine(pen, startPoint, point2);
                    g.DrawLine(pen, point2, point3);
                    g.DrawLine(pen, point3, new Point(b.Location.X,endPoint.Y));
                    pictureBox1.Refresh();
                }
                string childName = b.Name.Replace("Resistor", "");
                foreach (var el in resistors)
                {
                    if(el.electronicsName == name)
                    {
                        foreach (var el2 in resistors)
                        {
                            if (el2.electronicsName == childName)
                            {
                                el.child = el2;
                                MessageBox.Show("Parent: "+ el.electronicsName + " Child: "+ el2.electronicsName);
                            }
                        }
                            
                    } 
                }
                MessageBox.Show(name + " " + childName);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }
        private void addResistor_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Controls[constructorText.Text+"Resistor"] == null && constructorText.Text != "" )
            {
                Panel panel = new Panel();
                Label label1 = new Label();
                Label label2 = new Label();
                Label label3 = new Label();
                Label labelName = new Label();
                TextBox joinTextBox = new TextBox();
                Button joinButton = new Button();
                TextBox X = new TextBox();
                TextBox Y = new TextBox();
                Button resistor = new Button();
                

                try
                {
                    if (constructorText.Text != "electricity")
                    {
                        resistor.Name = constructorText.Text + "Resistor";
                        resistor.Image = Electronic_Circuit_Editor.Properties.Resources.resistor;
                    }
                    else
                    {
                        resistor.Name = constructorText.Text;
                        resistor.Image = Electronic_Circuit_Editor.Properties.Resources.enter;
                    }
                    
                    resistor.Location = new Point((int)(ClientSize.Width / 2), (int)(ClientSize.Width / 2));
                    
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
                    Resistor res = new Resistor(1f,resistor.Text);
                    resistors.Add(res);


                    panel.Name = constructorText.Text + "Panel";
                    panel.AutoSize = true;
                    panel.Controls.Add(labelName);
                    panel.Controls.Add(label1);
                    panel.Controls.Add(label2);
                    panel.Controls.Add(label3);
                    panel.Controls.Add(X);
                    panel.Controls.Add(Y);
                    panel.Controls.Add(joinButton);
                    panel.Controls.Add(joinTextBox);

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

                    label2.Location = new Point(label1.Location.X, label1.Location.Y + 60);
                    label2.Name = constructorText.Text + "Label2";
                    label2.Text = "Reverse Electronics: ";
                    label2.AutoSize = true;

                    label3.Location = new Point(label2.Location.X, label2.Location.Y + 60);
                    label3.Name = constructorText.Text + "Label3";
                    label3.Text = "Join Electronics: ";
                    label3.AutoSize = true;

                    joinTextBox.Location = new Point(label2.Location.X, label2.Location.Y + 75);
                    joinTextBox.Name = constructorText.Text + "Label3";
                    joinTextBox.AutoSize = true;
                    joinTextBox.Name = constructorText.Text + "joinTextBox";
                    joinButton.Location = new Point(label2.Location.X, label2.Location.Y + 95);
                    joinButton.Name = constructorText.Text + "Join";
                    joinButton.Text = "Join";
                    joinButton.AutoSize = true;
                    joinButton.Click += JoinElectronics;
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
