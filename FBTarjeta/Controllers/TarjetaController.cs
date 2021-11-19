﻿using FBTarjeta.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FBTarjeta.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        public TarjetaController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<TarjetaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listTarjetas = await _context.TarjetaCredito.ToListAsync();
                return Ok(listTarjetas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<TarjetaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var datosTarjeta = await _context.TarjetaCredito.FindAsync(id);
            return Ok(datosTarjeta);
        }

        // POST api/<TarjetaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TarjetaCredito tarjeta)
        {
            try 
            { 
                _context.Add(tarjeta);
                await _context.SaveChangesAsync();
                return Ok(tarjeta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<TarjetaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TarjetaCredito tarjeta)
        {
            try
            {
                if (id != tarjeta.Id) return NotFound();
                _context.Update(tarjeta);
                await _context.SaveChangesAsync();
                return Ok(new { messange = "La tarjeta fue actualizada con éxito!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<TarjetaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var datos = await _context.TarjetaCredito.FindAsync(id);
                _context.TarjetaCredito.Remove(datos);
                await _context.SaveChangesAsync();
                return Ok(new { message = "La tarjeta fue eliminada con éxito!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
