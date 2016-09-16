using System;
using System.Runtime.InteropServices;

namespace MechMK1.ConsoleScreenbuffers
{
	/// <summary>
	/// Represents a single character in an output buffer
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct ConsoleCharacter
	{
		/// <summary>
		/// The character data. See CharacterUnion for details
		/// </summary>
		[FieldOffset(0)]
		public CharacterUnion Char;
		/// <summary>
		/// The attributes. See CharacterAttribute for details.
		/// </summary>
		[FieldOffset(2)]
		public CharacterAttribute Attributes;
	}

	/// <summary>
	/// Represents a character as either ASCII or unicode, depending on the current codepage.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct CharacterUnion
	{
		/// <summary>
		/// Unicode representation of the character.
		/// </summary>
		[FieldOffset(0)]
		public char UnicodeChar;
		/// <summary>
		/// ASCII representation of the current codepage.
		/// </summary>
		[FieldOffset(0)]
		public byte AsciiChar;
	}

	/// <summary>
	/// Attribute flags of the data to be written. A single attribute contains foreground and background data, as well as "intensity" flags for increased brightness.
	/// Intensity flags alone yield light grey, while all colors together with intensity yield pure white.
	/// <example>ForegroundBlue | ForegroundIntensity | BackgroundGreen results in a bright-blue foreground (rgb: 0,0,255) and dark-green background (rgb: 0, 128, 0)</example>
	/// </summary>
	[Flags]
	public enum CharacterAttribute : short
	{
		ForegroundBlue =		0x01,
		ForegroundGreen =		0x02,
		ForegroundRed =			0x04,
		ForegroundIntensity =	0x08,
		BackgroundBlue =		0x10,
		BackgroundGreen =		0x20,
		BackgroundRed =			0x40,
		BackgroundIntensity =	0x80,

		/// <summary>
		/// Shortcut for white output
		/// </summary>
		ForegroundWhite = 0x0F
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct SmallRect
	{
		public short Left;
		public short Top;
		public short Right;
		public short Bottom;
	}
	

	
	[StructLayout(LayoutKind.Sequential)]
	internal struct Coord
	{
		public short X;
		public short Y;

		public Coord(short X, short Y)
		{
			this.X = X;
			this.Y = Y;
		}
	};

	public enum StandardDevices
	{
		StandardInput = -10,
		StandardOutput = -11,
		StandardError = -12
	}

	[Flags]
	public enum AccessRights : uint
	{
		Read = 0x80000000,
		Write = 0x40000000
	}
	[Flags]
	public enum ShareMode : uint
	{
		None = 0,
		Read = 1,
		Write = 2
	}
}
