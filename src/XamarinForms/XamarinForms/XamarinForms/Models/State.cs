using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForms.Models
{
    public class State
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public State(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
