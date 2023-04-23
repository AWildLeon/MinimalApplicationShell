namespace MinimalApplicationShell.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var commands = new Dictionary<string, Action<string[]>>();
            commands.Add("hello", (string[] args) => Console.WriteLine("Hello, world!"));
            commands.Add("greet", (_) =>
            {
                Console.Write("Enter your name: ");
                var name = Console.ReadLine();
                Console.WriteLine($"Hello, {name}!");
            });

            //cat
            commands.Add("cat", delegate(string[] args)
            {
                //Linux CAT command
                if (args.Length == 0)
                {
                    Console.WriteLine("No file specified");
                    return;
                }

                var fileName = args[0];
                if (!File.Exists(fileName))
                {
                    Console.WriteLine($"File {fileName} does not exist");
                    return;
                }

                var fileContents = File.ReadAllText(fileName);
                Console.WriteLine(fileContents);

            });

            //ls
            commands.Add("ls", delegate(string[] args)
            {
                var directory = Directory.GetCurrentDirectory();
                if (args.Length > 0)
                {
                    directory = args[0];
                }

                var files = Directory.GetDirectories(directory).ToList();
                files.AddRange(Directory.GetFiles(directory));

                // remove the path from the file names
                for (var i = 0; i < files.Count; i++)
                {
                    files[i] = Path.GetFileName(files[i]);
                }

                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
            });

            //cd
            commands.Add("cd", delegate(string[] args)
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("No directory specified");
                    return;
                }
                var directory = args[0];
                if (!Directory.Exists(directory))
                {
                    Console.WriteLine($"Directory {directory} does not exist");
                    return;
                }
                Directory.SetCurrentDirectory(directory);
            });

            //pwd
            commands.Add("pwd", delegate(string[] args)
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
            });

            //mkdir
            commands.Add("mkdir", delegate(string[] args)
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("No directory specified");
                    return;
                }
                var directory = args[0];
                if (Directory.Exists(directory))
                {
                    Console.WriteLine($"Directory {directory} already exists");
                    return;
                }
                Directory.CreateDirectory(directory);
            });

            //rm
            commands.Add("rm", delegate(string[] args)
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("No file specified");
                    return;
                }
                var fileName = args[0];
                if (!File.Exists(fileName))
                {
                    Console.WriteLine($"File {fileName} does not exist");
                    return;
                }
                File.Delete(fileName);
            });

            //echo
            commands.Add("echo", delegate(string[] args)
            {
                //echo all arguments
                foreach (var arg in args)
                {
                    Console.Write(arg + " ");
                }

                Console.WriteLine();
            });

            var shell = new Shell(commands);
            shell.Run();

        }
    }
}