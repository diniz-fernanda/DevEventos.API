using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DevEventos.API.Entities;
using DevEventos.API.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DevEventos.API.Controllers
{
    [ApiController]
    [Route("api/eventos")]
    public class EventoController : ControllerBase
    {
        private readonly EventoDbContext _contexto;
        public EventoController(EventoDbContext contexto)
        {
            _contexto = contexto;
        }
        //GET api/eventos
        [HttpGet]
        public IActionResult GetAll()
        {
            var eventos = _contexto.Eventos;
            return Ok(eventos);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var evento = _contexto.Eventos.SingleOrDefault(e => e.Id == id);

            if(evento == null)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpPost]
        public IActionResult Post(int id, Evento evento)
        {
            _contexto.Eventos.Add(evento);
            _contexto.SaveChanges();
            return CreatedAtAction(
                nameof(GetById),
                new { id = evento.Id},
                evento
                );

        }
        [HttpPut("{id}")]
        public IActionResult Put (int id, Evento evento)
        {
            var eventoExistente = _contexto.Eventos.SingleOrDefault(e => e.Id == id);

            if(eventoExistente == null)
            {
                return NotFound();
            }

            eventoExistente.Update(evento.Titulo, evento.Descricao, evento.DataInicio, evento.DataFim);

            _contexto.Eventos.Update(eventoExistente);
            _contexto.SaveChanges();
            return NoContent();

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var evento = _contexto.Eventos.SingleOrDefault(e => e.Id == id);

            if(evento == null)
            {
                return NotFound();
            }

            _contexto.Eventos.Remove(evento);
            _contexto.SaveChanges();
            return NoContent();
        }
    }
}