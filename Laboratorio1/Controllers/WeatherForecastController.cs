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

        [HttpGet]
        public ActionResult<string> Principal()
        {

            return "HOLA";
        }


        [HttpPost]
        public void Post([FromBody]object jsonObtenido)
        {
            var jsonString = jsonObtenido.ToString();
            var nueva = JsonConvert.DeserializeObject<Bebidas>(jsonString);

            Bebidas miBebida = new Bebidas();
            miBebida.Nombre = nueva.Nombre;
            miBebida.Sabor = nueva.Sabor;
            miBebida.Volumen = nueva.Volumen;
            miBebida.casaProductora = nueva.casaProductora;


            Data.Instance.Arbolito1.Insertar(nueva.Nombre, miBebida);

            Data.Instance.data1.Add(miBebida);

        }

        [HttpGet("{id?}")]
        public List<Bebidas> Return([FromBody]object id)
        {
            if(id != null)
            {
                var jsonString = id.ToString();
                var nueva = JsonConvert.DeserializeObject<Bebidas>(jsonString);
                Data.Instance.Arbolito1.Busqueda(nueva.Nombre);
                
            }
            return Data.Instance.data2;
        }

        [HttpGet("MostrarArbol")]
        public List<Bebidas> ReturnData()
        {
            return Data.Instance.data1.OrderBy(o => o.Nombre).ToList();
        }

    }
}
