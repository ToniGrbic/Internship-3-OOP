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
       
        public Call(DateTime dateTime, CallStatus status)
        {
            timeOfCall = dateTime;
            this.status = status;
        }

        public Call(CallStatus status)
        {
            timeOfCall = DateTime.Now;
            this.status = status;
        }

        public DateTime GetTime()
        {
            return timeOfCall;
        }
        public override string ToString()
        {
            return $"Call: \n" +
                   $"\tTime of call: {timeOfCall}\n" +
                   $"\tStatus: {status}\n";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Call call = (Call)obj;
            return (timeOfCall == call.timeOfCall) && (status == call.status);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(timeOfCall, status);
        }
    }
}
