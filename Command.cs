using System;
using System.Collections.Generic;
using System.Text;

namespace LethalFirstPlugin
{
    public abstract class Command
    {
        public virtual void Run()
        {

        }
    }

    public class SpawnCube : Command
    {
        public override void Run()
        {
            Plugin.mls.LogInfo("spawncube RAN!");
        }
    }
}
