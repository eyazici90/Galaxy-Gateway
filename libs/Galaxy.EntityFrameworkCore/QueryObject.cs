﻿using Galaxy.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Galaxy.EFCore
{
    public abstract class QueryObject<TEntity> : IQueryObject<TEntity>
    {
        private Expression<Func<TEntity, bool>> _query;

        public virtual Expression<Func<TEntity, bool>> Query()
        {
            return _query;
        }

        //public Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query)
        //{
        //    return _query = _query == null ? query : _query.And(query.Expand());
        //}

        //public Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query)
        //{
        //    return _query = _query == null ? query : _query.Or(query.Expand());
        //}

        public Expression<Func<TEntity, bool>> And(IQueryObject<TEntity> queryObject)
        {
            return And(queryObject.Query());
        }

        public Expression<Func<TEntity, bool>> Or(IQueryObject<TEntity> queryObject)
        {
            return Or(queryObject.Query());
        }

        public abstract Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query);
        public abstract Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query);
    }
}