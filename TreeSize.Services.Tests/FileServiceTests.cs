using System.Collections.ObjectModel;
using NUnit.Framework;
using System.IO.Abstractions.TestingHelpers;
using TreeSize.Models.Enums;
using TreeSize.Services.Implementations;
using TreeSize.Models.Models;


namespace TreeSize.Services.Tests;

public class FileServiceTests
{
    private FileService _fileService;

    [SetUp]
    public void Setup()
    {
        var mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            {@"F:\File1.txt", new MockFileData("Test file1")},
            {@"F:\Folder\File2.txt", new MockFileData("Some test file 2")},
        });

        _fileService = new FileService(mockFileSystem);
    }

    [Test]
    public void GetTree_CorrectData_AreEqual()
    {
        var expectedTree = new ObservableCollection<BaseFile>()
        {
            new BaseFile
            {
                Name = "Folder",
                Path = @"F:\Folder",
                FileType = FileType.Folder,
                Size = 16,
                Children = new ObservableCollection<BaseFile>
                {
                    new BaseFile
                    {
                        Name = "File2.txt",
                        Path = @"F:\Folder",
                        FileType = FileType.File,
                        Size = 16,
                    }
                }
            },

            new BaseFile
            {
            Name = "File1.txt",
            Path = @"F:\",
            FileType = FileType.File,
            Size = 10,
            },
        };

        var actualTree = new ObservableCollection<BaseFile>();
        _ = _fileService.GetTree(@"F:\", actualTree);
        Assert.Multiple(() =>
        {
            Assert.That(actualTree[0].Name, Is.EqualTo(expectedTree[0].Name));
            Assert.That(actualTree[0].Path, Is.EqualTo(expectedTree[0].Path));
            Assert.That(actualTree[0].FileType, Is.EqualTo(expectedTree[0].FileType));
            Assert.That(actualTree[0].Size, Is.EqualTo(expectedTree[0].Size));

            Assert.That(actualTree[1].Name, Is.EqualTo(expectedTree[1].Name));
            Assert.That(actualTree[1].Path, Is.EqualTo(expectedTree[1].Path));
            Assert.That(actualTree[1].FileType, Is.EqualTo(expectedTree[1].FileType));
            Assert.That(actualTree[1].Size, Is.EqualTo(expectedTree[1].Size));

            Assert.That(actualTree[0].Children[0].Name, Is.EqualTo(expectedTree[0].Children[0].Name));
            Assert.That(actualTree[0].Children[0].Path, Is.EqualTo(expectedTree[0].Children[0].Path));
            Assert.That(actualTree[0].Children[0].FileType, Is.EqualTo(expectedTree[0].Children[0].FileType));
            Assert.That(actualTree[0].Children[0].Size, Is.EqualTo(expectedTree[0].Children[0].Size));
        });
    }

    [Test]
    public void GetTree_InvalidData_Exception()
    {

        Assert.ThrowsAsync<ArgumentException>(() => _ = _fileService.GetTree(@"Wrong path", new ObservableCollection<BaseFile>()));
    }
}