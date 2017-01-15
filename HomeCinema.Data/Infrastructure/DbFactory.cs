namespace HomeCinema.Data.Infrastructure {
    public class DbFactory : Disposable, IDbFactory {
        HomeCinemaContext dbContext;
        public HomeCinemaContext Init() {
            return dbContext ?? (dbContext = new HomeCinemaContext());
        }
        protected override void DisposeCore() {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
