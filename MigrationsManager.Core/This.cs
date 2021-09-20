using System;
using System.Reflection;

namespace MigrationsManager.Core
{
    public static class This
    {
        public static Assembly Assembly => typeof(This).Assembly;
    }
}
