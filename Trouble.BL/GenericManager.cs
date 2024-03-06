namespace Trouble.BL
{
    public class GenericManager<T> where T : class, IEntity
    {

        protected DbContextOptions<TroubleEntities> options;

        public GenericManager(DbContextOptions<TroubleEntities> options)
        {
            this.options = options;
        }

        public GenericManager() { }

        public List<T> Load()
        {
            try
            {
                return new TroubleEntities(options)
                    .Set<T>()
                    .ToList<T>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T LoadById(Guid id)
        {
            try
            {
                return new TroubleEntities(options).Set<T>().Where(t => t.Id == id).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Insert(T entity, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    IDbContextTransaction dbTransaction = null;
                    if (rollback) dbTransaction = dc.Database.BeginTransaction();

                    entity.Id = Guid.NewGuid();

                    dc.Set<T>().Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) dbTransaction.Rollback();
                }
                //Added this to return results
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Update(T entity, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    IDbContextTransaction dbTransaction = null;
                    if(rollback) dbTransaction = dc.Database.BeginTransaction();

                    dc.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    results = dc.SaveChanges();

                    if (rollback) dbTransaction.Rollback();
                }
                //Added this to return results
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Delete(Guid id,  bool rollback = false)
        {
            try
            {
                int results = 0;
                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    IDbContextTransaction dbTransaction = null;
                    if (rollback) dbTransaction = dc.Database.BeginTransaction();

                    T row = dc.Set<T>().FirstOrDefault(t => t.Id == id);

                    if(row != null)
                    {
                        dc.Set<T>().Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) dbTransaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }
                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
