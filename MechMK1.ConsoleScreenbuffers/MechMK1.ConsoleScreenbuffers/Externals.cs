using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MechMK1.ConsoleScreenbuffers
{
    public class Externals
    {
		public const int InvalidHanldeValue = -1;
		public const int NullHandleValue = 0;

		#region extern
		/// <summary>
		/// Retrieves a handle to the specified standard device (standard input, standard output, or standard error).
		/// </summary>
		/// <param name="handle">The standard device. See StandardDevices</param>
		/// <returns>If the function succeeds, the return value is a handle to the specified device, or a redirected handle set by a previous call to SetStdHandle. The handle has GENERIC_READ and GENERIC_WRITE access rights, unless the application has used SetStdHandle to set a standard handle with lesser access. If the function fails, the return value is INVALID_HANDLE_VALUE. To get extended error information, call GetLastError. If an application does not have associated standard handles, such as a service running on an interactive desktop, and has not redirected them, the return value is NULL.</returns>
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetStdHandle(StandardDevices handle);
		
		/// <summary>
		/// Creates a console screen buffer.
		/// </summary>
		/// <param name="dwDesiredAccess">The access to the console screen buffer. See AccessRights.</param>
		/// <param name="dwShareMode">This parameter can be zero, indicating that the buffer cannot be shared, or it can be one or more of the following values. See ShareMode.</param>
		/// <param name="secutiryAttributes">Security attributes. Null by default</param>
		/// <param name="flags">Reserved: Set to 1</param>
		/// <param name="screenBufferData">Reserved: Set to null</param>
		/// <returns>If the function succeeds, the return value is a handle to the new console screen buffer. If the function fails, the return value is InvalidHanldeValue (-1).</returns>
		[DllImport("Kernel32.dll", SetLastError = true)]
		internal static extern IntPtr CreateConsoleScreenBuffer(
			AccessRights dwDesiredAccess,
			ShareMode dwShareMode,
			IntPtr secutiryAttributes,
			UInt32 flags,
			[MarshalAs(UnmanagedType.U4)] UInt32 screenBufferData
		);
		
		/// <summary>
		/// Sets the specified screen buffer to be the currently displayed console screen buffer.
		/// </summary>
		/// <param name="buffer">A handle to the console screen buffer.</param>
		/// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false. To get extended error information, call GetLastError.</returns>
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleActiveScreenBuffer(IntPtr buffer);
		
		/// <summary>
		/// Writes character and color attribute data to a specified rectangular block of character cells in a console screen buffer. The data to be written is taken from a correspondingly sized rectangular block at a specified location in the source buffer.
		/// </summary>
		/// <param name="consoleBufferHandle">Handle of the buffer to be written to</param>
		/// <param name="data">ConsoleCharacter array of size width*length</param>
		/// <param name="dataSize">Size of the ConsoleCharacter array</param>
		/// <param name="dataStart">Start point of the ConsoleCharacter array</param>
		/// <param name="writeRegion">Coordinates on the output screen buffer</param>
		/// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false. To get extended error information, call GetLastError.</returns>
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool WriteConsoleOutput(
		  IntPtr consoleBufferHandle,
		  ConsoleCharacter[] data,
		  Coord dataSize,
		  Coord dataStart,
		  ref SmallRect writeRegion
		);
		#endregion extern
    }
}
