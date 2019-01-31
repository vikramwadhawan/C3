using Colligo.O365Product.Helper.Helper;
using Colligo.O365Product.RM.Admin.DAL;
using Colligo.O365Product.RM.Admin.Data;
using Colligo.O365Product.RM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Admin.BLL
{
    public static class UserManager
    {
        public static async Task<string> ValidateCredential(string userId, string tenantId, string productName)
        {
            try
            {
                string retMessage = string.Empty;
                IEnumerable<VwOrganizationUserLicense> organization = await ColligoO365RMOManager<VwOrganizationUserLicense>.FindAsync(p => p.OrganizationId == tenantId && p.IsOrganizationActive, false);
                if (organization != null && organization.Count() > 0)
                {
                    IEnumerable<VwOrganizationUserLicense> productUser = organization.Where(p => !string.IsNullOrEmpty(p.EmailAddress) && Convert.ToString(p.EmailAddress).ToLower() == userId.ToLower());
                    bool createUser = false;
                    bool userExists = false;
                    if (productUser == null || (productUser != null && productUser.Count() == 0))
                    {
                        createUser = true;
                    }
                    else if (productUser.Count() > 0)
                    {
                        userExists = true;
                        var user = productUser.FirstOrDefault(p => p.Name.ToLower() == productName.ToLower());
                        if (user == null)
                            createUser = true;
                        else
                        {

                            if ((user.IsUserActive.HasValue && !user.IsUserActive.Value) || (user.IsProductUserActive.HasValue && !user.IsProductUserActive.Value))
                            {
                                retMessage = "InActiveUser";
                            }
                            else if (user.IsUserActive.Value && (user.IsProductUserActive == null))
                            {
                                createUser = true;

                            }
                            else
                            {
                                retMessage = "LoggedIn";
                            }
                        }
                    }
                    if (createUser)
                    {
                        retMessage = await SaveUser(userId, organization.FirstOrDefault().OrganizationMasterId, tenantId, productName, userExists);
                    }
                }
                else
                    retMessage = "OrganizationNotRegistered";
                return retMessage;
            }
            catch { throw; }
        }


        private static async Task<string> SaveUser(string userId, int organizationId, string tenantID, string productName, bool userExists)
        {
            string status = string.Empty;
            ProductUser productUser = null;
            ProductLicense productLicense = null;
            Product product = await ColligoO365RMOManager<Product>.FirstOrDefaultAsync(x => x.Name == productName);
            SystemUser user = null;
            if (!userExists)
            {

                user = new SystemUser();
                user.EmailAddress = userId;
                user.FirstName = GetNameFromMail(userId);
                user.LastName = GetNameFromMail(userId, true);
                user.DisplayName = user.FirstName + " " + user.LastName;
                user.OrganizationMasterId = organizationId;
                user.IsActive = true;
                user.CreatedBy = 1;
                user.CreatedOn = System.DateTime.Now;
            }
            else
            {
                user = await ColligoO365RMOManager<SystemUser>.FirstOrDefaultAsync(x => x.EmailAddress == userId);
            }


            //Check and Update organization product licenes
            productLicense = await ColligoO365RMOManager<ProductLicense>.FirstOrDefaultAsync(x => x.OrganizationMasterId == organizationId && x.OrganizationMasterId == organizationId && x.ProductId == product.ProductId);
            //organization doesn't have license to access the product
            if (productLicense == null)
                return "NoProductLicense";
            if (productLicense.LicenseCount == productLicense.LicenseUsed)
            {
                status = "LicenseFinished";
                return status;
            }
            else
            {
                productLicense.LicenseUsed = productLicense.LicenseUsed + 1;
                //Assgin product to new user
                productUser = new ProductUser();
                productUser.OrganizationMasterId = organizationId;
                productUser.ProductId = product.ProductId;
                productUser.CreatedBy = 1;
                productUser.CreatedOn = System.DateTime.Now;
                productUser.IsActive = true;
                var message = new UserDataManager().SaveUser(user, productLicense, productUser);
                if (message == "success")
                {
                    status = "UserCreated";
                }
            }
            return status;
        }


        private static string GetNameFromMail(string email, bool lastName = false)
        {
            string name = "";
            var init = email.Substring(0, email.LastIndexOf('@'));
            if (!lastName)
                name = init.Split(new char[] { '.' }).Length > 0 ? init.Split(new char[] { '.' })[0].ToString() : init.ToString();
            else
                name = init.Split(new char[] { '.' }).Length > 1 ? init.Split(new char[] { '.' })[1].ToString() : init.ToString();
            return name;
        }

        public static async Task<UserModel> GetUserDetailByUserId(string userEmail)
        {
            try
            {
                VwOrganizationUser userDetail = await ColligoO365RMOManager<VwOrganizationUser>.FirstOrDefaultAsync(p => p.EmailAddress == userEmail && p.IsOrganizationActive);
                if (userDetail != null)
                {
                    Type source = typeof(VwOrganizationUser);
                    Type destination = typeof(UserModel);
                    UserModel obj = new UserModel();
                    CopyHelper.Copy(source, userDetail, destination, obj);
                    return obj;
                }
                return null;
            }
            catch { throw; }
        }
    }
}
