using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionSample
{
    public class SoundHornService
    {
        public void SoundHorn(string volume)
        {
            Console.WriteLine($"Making noise with the volume turned up to {volume}.");
        }
    }
}
