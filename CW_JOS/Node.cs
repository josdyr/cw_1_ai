using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_JOS
{
    public class Node
    {
        public int x, y;
        public double cost;
        public Node parent;
        public int idx;
        public List<Node> neighbours = new List<Node>();

        public Node(int x, int y, int idx) {
            this.x = x;
            this.y = y;
            this.idx = idx;
        }

        public string printNode() {
            string nbrsStr = "";
            foreach (Node nbr in this.neighbours) {
                nbrsStr += (nbr.idx.ToString() + ", ");
            }
            return "Node[" + this.idx + "]: " + "x: " + this.x + ", y: " + this.y + ", neighbours: " + nbrsStr;
        }

        //Method to get length to specified node//
        public double getLengthTo(Node nbr) {
            int d_x = (nbr.x - this.x);
            int d_y = (nbr.y - this.y);
            double l = getLength(d_x, d_y);
            return l;
        }

        public double getLength(int delta_x, int delta_y) {
            return Math.Sqrt((1.0) * ((delta_x * delta_x) + (delta_y * delta_y)));
        }
        //-------------------------------------//



    }
}
