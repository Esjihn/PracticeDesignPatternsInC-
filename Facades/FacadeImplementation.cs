using System;
using System.Collections.Generic;
using System.Text;

namespace Facades
{
    public abstract class EnginePartDescription
    {
        public abstract override string ToString();
    }


    public class EngineFacade 
    {
        EngineBlock engineBlock = new EngineBlock();
        Pistons piston = new Pistons();
        CylinderHead cylinderHead = new CylinderHead();
        CrankShaft crankShaft = new CrankShaft();
        CamShaft camShaft = new CamShaft();
        Valves valves = new Valves();
        OilPan oilPan = new OilPan();


        public void BuildEngine()
        {
            Console.WriteLine(engineBlock.ToString());
            Console.WriteLine(piston.ToString());
            Console.WriteLine(cylinderHead.ToString());
            Console.WriteLine(crankShaft.ToString());
            Console.WriteLine(camShaft.ToString());
            Console.WriteLine(valves.ToString());
            Console.WriteLine(oilPan.ToString());
        }

        public void BuildShafts()
        {
            Console.WriteLine(crankShaft.ToString());
            Console.WriteLine(camShaft.ToString());
        }

        private class EngineBlock : EnginePartDescription
        {
            public override string ToString()
            {
                return "Building engine block...";
            }
        }

        private class Pistons : EnginePartDescription
        {
            public override string ToString()
            {
                return "Building pistons...";
            }
        }

        private class CylinderHead : EnginePartDescription
        {
            public override string ToString()
            {
                return "Building cylinder head...";
            }
        }

        private class CrankShaft : EnginePartDescription
        {
            public override string ToString()
            {
                return "Building crankshaft...";
            }
        }

        private class CamShaft : EnginePartDescription
        {
            public override string ToString()
            {
                return "Building camshaft...";
            }
        }

        private class Valves : EnginePartDescription
        {
            public override string ToString()
            {
                return "Building valves...";
            }
        }

        private class OilPan : EnginePartDescription
        {
            public override string ToString()
            {
                return "Building oil pan...";
            }
        }
    }

    public class FacadeImplementation
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            EngineFacade ef = new EngineFacade();
            ef.BuildEngine();
            Console.WriteLine();
            ef.BuildShafts();
        }
    }
}
