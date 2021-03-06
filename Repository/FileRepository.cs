using AttrOleo.Data;
using AttrOleo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AttrOleo.Repository
{
    public class FileRepository : IFileRepository
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> userManager;
        private readonly ILogger _logger;
        private RoleManager<IdentityRole> roleManager;
        private readonly string authenticatedUser;

        public FileRepository(ApplicationDbContext context, ILoggerFactory loggerFactory, RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("FileRepository");
            userManager = userMrg;
            roleManager = roleMgr;
            authenticatedUser = contextAccessor.HttpContext.User.Identity.Name;
        }

        public async Task<List<FileDescription>> GetAllFiles()
        {

            return await _context.FileDescriptions.ToListAsync();

        }
        
        public async Task<List<FileDescription>> GetAllFilesPress(int? id)
        {

           
            if (id == null)
            {
                return null;
            }

            var pressione = await _context.Pressioni.FirstOrDefaultAsync(m => m.Id == id);
            if (pressione == null)
            {
                return null;
            }

            // return await _context.FileDescriptions.Where(i => i.== id).ToListAsync();
            var files = await _context.Pressioni.Include(d => d.Files).ToListAsync();
            //return await _context.Pressioni.Include(d => d.Files).ToListAsync();
            return null;
        }

        public async Task<FileDescription> GetFirstOrDefaultAsync(Guid? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _context.FileDescriptions.Include(p=>p.Pressione).Include(v=>v.Valvola).Include(d=>d.Disco).FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<FileDescription> PutFileToDB(FileDescriptionShort fileDescriptionShort)
        {
            using (var memoryStream = new MemoryStream())
            {
                await fileDescriptionShort.File.CopyToAsync(memoryStream);

                // Upload the file if less than 20 MB
                if (memoryStream.Length < 20971520)
                {
                    Console.WriteLine("Dimensione del file OK");

                    
                    var file = new FileDescription()
                    {
                        File = memoryStream.ToArray()
                    };
                    file.FileName = fileDescriptionShort.File.FileName;
                    file.Category = fileDescriptionShort.Category.Trim().ToUpper();
                    file.Description = fileDescriptionShort.Description.Trim();
                    file.CreatedTimestamp = DateTime.Now;
                    file.UpdatedTimestamp = DateTime.Now;
                    file.ContentType = fileDescriptionShort.File.ContentType;
                    _context.Add(file);

                    await _context.SaveChangesAsync();


                    return file;

                }
                return null;
            }
        }
        public bool FileDescriptionExists(Guid id)
        {
            return _context.FileDescriptions.Any(e => e.Id == id);
        }

        public async Task<int> SaveChangesAsync(FileDescription fileDescription)
        {
            _context.Update(fileDescription);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteConfirmed(Guid id)
        {
            //var fileDescription = await _context.FileDescriptions.FindAsync(id);
            var fileDescription = await GetFirstOrDefaultAsync(id);
            ApplicationUser user2 = await userManager.FindByNameAsync(authenticatedUser);
            if (fileDescription.Pressione != null)
            {
                fileDescription.Pressione.TecnicoUltimoAggiornamento = user2;
                if (fileDescription.Pressione.DataCreazione.Year < 2019) fileDescription.Pressione.DataCreazione = DateTime.Now;
                fileDescription.Pressione.DataAggiornamento = DateTime.Now;
            }
            if (fileDescription.Valvola != null)
            {
                fileDescription.Valvola.TecnicoUltimoAggiornamento = user2;
                if (fileDescription.Valvola.DataCreazione.Year < 2019) fileDescription.Valvola.DataCreazione = DateTime.Now;
                fileDescription.Valvola.DataAggiornamento = DateTime.Now;
            }
            if (fileDescription.Disco != null)
            {
                fileDescription.Disco.TecnicoUltimoAggiornamento = user2;
                if (fileDescription.Disco.DataCreazione.Year < 2019) fileDescription.Disco.DataCreazione = DateTime.Now;
                fileDescription.Disco.DataAggiornamento = DateTime.Now;
            }
            _context.FileDescriptions.Remove(fileDescription);

            


            return await _context.SaveChangesAsync();
        }
    }
}
