using GenshinPray.Common;
using GenshinPray.Models.PO;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Dao
{
    public class DBClient
    {
        public void CreateDB()
        {
            DbScoped.SugarScope.DbMaintenance.CreateDatabase();
            DbScoped.SugarScope.CodeFirst.InitTables(typeof(AuthorizePO));
            DbScoped.SugarScope.CodeFirst.InitTables(typeof(GoodsPO));
            DbScoped.SugarScope.CodeFirst.InitTables(typeof(MemberGoodsPO));
            DbScoped.SugarScope.CodeFirst.InitTables(typeof(MemberPO));
            DbScoped.SugarScope.CodeFirst.InitTables(typeof(PondGoodsPO));
            DbScoped.SugarScope.CodeFirst.InitTables(typeof(PrayRecordPO));
        }

        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool CheckTable(System.Type type)
        {
            string tableName = DbScoped.SugarScope.EntityMaintenance.GetTableName(type);
            return DbScoped.SugarScope.DbMaintenance.IsAnyTable(tableName, false);
        }

        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool CheckTable<T>()
        {
            string tableName = DbScoped.SugarScope.EntityMaintenance.GetTableName(typeof(T));
            return DbScoped.SugarScope.DbMaintenance.IsAnyTable(tableName, false);
        }

        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public bool CheckTable(string TableName)
        {
            return DbScoped.SugarScope.DbMaintenance.IsAnyTable(TableName, false);
        }

    }
}
