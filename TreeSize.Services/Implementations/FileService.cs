using System.Collections.ObjectModel;
using System.IO.Abstractions;
using TreeSize.Models.Enums;
using TreeSize.Models.Models;
using TreeSize.Services.Interfaces;

namespace TreeSize.Services.Implementations;

public class FileService : IFileService
{
    private readonly IFileSystem _fileSystem;

    private static readonly List<string> ExcludedFolders = new() { "$Recycle.Bin" };

    public FileService(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public FileService() : this(new FileSystem())
    {
    }

    public Task<ObservableCollection<Storage>> GetDrivesList()
    {
        var drives = new ObservableCollection<Storage>();

        var drivesInfos = _fileSystem.DriveInfo.GetDrives().ToList();

        foreach (var drive in drivesInfos)
        {
            drives.Add(new Storage
            {
                Name = drive.Name,
                FreeSpace = drive.AvailableFreeSpace,
                TotalCapacity = drive.TotalSize,
                FileType = FileType.Drive,
                Path = string.Empty
            });
        }

        return Task.FromResult(drives);
    }

    public async Task<BaseFile> GetTree(string path, ObservableCollection<BaseFile> files)
    {
        path = Environment.ExpandEnvironmentVariables(path);

        if (!_fileSystem.Directory.Exists(path))
        {
            throw new ArgumentException("Wrong drive path.");
        }

        var info = _fileSystem.DirectoryInfo.FromDirectoryName(path);

        var tempFile = new BaseFile
        {
            Name = info.Name,
            Path = info.FullName,
            FileType = FileType.Folder,
            Children = new ObservableCollection<BaseFile>(),
            Size = 0
        };

        FileFiller(info, tempFile);

        for (var i = 0; i < tempFile.Children.Count; i++)
        {
            if (tempFile.Children[i].FileType == FileType.Folder)
            {
                var folder = await GetTree(tempFile.Children[i].Path, tempFile.Children[i].Children);
                tempFile.Size += folder.Size;
                tempFile.Children[i] = folder;
                files.Add(folder);
            }
            else
            {
                files.Add(tempFile.Children[i]);
            }
        }

        tempFile.Children =
            new ObservableCollection<BaseFile>(tempFile.Children.OrderBy(f => f.FileType)
                .ThenByDescending(f => f.Size));

        return tempFile;
    }

    private static void FileFiller(IDirectoryInfo rootFolder, BaseFile tempFile)
    {
        try
        {
            foreach (var folder in rootFolder.GetDirectories().Where(d => !IsExcluded(ExcludedFolders, d)))
            {
                tempFile.Children.Add(new BaseFile()
                {
                    Name = folder.Name,
                    Path = folder.FullName,
                    FileType = FileType.Folder
                });
            }

            foreach (var file in rootFolder.GetFiles())
            {
                tempFile.Children.Add(new BaseFile()
                {
                    Name = file.Name,
                    Size = file.Length,
                    Path = file.DirectoryName!,
                    FileType = FileType.File
                });

                tempFile.Size += file.Length;
            }
        }
        catch (UnauthorizedAccessException)
        {
        }
    }

    private static bool IsExcluded(IEnumerable<string> excludedFoldersList, IFileSystemInfo info)
    {
        return excludedFoldersList.Any(folder => info.Name.Equals(folder));
    }
}