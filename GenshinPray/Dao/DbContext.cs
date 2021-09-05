using GenshinPray.Models.PO;
using SqlSugar;

namespace GenshinPray.Dao
{
    public class DbContext<T> where T : BasePO, new()
    {
        protected readonly DBClient DBClient;

        public DbContext()
        {
            DBClient = new DBClient();
        }

        protected SqlSugarClient Db
        {
            get { return DBClient.SqlSugarClient; }
        }

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
        /// 添加一条记录
        /// </summary>
        /// <returns></returns>
        public virtual T Insert(T t)
        {
            return Db.Insertable(t).ExecuteReturnEntity();
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <returns></returns>
        public virtual int Update(T t)
        {
            return Db.Updateable(t).ExecuteCommand();
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <returns></returns>
        public virtual int Delete(T t)
        {
            return Db.Deleteable(t).ExecuteCommand();
        }


    }
}
