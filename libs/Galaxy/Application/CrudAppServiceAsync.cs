﻿using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application
{
    public class CrudAppServiceAsync<TEntity, TEntityDto, TKey> : CrudAppService<TEntity, TEntityDto, TKey>, ICrudAppServiceAsync 
        where TEntity : class, IEntity<TKey>, IAggregateRoot, IObjectState
    {
        public CrudAppServiceAsync(IRepositoryAsync<TEntity, TKey> repositoryAsync
            , IObjectMapper objectMapper) : base(repositoryAsync, objectMapper)
        {
        }

        public virtual async Task<TEntityDto> AddAsync(Func<Task<TEntity>> when)
        {
            var aggregate = await when();
            var insertedAggregate = await _repositoryAsync.InsertAsync(aggregate);
            return base._objectMapper.MapTo<TEntityDto>(
                insertedAggregate
                );
        }

        public virtual async Task<TEntityDto> UpdateAsync(TKey id, Func<TEntity, Task> when)
        {
            TEntity aggregate = await base._repositoryAsync.FindAsync(id);
            await when(aggregate);
            aggregate =_repositoryAsync.Update(aggregate);
            return base._objectMapper.MapTo<TEntityDto>(
                aggregate
                );
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            await base._repositoryAsync.DeleteAsync(id);
        }
    }


}
