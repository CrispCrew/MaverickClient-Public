using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBot.Functions
{
    public class Counters
    {
        public static float GetCPU()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            float cpu = cpuCounter.NextValue();
            Thread.Sleep(500);
            cpu = cpuCounter.NextValue();

            return cpu;
        }

        public static double GetMem()
        {
            ulong Available = new ComputerInfo().AvailablePhysicalMemory;
            ulong Total = new ComputerInfo().TotalPhysicalMemory;

            return ConvertMegabytesToGigabytes(ConvertBytesToMegabytes(((long)Total - (long)Available)));
        }

        public static float GetProcessCPU()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", "MaverickServer", true);
            cpuCounter.NextValue();

            Thread.Sleep(500);

            return cpuCounter.NextValue();
        }

        public static long GetProcessMem()
        {
            return ConvertBytesToMegabytes(Process.GetCurrentProcess().PrivateMemorySize64);
        }

        private static long ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024) / 1024;
        }

        private static double ConvertMegabytesToGigabytes(double megabytes) 
        {
            return (megabytes / 1024.0f);
        }
    }
}
