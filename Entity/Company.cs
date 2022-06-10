using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Company
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public List<Worker> Workers { get; set; }

        public Company()
        {
            Workers = new List<Worker>();
        }
    }
}
