using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meshParser
{
    public struct Point3D
    {
        public double x; public double y; public double z;
        public override string ToString()
        {
            return $"{x} {y} {z}";
        }
    }
    
}
