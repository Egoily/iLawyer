using NHibernate;

namespace ee.SessionFactory
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory CreateSessionFactory();
        void Build(params string[] args);
    }
}
