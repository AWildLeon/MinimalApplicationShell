# MinimalApplicationShell

MinimalApplicationShell is a C# library that provides a minimal Bash-like shell interface for console applications, complete with history and tab completion. It's designed to make it easy to add a command-line interface to your application without having to deal with the intricacies of console input parsing.

## Features

- Tab completion
- Command history
- Easy to use
- No dependencies
- Commands are defined with a simple dictionary of delegates

## Installation

MinimalApplicationShell is available as a NuGet package. You can install it using the following command in the Package Manager Console:

```
Install-Package MinimalApplicationShell
```

Alternatively, you can download the source code and include it in your project.

## Usage

Here's an example of how to use MinimalApplicationShell in your application:

```csharp
using MinimalApplicationShell;

// Define your commands as a dictionary of delegates
var commands = new Dictionary<string, Action<string[]>>();
commands.Add("hello", (string[] args) => Console.WriteLine("Hello, world!"));

// Create a new instance of the shell and pass in your commands
var shell = new Shell(commands);

// Run the shell
shell.Run();
```

That's it! Now you have a minimal shell interface for your application.

## Demo

A demo application is included in the [MinimalApplicationShell.Demo](https://github.com/AWildLeon/MinimalApplicationShell/tree/main/MinimalApplicationShell.Demo) project. It demonstrates how to define commands and use the shell in your application.

## Contributing

Contributions are welcome! If you find a bug or have a feature request, please open an issue or submit a pull request.

## License

MinimalApplicationShell is licensed under the [GPL V3](https://github.com/AWildLeon/MinimalApplicationShell/blob/main/LICENSE).
