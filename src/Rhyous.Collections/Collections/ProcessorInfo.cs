﻿using System;

namespace Rhyous.Collections
{
    /// <summary>Used by ConcurrentHashSet.</summary>
    internal static class ProcessorInfo
    {
        private const int ProcessorCountRefreshIntervalMs = 30000;

        private static volatile int _processorCount;
        private static volatile int _lastProcessorCountRefreshTicks;

        internal static int ProcessorCount
        {
            get
            {
                var now = Environment.TickCount;
                if (_processorCount == 0 || now - _lastProcessorCountRefreshTicks >= ProcessorCountRefreshIntervalMs)
                {
                    _processorCount = Environment.ProcessorCount;
                    _lastProcessorCountRefreshTicks = now;
                }

                return _processorCount;
            }
        }
    }
}