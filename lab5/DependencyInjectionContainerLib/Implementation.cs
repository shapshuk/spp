using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainerLib
{
    public class Implementation
    {
        public enum LiveTimes { InstPerDep, Singleton}
        public Type TImplementation { get; }

        public LiveTimes LiveTime { get; }

        public Implementation(Type implementation, LiveTimes liveTime)
        {
            TImplementation = implementation;
            LiveTime = liveTime;
        }

    }
}
