/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Repository
*文件名： EFRepository
*创建人： Lxsh
*创建时间：2019/1/9 14:27:57
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/9 14:27:57
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Repository
{
   public class EFRepository <TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// EF数据上下文
        /// </summary>
        public DbContext _dbContent;


        public EFRepository()
        {
          //  _dbContent = CreateDBInstance.GetInstance();
        }

        public void SetDataContext(object db)
        {
             _dbContent = (DbContext)db;
        }

        #region Insert     

        public void Insert(TEntity entity)
        {
            if (entity != null)
            {
                _dbContent.Entry<TEntity>(entity as TEntity);
                _dbContent.Set<TEntity>().Add(entity as TEntity);
                this.SaveChanges();
            }
        }
        public int InsertReturneCommand(TEntity entity)
        {
            throw new NotImplementedException();
        }
        public TEntity InsertReturnEntity(TEntity entity)
        {
            throw new NotImplementedException("EFRepository没有实现该接口");
        }

        public int InsertReturnIdentity(TEntity entity)
        {
            throw new NotImplementedException("EFRepository没有实现该接口");
        }

        public int BulkInsert(List<TEntity> entities)
        {
            throw new NotImplementedException("EFRepository没有实现该接口");
        }
        #endregion

        #region Delete  
        public void Delete(TEntity entity)
        {
            _dbContent.Set<TEntity>().Attach(entity as TEntity);
            _dbContent.Set<TEntity>().Remove(entity as TEntity);
            this.SaveChanges();
        }

        public void Delete(Expression<Func<TEntity, bool>> funcWhere)
        {
            throw new NotImplementedException("EFRepository没有实现该接口");
        }

        public void DeleteAll()
        {
            throw new NotImplementedException("EFRepository没有实现该接口");
        }
        #endregion

        #region SQL语句
        public List<TEntity> ExcuteSqlQuery(string sql, SqlParameter[] parameters)
        {    
            throw new NotImplementedException("EFRepository没有实现该接口"); 

        }     
        public DataTable ExcuteSqlQuery(string sql)
        {
            throw new NotImplementedException("EFRepository没有实现该接口");
        }

        public bool ExecuteSqlCommand(string sql)
        {
            int nResult = _dbContent.Database.ExecuteSqlCommand(sql);
            return nResult > 0 ? true : false;
        }
        #endregion

        #region Query

        public TEntity Find(params object[] id)
        {
            return _dbContent.Set<TEntity>().Find(id);
        }
        /// <summary>
        /// 查询所有数据，不是延时加载，慎用
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetModel()
        {
            return _dbContent.Set<TEntity>();
        }

        public IQueryable<TEntity> GetModel(Expression<Func<TEntity, bool>> funcWhere)
        {
            return GetModel().Where(funcWhere);
        }

        #endregion

        #region Update

        public void Update(TEntity entity)
        {
            if (entity != null)
            {
                _dbContent.Set<TEntity>().Attach(entity);
                _dbContent.Entry(entity).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        public int UpdateReturneCommand(TEntity entity)
        {
            throw new NotImplementedException("EFRepository没有实现该接口");
        }

        public int UpdateReturneCommand(List<TEntity> entities)
        {
            throw new NotImplementedException("EFRepository没有实现该接口");
        }

        public int UpdateReturneCommand(TEntity entity, Expression<Func<TEntity, object>> columns)
        {

            throw new NotImplementedException("EFRepository没有实现该接口");
        }

        public int UpdateReturneCommand(TEntity entity, Expression<Func<TEntity, bool>> funcWhere)
        {
            throw new NotImplementedException("EFRepository没有实现该接口");
        }

        /// <summary>
        /// 提交到数据库
        /// </summary>
        protected void SaveChanges()
        {
            try
            {

                _dbContent.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
              
                throw new DbUpdateConcurrencyException("Lind.DDD框架在更新时引起了乐观并发，后修改的数据不会被保存");
            }
            catch (DbEntityValidationException ex)
            {
                List<string> errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    string entityName = validationResult.Entry.Entity.GetType().Name;
                    foreach (DbValidationError error in validationResult.ValidationErrors)
                    {
                        errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                    }
                }
               
                throw;
            }
            catch (Exception ex)
            {
              
                throw;
            }
        }

        #endregion
        public void Dispose()
        {
            this._dbContent.Dispose();
        }

       
    }
}