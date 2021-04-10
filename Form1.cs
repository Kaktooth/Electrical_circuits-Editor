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
        
        List<Electronics> electronics = new List<Electronics>();
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
                foreach (var el in electronics)
                {
                    if (el.electronicsName == b.Name) el.Location = b.Location;
                }

            }
            else if (b.Name.Contains("electricity"))
            {
                string name = b.Name;
                Control x = (TextBox)Controls.Find(name + "X", true)[0];
                Control y = (TextBox)Controls.Find(name + "Y", true)[0];

                x.Text = b.Location.X.ToString();
                y.Text = b.Location.Y.ToString();
                foreach (var el in electronics)
                {
                    if (el.electronicsName == b.Name) el.Location = b.Location;
                }
            }

           
        }

     
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
                Pen pen = new Pen(Color.Black, 3.5f);
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
                Point point2 = new Point();
                Point point3 = new Point();
                Point point4 = new Point();
                MessageBox.Show("thisControl "+thisControl.Name+ thisControl.Location.X + " b "+ b.Name+ b.Location.X);
                if (thisControl.Location.X < (b.Location.X-b.Size.Width))
                {
                   
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
                    point2 = new Point(startPoint.X + addLength, startPoint.Y);
                    point3 = new Point(endPoint.X, startPoint.Y);
                    point4 = new Point(endPoint.X, endPoint.Y);
                }
                else
                {
                    int minHeight = GetMinHeight();
                    int maxHeight = GetMaxHeight();
                    if (b.Location.Y == minHeight || b.Location.Y == maxHeight)
                    {
                        startPoint.X = thisControl.Location.X + thisControl.Size.Width;
                        endPoint.X = b.Location.X + b.Size.Width - addLength;
                        if (thisControl.Size.Height % 2 == 0)
                        {
                            startPoint.Y = thisControl.Location.Y + (thisControl.Size.Height / 2);

                        }
                        else
                        {
                            startPoint.Y = thisControl.Location.Y + ((thisControl.Size.Height + 1) / 2);
                        }
                        endPoint.Y = b.Location.Y + (b.Size.Height / 2);
                        point2 = new Point(startPoint.X + addLength, startPoint.Y);
                        point3 = new Point(startPoint.X + addLength, endPoint.Y);
                        point4 = new Point(endPoint.X, endPoint.Y);
                    }
                    else
                    {

                    }
                }
                    using (var g = Graphics.FromImage(pictureBox1.Image))
                    {
                        g.DrawLine(pen, startPoint, point2);
                        g.DrawLine(pen, point2, point3);
                        g.DrawLine(pen, point3, point4);
                        g.DrawLine(pen, point4, new Point(b.Location.X, endPoint.Y));
                        pictureBox1.Refresh();
                    }
                
                string childName = b.Name;
                foreach (var el in electronics)
                {
                    if (el.electronicsName == name|| el.electronicsName == name+"Resistor")
                    {
                        foreach (var el2 in electronics)
                        {
                            if (el2.electronicsName == childName)
                            {
                                el.childList.Add(el2);
                                MessageBox.Show("Parent: "+ el.electronicsName + " Child: "+ el2.electronicsName);
                            }
                        }
                            
                    } 
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }
        int GetMinHeight() 
        { 
            int[] heights = new int[electronics.Count];
            int i = 0;
            foreach (var el in electronics)
            {
                heights[i] = el.Location.Y;
                    i++;
            }
            return heights.Min();
        }
        int GetMaxHeight()
        {
            int[] heights = new int[electronics.Count];
            int i = 0;
            foreach (var el in electronics)
            {
                heights[i] = el.Location.Y;
                i++;
            }
            return heights.Max();
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
                Button elementButton = new Button();
                

                try
                {
                    if (constructorText.Text != "electricity")
                    {
                        elementButton.Name = constructorText.Text + "Resistor";
                        elementButton.Image = Electronic_Circuit_Editor.Properties.Resources.resistor;
                    }
                    else
                    {
                        elementButton.Name = constructorText.Text;
                        elementButton.Image = Electronic_Circuit_Editor.Properties.Resources.enter;
                    }

                    elementButton.Location = new Point((int)(ClientSize.Width / 2), (int)(ClientSize.Width / 2));

                    elementButton.Text = "";
                    elementButton.Width = 69;
                    elementButton.Height = 50;
                    elementButton.FlatAppearance.BorderSize = 0;
                    elementButton.MouseDown += DragMouseDown;
                    elementButton.MouseUp += DragMouseUp;
                    elementButton.MouseMove += DragMouseMove;
                    elementButton.Cursor = Cursors.Cross;
                    elementButton.FlatStyle = FlatStyle.Flat;
                    elementButton.BackColor = Color.White;
                    elementButton.ForeColor = Color.White;
                    elementButton.UseVisualStyleBackColor = true;
                    pictureBox1.Controls.Add(elementButton);
                    Resistor res = new Resistor(1f, elementButton.Name);
                    res.Location = elementButton.Location;
                    if (elementButton.Name != "electricity")
                    {
                        electronics.Add(res);
                    }
                    else
                    {
                        Electricity electricity = new Electricity("electricity");
                        electronics.Add(electricity);
                    }

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
                    X.Text = elementButton.Location.X.ToString();
                    Y.Text = elementButton.Location.Y.ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            foreach (var el in electronics)
            {
                el.childList.Clear();
            }
        }
    }
}
