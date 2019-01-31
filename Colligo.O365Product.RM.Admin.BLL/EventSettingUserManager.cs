using Colligo.O365Product.Helper.Constant;
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
    public class EventSettingUserManager
    {
        public static async Task<string> SaveEventSettingUser(EventSettingUserModel eventSettingUserModel)
        {
            // Sp User Update or Create

            OrganizationUser user = null;
            string status = "fail";
            string spStatus = "success";
            string adgStatus = "success";


            var organization = await ColligoO365RMOManager<VwEventSettingUserSetup>.FirstOrDefaultAsync(p => p.OrganizationRootUrl == eventSettingUserModel.SiteUrl);
            if (organization != null)
            {
                var spUser = await ColligoO365RMOManager<OrganizationUser>.FirstOrDefaultAsync(p => p.OrganizationMasterId == organization.OrganizationMasterId && p.UserAccessTo == GlobalConstants.SPAccessToType);
                int spUserUpdateCount;

                if (spUser != null)
                {
                    spUser.UserId = eventSettingUserModel.SpUserId;
                    spUser.Password = EncryptionHelper.Encrypt(eventSettingUserModel.SpUserPassword, GlobalConstants.EncryptionKey);
                    spUser.ModifiedBy = organization.OrganizationUserId;
                    spUser.ModifiedOn = System.DateTime.Now;
                    spUserUpdateCount = await ColligoO365RMOManager<OrganizationUser>.SaveAsync(null, spUser);
                }

                else
                {
                    user = new OrganizationUser();
                    user.UserId = eventSettingUserModel.SpUserId;
                    user.OrganizationMasterId = organization.OrganizationMasterId;
                    user.Password = EncryptionHelper.Encrypt(eventSettingUserModel.SpUserPassword, GlobalConstants.EncryptionKey);
                    user.CreatedOn = System.DateTime.Now;
                    user.CreatedBy = organization.OrganizationUserId;
                    user.UserAccessTo = GlobalConstants.ADGccessToType;
                    spUserUpdateCount = await ColligoO365RMOManager<OrganizationUser>.SaveAsync(user);
                }

                if (spUserUpdateCount == 0)
                {
                    spStatus = "fail";
                }



                var adgUser = await ColligoO365RMOManager<OrganizationUser>.FirstOrDefaultAsync(p => p.OrganizationMasterId == organization.OrganizationMasterId && p.UserAccessTo == GlobalConstants.ADGccessToType);
                int adgUserUpdateCount;

                if (adgUser != null)
                {
                    adgUser.OrganizationMasterId = organization.OrganizationMasterId;
                    adgUser.UserId = eventSettingUserModel.AdgUserId;
                    adgUser.Password = EncryptionHelper.Encrypt(eventSettingUserModel.AdgUserPassword, GlobalConstants.EncryptionKey); ;
                    adgUser.ModifiedBy = organization.OrganizationUserId;
                    adgUser.ModifiedOn = System.DateTime.Now;
                    adgUserUpdateCount = await ColligoO365RMOManager<OrganizationUser>.SaveAsync(null, adgUser);
                }

                else
                {
                    user = new OrganizationUser();
                    user.UserId = eventSettingUserModel.AdgUserId;
                    user.Password = EncryptionHelper.Encrypt(eventSettingUserModel.AdgUserPassword, GlobalConstants.EncryptionKey);
                    user.CreatedOn = System.DateTime.Now;
                    user.CreatedBy = organization.OrganizationUserId;
                    user.UserAccessTo = GlobalConstants.ADGccessToType;
                    adgUserUpdateCount = await ColligoO365RMOManager<OrganizationUser>.SaveAsync(user);
                }
                if (adgUserUpdateCount == 0)
                {
                    adgStatus = "fail";
                }
                // Saving Agent Exexution Time Below
                if (eventSettingUserModel.agentTimeZoneModel != null)
                {
                    int rowsUpdated;

                    var organizationAgentTime = await ColligoO365RMOManager<AgentExecutionTime>.FirstOrDefaultAsync(p => p.OrganizationMasterId == organization.OrganizationMasterId);

                    if (organizationAgentTime != null)
                    {
                        organizationAgentTime.ModifiedOn = System.DateTime.Now;
                        organizationAgentTime.ModifiedBy = organization.OrganizationMasterId;
                        organizationAgentTime.Time = eventSettingUserModel.agentTimeZoneModel.Time;
                        organizationAgentTime.TimeZone = eventSettingUserModel.agentTimeZoneModel.TimeZone;
                        organizationAgentTime.TimeZoneDisplayName = eventSettingUserModel.agentTimeZoneModel.TimeZoneDisplayName;
                        rowsUpdated = await ColligoO365RMOManager<AgentExecutionTime>.SaveAsync(null, organizationAgentTime);
                    }

                    else
                    {
                        organizationAgentTime = new AgentExecutionTime();
                        organizationAgentTime.OrganizationMasterId = organization.OrganizationMasterId;
                        organizationAgentTime.Time = eventSettingUserModel.agentTimeZoneModel.Time;
                        organizationAgentTime.TimeZone = eventSettingUserModel.agentTimeZoneModel.TimeZone;
                        organizationAgentTime.TimeZoneDisplayName = eventSettingUserModel.agentTimeZoneModel.TimeZoneDisplayName;
                        organizationAgentTime.CreatedOn = System.DateTime.Now;
                        organizationAgentTime.CreatedBy = organization.OrganizationUserId;
                        rowsUpdated = await ColligoO365RMOManager<AgentExecutionTime>.SaveAsync(organizationAgentTime);
                    }
                    if (rowsUpdated==0)
                    {
                        status = "fail";
                    }
                }

            }
            if (spStatus != "success" || adgStatus != "success")
            {
                status = "fail";
            }
            else
            {
                status = "success";

            }




            return status;
        }


        public static async Task<EventSettingUserModel> GetEventSettingUser(string siteUrl)
        {

            EventSettingUserModel user = new EventSettingUserModel();

            var organization = await ColligoO365RMOManager<VwEventSettingUserSetup>.FirstOrDefaultAsync(p => p.OrganizationRootUrl == siteUrl);

            if (organization != null)
            {
                var spOrganizationUser = await ColligoO365RMOManager<OrganizationUser>.FirstOrDefaultAsync(p => p.OrganizationMasterId == organization.OrganizationMasterId && p.UserAccessTo == GlobalConstants.SPAccessToType);
                if (spOrganizationUser != null)
                {
                    user.SpUserId = spOrganizationUser.UserId;
                    user.SpUserPassword = EncryptionHelper.Decrypt(spOrganizationUser.Password, GlobalConstants.EncryptionKey); ;

                }

                var adgOrganizationUser = await ColligoO365RMOManager<OrganizationUser>.FirstOrDefaultAsync(p => p.OrganizationMasterId == organization.OrganizationMasterId && p.UserAccessTo == GlobalConstants.ADGccessToType);
                if (adgOrganizationUser != null)
                {
                    user.AdgUserId = adgOrganizationUser.UserId;
                    user.AdgUserPassword = EncryptionHelper.Decrypt(adgOrganizationUser.Password, GlobalConstants.EncryptionKey);

                }
                // Getting Agent Execution Time below

                var organizationAgentExecutionTime = await ColligoO365RMOManager<AgentExecutionTime>.FirstOrDefaultAsync(p => p.OrganizationMasterId == organization.OrganizationMasterId);
                if (organization != null && organizationAgentExecutionTime != null)
                {

                    AgentExecutionTimeModel timeZoneModel = new AgentExecutionTimeModel();

                    timeZoneModel.Time = organizationAgentExecutionTime.Time;
                    timeZoneModel.TimeZone = organizationAgentExecutionTime.TimeZone;
                    timeZoneModel.TimeZoneDisplayName = organizationAgentExecutionTime.TimeZoneDisplayName;

                    user.agentTimeZoneModel = timeZoneModel;
                }


            }
            return user;
        }




    }

}