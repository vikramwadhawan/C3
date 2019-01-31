using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.ScriptRunner
{
    public class PowerShellHelper
    {
        private Runspace runspace = null;
        private PowerShell powershell = null;
        private Collection<PSObject> sessionResult = null;
        private PSCredential credential = null;
        private PSCommand command = null;
        private Uri connectionUri;

        public PowerShellHelper()
        {
            runspace = RunspaceFactory.CreateRunspace();
            powershell = PowerShell.Create();
        }

        public void OpenSpace()
        {
            if (runspace != null)
                runspace.Open();
        }

        public void CloseSpace()
        {
            if (runspace != null)
                runspace.Close();
        }

        public Collection<PSObject> ExecuteScriptBlock(string script)
        {
            command = new PSCommand();
            command.AddCommand("Invoke-Command");
            command.AddParameter("ScriptBlock", ScriptBlock.Create(script));
            command.AddParameter("Session", sessionResult[0]);
            powershell.Commands = command;
            powershell.Runspace = runspace;
            return powershell.Invoke();
        }

        public Collection<PSObject> ExecuteCommand(string scriptCommand, Dictionary<string, object> parameter = null, bool useCurrentSession = true)
        {
            command = new PSCommand();
            command.AddCommand(scriptCommand);
            if (parameter != null && parameter.Count > 0)
            {
                foreach (var item in parameter)
                {
                    command.AddParameter(item.Key, item.Value);
                }
            }
            if (useCurrentSession)
                command.AddParameter("Session", sessionResult[0]);
            powershell.Commands = command;
            powershell.Runspace = runspace;
            return powershell.Invoke();
        }

        public bool StartNewSession(string url, string userId, string password, string configuartionName = "Microsoft.Exchange", string authenticationType = "Basic", bool allowRedirection = true)
        {
            bool connect = false;
            try
            {
                connectionUri = new Uri(url);
                SetCredential(userId, password);
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ConfigurationName", configuartionName);
                param.Add("ConnectionUri", connectionUri);
                param.Add("Credential", credential);
                param.Add("Authentication", authenticationType);
                param.Add("AllowRedirection", allowRedirection);
                sessionResult = ExecuteCommand("New-PSSession", param, false);
                connect = (powershell.Streams.Error.Count == 0) && sessionResult.Count > 0;
            }
            catch
            {
                connect = false;
                throw;
            }
            return connect;
        }

        public bool EndSession()
        {
            bool connect = false;
            try
            {
                sessionResult = ExecuteCommand("Remove-PSSession");
                connect = sessionResult.Count != 1;
            }
            catch
            {
                connect = false;
                throw;
            }
            return connect;
        }

        public string GetError()
        {
            string error = string.Empty;
            if (powershell != null)
            {
                if (powershell.Streams.Error.Count > 0)
                    error = powershell.Streams.Error.LastOrDefault()?.Exception.ToString() ?? "unknown";
            }
            return error;
        }

        public int GetCount()
        {
            int error = 0;
            if (powershell != null)
            {
                if (powershell.Streams.Error != null)
                    error = powershell.Streams.Error.Count;
            }
            return error;
        }


        private void SetCredential(string userId, string password)
        {
            SecureString secpassword = new SecureString();
            foreach (char c in password)
            {
                secpassword.AppendChar(c);
            }
            credential = new PSCredential(userId, secpassword);
        }

    }
}
