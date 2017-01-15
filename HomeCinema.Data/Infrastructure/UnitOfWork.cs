namespace HomeCinema.Data.Infrastructure {
    public class UnitOfWork : IUnitOfWork {

        private readonly IDbFactory dbFactory;
        private HomeCinemaContext dbContext;

        public UnitOfWork(IDbFactory dbFactory) {
            this.dbFactory = dbFactory;
        }

        public HomeCinemaContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }
        public void Commit() {
            DbContext.Commit();
        }
    }
}
