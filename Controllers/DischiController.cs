using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttrOleo.Data;
using AttrOleo.Models;
using AttrOleo.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace AttrOleo.Controllers
{
    public class DischiController : Controller
    {

        private readonly IFileRepository _fileRepository;
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public DischiController(IFileRepository fileRepository, ApplicationDbContext context, RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg)
        {
            _fileRepository = fileRepository;
            _context = context;
            userManager = userMrg;
            roleManager = roleMgr;
        }


        // GET: Dischi
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Dischi.ToListAsync());
        //}
        public async Task<IActionResult> Index(
               string sortOrder,
               bool sortParam,
               bool toggle,
               string currentFilter,
               string searchString,
               int? pageNumber)
        {

            ViewData["SortOrder"] = sortOrder;
            ViewData["CurrentFilter"] = currentFilter;
            ViewData["SearchString"] = searchString;
            if (toggle)
            {
                sortParam = !sortParam;
                toggle = false;
            }
            ViewData["SortParam"] = sortParam;
            ViewData["Toggle"] = toggle;


            var query = from m in _context.Dischi
                        select m;
            if (!String.IsNullOrEmpty(currentFilter) && !String.IsNullOrEmpty(searchString))
            {
                switch (currentFilter.ToLower().Trim())
                {
                    case "numero lotto":
                        query = query.Where(s => s.LottoNo.Contains(searchString.ToUpper().Trim()));
                        break;
                    case "costruttore":
                        query = query.Where(s => s.Costruttore.Contains(searchString.ToUpper().Trim()));
                        break;
                }
            }
            if (!String.IsNullOrEmpty(sortOrder) && !String.IsNullOrEmpty(sortOrder))
            {
                if (sortParam)
                {
                    switch (sortOrder.ToLower().Trim())
                    {
                        case "numero lotto":
                            query = query.OrderBy(s => s.LottoNo);
                            break;
                        case "costruttore":
                            query = query.OrderBy(s => s.Costruttore);
                            break;
                        case "data ultimo aggiornamento":
                            query = query.OrderBy(s => s.DataAggiornamento);
                            break;
                        case "data creazione":
                            query = query.OrderBy(s => s.DataCreazione);
                            break;
                        case "pressione di rottura [bar]":
                            query = query.OrderBy(s => s.PressioneRottura);
                            break;
                    }
                }
                else
                {
                    switch (sortOrder.ToLower().Trim())
                    {
                        case "numero lotto":
                            query = query.OrderByDescending(s => s.LottoNo);
                            break;
                        case "costruttore":
                            query = query.OrderByDescending(s => s.Costruttore);
                            break;
                        case "data ultimo aggiornamento":
                            query = query.OrderByDescending(s => s.DataAggiornamento);
                            break;
                        case "data creazione":
                            query = query.OrderByDescending(s => s.DataCreazione);
                            break;
                        case "pressione di rottura [bar]":
                            query = query.OrderByDescending(s => s.PressioneRottura);
                            break;
                    }
                }
            }


            var dischi = await query.AsNoTracking().ToListAsync();

            int pageSize = 10;
            return View(PaginatedList<Disco>.Create(dischi, pageNumber ?? 1, pageSize));
        }

        // GET: Dischi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.Dischi.Include(d => d.Files).Include(b => b.TecnicoUltimoAggiornamento).Include(p => p.PressioneMontata)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disco == null)
            {
                return NotFound();
            }

            return View(disco);
        }


        private void PopulatePressioniDropDownList()
        {
            var Query = _context.Pressioni;
            var lista = new List<SelectListItem>();
            foreach (var item in Query)
            {
                if (ControlloUtente(item)) lista.Add(new SelectListItem(item.Matricola, item.Matricola));
            }
            ViewBag.Pressioni = lista;//new SelectList(Query.AsNoTracking(), "Matricola", "Matricola");
        }


        // GET: Dischi/Edit/5
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> Edit(int? id)
        {
            Disco disco;
            if (id == null)
            {
                //return NotFound();
                disco = new Disco();
            }
            else
            {
                disco = await _context.Dischi.Include(d => d.Files).Include(p => p.PressioneMontata).FirstOrDefaultAsync(m => m.Id == id);
            }
            PopulatePressioniDropDownList();
            return View(disco);
        }

        // POST: Dischi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> Edit(int? id, [Bind("LottoNo,PressioneRottura,Costruttore")] Disco disco, [Bind("AggiungiPressione")] string AggiungiPressione, [Bind("operationType")] string operationType)
        {

            if (ModelState.IsValid)
            {
                if ((id == null) || (id < 1))
                {

                    Disco nuovadisco = new Disco();

                    _context.Add(nuovadisco);
                    await _context.SaveChangesAsync();
                    id = nuovadisco.Id;


                }
                Disco original = _context.Dischi.Include(d => d.Files).Include(p=>p.PressioneMontata).Single(x => x.Id == id);
                if (await this.TryUpdateModelAsync(original))
                {
                    try
                    {
                        ApplicationUser user2 = await userManager.FindByNameAsync(User.Identity.Name);

                        if (original.DataCreazione.Year < 2019) original.DataCreazione = DateTime.Now;
                        original.DataAggiornamento = DateTime.Now;
                        original.TecnicoUltimoAggiornamento = user2;
                        
                        
                        if (!String.IsNullOrEmpty(AggiungiPressione))
                        {
                            bool presstrovata = false;
                            if (original.PressioneMontata != null)
                            {


                                foreach (var item in original.PressioneMontata)
                                {
                                    if (item.Matricola == AggiungiPressione.Trim())
                                    {

                                        presstrovata = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                original.PressioneMontata = new List<Pressione>();

                            }

                            if (!presstrovata)
                            {


                                Pressione pressione = await _context.Pressioni.FirstOrDefaultAsync(m => m.Matricola == AggiungiPressione.Trim());
                                if (pressione != null)
                                {
                                    pressione.DiscoMontato = original;
                                    pressione.TecnicoUltimoAggiornamento = user2;
                                    pressione.DataAggiornamento = DateTime.Now;
                                    original.PressioneMontata.Add(pressione);
                                }
                                
                            }

                        }
                                                                                          
                            //_context.Update(disco);
                            await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DiscoExists(disco.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    if (operationType == "AddFile")
                    {
                        return RedirectToAction(nameof(Upload), new FileDescriptionShort()
                        {
                            Id = id
                        });
                    }
                    else
                    if (operationType != "AddPressione")
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                return RedirectToAction(nameof(Edit), id);
                //return RedirectToAction(nameof(Index));
            }
            return View(disco);
        }
        [HttpPost]
        public bool ControlloUtente(Pressione pressione)
        {
            bool controllo = false;
            if (pressione != null)
            {
                if (!User.IsInRole("Admin") && !User.IsInRole("Manager"))
                {
                    if ((pressione.Area != null) &&
                           User.IsInRole(pressione.Area.ToUpper().Trim()) &&
                           (pressione.Impianto != null) &&
                           User.IsInRole(pressione.Impianto.ToUpper().Trim()) &&
                           (pressione.Reparto != null) &&
                           User.IsInRole(pressione.Reparto.ToUpper().Trim()))
                    {
                        controllo = true;
                    }
                    else
                    {
                        controllo = false;
                    }

                }
                else

                {
                    controllo = true;
                }
            }
            return controllo;
        }
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> DissociaPressione(int id, int pressioneid)
        {

            Disco original = _context.Dischi.Include(d => d.Files).Include(p => p.PressioneMontata).Single(x => x.Id == id);
            Pressione pressione = _context.Pressioni.Single(m => m.Id == pressioneid);
            if (original != null && pressione != null && ControlloUtente(pressione))
            {
                
                try
                {
                    ApplicationUser user2 = await userManager.FindByNameAsync(User.Identity.Name);
                    //original.TecnicoUltimoAggiornamento = user2;
                    //original.DataAggiornamento = DateTime.Now;
                    original.PressioneMontata.Remove(pressione);
                    pressione.TecnicoUltimoAggiornamento = user2;
                    pressione.DataAggiornamento = DateTime.Now;
 
                    pressione.DiscoMontato = null;
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }

            } else
            {
                return NotFound();
            }


            //return RedirectToAction(nameof(Edit), new { id });
            return Redirect(Request.Headers["Referer"].ToString());
        }
        // GET: Dischi/Delete/5
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.Dischi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disco == null)
            {
                return NotFound();
            }

            return View(disco);
        }

        // POST: Dischi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disco = await _context.Dischi.Include(d => d.Files).Include(p => p.PressioneMontata).FirstOrDefaultAsync(m => m.Id == id);
            foreach (FileDescription file in disco.Files) _context.FileDescriptions.Remove(file);
            disco.PressioneMontata.Clear();
            _context.Dischi.Remove(disco);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscoExists(int id)
        {
            return _context.Dischi.Any(e => e.Id == id);
        }


        // GET: FileDescriptions/Create
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> Upload(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.Dischi.Include(d => d.Files).FirstOrDefaultAsync(m => m.Id == id);
            if (disco == null)
            {
                return NotFound();
            }

            return View(new FileDescriptionShort()
            {
                Id = id
            });
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> Upload(FileDescriptionShort fileDescriptionShort)
        {

            if (ModelState.IsValid)
            {

                if (fileDescriptionShort.Id == null)
                {
                    return NotFound();
                }

                var disco = await _context.Dischi.Include(d => d.Files).FirstOrDefaultAsync(m => m.Id == fileDescriptionShort.Id);
                if (disco == null)
                {
                    return NotFound();
                }


                Console.WriteLine("Model Bind OK");


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
                        if (file.Category != null) file.Category = fileDescriptionShort.Category.Trim().ToUpper();
                        if (file.Description != null) file.Description = fileDescriptionShort.Description.Trim();
                        file.CreatedTimestamp = DateTime.Now;
                        file.UpdatedTimestamp = DateTime.Now;
                        file.ContentType = fileDescriptionShort.File.ContentType;
                        file.Disco = disco;
                        ApplicationUser user2 = await userManager.FindByNameAsync(User.Identity.Name);

                        if (disco.DataCreazione.Year < 2019) disco.DataCreazione = DateTime.Now;
                        disco.DataAggiornamento = DateTime.Now;
                        disco.TecnicoUltimoAggiornamento = user2;

                        //_context.Add(file);
                        if (disco.Files == null)
                        {
                            disco.Files = new List<FileDescription>();
                        }

                        disco.Files.Add(file);


                        
                        disco.TecnicoUltimoAggiornamento = user2;
                        disco.DataAggiornamento = DateTime.Now;
                        _context.Update(disco);

                        await _context.SaveChangesAsync();

                        Console.WriteLine("Dimensione del file OK");

                        //return RedirectToAction(nameof(Index));
                        return RedirectToAction(nameof(Edit), new { fileDescriptionShort.Id });

                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                    }
                }

            }
            return View(fileDescriptionShort);
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
        // GET: FileDescriptions/Edit/5
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> EditFile(Guid? id)
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
        [Authorize(Roles = "Admin,Manager,RW")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFile(Guid id, [Bind("Id,Category,Description")] FileDescriptionNoFile fileDescriptionNoFile)
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
                ApplicationUser user2 = await userManager.FindByNameAsync(User.Identity.Name);
                fileDescription.Disco.TecnicoUltimoAggiornamento = user2;
                if (fileDescription.Disco.DataCreazione.Year < 2019) fileDescription.Disco.DataCreazione = DateTime.Now;
                fileDescription.Disco.DataAggiornamento = DateTime.Now;

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
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Edit), new { fileDescription.Disco.Id });
            }

            return View(fileDescriptionNoFile);
        }

        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> DeleteFile(Guid id)
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
        [HttpPost, ActionName("DeleteFile")]
        [Authorize(Roles = "Admin,Manager,RW")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedFile(Guid id)
        {
            /*
            var fileDescription = await _fileRepository.GetFirstOrDefaultAsync(id);

            _context.FileDescriptions.Remove(fileDescription);

            await _context.SaveChangesAsync();
            */
            var fileDescription = await _fileRepository.GetFirstOrDefaultAsync(id);

            if (fileDescription == null)
            {
                return NotFound();
            }
            await _fileRepository.DeleteConfirmed(id);

            //return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Edit), new { fileDescription.Disco.Id });
        }


    }
}
