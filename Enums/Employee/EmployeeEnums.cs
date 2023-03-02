using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspapp.Enums.Employee
{
    public class Roles
    {
        private Roles(string value) { Value = value; }

        public string Value { get; private set; }

        public static Roles Manager   { get { return new Roles("Manager"); } }
        public static Roles Employee   { get { return new Roles("Employee"); } }
        public static Roles Admin    { get { return new Roles("Admin"); } }


        public override string ToString()
        {
            return Value;
        }

    }
}