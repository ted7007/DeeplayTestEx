using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Post
    {
        public int? Id { get; set; }
        
        public string Name { get; set; }

        public List<Worker> Workers { get; set; }

        public string NameUniqueInformation { get; set; }

        public Post()
        {
            Workers = new List<Worker>();
        }
    }
}
