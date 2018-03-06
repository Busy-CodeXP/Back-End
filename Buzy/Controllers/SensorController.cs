using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buzy.DataAccess;
using Buzy.DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Buzy.Controllers
{
    [Route("api/[controller]")]
    public class SensorController : Controller
    {
        public BusHelperContext _db { get; set; }

        public SensorController(BusHelperContext db)
        {
            this._db = db;
        }

        [HttpGet]
        public IEnumerable<Sensor> Get()
        {
            return this._db.Sensores.Include(s => s.veiculo).ToList();
        }

        [HttpGet("{id}")]
        public Sensor Get(int id)
        {
            return this._db.Sensores.Include(s => s.veiculo).Single(v => v.Id == id);
        }

        [HttpGet("{id}/historico")]
        public IEnumerable<HistoricoSensor> GetHistorico(int id)
        {
            return this._db.HistoricoSensores.Include(h => h.sensor).Where(h => h.sensor.Id == id).ToList();
        }

        [HttpPost]
        public Sensor Post([FromBody] SensorViewModel model)
        {
            var sensor = new Sensor();
            sensor.acao = model.acao;
            sensor.tipo = model.tipo;
            sensor.valor = model.valor;
            sensor.veiculo = this._db.Veiculo.Single(s => s.Id == model.veiculoId);

            this._db.Sensores.Add(sensor);
            this._db.SaveChanges();

            return sensor;
        }

        [HttpPost("{id}/atualizar-posicao-e-valor")]
        public HistoricoSensor Post(int id, [FromBody] AtualizaSensorViewModel model)
        {
            var sensor = this._db.Sensores.Single(s => s.Id == id);
            sensor.valor = model.valor;
            sensor.latitude = model.latitude;
            sensor.longitude = model.longitude;

            this._db.Sensores.Update(sensor);
            this._db.SaveChanges();

            var historicoSensor = new HistoricoSensor();
            historicoSensor.data = DateTime.Now;
            historicoSensor.latitude = model.latitude;
            historicoSensor.longitude = model.longitude;
            historicoSensor.sensor = sensor;
            historicoSensor.valor = model.valor;

            this._db.HistoricoSensores.Add(historicoSensor);
            this._db.SaveChanges();

            return historicoSensor;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] SensorViewModel model)
        {
            var sensor = this._db.Sensores.Single(s => s.Id == id);
            sensor.acao = model.acao;
            sensor.tipo = model.tipo;
            sensor.valor = model.valor;

            this._db.Sensores.Update(sensor);
            this._db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var sensor = this._db.Sensores.Single(s => s.Id == id);

            this._db.Sensores.Remove(sensor);
            this._db.SaveChanges();
        }
    }
}