using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GaseosaLab01.Models;
using System.Diagnostics;

namespace Laboratorio1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        
        [HttpPost]
        public void Post([FromBody]object jsonObtenido)
        {
            var jsonString = jsonObtenido.ToString();
            var nueva = JsonConvert.DeserializeObject<Bebidas>(jsonString);

            Bebidas miBebida = new Bebidas();
            miBebida.Nombre = nueva.Nombre;
            miBebida.Posicion = nueva.Posicion;
            miBebida.Sabor = nueva.Sabor;
            miBebida.Volumen = nueva.Volumen;
            miBebida.casaProductora = miBebida.casaProductora;


            Data.Instance.Arbolito1.Insertar(nueva.Nombre, miBebida);

            Data.Instance.data1.Add(miBebida);

        }

        [HttpGet]

        public List<Bebidas> ReturnDatas()
        {
            return Data.Instance.data1.OrderBy(o => o.Nombre).ToList();
        }
  
    }
}
