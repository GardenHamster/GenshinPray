using GenshinPray.Common;
using GenshinPray.Models.PO;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Dao
{
    public class DBClient
    {
        public SqlSugarClient SqlSugarClient;//用来处理事务多表查询和复杂的操作 //注意：不能写成静态的

        public DBClient()
        {
            SqlSugarClient = GetInstance();
        }

        public SqlSugarClient GetInstance()
        {
            return new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.MySql,
                ConnectionString = SiteConfig.ConnectionString,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true//开启自动释放模式和EF原理一样我就不多解释了
            });
        }

        public void CreateDB()
        {
            SqlSugarClient.DbMaintenance.CreateDatabase();
            SqlSugarClient.CodeFirst.InitTables(typeof(AuthorizePO));
            SqlSugarClient.CodeFirst.InitTables(typeof(GoodsPO));
            SqlSugarClient.CodeFirst.InitTables(typeof(MemberGoodsPO));
            SqlSugarClient.CodeFirst.InitTables(typeof(MemberPO));
            SqlSugarClient.CodeFirst.InitTables(typeof(PondGoodsPO));
            SqlSugarClient.CodeFirst.InitTables(typeof(PrayRecordPO));
        }

    }
}
