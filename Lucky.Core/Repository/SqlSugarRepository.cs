/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Repository
*文件名： SqlSugarRepository
*创建人： Lxsh
*创建时间：2019/1/9 13:14:18
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/9 13:14:18
*修改人：Lxsh
*描述：
************************************************************************/
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;  
using System.Configuration;


namespace Lucky.Core.Repository
{
    public class SqlSugarRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        public SqlSugarClient _dbContent;
      
     
        public SqlSugarRepository()
        {
           _dbContent=CreateDBInstance.GetInstance();
        }

        public void SetDataContext(object db)
        {
            SqlSugarClient _dbContent = (SqlSugarClient)db;
        }

        #region Insert     

        public void Insert(TEntity entity)
        {
            _dbContent.Insertable(entity).ExecuteCommand();
        }

        public int InsertReturneCommand(TEntity entity)
        {
            return _dbContent.Insertable(entity).ExecuteCommand();
        }

        public TEntity InsertReturnEntity(TEntity entity)
        {
            return _dbContent.Insertable(entity).ExecuteReturnEntity();
        }

        public int InsertReturnIdentity(TEntity entity)
        {
            return _dbContent.Insertable(entity).ExecuteReturnIdentity();
        }

        public int BulkInsert(List<TEntity> entities)
        {
            return _dbContent.Insertable(entities.ToArray()).ExecuteCommand();
        }
        #endregion

        #region Delete  
        public void Delete(TEntity entity)
        {
            _dbContent.Deleteable<TEntity>().Where(entity).ExecuteCommand();
        }

        public void Delete(Expression<Func<TEntity, bool>> funcWhere)
        {
            _dbContent.Deleteable<TEntity>().Where(funcWhere).ExecuteCommand();
        }

        public void DeleteAll()
        {
            _dbContent.Deleteable<TEntity>().Where(t => true).ExecuteCommand();
        }
        #endregion

        #region SQL语句
        public List<TEntity> ExcuteSqlQuery(string sql, SqlParameter[] parameters)
        {
          return  _dbContent.Ado.SqlQuery<TEntity>(sql, parameters);
        }

        public DataTable ExcuteSqlQuery(string sql)
        {
            DataTable dt;
            try
            {
                _dbContent.Ado.BeginTran();
                dt = _dbContent.Ado.GetDataTable(sql);
                _dbContent.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContent.Ado.RollbackTran();
                throw ex;
            }
            return dt;
        }

        public bool ExecuteSqlCommand(string sql)
        {
            int nResult = _dbContent.Ado.ExecuteCommand(sql);
            return nResult > 0 ? true : false;
        }
        #endregion

        #region Query

        public TEntity Find(params object[] id)
        {
          return  _dbContent.Queryable<TEntity>().InSingle(id);
        }
         /// <summary>
         /// 查询所有数据，不是延时加载，慎用
         /// </summary>
         /// <returns></returns>
        public IQueryable<TEntity> GetModel()
        {
            return _dbContent.Queryable<TEntity>().ToList().AsQueryable();
        }

        public IQueryable<TEntity> GetModel(Expression<Func<TEntity, bool>> funcWhere)
        {
            return _dbContent.Queryable<TEntity>().Where(funcWhere).ToList().AsQueryable();
        }
       
        #endregion   

        #region Update

        public void Update(TEntity entity)
        {
            _dbContent.Updateable(entity).ExecuteCommand();
        }

        public int UpdateReturneCommand(TEntity entity)
        {
            return _dbContent.Updateable(entity).ExecuteCommand();
        }

        public int UpdateReturneCommand(List<TEntity> entities)
        {
            return _dbContent.Updateable(entities.ToArray()).ExecuteCommand();
        }

        public int UpdateReturneCommand(TEntity entity, Expression<Func<TEntity, object>> columns)
        {

            return _dbContent.Updateable(entity).UpdateColumns(columns).ExecuteCommand();
        }

        public int UpdateReturneCommand(TEntity entity, Expression<Func<TEntity, bool>> funcWhere)
        {
            return _dbContent.Updateable(entity).Where(funcWhere).ExecuteCommand();
        }



        #endregion
        public void Dispose()
        {
            this._dbContent.Dispose();
        }
    }
}