using System;

namespace MechMK1.ConsoleScreenbuffers
{
	/// <summary>
	/// Represents a single console buffer
	/// </summary>
	public class ConsoleBuffer
	{
		/// <summary>
		/// Internal handle for the created buffer
		/// </summary>
		internal IntPtr BufferHandle { get; private set; }
		/// <summary>
		/// Internal constructor. To create a new ConsoleBuffer, call either Create or GetCurrentConsoleScreenBuffer
		/// </summary>
		/// <param name="bufferHandle">The handle of the existing screen buffer</param>
		internal ConsoleBuffer(IntPtr bufferHandle)
		{
			//If a handle is either -1 or 0, it's invalid and will throw.
			if (bufferHandle.ToInt32() != Externals.InvalidHanldeValue && bufferHandle.ToInt32() != Externals.NullHandleValue)
			{
				this.BufferHandle = bufferHandle;
			}
			else
			{
				throw new Exception("Invalid handle");
			}
			
		}

		/// <summary>
		/// Sets the buffer as the currently active buffer
		/// </summary>
		/// <returns>Returns true on success, false on error.</returns>
		public bool SetAsActiveBuffer()
		{
			return Externals.SetConsoleActiveScreenBuffer(this.BufferHandle);
		}

		/// <summary>
		/// Creates a new console buffer.
		/// </summary>
		/// <returns>Returns a new ConsoleBuffer on success, throws on error.</returns>
		public static ConsoleBuffer Create()
		{
			IntPtr buffer = Externals.CreateConsoleScreenBuffer(AccessRights.Read | AccessRights.Write, ShareMode.Read | ShareMode.Write, IntPtr.Zero, 1, 0);
			return new ConsoleBuffer(buffer);
		}

		/// <summary>
		/// Gets the current console buffer.
		/// </summary>
		/// <returns>Returns a new ConsoleBuffer on success, throws on error.</returns>
		public static ConsoleBuffer GetCurrentConsoleScreenBuffer()
		{
			IntPtr buffer = Externals.GetStdHandle(StandardDevices.StandardOutput);
			return new ConsoleBuffer(buffer);
		}

		/// <summary>
		/// Write data to the console buffer.
		/// </summary>
		/// <param name="data">A ConsoleCharacter array containing the data. Should be as big as the data to be written.</param>
		/// <param name="x">X coordinate on the output buffer</param>
		/// <param name="y">Y coordinate on the output buffer</param>
		/// <param name="width">Width of the data</param>
		/// <param name="height">Height of the data</param>
		/// <returns>Returns true on success, false on error.</returns>
		public bool Write(ConsoleCharacter[] data, short x, short y, short width, short height)
		{
			SmallRect rect = new SmallRect() { Left = x, Top = y, Right = (short)(x+width), Bottom = (short)(y+height) };
			return Externals.WriteConsoleOutput(this.BufferHandle, data, new Coord() { X = width, Y = height }, new Coord() { X = 0, Y = 0 }, ref rect);
		}
	}
}
