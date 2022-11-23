using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNETStudy.AOP.OOP2AOP
{
    public static class TransparentProxy
    {
        public static T Create<T>()
        {
            T instance = Activator.CreateInstance<T>();
            return instance;
        }
    }
}
