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
        public DateTime timeOfCall;
        private CallStatus status;
       
        public Call(DateTime dateTime)
        {
            this.timeOfCall = dateTime;
            this.status = CallStatus.OUTGOING;
        }

        public Call(CallStatus status)
        {
            this.timeOfCall = DateTime.Now;
            this.status = status;
        }
        public override string ToString()
        {
            return $"Call: " +
                   $"\n\tTime of call: {timeOfCall}" +
                   $"\n\tStatus: {status}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Call other = (Call)obj;
            return timeOfCall == other.timeOfCall && status == other.status;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(timeOfCall, status);
        }

    }
}
