using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;
using Model.Interfaces;

namespace Accountant_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DropDownController : ControllerBase
    {
        private readonly DataContext _context;

        public DropDownController(DataContext context)
        {
            _context = context;
        }

        //Api/DropDown/GetMonthList
        [HttpGet]
        [Route("GetMonthList")]
        public async Task<ActionResult<IEnumerable<Month>>> GetMonthList()
        {
            try
            {
                return await _context.Months.OrderBy(m => m.Id).ToListAsync();
            }
            catch (Exception)
            {

                return StatusCode(500, "Month List Not Available");
            }
        }

        //Api/DropDown/GetSpendingCategories
        [HttpGet]
        [Route("GetSpendingCategories")]
        public async Task<ActionResult<IEnumerable<SpendingType>>> GetSpendingCategories()
        {
            try
            {
                return await _context.SpendingCategories.OrderBy(c => c.Name).ToListAsync();
            }
            catch (Exception)
            {

                return StatusCode(500, "Categories Not Available");
            }
        }

        //Api/DropDown/GetIncomeCategories
        [HttpGet]
        [Route("GetIncomeCategories")]
        public async Task<ActionResult<IEnumerable<IncomeType>>> GetIncomeCategories()
        {
            try
            {
                return await _context.IncomeCategories.OrderBy(c => c.Name).ToListAsync();
            }
            catch (Exception)
            {

                return StatusCode(500, "Categories Not Available");
            }
        }

    }
}
