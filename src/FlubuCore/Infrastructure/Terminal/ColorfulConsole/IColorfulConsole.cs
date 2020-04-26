﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlubuCore.Infrastructure.Terminal.ColorfulConsole
{
    public interface IColorfulConsole
    {
        void Flush();

        void Write(string message, ConsoleColor? background, ConsoleColor? foreground);

        void WriteLine(string message, ConsoleColor? background, ConsoleColor? foreground);
    }
}
