using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UniqueInfo
    {

        public int? Id { get; set; }

        public int? WorkerId { get; set; }

        public Worker Worker { get; set; }

        public string Text { get; set; }
    }
}
