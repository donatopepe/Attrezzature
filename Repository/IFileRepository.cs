using AttrOleo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttrOleo.Repository
{
    public interface IFileRepository 
    {

        Task<List<FileDescription>> GetAllFiles();

        Task<FileDescription> GetFirstOrDefaultAsync(Guid? id);

        Task<FileDescription> PutFileToDB(FileDescriptionShort fileDescriptionShort);

        bool FileDescriptionExists(Guid id);

        Task<int> SaveChangesAsync(FileDescription fileDescription);

        Task<int> DeleteConfirmed(Guid id);

        Task<List<FileDescription>> GetAllFilesPress(int? id);

    }
}
