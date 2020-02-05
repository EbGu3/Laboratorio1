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
		public BArbol<string, string> Arbolito1 = new BArbol<string, string>(5);
	}
}
