using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTreeCS
{
    class Feature
    {
        public GeoType type { get; set; }
        public IDictionary<string,object> properties { get; set; }
        public Geometry geometry { get; set; }
    }
}
