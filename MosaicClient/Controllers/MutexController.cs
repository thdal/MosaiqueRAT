using System.Threading;

namespace Client.Controllers
{
    class MutexController
    {
        private static Mutex _appMutex;

        public static string mutexKey;

        public static bool createMutex()
        {
            bool createdNew;
            _appMutex = new Mutex(false, mutexKey, out createdNew);
            return createdNew;
        }

        public static void closeMutex()
        {
            if (_appMutex != null)
            {
                _appMutex.Close();
                _appMutex = null;
            }
        }
    }
}
