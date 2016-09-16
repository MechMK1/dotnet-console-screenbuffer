# Console Screenbuffers for .NET

## Why would I want Console Screenbuffers?
Have you ever used a text editor such as `vi`, `emacs` or `nano`? They open up in a new "window" (called screenbuffer) where they do their thing, and when they quit, the original console is restored without any output on the screen. Other tools such as `top`, `iotop` or `less` do the same. It's amazing! Sadly, the `System.Console`-class, which the vast majority of developers use for their Console I/O needs, doesn't support multiple screenbuffers. Not even a tiny bit.

## Why would I want to use your implementation of screenbuffers?
Because it's there. And after using Google for a bit, I couldn't find another library which enables you to use multiple screenbuffers in C# and other .NET languages.

## How is it licensed?
I am using the MIT License, which should be the most permissive one. Just add a note somewhere that your program uses my thingy!
