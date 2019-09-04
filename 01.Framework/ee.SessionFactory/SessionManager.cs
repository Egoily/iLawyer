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
                {
                    throw new Exception("Session Factory Builder is null.");
                }

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
                System.Diagnostics.Debug.WriteLine($"OpenSession({session.GetHashCode()})");
            }

            return session;
        }
        public static void DisposeConnection()
        {
            CloseConnection();
            session.Dispose();
            session = null;
        }

        public static void CloseConnection()
        {
            if (session != null && session.IsOpen || session.IsConnected)
            {
                System.Diagnostics.Debug.WriteLine($"CloseSession({session.GetHashCode()})");
                session.Close();
            }

        }
    }
}
