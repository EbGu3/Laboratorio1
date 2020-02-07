using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GaseosaLab01.Models
{
	public class Data
	{
		private static Data _instance = null;
		public static Data Instance
		{
			get
			{
				if (_instance == null) _instance = new Data();
				return _instance;
			}
		}

		public const int GRADO_A = 5;
		public BArbol<string, Bebidas> Arbolito1 = new BArbol<string, Bebidas>(GRADO_A);
		public List<Bebidas> data1 = new List<Bebidas>();
	}
}
