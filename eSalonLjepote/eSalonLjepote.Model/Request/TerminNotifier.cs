using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSalonLjepote.Model.Request
{
    public class TerminNotifier
    {
        public TerminNotifier()
        {
        }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
