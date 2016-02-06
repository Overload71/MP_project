using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP_project
{
    class Draw
    {
        private Graphics formGraphics,formGraphics2;
        List<string> str = new List<string>();
        public int Num_points=0;
        public List<Image>img = new List<Image>();
        int width, height;
        public Point[] points = new Point[100];
        public Draw(Panel panel1,Panel panel2)
        {
            width = panel2.Width;
            height = panel2.Height;
            formGraphics = panel1.CreateGraphics();
            formGraphics2 = panel2.CreateGraphics();
        }
        public void Set_Node(int x, int y)
        {
            points[Num_points].X = x;
            points[Num_points].Y = y;
            formGraphics.FillEllipse(new SolidBrush(Color.Black), new Rectangle(points[Num_points], new Size(50, 50)));
            formGraphics.DrawString((Num_points).ToString(), new Font("Arial", 24), Brushes.White, new Point(points[Num_points].X + 10, points[Num_points].Y + 5));
            Num_points++;
        }
        public void Draw_Edge(int A, int B)
        {
            Point pA = new Point();
            Point pB = new Point();
            pA.X = points[A].X + 25;
            pA.Y = points[A].Y + 25;
            pB.X = points[B].X + 25;
            pB.Y = points[B].Y + 25;
            Pen pen = new Pen(Color.Orange, 5);
            formGraphics.DrawLine(pen, pA, pB);
            formGraphics.FillEllipse(new SolidBrush(Color.Black), new Rectangle(points[A], new Size(50, 50)));
            formGraphics.DrawString(A.ToString(), new Font("Arial", 24), Brushes.White, new Point(points[A].X + 10, points[A].Y + 5));
            formGraphics.FillEllipse(new SolidBrush(Color.Black), new Rectangle(points[B], new Size(50, 50)));
            formGraphics.DrawString(B.ToString(), new Font("Arial", 24), Brushes.White, new Point(points[B].X + 10, points[B].Y + 5));
        }
        public void Draw_string()
        {
            Graphics g;
            for(int i=0;i<img.Count-1;i++)
            {
                g= Graphics.FromImage(img[i]);
                g.DrawString(str[i], new Font("Arial", 16), Brushes.Black, new Point(100, 20));
            }
            g = Graphics.FromImage(img[img.Count-1]);
            g.DrawString("Finished", new Font("Arial", 16), Brushes.Black, new Point(220, 20));
        }
        public void Draw_Graph(Graph G,int A,int B,char c)
        {
            img.Add(new Bitmap(width,height));
            Graphics g = Graphics.FromImage(img[img.Count-1]);
            if(img.Count>1)
            {
                String s = "Removing ";
                s += B.ToString();
                if (c == 'c')
                    s += " covered by ";
                else if (c == 'p')
                    s += " psuedo covered by ";   
                s += A.ToString();
                if (c == 'r')
                {
                    s = "";
                    s = "Removing random node ";
                    s += A;
                }
                str.Add(s);
               //g.DrawString(s, new Font("Arial", 16), Brushes.Black, new Point(45, 20));
            }
            for(int i=0;i<G.Size;i++)
                if(G.adj[i].Count!=0)  //have no edges
                {
                    g.FillEllipse(new SolidBrush(Color.Black), new Rectangle(points[i], new Size(50, 50)));
                    g.DrawString(i.ToString(), new Font("Arial", 24), Brushes.White, new Point(points[i].X + 10, points[i].Y + 5));
                }
            for (int i = 0; i < G.Size; i++)
            {
                for (int j = 0; j < G.adj[i].Count; j++)
                {
                    Point pA = new Point();
                    Point pB = new Point();
                    pA.X = points[i].X + 25;
                    pA.Y = points[i].Y + 25;
                    pB.X = points[G.adj[i][j]].X + 25;
                    pB.Y = points[G.adj[i][j]].Y + 25;
                    Pen pen = new Pen(Color.Orange, 5);
                    g.DrawLine(pen, pA, pB);
                    g.FillEllipse(new SolidBrush(Color.Black), new Rectangle(points[i], new Size(50, 50)));
                    g.DrawString(i.ToString(), new Font("Arial", 24), Brushes.White, new Point(points[i].X + 10, points[i].Y + 5));
                    g.FillEllipse(new SolidBrush(Color.Black), new Rectangle(points[G.adj[i][j]], new Size(50, 50)));
                    g.DrawString(G.adj[i][j].ToString(), new Font("Arial", 24), Brushes.White, new Point(points[G.adj[i][j]].X + 10, points[G.adj[i][j]].Y + 5));
                }
            }

        }
    
    }
}
