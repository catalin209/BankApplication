using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string s= Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string file = s + "\\name_list.txt" ;
            string[] namesToCompare = File.ReadAllLines(file);
            GUI g = new GUI();
            g.ShowDialog();
        }
    }
}
