using System;
using System.Collections.Generic;
using System.Text;

namespace EverybodyCodes.CLI.Commands
{
    public class SearchCommandArgs
    {

        public string Name { get; set; }

        public SearchCommandArgs(string name)
        {
            Name = name;
        }
    }
}
