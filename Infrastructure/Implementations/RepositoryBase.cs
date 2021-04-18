namespace Infrastructure.Implementations
{
    public class RepositoryBase
    {
        protected TitsDbContext Context;

        public RepositoryBase(TitsDbContext context)
        {
            Context = context;
        }
    }
}