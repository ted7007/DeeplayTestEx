using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Worker
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        

        public Sex Sex { get; set; }

        public int? PostId { get; set; }

        public Post Post { get; set; }

        public int? UIId { get; set; }

        public UniqueInfo UI { get; set; }

        public int? CompanyId { get; set ; }

        public Company Company { get; set; }
    
    }



    public enum Sex
    {
        Male,
        Female
    }
}
