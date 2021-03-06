using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttrOleo.Models;
using AttrOleo.Data;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Http;
using AttrOleo.Repository;
using Microsoft.AspNetCore.Authorization;

namespace AttrOleo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FileDescriptionsController : Controller
    {
        private readonly IFileRepository _fileRepository;
        private readonly ApplicationDbContext _context;

        public FileDescriptionsController(IFileRepository fileRepository, ApplicationDbContext context)
        {
            _fileRepository = fileRepository;
            _context = context;
        }

        // GET: FileDescriptions
        public async Task<IActionResult> Index()
        {
            //return View(await _context.FileDescriptions.ToListAsync());
            return View(await _fileRepository.GetAllFiles());

        }

        // GET: FileDescriptions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            //var fileDescription = await _context.FileDescriptions.FirstOrDefaultAsync(m => m.Id == id);

            var fileDescription = await _fileRepository.GetFirstOrDefaultAsync(id);


            if (fileDescription == null)
            {
                return NotFound();
            }

            return View(fileDescription);



        }

        public async Task<IActionResult> Download(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var fileDescription = await _context.FileDescriptions.FirstOrDefaultAsync(m => m.Id == id);
            var fileDescription = await _fileRepository.GetFirstOrDefaultAsync(id);

            if (fileDescription == null)
            {
                return NotFound();
            }

            //return View(fileDescription);


            return File(fileDescription.File, fileDescription.ContentType);
        }


        // GET: FileDescriptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FileDescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FileName,Category,Description,File,CreatedTimestamp,UpdatedTimestamp,ContentType")] FileDescription fileDescription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileDescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fileDescription);
        }
        */


        [HttpPost]
        public async Task<IActionResult> Create(FileDescriptionShort fileDescriptionShort)
        {
            /*
            using (var memoryStream = new MemoryStream())
            {
                await fileDescriptionShort.File.CopyToAsync(memoryStream);

                // Upload the file if less than 20 MB
                if (memoryStream.Length < 20971520)
                {
                    Console.WriteLine("Dimensione del file OK");
                    if (ModelState.IsValid)
                    {
                        Console.WriteLine("Model Bind OK");
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


                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }
            */
            if (ModelState.IsValid)
            {
                Console.WriteLine("Model Bind OK");

                if (await _fileRepository.PutFileToDB(fileDescriptionShort)!=null)
                {
                    Console.WriteLine("Dimensione del file OK");

                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }
            return View(fileDescriptionShort);
            
        }


        // GET: FileDescriptions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var fileDescription = await _context.FileDescriptions.FindAsync(id);
            var fileDescription = await _fileRepository.GetFirstOrDefaultAsync(id);

            if (fileDescription == null)
            {
                return NotFound();
            }
            var fileDescriptionNoFile = new FileDescriptionNoFile();

            fileDescriptionNoFile.Id = fileDescription.Id;
            fileDescriptionNoFile.Description = fileDescription.Description;
            fileDescriptionNoFile.Category = fileDescription.Category;




            return View(fileDescriptionNoFile);
        }

        // POST: FileDescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Category,Description")] FileDescriptionNoFile fileDescriptionNoFile)
        {
            if (id != fileDescriptionNoFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //var fileDescription = await _context.FileDescriptions.FindAsync(id);
                var fileDescription = await _fileRepository.GetFirstOrDefaultAsync(id);

                if (fileDescription == null)
                {
                    return NotFound();
                }
                fileDescription.Category = fileDescriptionNoFile.Category;
                fileDescription.Description = fileDescriptionNoFile.Description;
                try
                {
                    /*
                    _context.Update(fileDescription);
                    await _context.SaveChangesAsync();
                    */
                    await _fileRepository.SaveChangesAsync(fileDescription);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_fileRepository.FileDescriptionExists(fileDescription.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(fileDescriptionNoFile);
        }

        // GET: FileDescriptions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
            var fileDescription = await _context.FileDescriptions
                .FirstOrDefaultAsync(m => m.Id == id);
                */
            var fileDescription = await _fileRepository.GetFirstOrDefaultAsync(id);

            if (fileDescription == null)
            {
                return NotFound();
            }

            return View(fileDescription);
        }

        // POST: FileDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            /*
            var fileDescription = await _fileRepository.GetFirstOrDefaultAsync(id);

            _context.FileDescriptions.Remove(fileDescription);

            await _context.SaveChangesAsync();
            */

            await _fileRepository.DeleteConfirmed(id);

            return RedirectToAction(nameof(Index));
        }


    }
}
