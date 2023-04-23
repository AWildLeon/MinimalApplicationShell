namespace MinimalApplicationShell
{
    public class Shell
    {
        private Dictionary<string, Action<string[]>> _commands;
        private List<string> history = new List<string>();

        public Shell(Dictionary<string, Action<string[]>> commands)
        {
            _commands = commands;
        }

        public void Run()
        {
            while (true)
            {
                Console.Write("> ");

                var input = ReadLineWithTabCompletion(_commands);

                var inputParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (inputParts.Length == 0)
                {
                    continue;
                }

                var command = inputParts[0];
                var arguments = inputParts.Length > 1 ? inputParts[1..] : new string[0];

                if (_commands.TryGetValue(command, out var action))
                {
                    action(arguments);
                }
                else
                {
                    Console.WriteLine($"Unknown command: {command}");
                }
            }
        }

        private List<string> GetCommandSuggestions(string prefix, IEnumerable<string> commands)
        {
            var suggestions = new List<string>();
            foreach (var command in commands)
            {
                if (command.StartsWith(prefix))
                {
                    suggestions.Add(command);
                }
            }
            return suggestions;
        }

        private string ReadLineWithTabCompletion(Dictionary<string, Action<string[]>> commands)
        {
            var input = "";
            var suggestions = new List<string>();
            var selectedIndex = 0;
            int historyIndex = history.Count +1;
            string savedNewLine = "";
            

            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Tab:
                    {
                        if (suggestions.Count == 0)
                        {
                            suggestions = GetCommandSuggestions(input, commands.Keys);
                        }
                        if (suggestions.Count > 0)
                        {
                            selectedIndex = (selectedIndex + 1) % suggestions.Count;
                            input = suggestions[selectedIndex];
                            Console.CursorLeft = 2;
                            Console.Write(new string(' ', Console.BufferWidth - 2));
                            Console.CursorLeft = 2;
                            Console.Write(input);
                        }

                        break;
                    }
                    case ConsoleKey.Backspace:
                    {
                        if (input.Length > 0)
                        {
                            input = input.Substring(0, input.Length - 1);
                            Console.Write("\b \b");
                        }
                        suggestions.Clear();
                        break;
                    }
                    case ConsoleKey.Enter:
                    {
                        Console.WriteLine();

                        if (input.Trim().Length > 0)
                        {
                            history.Add(input);
                        }

                        return input;
                    }
                    case ConsoleKey.UpArrow:
                    {
                        if (historyIndex > 0)
                        {
                            historyIndex --;
                        }

                        //if line is empty, skip it
                        if (historyIndex == history.Count)
                        {
                            historyIndex--;
                        }

                        //check if history index is in range
                        if (historyIndex >= 0 && historyIndex < history.Count)
                        {
                            if (historyIndex == history.Count - 1)
                            {
                                savedNewLine = input;
                            }
                            input = history[historyIndex];
                            Console.CursorLeft = 2;
                            Console.Write(new string(' ', Console.BufferWidth - 2));
                            Console.CursorLeft = 2;
                            Console.Write(input);
                        }

                        break;
                    }
                    case ConsoleKey.DownArrow:
                    {
                        if (historyIndex < history.Count - 1)
                        {
                            historyIndex++;
                            input = history[historyIndex];
                            Console.CursorLeft = 2;
                            Console.Write(new string(' ', Console.BufferWidth - 2));
                            Console.CursorLeft = 2;
                            Console.Write(input);
                        }
                        else if (historyIndex == history.Count - 1)
                        {
                            historyIndex++;
                            input = savedNewLine;
                            Console.CursorLeft = 2;
                            Console.Write(new string(' ', Console.BufferWidth - 2));
                            Console.CursorLeft = 2;
                            Console.Write(input);
                        }

                        break;
                    }
                    default:
                    {
                        if (key.KeyChar != '\0')
                        {
                            input += key.KeyChar;
                            Console.Write(key.KeyChar);
                            suggestions.Clear();
                        }
                        else
                        {
                            suggestions.Clear();
                        }

                        break;
                    }
                }
            }
        }
    }
}
