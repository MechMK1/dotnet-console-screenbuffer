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
		/// The attributes. See CharacterAttributes for details.
		/// </summary>
		[FieldOffset(2)]
		public CharacterAttributes Attributes;

		public override int GetHashCode()
		{
			return this.Char.AsciiChar ^ (int)this.Attributes;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ConsoleCharacter))
				return false;

			return Equals((ConsoleCharacter)obj);
		}

		public bool Equals(ConsoleCharacter other)
		{
			if (this.Char != other.Char)
				return false;

			return this.Attributes == other.Attributes;
		}

		public static bool operator ==(ConsoleCharacter c1, ConsoleCharacter c2)
		{
			return c1.Equals(c2);
		}

		public static bool operator !=(ConsoleCharacter c1, ConsoleCharacter c2)
		{
			return !c1.Equals(c2);
		}  
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

		public override int GetHashCode()
		{
			return this.AsciiChar.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is CharacterUnion))
				return false;

			return Equals((CharacterUnion)obj);
		}

		public bool Equals(CharacterUnion other)
		{
			return this.AsciiChar == other.AsciiChar;
		}

		public static bool operator ==(CharacterUnion c1, CharacterUnion c2)
		{
			return c1.Equals(c2);
		}

		public static bool operator !=(CharacterUnion c1, CharacterUnion c2)
		{
			return !c1.Equals(c2);
		} 
	}

	/// <summary>
	/// Attribute flags of the data to be written. A single attribute contains foreground and background data, as well as "intensity" flags for increased brightness.
	/// Intensity flags alone yield light grey, while all colors together with intensity yield pure white.
	/// <example>ForegroundBlue | ForegroundIntensity | BackgroundGreen results in a bright-blue foreground (rgb: 0,0,255) and dark-green background (rgb: 0, 128, 0)</example>
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32"), Flags] //Has to be short for the native method
	public enum CharacterAttributes : short
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
	};

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")] //Zero value doesn't make sense here
	public enum StandardDevice
	{
		StandardInput = -10,
		StandardOutput = -11,
		StandardError = -12
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32"), Flags] //Must be uint for native method
	public enum AccessRights : uint
	{
		Read = 0x80000000,
		Write = 0x40000000
	}
	[Flags]
	public enum ShareModes
	{
		None = 0,
		Read = 1,
		Write = 2
	}
}
