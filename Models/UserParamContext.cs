using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class UserParamContext : DbContext
    {
        public UserParamContext(DbContextOptions<UserParamContext> options)
            : base(options)
        {
        }

        public DbSet<UserParam> user_param { get; set; }


        public async Task<ActionResult<UserParam>> getUserParam(int id)
        {
            return await user_param.FindAsync(id);
        }

        public async Task<ActionResult<UserParam>> getUserParam(string user_id)
        {
            var user_data = user_param.Where(c => c.user_id == user_id);
            
            if(user_data.Count() == 0)
            {
                return null;
            }
            return user_data.First();
        }

        public void setUserParam(UserParam userParam)
        {
            user_param.Add(userParam);
        }
    }
}
