using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Nlog
{
    public interface ILog
    {
        void Debug(string message);
        void Error(string message);
        void Informatiom(string message);
        void Warning(string message);
    }
}
