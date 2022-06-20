using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.Lib.Info
{
    internal class ActorInfo
    {
        private Actor? actor;
        private FarActor? far;

        public ActorInfo(Actor actor)
        {
            this.actor = actor;
        }
        public ActorInfo(FarActor far)
        {
            this.far = far;
        }

        public void Update()
        {

        }
    }
}
