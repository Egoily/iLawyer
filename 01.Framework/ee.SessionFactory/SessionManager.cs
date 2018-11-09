using NHibernate;
using System;

namespace ee.SessionFactory
{
    public static class SessionManager
    {

        public static ISessionFactoryBuilder Builder;

        private static object Locker = new object();
        private static ISessionFactory factory = null;
        private static ISessionFactory Factory
        {
            get
            {
                if (Builder == null)
                    throw new Exception("Session Factory Builder is null.");
                lock (Locker)
                {
                    return (factory ?? (factory = Builder.CreateSessionFactory()));
                }
            }

        }

        [ThreadStatic]
        private static ISession session;
        public static ISession GetConnection()
        {
            if (session == null || !session.IsOpen)
            {
                session = Factory.OpenSession();
            }
            return session;
        }
        public static void CloseConnection()
        {
            session?.Close();
            session = null;
        }
    }
}
