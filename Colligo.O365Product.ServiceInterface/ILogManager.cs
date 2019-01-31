using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.ServiceInterface
{
    public interface ILogManager
    {
        void Error(string message);
        void Error(List<string> exception);
        void Error(Exception ex);
        void Error(string message, List<string> exception, Exception ex = null);
        void Error(string message, Exception ex);
        void Info(string message);
        void Info(string format, params object[] args);
        void Debug(string message);
        void Debug(string message, Exception ex);
        void Debug(string format, params object[] args);
        void Warn(string message);
    }
}
