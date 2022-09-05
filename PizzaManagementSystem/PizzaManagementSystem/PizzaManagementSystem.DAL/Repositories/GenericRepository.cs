using Microsoft.EntityFrameworkCore;
using PizzaManagementSystem.DAL.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PizzaManagementSystem.DAL.Repositories
{
    /// <summary>
    /// Repository generico che viene ereditato da tutti i repository delle entità.
    /// Contiene i metodi per recuperare e salvare le entità con la logica di default ma è possibile
    /// eseguire l'override dei metodi all'interno dei singoli repository.
    /// </summary>
    /// <typeparam name="TEntity">Classe dell'entità su cui eseguire le operazioni di get/save. Questa entità deve implementare l'interfaccia IBaseEntity.</typeparam>
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected DbContext _context;
        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo per impostare nel contesto l'entità da salvare come ADDED (se l'ID è 0) o MODIFIED.
        /// Non applica la modifica al DB.
        /// </summary>
        /// <param name="entity">Record da salvare</param>
        public void Save(TEntity entity)
        {
            if (entity.ID == 0)
                _context.Entry(entity).State = EntityState.Added;
            else
                _context.Entry(entity).State = EntityState.Modified;
        }


        #region Metodi di GET

        /// <summary>
        /// Metodo per recuperare una singola entità tramite ID con eventuali relazioni (se specificate).
        /// </summary>
        /// <param name="ID">ID del record da recuperare</param>
        /// <param name="includes">Relazioni da includere nella get</param>
        /// <returns>Restituisce la singola entità recuperata tramite ID</returns>
        public TEntity Get(int ID, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = CreateBaseQuery(includes);

            TEntity entity = query.Where(e => e.ID == ID).FirstOrDefault();

            if (entity == null)
                throw new Exception($"Element with ID {ID} not found.");

            return entity;
        }

        /// <summary>
        /// Metodo per recuperare tutte le entità presenti nella tabella (includendo le eventuali relazioni specificate)
        /// che rispettano il filtro passato.
        /// </summary>
        /// <param name="predicate">Espressione usata per filtrare i dati sulla tabella</param>
        /// <param name="includes">Relazioni da includere nella get</param>
        /// <returns>Restituisce un IQueryable che contiene la lista delle entità recuperate</returns>
        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes = null)
        {
            IQueryable<TEntity> query = CreateBaseQuery(includes);
            return query.Where(predicate);
        }

        /// <summary>
        /// Metodo per recuperare tutte le entità presenti nella tabella (includendo le eventuali relazioni specificate).
        /// </summary>
        /// <param name="includes">Relazioni da includere nella get</param>
        /// <returns>Restituisce un IQueryable che contiene la lista delle entità recuperate</returns>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, object>>[] includes = null)
        {
            return CreateBaseQuery(includes);
        }

        #endregion

        /// <summary>
        /// Metodo per marcare come DELETED l'entità nel contesto solo se questa era stata precedentemente salvata.
        /// Non aggiorna i dati sul DB.
        /// </summary>
        /// <param name="entity">Record da eliminare</param>
        public void Delete(TEntity entity)
        {
            if (entity.ID != 0)
                _context.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Recupera il DbSet relativo alla TEntity e applica tutti gli include passati in input per restituire un IQueryable. 
        /// </summary>
        /// <param name="includes">Eventuali relazioni da incluedere</param>
        /// <returns>Restituisce un IQueryable<TEntity> compreso di tutte le relazioni richieste.</returns>
        private IQueryable<TEntity> CreateBaseQuery(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable<TEntity>();
            if (includes != null && includes.Length > 0)
                foreach (var include in includes)
                        query = query.Include(include);

            return query;
        }
    }
}
