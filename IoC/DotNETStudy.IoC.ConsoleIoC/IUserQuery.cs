using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNETStudy.IoC.ConsoleIoC
{
    internal interface IUserQuery
    {
        object GetUserInfo(int id);
    }
}
