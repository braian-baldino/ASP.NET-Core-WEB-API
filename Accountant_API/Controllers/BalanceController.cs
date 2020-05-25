using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Interfaces;

namespace Accountant_API.Controllers
{   

    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceRepository _repository;

        public BalanceController(IBalanceRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Balance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Balance>>> GetBalances()
        {
            var balances = await _repository.GetAll();

            if (balances == null)
            {
                return NotFound();
            }

            return Ok(balances);
        }

        // GET: api/Balance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Balance>> GetBalance(int id)
        {
            var balance = await _repository.Get(id);

            if (balance == null)
            {
                return NotFound();
            }

            return Ok(balance);
        }

        // PUT: api/Balance/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBalance(int id, Balance balance)
        {
            try
            {
                if (id != balance.Id)
                {
                    return BadRequest();
                }

                if (await _repository.Update(balance) == null)
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

        // DELETE: api/Balance/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Balance>> DeleteBalance(int id)
        {
            var balance = await _repository.Get(id);

            if (balance == null)
            {
                return NotFound();
            }

            if (await _repository.Delete(id) == null)
            {
                return BadRequest();
            }

            return balance;
        }
    }
}
