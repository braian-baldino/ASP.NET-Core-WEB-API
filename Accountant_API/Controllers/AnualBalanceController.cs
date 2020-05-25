using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Entities;
using Model.Interfaces;

namespace Accountant_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AnualBalanceController : ControllerBase
    {
        private readonly IAnualBalanceRepository _repository;

        public AnualBalanceController(IAnualBalanceRepository repository)
        {
            _repository = repository;
        }

        // GET: api/AnualBalance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnualBalance>>> GetAnualBalances()
        {
            var anualBalances = await _repository.GetAll();

            if (anualBalances == null)
            {
                return NotFound();
            }

            return Ok(anualBalances);
        }

        // GET: api/AnualBalance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnualBalance>> GetAnualBalance(int id)
        {
            var anualBalance = await _repository.Get(id);

            if (anualBalance == null)
            {
                return NotFound();
            }

            return Ok(anualBalance);
        }

        // PUT: api/AnualBalance/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnualBalance(int id, AnualBalance anualBalance)
        {
            try
            {
                if (id != anualBalance.Id)
                {
                    return BadRequest();
                }

                if (await _repository.Update(anualBalance) == null)
                {
                    return BadRequest();
                }

                return NoContent();
            }
            catch (Exception)
            {
                if (!_repository.Exist(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // POST: api/AnualBalance
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AnualBalance>> PostAnualBalance(AnualBalance anualBalance)
        {
            var response = await _repository.Add(anualBalance);

            if ( response == null)
            {
                return BadRequest();
            }

            if(! await _repository.AddMonths(response.Id))
            {
                return BadRequest();
            }

            return Created("POST",anualBalance);
        }

        // DELETE: api/AnualBalance/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AnualBalance>> DeleteAnualBalance(int id)
        {
            var anualBalance = await _repository.Get(id);

            if (anualBalance == null)
            {
                return NotFound();
            }

            if(await _repository.Delete(id) == null)
            {
                return BadRequest();
            }

            return anualBalance;
        }

    }
}
