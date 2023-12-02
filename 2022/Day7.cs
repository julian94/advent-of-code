using System.Runtime.Serialization;
using System.Xml.Linq;

namespace _2022;

public static class Day7
{
    const string commandIndicator = "$";
    const string cd = "$ cd ";
    const string cdRoot = "$ cd /";
    const string cdUp = "$ cd ..";
    const string ls = "$ ls";
    const string dir = "dir ";

    public static string SolvePartOne(IList<string> input)
    {
        const int desiredFolderMaxSize = 100_000;

        var folders = new List<SimpleFolder>()
        {
            new() {
                Name = "/",
                Folders = [],
                Files = [],
            }
        };

        var currentPath = "/";

        for (int i = 1; i < input.Count(); i++)
        {
            //Console.WriteLine($"{i+1} {currentPath}");
            if (input[i] == cdRoot)
            {
                // Do nothing;
            }
            else if (input[i] == cdUp)
            {
                currentPath = currentPath[..currentPath.LastIndexOf('/')];
            }
            else if (input[i].StartsWith(cd))
            {
                var name = input[i].Split()[2];
                currentPath = $"{currentPath}/{name}".Replace("//", "/");
            }
            else if (input[i] == ls)
            {
                // do nothing
            }
            else
            {
                // Add folders to current dir
                var currentFolder = folders.Where(f => f.Name == currentPath).First();
                if (input[i].StartsWith(dir))
                {
                    var folder = new SimpleFolder()
                    {
                        Name = $"{currentPath}/{input[i].Split()[1]}".Replace("//", "/"),
                        Folders = [],
                        Files = [],
                    };
                    folders.Add(folder);
                    currentFolder.Folders.Add(folder);
                }
                else
                {
                    var parts = input[i].Split();
                    currentFolder.Files.Add(new()
                    {
                        Size = int.Parse(parts[0]),
                        Name = parts[1],
                    });
                }
            }
        }

        var stupidTotalSize = 0;

        foreach (var folder in folders)
        {
            var size = folder.GetSize();
            if (size < desiredFolderMaxSize)
            {
                stupidTotalSize += size;
            }
        }

        return stupidTotalSize.ToString();
    }

    public static string SolvePartTwo(IList<string> input)
    {
        const int totalDiskSize = 70_000_000;
        const int requiredEmptyDiskSize = 30_000_000;

        var folders = new List<SimpleFolder>()
        {
            new() {
                Name = "/",
                Folders = [],
                Files = [],
            }
        };

        var currentPath = "/";

        for (int i = 1; i < input.Count(); i++)
        {
            //Console.WriteLine($"{i + 1} {currentPath}");
            if (input[i] == cdRoot)
            {
                // Do nothing;
            }
            else if (input[i] == cdUp)
            {
                currentPath = currentPath[..currentPath.LastIndexOf('/')];
            }
            else if (input[i].StartsWith(cd))
            {
                var name = input[i].Split()[2];
                currentPath = $"{currentPath}/{name}".Replace("//", "/");
            }
            else if (input[i] == ls)
            {
                // do nothing
            }
            else
            {
                // Add folders to current dir
                var currentFolder = folders.Where(f => f.Name == currentPath).First();
                if (input[i].StartsWith(dir))
                {
                    var folder = new SimpleFolder()
                    {
                        Name = $"{currentPath}/{input[i].Split()[1]}".Replace("//", "/"),
                        Folders = [],
                        Files = [],
                    };
                    folders.Add(folder);
                    currentFolder.Folders.Add(folder);
                }
                else
                {
                    var parts = input[i].Split();
                    currentFolder.Files.Add(new()
                    {
                        Size = int.Parse(parts[0]),
                        Name = parts[1],
                    });
                }
            }
        }

        var occupiedSize = folders[0].GetSize();

        var neededDeletion = (requiredEmptyDiskSize + occupiedSize) - totalDiskSize;

        var sortedFolders = folders.Where(f => f.GetSize() > neededDeletion).OrderBy(f => f.GetSize());


        return sortedFolders.First().GetSize().ToString();
    }
}

public struct SimpleFile
{
    public string Name { get; set; }

    public int Size { get; set; }
}

public struct SimpleFolder
{
    public string Name { get; set; }

    public List<SimpleFolder> Folders { get; set; }

    public List<SimpleFile> Files { get; set; }

    public int GetSize()
    {
        var size = 0;
        foreach (var file in Files)
        {
            size += file.Size;
        }
        foreach (var folder in Folders)
        {
            size += folder.GetSize();
        }
        return size;
    }
}
