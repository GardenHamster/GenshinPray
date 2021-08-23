using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.AppSetting
{
    public class MysqlSetting
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string DataBase { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
