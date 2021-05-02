using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MuCore.Input.Core;

namespace MuCore.Input.Infrastructure
{
    internal class InputParser
    {
        public async Task Parse(ClientInputEntity inputEntity)
        {
            await Task.Run(()=> { });
            //Retrieve client context:
            //Right now i'm thinking context-dependent parsers
            //  Each sub-parser associates with a different 'section'


        }
    }
}
