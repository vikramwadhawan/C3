using Colligo.O365Product.RM.Admin.Data;
using Colligo.O365Product.RM.Admin.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Colligo.O365Product.RM.Admin.DAL
{
    public class UserDataManager : DataManager<UserDataManager>
    {
        public string SaveUser(SystemUser newUser, ProductLicense productLicense, ProductUser productUser)
        {
            try
            {
                if (newUser != null)
                {
                    int systemUserId = 0;
                    using (var context = new ColligoO365RMOEntities())
                    {
                        context.Database.Connection.Open();

                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            if (newUser.SystemUserId != 0)
                            {
                                systemUserId = newUser.SystemUserId;
                            }
                            else
                            {
                                using (DataRepository<SystemUser> objUserRepository = new DataRepository<SystemUser>(context, false))
                                {
                                    objUserRepository.Save(newUser, null, null, true);
                                }
                            }
                            if (productUser != null)
                            {
                                productUser.SystemUserId = newUser.SystemUserId;
                                using (DataRepository<ProductUser> objProductUserRepository = new DataRepository<ProductUser>(context, false))
                                {
                                    if (productUser.ProductUserId == 0)
                                    {
                                        objProductUserRepository.Save(productUser, null, commitChanges: true);
                                    }
                                    else
                                    {
                                        objProductUserRepository.Save(null, productUser, commitChanges: true);
                                    }
                                }
                            }

                            if (productLicense != null)
                            {
                                using (DataRepository<ProductLicense> objProductLicenseRepository = new DataRepository<ProductLicense>(context, false))
                                {
                                    if (productUser.ProductUserId == 0)
                                    {
                                        objProductLicenseRepository.Save(productLicense, null, commitChanges: true);
                                    }
                                    else
                                    {
                                        objProductLicenseRepository.Save(null, productLicense, commitChanges: true);
                                    }
                                }
                            }
                            scope.Complete();
                        }
                        if (context.Database.Connection.State == System.Data.ConnectionState.Open)
                        {
                            context.Database.Connection.Close();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return "success";
        }
    }
}
