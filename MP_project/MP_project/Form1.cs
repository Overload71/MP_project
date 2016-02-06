using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP_project
{
    public partial class Form1 : Form
    {
        Draw D;
        Graph g;
        int Img_num=0;
        public Form1()
        {
            InitializeComponent();
            panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Border);
            panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.Pane2_Border);
            D = new Draw(panel1,panel2);
            label1.Font = new Font("Arial", 12);
        }
        public void Panel_Border(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.DarkBlue, ButtonBorderStyle.Solid);  
        }
        public void Pane2_Border(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel2.ClientRectangle, Color.DarkBlue, ButtonBorderStyle.Solid);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            g = new Graph(Convert.ToInt32(textBox1.Text));
            for (int i = 0; i < g.Size; i++)
            {
                comboBox1.Items.Add(i);
                comboBox2.Items.Add(i);
            }
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            Point p = panel1.PointToClient(Cursor.Position);
            D.Set_Node(p.X - 15, p.Y - 15);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(comboBox1.SelectedItem) != Convert.ToInt32(comboBox2.SelectedItem))
            {
                g.AddEdge(Convert.ToInt32(comboBox1.SelectedItem), Convert.ToInt32(comboBox2.SelectedItem));
                D.Draw_Edge(Convert.ToInt32(comboBox1.SelectedItem), Convert.ToInt32(comboBox2.SelectedItem));
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            D.Draw_Graph(g,0,0,'c');
            bool cover = false, psuedo = false;
            int tmp = 0;
            Random rnd = new Random();
            while(!g.isComplete())
            {
                //Covering nodes
                for(int i=0;i<g.Size;i++)
                {
                    for(int j=0;j<g.Size;j++)
                    {
                        if(i!=j && g.Cover(i,j))
                        {
                            cover = true;
                            g.Remove(j);
                            D.Draw_Graph(g,i,j,'c');
                        }
                    }
                }
                //Psuedo covering nodes
                for (int i = 0; i < g.Size; i++)
                {
                    for (int j = 0; j < g.Size; j++)
                    {
                        if (i != j && g.Psuedo_Cover(i, j))
                        {
                            psuedo = true;
                            g.Remove(j);
                            D.Draw_Graph(g, i, j,'p');
                        }
                    }
                }

                if(cover==false && psuedo==false) //non-reducible
                {
                    int R = rnd.Next(0,g.Size-1);
                    g.Remove(R);
                    D.Draw_Graph(g,R,0,'r');
                }
            }
            D.Draw_string();
            panel2.BackgroundImage = D.img[Img_num];
            textBox2.Text = Convert.ToString(g.Size-g.removed);
        }

        private void button4_Click(object sender, EventArgs e) //next
        {
            Img_num++;
            if (Img_num == D.img.Count)
                Img_num = 0;
            panel2.BackgroundImage = D.img[Img_num];
        }

        private void button5_Click(object sender, EventArgs e) //prev
        {
            Img_num--;
            if (Img_num ==-1)
                Img_num = D.img.Count-1;
            panel2.BackgroundImage = D.img[Img_num];
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
