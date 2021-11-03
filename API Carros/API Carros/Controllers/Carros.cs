using API_CARROS.DataBase;
using API_CARROS.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CARROS.Controllers
{
    

    [ApiController]
    [Route("[controller]")]
    public class Carros : Controller
    {
        Db db = new();

        [HttpGet]
        public IActionResult GetCarros()
        {
            db.Conectar();
            var lst = db.GetAllCarros();
            db.Desconectar();
            return Ok(lst);
        }

        [HttpGet("placa:string")]
        public IActionResult GetByPlaca(string placa)
        {
            db.Conectar();
            var lst = db.GetByPlaca(placa);
            db.Desconectar();
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult PostCarro([FromBody] Carro carro)
        {
            db.Conectar();
            var lst = db.PostCarro(carro);
            db.Desconectar();
            return Ok(lst);
        }

        [HttpPut]
        public IActionResult PutCarros([FromBody] Carro carro)
        {
            db.Conectar();
            var lst = db.PutCarro(carro);
            db.Desconectar();
            return Ok(lst);
          
        }

        [HttpDelete("placa:string")]
        public IActionResult DeletarByPlaca(string placa)
        {
            db.Conectar();
            var lst = db.DeleteCarro(placa);
            db.Desconectar();
            return Ok(lst);
        }



    }
}
