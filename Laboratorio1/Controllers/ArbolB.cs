using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio1.Controllers
{
    public class ArbolB
    {
        internal int PosicionAproxNodo(int key )
        {
            int pos = key.Count; 
            for (i = 0; i< key.Count; i++)
            {
                if ((key[i]> key) || (key[1] == Utilidades.ApuntadorVac))
                {
                    pos = i;
                    break;
                }
            }
            return pos;
        }
    }
}
