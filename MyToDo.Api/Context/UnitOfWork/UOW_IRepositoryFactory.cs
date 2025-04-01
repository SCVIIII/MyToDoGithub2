//-----------------------------------------------------------------------
// <copyright file="IRepositoryFactory.cs" company="Arch team">
// Copyright (c) Arch team. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MyToDo.Api
{
    /// <summary>
    /// Defines the interfaces for <see cref="UOW_IRepository{TEntity}"/> interfaces.
    /// </summary>
    public interface UOW_IRepositoryFactory
    {
        /// <summary>
        /// Gets the specified repository for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="hasCustomRepository"><c>True</c> if providing custom repositry</param>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="UOW_IRepository{TEntity}"/> interface.</returns>
        UOW_IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;
    }
}