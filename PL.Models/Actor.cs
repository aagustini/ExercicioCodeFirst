using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    public class Actor
    {
        public int ActorId { get; set;  }
        public String Name { get; set; }
        public DateTime DateBirth { get; set; }

        public virtual ICollection<ActorMovie> Characters { get; set; }
    }
}
