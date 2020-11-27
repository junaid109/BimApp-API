using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BimApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BimApp.API.Controllers
{
    [Authorize]
    public class ValuesController : BaseApiController
    {
        private readonly DataContext DataContext;

        public ValuesController(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetVales()
        {
            var values = await DataContext.Values.ToListAsync();

            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await DataContext.Values.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(value);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        [HttpPut("{id}")]
        public void Delete(int id)
        {

        }
    }
}