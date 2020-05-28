using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Interfaces;

namespace Accountant_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase,IToken
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
            var user = await _repository.ValidUser(GetTokenUserId());

            if (user == null)
            {
                return NotFound();
            }

            var incomes = await _repository.GetAll(user.Id);

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
            var user = await _repository.ValidUser(GetTokenUserId());

            if (user == null)
            {
                return NotFound();
            }

            var income = await _repository.Get(id,user.Id);

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

            var user = await _repository.ValidUser(GetTokenUserId());

            if (user == null)
            {
                return NotFound();
            }

            if (await _repository.Add(income,user.Id) == null)
            {
                return BadRequest();
            }

            await _repository.UpdateBalance(income.BalanceId);

            return StatusCode(201);
        }

        // DELETE: api/Income/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Income>> DeleteIncome(int id)
        {
            var user = await _repository.ValidUser(GetTokenUserId());

            if (user == null)
            {
                return NotFound();
            }

            var income = await _repository.Get(id,user.Id);

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

        public int GetTokenUserId()
        {
            //NameIdentifier has the UserId value in the token.
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
