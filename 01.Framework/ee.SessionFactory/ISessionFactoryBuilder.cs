using NHibernate;

namespace ee.SessionFactory
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory CreateSessionFactory();
        void Build(string para);
    }
}
