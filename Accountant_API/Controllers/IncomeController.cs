using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Interfaces;

namespace Accountant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeRepository _repository;

        public IncomeController(IIncomeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Income
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomes()
        {
            var incomes = await _repository.GetAll();

            if (incomes == null)
            {
                return NotFound();
            }

            return Ok(incomes);
        }

        // GET: api/Income/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> GetIncome(int id)
        {
            var income = await _repository.Get(id);

            if (income == null)
            {
                return NotFound();
            }

            return Ok(income);
        }

        // PUT: api/Income/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncome(int id, Income income)
        {
            try
            {
                if (id != income.Id)
                {
                    return BadRequest();
                }

                if (await _repository.Update(income) == null)
                {
                    return BadRequest();
                }

                await _repository.UpdateBalance(income.BalanceId);

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

        // POST: api/Income
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Income>> PostIncome(Income income)
        {
            
            if (await _repository.Add(income) == null)
            {
                return BadRequest();
            }

            await _repository.UpdateBalance(income.BalanceId);

            return CreatedAtAction("POST", income);
        }

        // DELETE: api/Income/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Income>> DeleteIncome(int id)
        {
            var income = await _repository.Get(id);

            if (income == null)
            {
                return NotFound();
            }

            if (await _repository.Delete(id) == null)
            {
                return BadRequest();
            }

            return income;
        }
    }
}
