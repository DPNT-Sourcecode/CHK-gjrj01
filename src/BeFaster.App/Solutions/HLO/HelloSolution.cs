using BeFaster.Runner.Exceptions;
using System;

namespace BeFaster.App.Solutions.HLO
{
    public static class HelloSolution
    {
        public static string Hello(string friendName)
        {
            if (string.IsNullOrEmpty(friendName))
            {
                throw new ArgumentException("Invalid parameter", nameof(friendName));
            }

            return $"Hello, {friendName}!";
        }
    }
}