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

        //Months
        //Spending Categories
        //Income Categories

    }
}
