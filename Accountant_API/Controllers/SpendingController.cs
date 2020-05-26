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
    public class SpendingController : ControllerBase,IToken
    {
        private readonly ISpendingRepository _repository;

        public SpendingController(ISpendingRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Spending
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spending>>> GetSpendings()
        {
            var user = await _repository.ValidUser(GetTokenUserId());

            if (user == null)
            {
                return NotFound();
            }

            var spendings = await _repository.GetAll(user.Id);

            if (spendings == null)
            {
                return NotFound();
            }

            return Ok(spendings);
        }

        // GET: api/Spending/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Spending>> GetSpending(int id)
        {
            var user = await _repository.ValidUser(GetTokenUserId());

            if (user == null)
            {
                return NotFound();
            }

            var spending = await _repository.Get(id,user.Id);

            if (spending == null)
            {
                return NotFound();
            }

            return Ok(spending);
        }

        // PUT: api/Spending/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpending(int id, Spending spending)
        {
            try
            {
                if (id != spending.Id)
                {
                    return BadRequest();
                }

                if (await _repository.Update(spending) == null)
                {
                    return BadRequest();
                }

                await _repository.UpdateBalance(spending.BalanceId);

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

        // POST: api/Spending
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Spending>> PostSpending(Spending spending)
        {
            var user = await _repository.ValidUser(GetTokenUserId());

            if(user == null)
            {
                return NotFound();
            }

            if (await _repository.Add(spending,user.Id) == null)
            {
                return BadRequest();
            }

            await _repository.UpdateBalance(spending.BalanceId);

            return CreatedAtAction("POST", spending);
        }

        // DELETE: api/Spending/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Spending>> DeleteSpending(int id)
        {
            var spending = await _repository.Get(id,GetTokenUserId());

            if (spending == null)
            {
                return NotFound();
            }

            if (await _repository.Delete(id) == null)
            {
                return BadRequest();
            }

            return spending;
        }

        public int GetTokenUserId()
        {
            //NameIdentifier has the UserId value in the token.
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

    }
}
