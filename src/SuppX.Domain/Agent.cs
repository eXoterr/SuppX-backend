using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuppX.Domain
{
    public class Agent
    {
        public int UserId { get; set; }
        public required User User { get; set; }
        public AgentGroup? Group { get; set; }
        public int Rating { get; set; }
        public List<Ticket>? Tickets { get; set; }
    }
}
