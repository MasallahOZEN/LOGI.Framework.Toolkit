using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Reflection
{
    public static class SingletonBase<T> where T : new()
    {
        static SingletonBase()
        {
        }
        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                // Thread safe, lazy implementation of the singleton pattern.
                // See http://www.yoda.arachsys.com/csharp/singleton.html
                // for the full story.
                return SingletonInternal.instance;
            }
        }

        private class SingletonInternal
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit.
            static SingletonInternal()
            {
            }

            internal static readonly T instance = new T();
        }


    }
}
