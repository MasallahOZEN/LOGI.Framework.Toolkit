using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using LOGI.Framework.Toolkit.Foundation.Repository;

namespace LOGI.Framework.Toolkit.Core.SendMessage.Email.Provider.Database
{
    /*
     Sınıfın MarshalByRefObject'den kalıtılmasının nedeni, Unity interface interception
     */
    ///<summary>
    /// EntityFrameworkv40 GenericRepository
    ///</summary>
    ///<typeparam name="T"></typeparam>
    public class GenericRepository<T> :MarshalByRefObject, IGenericRepository<T> where T : EntityBase
    {
        #region Field

        public ObjectContext ObjectContext { get; set; }

        #endregion

        #region Constructor

        public GenericRepository()
            : this(new LOGIToolkitMessageQueueEntities())
        {

        }

        public GenericRepository(ObjectContext dataContext)
        {
            if (dataContext == null)
                throw new ArgumentNullException("dataContext");

            ObjectContext = dataContext;
            if (dataContext.Connection != null)
            {
                //dataContext.Connection.ConnectionString = conStr;
                
            }
            
            this.ObjectContext.ContextOptions.LazyLoadingEnabled = true;            
        }
        #endregion

        #region IGenericRepository<T> Members

        public IQueryable<T> GetQuery()
        {
            // TODO Check this works with inheritance
            return this.ObjectContext.CreateObjectSet<T>();
        }

        public T Load(Expression<Func<T, bool>> whereCondition)
        {
            return GetQuery().Where<T>(whereCondition).FirstOrDefault();
        }

        public void LoadProperty(T entity, Expression<Func<T, object>> selector)
        {
            this.ObjectContext.LoadProperty(entity, selector);
        }

        public IEnumerable<T> GetAll()
        {
            return GetQuery().AsEnumerable();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return GetQuery().Where<T>(predicate);
        }

        public void Add(T entity)
        {
            this.ObjectContext.AddObject(GetBaseType(typeof(T)).Name.ToString(), entity);
        }

        public int SaveChanges()
        {
            return this.ObjectContext.SaveChanges(); 
        }

        public void Delete(T entity)
        {
            this.ObjectContext.DeleteObject(entity);
        }

        #region Wcf RadGrid Methods

        public ResultData<T> GetDataAndCount(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            ResultData<T> returnObj = new ResultData<T>();
            IQueryable<T> _sourceObj = this.GetQuery();
            try
            {

                if (!String.IsNullOrEmpty(filterExpression))
                {
                    _sourceObj = _sourceObj.Where(filterExpression);
                }

                if (!String.IsNullOrEmpty(sortExpression))
                {
                    _sourceObj = _sourceObj.OrderBy(sortExpression);
                }

                returnObj.Count = GetDataCount(filterExpression);
                startRowIndex = returnObj.Count < startRowIndex ? startRowIndex = 0 : startRowIndex;

                returnObj.Data = _sourceObj.Skip(startRowIndex).Take(maximumRows).ToList();
                return returnObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public int GetDataCount(string filterExpression)
        {
            IQueryable<T> _sourceObj = this.GetQuery();
            if (!String.IsNullOrEmpty(filterExpression))
            {
                _sourceObj = _sourceObj.Where(filterExpression);
            }

            return _sourceObj.Count();

        }
        #endregion
        #endregion

        #region Customs
        private static Type GetBaseType(Type type)
        {
            Type baseType = type.BaseType;
            if (baseType != null && baseType != typeof(EntityBase))
            {
                return GetBaseType(type.BaseType);
            }
            return type;
        }

        private static bool HasBaseType(Type type, out Type baseType)
        {
            Type originalType = type.GetType();
            baseType = GetBaseType(type);
            return baseType != originalType;
        }
        #endregion
    }
}
