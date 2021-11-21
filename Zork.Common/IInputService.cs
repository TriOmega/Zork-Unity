using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    public interface IInputService
    {
        string ReadLine();
        //event EventHandler<string> InputReceived;
    }
}
