using NHibernate;

namespace ee.SessionFactory
{
    public abstract class SessionFactory
    {
        private static object Locker = new object();
        private static ISessionFactory factory = null;
        protected ISessionFactory Factory
        {
            get
            {
                lock (Locker)
                {
                    return (factory ?? (factory = CreateSessionFactory()));
                }
            }

        }

        public abstract ISessionFactory CreateSessionFactory();
    }
}
