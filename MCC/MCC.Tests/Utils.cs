using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCC.Tests
{
    public static class Utils
    {
        public static bool Equals( int val, int desired )
        {
            return val == desired;
        }
        public static bool Equals( bool val, bool desired )
        {
            return val == desired;
        }
        public static bool Equals( string val, string desired )
        {
            return val == desired;
        }
        public static bool SequenceEquals<T>( IEnumerable<T> val, IEnumerable<T> desired )
        {
            return val.SequenceEqual( desired );
        }

        public static void AssertTrueAndLog<T>( string logName, T val, T desired, Func<T, T, bool> comparison )
        {
            bool result = comparison( val, desired );
            Console.WriteLine( $"{logName}: {val} ({result})" );
            Assert.IsTrue( result );
        }
    }
}
