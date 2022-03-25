using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class UserParam
    {
        public int id { get; set; }
        // 유저 고유키
        public string user_key { get; set; }
        //유저 아이디
        public string user_id { get; set; }
        public string name { get; set; }
        public int lv { get; set; }
        public int exp { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        // 등등 유저 정보

    }
}
