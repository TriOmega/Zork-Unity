using System;

namespace Zork
{
    class ConsoleInputService : IInputService
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
