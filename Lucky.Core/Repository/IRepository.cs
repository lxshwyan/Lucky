/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Repository
*文件名： IRepository
*创建人： Lxsh
*创建时间：2019/1/4 10:37:43
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/4 10:37:43
*修改人：Lxsh
*描述：
************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Repository
{  
    /// <summary>
     /// 基本仓储操作接口      
     /// </summary>
     /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>:IDisposable where TEntity : class, new()
    {
        #region 设置当前数据库上下文对象，与具体ORM无关    
        void SetDataContext(object db);
        #endregion

        #region Insert
        /// <summary>
        /// 单个对象插入
        /// </summary>
        /// <param name="entity">插入对象</param>
        void Insert(TEntity entity);
        /// <summary>
        ///  单个对象插入(返回新增实体)
        /// </summary>
        /// <param name="entity">插入对象</param>
        /// <returns>返回新增实体</returns>
        TEntity InsertReturnEntity(TEntity entity);

        /// <summary>
        ///  单个对象插入(返回插入自增列)
        /// </summary>
        /// <param name="entity">插入对象</param>
        /// <returns>返回插入自增列</returns>
        int InsertReturnIdentity(TEntity entity);

        /// <summary>
        ///  单个对象插入(返回插入影响行数)
        /// </summary>
        /// <param name="entity">插入对象</param>
        /// <returns>返回插入影响行数</returns>
        int InsertReturneCommand(TEntity entity);
        /// <summary>
        ///   批量对象插入
        /// </summary>
        /// <param name="">插入对象集合</param>
        /// <returns>返回插入影响行数</returns>
        int BulkInsert(List<TEntity> entities);

        #endregion

        #region Delete
        /// <summary>
        ///   删除单个对象
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);    
        /// <summary>
        /// 按条件批量对象(删除条目)
        /// </summary>
        /// <param name="funcWhere">删除对象的条件</param> 
        void Delete(Expression<Func<TEntity, bool>> funcWhere);
        /// <summary>
        ///  删除所有对象
        /// </summary>
        void DeleteAll();

        #endregion

        #region Update
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="entity">更新对象</param>
        void Update(TEntity entity);
        /// <summary>
        ///  更新实体指定的列
        /// </summary>
        /// <param name="entity">更新对象</param>
        /// <returns></returns>
        int UpdateReturneCommand(TEntity entity);
        /// <summary>
        ///  批量更新实体对象集合
        /// </summary>
        /// <param name="entities"> 批量更新对象集合</param>
        /// <returns></returns>
        int UpdateReturneCommand(List<TEntity> entities);

        /// <summary>
        /// 更新实体指定的列
        /// </summary>
        /// <param name="entity">更新对象</param>
        /// <param name="columns">指定更新的列</param>
        /// <returns></returns>
        int UpdateReturneCommand(TEntity entity, Expression<Func<TEntity, object>> columns);
        /// <summary>
        /// 按条件更新对象
        /// </summary>
        /// <param name="entity">更新对象条件</param>
        /// <param name="funcWhere">更新条件</param>
        /// <returns></returns>
        int UpdateReturneCommand(TEntity entity, Expression<Func<TEntity, bool>> funcWhere) ;

        #endregion

        #region Query（PartitionBy(st => new { st.Name }).Take(2).OrderBy(st => st.Id, OrderByType.Desc).Select(st => st).ToPageList(1, 1000, ref count);）
        /// 通过主键拿一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(params object[] id);
        /// <summary>
        ///  查找该实体所有集合（延迟加载的，需要及时加载使用toList）
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetModel();
        /// <summary>
        ///  按条件查找实体集合（延迟加载的，需要及时加载使用toList）
        /// </summary>
        /// <param name="funcWhere">查询条件</param>
        /// <returns></returns>
        IQueryable<TEntity> GetModel(Expression<Func<TEntity, bool>> funcWhere);

        #endregion

        #region 直接操作sql语句
        /// <summary>
        ///  返回 List集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<TEntity> ExcuteSqlQuery(string sql, SqlParameter[] parameters);  
        /// <summary>
        ///  返回 DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable ExcuteSqlQuery(string sql);   
        /// <summary>
        ///  返回成功或失败
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        bool ExecuteSqlCommand(string sql);  
        #endregion
    }
}
