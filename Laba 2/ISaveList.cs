using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public interface ISaveList<T>
    {

        T Load(string path);
        void Save(T data, string path);
    }
}
