using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorToolLib.MSBT
{
    interface ICalculatesSize
    {
        public ulong CalcSize();
    }
    interface IUpdates
    {
        public void Update();
    }
}
