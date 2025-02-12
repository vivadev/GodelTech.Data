﻿using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using GodelTech.Data.Specification;

namespace GodelTech.Data
{
    public static partial class RepositoryExtensions
    {
        /// <summary>
        /// Checks if entity of type T with identifier exists in repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        public static bool Exists<TEntity, TKey>(this IRepository<TEntity, TKey> repository, TKey id)
            where TEntity : class, IEntity<TKey>
        {
            return repository.Exists(
                FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id)
            );
        }

        /// <summary>
        /// Checks if entity of type T that satisfies an expression exists in repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        public static bool Exists<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.Exists(
                filterExpression?.CreateQueryParameters<TEntity, TKey>()
            );
        }

        /// <summary>
        /// Checks if entity of type T that satisfies a specification exists in repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="specification">The specification.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        public static bool Exists<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            ISpecification<TEntity, TKey> specification)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.Exists(
                specification.CreateQueryParameters()
            );
        }

        /// <summary>
        /// Asynchronously checks if entity of type T with identifier exists in repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        public static async Task<bool> ExistsAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            TKey id,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            return await repository.ExistsAsync(
                FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id),
                cancellationToken
            );
        }

        /// <summary>
        /// Asynchronously checks if entity of type T that satisfies an expression exists in repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        public static Task<bool> ExistsAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression = null,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.ExistsInternalAsync(filterExpression, cancellationToken);
        }

        /// <summary>
        /// Asynchronously checks if entity of type T that satisfies a specification exists in repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="specification">The specification.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        public static Task<bool> ExistsAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            ISpecification<TEntity, TKey> specification,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.ExistsInternalAsync(specification, cancellationToken);
        }

        private static async Task<bool> ExistsInternalAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression = null,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            return await repository.ExistsAsync(
                filterExpression?.CreateQueryParameters<TEntity, TKey>(),
                cancellationToken
            );
        }

        private static async Task<bool> ExistsInternalAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            ISpecification<TEntity, TKey> specification,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            return await repository.ExistsAsync(
                specification.CreateQueryParameters(),
                cancellationToken
            );
        }
    }
}
