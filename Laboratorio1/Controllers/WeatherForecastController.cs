using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GaseosaLab01.Models;

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
            string nnueva = nueva.ToString();
            Bebidas miBebida = new Bebidas();
            
            Data.Instance.Arbolito1.Insertar(nueva.Posicion, nueva.Sabor);
            Data.Instance.Arbolito1.Insertar(nueva.Posicion, nueva.Sabor);
        }
  
    }
}
