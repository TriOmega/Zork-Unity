using System;
using System.Collections.Generic;
using System.Text;

namespace Zork.Common
{
    interface IOutputServices
    {
        void Write(object value);

        void WriteLine(object value);
    }
}
