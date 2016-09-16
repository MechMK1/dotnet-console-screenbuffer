using System;

namespace MechMK1.ConsoleScreenbuffers.ConApp
{
	class Program
	{
		static void Main(string[] args)
		{
			//Get the handle of the current console buffer, which which the program already starts
			//This is needed because we want to restore to this buffer after we're done
			ConsoleBuffer oldBuffer = ConsoleBuffer.GetCurrentConsoleScreenBuffer();

			//Create a new buffer, to which this program will draw to
			ConsoleBuffer newBuffer = ConsoleBuffer.Create();

			//A sample message we'll output
			string message = "Hello, World!";

			//An array of data. This will be what we'll output
			//It has to be exactly as big as the data you want to output.
			//In our case "Hello, World!" four times, 64 rows.
			//Note that this is not a multidimensional array!
			ConsoleCharacter[] data = new ConsoleCharacter[message.Length * 4 * 64];

			//Loop over the data to set both the character and the colors
			for (int i = 0; i < data.Length; i++)
			{
				//This sets the character to be written.
				//Note: The data is interpreted internally by the set codepage.
				//CharacterUnion.AsciiChar is more reliable in that regards.
				//If your code displays wrong data for non-ASCII characters, it's likely that the codepage is wrong
				data[i].Char.UnicodeChar = message[i%message.Length];

				//Sets the attributes. You can either use the integer value or look at CharacterAttributes
				data[i].Attributes = (CharacterAttributes)(i / message.Length);
			}

			//This draws to the buffer.
			//"data" is the actual data to be drawn
			//x and y are the positions of the top-left corner of the rectangle
			//width and length determine how large the rectangle to be drawn should be
			//Note: You can draw a smaller rectangle than your data is big
			newBuffer.Draw(data, 0, 0, (short)(message.Length * 4), 64);

			//This sets newBuffer as the currently active one,
			//which means that "Hello, World!" is now visible on the console.
			//...hopefully!
			newBuffer.SetAsActiveBuffer();

			//This simply waits for any input to continue.
			//Thread.Sleep() would have sufficed too
			Console.ReadKey(true);

			//This restores the old buffer, which causes the console to go back to normal.
			oldBuffer.SetAsActiveBuffer();
		}
	}
}
