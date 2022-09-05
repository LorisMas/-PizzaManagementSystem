using PizzaManagementSystem.DAL.Context;
using PizzaManagementSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PizzaManagementSystem.Core.EntityServices
{
    public abstract class GenericEntityService<TRepository, TEntity> where TRepository : IRepository<TEntity> where TEntity : IBaseEntity
    {
        protected DatabaseContext _context;
        protected TRepository _repository;

        protected GenericEntityService(DatabaseContext context)
        {
            _context = context;
            _repository = (TRepository)Activator.CreateInstance(typeof(TRepository), _context);
        }

        #region Metodi di SAVE

        /// <summary>
        /// Metodo per salvare il record ricevuto su database.
        /// Esegue prima il salvataggio tramite il repository relativo all'entità passata e 
        /// poi esegue il SaveChanges del contesto per aggiornare i dati sul DB.
        /// </summary>
        /// <param name="entity">Entità di tipo IBaseEntity a cui impostate lo stato ADDED o MODIFIED prima di salvare le modifiche nel contesto.</param>
        /// <param name="saveChanges">Se TRUE aggiorna i dati sul db altrimenti no.</param>
        /// <returns>Restituisce l'ID dell'entità appena salvata.</returns>
        public virtual int Save(TEntity entity, bool saveChanges = true)
        {
            _repository.Save(entity);

            if (saveChanges == true)
                _context.SaveChanges();

            return entity.ID;
        }

        /// <summary>
        /// Metodo per salvare direttamente una lista di entità. Per ogni record della lista invoca il metodo Save
        /// passando a FALSE il paramentro saveChanges per aggiornare i dati sul DB solo dopo aver marcato tutte le
        /// entità come ADDED o MODIFIED. Tutta la procedura può essere eseguita in transaction.
        /// </summary>
        /// <param name="entities">Lista delle entità da salvare</param>
        /// <param name="createTransaction">Se TRUE allora esegue il salvataggio delle entità all'interno di un'unica transaction.</param>
        public virtual void Save(List<TEntity> entities, bool createTransaction = true)
        {
            if (createTransaction)
            {
                try
                {
                    _context.Database.BeginTransaction();

                    foreach (TEntity entity in entities)
                        Save(entity, false);

                    _context.SaveChanges();

                    _context.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                    _context.Database.RollbackTransaction();
                    throw ex;
                }
            }
            else
            {
                foreach (TEntity entity in entities)
                    Save(entity, false);

                _context.SaveChanges();
            }
        }

        #endregion


        #region Metodi di GET

        /// <summary>
        /// Recupera l'entità avente ID uguale a quello ricevuto in input e se non viene trovata
        /// allora genera un'eccezione. Se vengono passati anche degli include allora recupera anche 
        /// le relazioni richieste.
        /// </summary>
        /// <param name="ID">ID del record da recuperare</param>
        /// <param name="includes">Relazioni da includere nella get</param>
        /// <returns>Restituisce il record recuperato dal DB</returns>
        public virtual TEntity Get(int ID, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity entity = _repository.Get(ID, includes);

            return entity;
        }

        /// <summary>
        /// Recupera tutte le entità presenti nella tabella (includendo le eventuali relazioni specificate)
        /// che rispettano il filtro passato.
        /// </summary>
        /// <param name="predicate">Predicato LINQ contenente la where part da applicare</param>
        /// <param name="includes">Relazioni da includere nella get</param>
        /// <returns>Restituisce la lista dei record recuperati dal DB.</returns>
        public virtual List<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            List<TEntity> filteredRecords = _repository.GetBy(predicate, includes).ToList();

            return filteredRecords;
        }

        /// <summary>
        /// Recupera tutte le entità presenti nella tabella (includendo le eventuali relazioni specificate).
        /// </summary>
        /// <param name="includes">Relazioni da includere nella get</param>
        /// <returns>Restituisce la lista dei record recuperati dal DB.</returns>
        public virtual List<TEntity> GetAll(Expression<Func<TEntity, object>>[] includes = null)
        {
            return _repository.GetAll(includes).ToList();
        }

        #endregion


        /// <summary>
        /// Metodo per eliminare il record ricevuto dal database. Esegue prima l'eliminazione
        /// dell'entità dal contesto tramite il metodo di delete del repository e poi, se il 
        /// parametro saveChanges è true, applica la modifica al DB.
        /// </summary>
        /// <param name="entity">Record da eliminare.</param>
        /// <param name="saveChanges">Se TRUE applica la modifica al DB altriemtni no.</param>
        public virtual void Delete(TEntity entity, bool saveChanges = true)
        {
            _repository.Delete(entity);

            if (saveChanges == true)
                _context.SaveChanges();
        }
    }
}
