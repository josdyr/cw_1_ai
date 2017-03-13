using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CW_JOS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public Node getMinCostNode(List<Node> queue) {
            Node minNode = queue[0];
            for (int d = 0; d < queue.Count; d++) {
                if (queue[d].cost < minNode.cost) {
                    minNode = queue[d];
                }
            }
            return minNode;
        }

        public void button_Click_1(object sender, RoutedEventArgs e)
        {
            List<Node> nodeMap = new List<Node>(); //hold all nodes in this list

            string data = System.IO.File.ReadAllText(@"c:\users\josdyr\documents\visual studio 2015\Projects\CW_JOS\CW_JOS\input.cav");
            string[] stringData = data.Split(',');
            int[] intData = Array.ConvertAll(stringData, s => int.Parse(s));
            Console.WriteLine(data);

            //extract the number of nodes
            int numberOfNodes = intData[0];

            //extract the coordinates
            int[,] coordinates = new int[numberOfNodes,2];
            int row = 0;
            for (var i = 1; i < ((numberOfNodes * 2) + 1); i = i + 2)
            {
                Console.WriteLine("[" + intData[i] + "," + intData[i + 1] + "]");
                coordinates[row,0] = intData[i];
                coordinates[row,1] = intData[i + 1];
                row++;
            }

            //extract the matrix
            Boolean[,] isConnected = new Boolean[numberOfNodes, numberOfNodes];

            int col2 = 0;
            int row2 = 0;
            for (int i = ((numberOfNodes * 2) + 1); i < intData.Length; i++)
            {
                if (intData[i] == 1)
                {
                    isConnected[row2,col2] = true;
                }
                else
                {
                    isConnected[row2,col2] = false;
                }
                col2++;
                if (col2 == numberOfNodes)
                {
                    col2 = 0;
                    row2++;
                }
            }

            //declare and store all ndoes inside of the nodeMap list
            int idx = 1;
            for (int node = 0; node < numberOfNodes; node++)
            {
                int x = coordinates[node,0];
                int y = coordinates[node,1];
                Node parent = null;
                Node myNode = new Node(x, y, idx);
                nodeMap.Add(myNode);
                idx++;
            }

            int myRow = 0;
            foreach (Node currentNode in nodeMap)
            {
                for (int potentialNbr = 0; potentialNbr < numberOfNodes; potentialNbr++)
                {
                    if (isConnected[potentialNbr,myRow])
                    {
                        currentNode.neighbours.Add(nodeMap[potentialNbr]);
                        //Console.WriteLine("found neighbour: " + potentialNbr);

                    }
                    
                }
                Console.WriteLine();
                Console.WriteLine("Evaluate " + currentNode.printNode());

                myRow++;
            }

            //Dijkstra's Algorithm:
            List<Node> queue = new List<Node>();

            foreach (Node n in nodeMap) {
                n.cost = Double.PositiveInfinity;
                n.parent = null;
                queue.Add(n);
            }


            queue[0].cost = 0;
            queue[0].parent = nodeMap[0];

            Console.WriteLine();

            while (queue.Any<Node>()) {
                Node minCostNode = getMinCostNode(queue);
                queue.Remove(minCostNode);
                Console.WriteLine("minCostNode: " + minCostNode.printNode());

                foreach (Node nb in minCostNode.neighbours)
                {
                    double alt = minCostNode.cost + minCostNode.getLengthTo(nb);
                    if (alt < nb.cost)
                    {
                        nb.cost = alt;
                        nb.parent = minCostNode;
                    }
                }
                Console.WriteLine();
            }

            //reverse iteration: add all nodes to the list
            List<Node> myPath = new List<Node>(); //declare an empty path
            Node current = nodeMap[numberOfNodes - 1]; //declare a current node
            Node target = nodeMap[0]; //set tharget to the very first node
            myPath.Add(current); //add the current node to the path
            do {
                //add the parent-node to the myPath list
                myPath.Add(current.parent);
                current = current.parent;
            } while (!current.Equals(target));

            //print myPath
            Console.WriteLine("size: " + myPath.Count);
            for (int i = 0; i < myPath.Count; i++) {
                Console.WriteLine(myPath[i].printNode());
            }
        }
    }
}