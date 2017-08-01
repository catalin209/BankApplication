using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp
{
    interface InterfaceB
    {
        int MaleCount();
        int FemaleCount();
        int MaleCount(int age);
        int FemaleCount(int age);
        int MaleAverage();
        int FemaleAverage();
        int MaleAverage(string pattern);
        int FemaleAverage(string pattern);

    }
}
