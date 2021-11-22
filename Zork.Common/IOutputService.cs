using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    public interface IOutputService
    {
        void Write(string value);

        void WriteLine(string value);
    }
}
