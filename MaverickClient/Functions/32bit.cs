using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaverickClient.Functions
{
    public class _32bit
    {
        public static uint PROCESS_ALL_ACCESS = (uint)(0x000F0000L | 0x00100000L | 0xFFF);

        public static uint CREATE_THREAD = 0x0002;
        public static uint PROCESS_VM_OPERATION = 0x0008;
        public static uint READ_MEMORY = 0x0010;
        public static uint WRITE_MEMORY = 0x0020;
        public static uint QUERY_PROCESS = 0x0400;

        public static uint LIST_MODULES_ALL = 0x03;

        #region Natives
        [DllImport("kernel32")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("psapi.dll")]
        public static extern bool EnumProcessModulesEx(IntPtr hProcess, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] [In][Out] IntPtr[] lphModule, int cb, [MarshalAs(UnmanagedType.U4)] out int lpcbNeeded, uint dwFilterFlag);

        [DllImport("psapi.dll")]
        public static extern uint GetModuleBaseName(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In] [MarshalAs(UnmanagedType.U4)] int nSize);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);
        #endregion

        private static readonly IntPtr INTPTR_ZERO = (IntPtr)0;

        public static bool ModuleExists(uint ProcessID, string Name)
        {
            Process process = Process.GetProcessById((int)ProcessID);

            IntPtr hndProc = OpenProcess(PROCESS_ALL_ACCESS, false, ProcessID);

            if (hndProc == INTPTR_ZERO)
            {
                return false;
            }

            if (IsWin64Emulator(process))
            {
                IntPtr[] stuff = new IntPtr[0];
                int size = 0;
                EnumProcessModulesEx(hndProc, stuff, 0, out size, LIST_MODULES_ALL);

                stuff = new IntPtr[size / IntPtr.Size];
                EnumProcessModulesEx(hndProc, stuff, stuff.Length * IntPtr.Size, out size, LIST_MODULES_ALL);

                for (int i = 0; i < stuff.Length; i++)
                {
                    StringBuilder sb = new StringBuilder(256);
                    if (GetModuleBaseName(hndProc, stuff[i], sb, sb.Capacity) != 0)
                        if (sb.ToString().ToLower() == Name.ToLower())
                            return true;
                }
            }
            else
            {
                foreach (ProcessModule module in process.Modules)
                    if (module.ModuleName.ToLower() == Name.ToLower())
                        return true;
            }

            return false;
        }

        public static bool ModuleExists(Process process, string Name)
        {
            IntPtr hndProc = OpenProcess(PROCESS_ALL_ACCESS, false, (uint)process.Id);

            if (hndProc == INTPTR_ZERO)
            {
                return false;
            }

            if (IsWin64Emulator(process))
            {
                IntPtr[] stuff = new IntPtr[0];
                int size = 0;
                EnumProcessModulesEx(hndProc, stuff, 0, out size, LIST_MODULES_ALL);

                stuff = new IntPtr[size / IntPtr.Size];
                EnumProcessModulesEx(hndProc, stuff, stuff.Length * IntPtr.Size, out size, LIST_MODULES_ALL);

                for (int i = 0; i < stuff.Length; i++)
                {
                    StringBuilder sb = new StringBuilder(256);
                    if (GetModuleBaseName(hndProc, stuff[i], sb, sb.Capacity) != 0)
                        if (sb.ToString().ToLower() == Name.ToLower())
                            return true;
                }
            }
            else
            {
                foreach (ProcessModule module in process.Modules)
                    if (module.ModuleName.ToLower() == Name.ToLower())
                        return true;
            }

            return false;
        }

        private static bool IsWin64Emulator(Process process)
        {
            if ((Environment.OSVersion.Version.Major > 5)
                || ((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor >= 1)))
            {
                bool retVal;

                return IsWow64Process(process.Handle, out retVal) && retVal;
            }

            return false;
        }
    }
}
