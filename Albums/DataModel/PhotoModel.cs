using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albums.DataModel
{
    [Serializable]
    public class PhotoModel
    {
        private String source;
        private String name;

        public PhotoModel(String source,String name)
        {
            this.source = source;
            this.name = name;
        }

        public String Source
        {
            get
            {
                return source;
            }
        }

        public String Name
        {
            get
            {
                return name;
            }
        }

    }
}
