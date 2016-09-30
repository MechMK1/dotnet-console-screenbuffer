using System;

namespace MechMK1.ConsoleScreenbuffers
{
	/// <summary>
	/// Represents a single console buffer
	/// </summary>
	public class ConsoleBuffer : IDisposable
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
			if (bufferHandle.ToInt32() != NativeMethods.InvalidHanldeValue && bufferHandle.ToInt32() != NativeMethods.NullHandleValue)
			{
				this.BufferHandle = bufferHandle;
			}
			else
			{
				throw new ArgumentException("Invalid handle", "bufferHandle");
			}
			
		}

		/// <summary>
		/// Sets the buffer as the currently active buffer
		/// </summary>
		/// <returns>Returns true on success, false on error.</returns>
		public bool SetAsActiveBuffer()
		{
			return NativeMethods.SetConsoleActiveScreenBuffer(this.BufferHandle);
		}

		/// <summary>
		/// Creates a new console buffer.
		/// </summary>
		/// <returns>Returns a new ConsoleBuffer on success, throws on error.</returns>
		public static ConsoleBuffer Create()
		{
			IntPtr buffer;
			try
			{
				buffer = NativeMethods.CreateConsoleScreenBuffer(AccessRights.Read | AccessRights.Write, ShareModes.Read | ShareModes.Write, IntPtr.Zero, 1, 0);
				return new ConsoleBuffer(buffer);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidOperationException("Creation of the Console Buffer was unsuccessful.", ex);
			}
		}

		/// <summary>
		/// Gets the current console buffer.
		/// </summary>
		/// <returns>Returns a new ConsoleBuffer on success, throws on error.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")] //Is not a property, so this message is suppressed
		public static ConsoleBuffer GetCurrentConsoleScreenBuffer()
		{
			IntPtr buffer;
			try
			{
				buffer = NativeMethods.GetStdHandle(StandardDevice.StandardOutput);
				return new ConsoleBuffer(buffer);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidOperationException("Getting the current buffer was unsuccessful", ex);
			}
		}

		/// <summary>
		/// Draw data to the console buffer.
		/// </summary>
		/// <param name="data">A ConsoleCharacter array containing the data. Should be as big as the data to be written.</param>
		/// <param name="x">X coordinate on the output buffer</param>
		/// <param name="y">Y coordinate on the output buffer</param>
		/// <param name="width">Width of the data</param>
		/// <param name="height">Height of the data</param>
		/// <returns>Returns true on success, false on error.</returns>
		public bool Draw(ConsoleCharacter[] data, short x, short y, short width, short height)
		{
			SmallRect rect = new SmallRect() { Left = x, Top = y, Right = (short)(x+width), Bottom = (short)(y+height) };
			return NativeMethods.WriteConsoleOutput(this.BufferHandle, data, new Coord() { X = width, Y = height }, new Coord() { X = 0, Y = 0 }, ref rect);
		}

		/// <summary>
		/// Draws text to the console buffer.
		/// </summary>
		/// <param name="text">The text to be drawn</param>
		/// <param name="x">X coordinate on the output buffer</param>
		/// <param name="y">Y coordinate on the output buffer</param>
		/// <param name="isAscii">Whether or not the output should be treaten as ASCII</param>
		/// <param name="attributes">Attributes of the text</param>
		/// <returns>Returns true on success, false on error.</returns>
		public bool DrawString(
			string text,
			short x,
			short y,
			bool isAscii = false,
			CharacterAttributes attributes = CharacterAttributes.ForegroundWhite)
		{
			if (text == null) return false;
			if (text.Length > short.MaxValue) throw new ArgumentOutOfRangeException("text", "Text must not be longer than short.MaxValue");
			return DrawString(text, x, y, (short)text.Length, 1, isAscii, attributes);
		}

		/// <summary>
		/// Draws text to the console buffer.
		/// </summary>
		/// <param name="text">The text to be drawn</param>
		/// <param name="x">X coordinate on the output buffer</param>
		/// <param name="y">Y coordinate on the output buffer</param>
		/// <param name="width">Width of the text</param>
		/// <param name="height">Height of the text</param>
		/// <param name="isAscii">Whether or not the output should be treaten as ASCII</param>
		/// <param name="attributes">Attributes of the text</param>
		/// <returns>Returns true on success, false on error.</returns>
		public bool DrawString(
			string text,
			short x,
			short y,
			short width,
			short height,
			bool isAscii = false,
			CharacterAttributes attributes = CharacterAttributes.ForegroundWhite)
		{
			if (text == null) return false;
			if (text.Length > short.MaxValue) throw new ArgumentOutOfRangeException("text", "Text must not be longer than short.MaxValue");

			ConsoleCharacter[] data = new ConsoleCharacter[text.Length];
			for (int i = 0; i < data.Length; i++)
			{
				if (isAscii) data[i].Char.AsciiChar = (byte)text[i];
				else data[i].Char.UnicodeChar = text[i];
				data[i].Attributes = attributes;
			}
			return Draw(data, x, y, width, height);
		}

		public void Dispose()
		{
			NativeMethods.CloseHandle(this.BufferHandle);
		}
	}
}
