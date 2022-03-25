using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.DTO;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserParamContext _context;
        public LoginController(UserParamContext context)
        {
            _context = context;
        }

        // GET: api/UserParams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserParam>>> Getuser_param()
        {
            return await _context.user_param.ToListAsync();
        }

        // Index를 이용한 유저 정보 조회
        // GET: api/UserParams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserParam>> GetUserParam(int id)
        {
            var userParam = await _context.getUserParam(id);

            if (userParam == null)
            {
                return NotFound();
            }

            return userParam;
        }

        #region 계정 로그인 처리
        // 계정 로그인 관련 처리
        [HttpPost]
        public async Task<ActionResult<LoginDTO>> UserLogin(UserParam userParam)
        {
            var user = await _context.getUserParam(userParam.user_id);

            if (user == null)
            {
                NewUserLogin(userParam);
                //DB 비동기적으로 저장
                await _context.SaveChangesAsync();
                user = await _context.getUserParam(userParam.user_id);
            }

            LoginDTO dto = new LoginDTO();
            dto.id = user.Value.id;
            dto.user_key = user.Value.user_key;
            dto.user_id = user.Value.user_id;
            dto.name = user.Value.name;
            dto.lv = user.Value.lv;
            dto.exp = user.Value.exp;

            return dto;
        }

        private bool UserParamExists(int id)
        {
            return _context.user_param.Any(e => e.id == id);
        }


        private void NewUserLogin(UserParam userParam)
        {
            _context.setUserParam(userParam);
        }
        #endregion
    }
}
