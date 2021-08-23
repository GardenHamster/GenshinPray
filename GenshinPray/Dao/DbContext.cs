using GenshinPray.Models.PO;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Dao
{
    public class DbContext<T> where T : BasePO, new()
    {
        private static readonly string ConnectionString = $"Data Source=127.0.0.1;port=3306;Initial Catalog=genshinpray;uid=root;pwd=123456;";

        protected SqlSugarClient Db;//用来处理事务多表查询和复杂的操作 //注意：不能写成静态的

        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true//开启自动释放模式和EF原理一样我就不多解释了
            });
        }

        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来处理T表的常用操作

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(int id)
        {
            return Db.Queryable<T>().InSingle(id);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Insert(T t)
        {
            int id = Db.Insertable(t).ExecuteReturnIdentity();
            return GetById(id);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Update(T t)
        {
            Db.Updateable(t).ExecuteCommand();
            return GetById(t.Id);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic id)
        {
            return CurrentDb.Delete(id);
        }



    }
}
