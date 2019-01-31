using System;

namespace Colligo.O365Product.RM.Admin.Framework.Data
{
    //Create single object of data manager for all repository
    public class DataManager<T> where T : class
    {
        private static T repository = default(T);
        public static T GetInstance()
        {
            if (repository == null)
            {
                repository = Activator.CreateInstance<T>();
            }
            return repository;
        }
    }
}
