using Internship_3_OOP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP.Classes
{
    public class Call
    {
        private DateTime timeOfCall;
        private CallStatus status;
        public Call() {
            this.timeOfCall = DateTime.Now;
            this.status = CallStatus.OUTGOING;
        }
        
    }
}
