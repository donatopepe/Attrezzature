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
    public class ValvoleController : Controller
    {
        private readonly IFileRepository _fileRepository;
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public ValvoleController(IFileRepository fileRepository, ApplicationDbContext context, RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg)
        {
            _fileRepository = fileRepository;
            _context = context;
            userManager = userMrg;
            roleManager = roleMgr;
        }

        // GET: Valvole
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Valvole.ToListAsync());
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


            var query = from m in _context.Valvole
                        select m;
            if (!String.IsNullOrEmpty(currentFilter) && !String.IsNullOrEmpty(searchString))
            {
                switch (currentFilter.ToLower().Trim())
                {
                    case "area":
                        query = query.Where(s => s.Area.Contains(searchString.ToUpper().Trim()));
                        break;
                    case "reparto":
                        query = query.Where(s => s.Reparto.Contains(searchString.ToUpper().Trim()));
                        break;
                    case "impianto":
                        query = query.Where(s => s.Impianto.Contains(searchString.ToUpper().Trim()));
                        break;
                    case "matricola":
                        query = query.Where(s => s.Matricola.Contains(searchString.ToUpper().Trim()));
                        break;
                    case "stato":
                        query = query.Where(s => s.Stato.Contains(searchString.ToUpper().Trim()));
                        break;
                    case "modello":
                        query = query.Where(s => s.Modello.Contains(searchString.ToUpper().Trim()));
                        break;
                }
            }
            if (!String.IsNullOrEmpty(sortOrder) && !String.IsNullOrEmpty(sortOrder))
            {
                if (sortParam)
                {
                    switch (sortOrder.ToLower().Trim())
                    {
                        case "area":
                            query = query.OrderBy(s => s.Area);
                            break;
                        case "reparto":
                            query = query.OrderBy(s => s.Reparto);
                            break;
                        case "impianto":
                            query = query.OrderBy(s => s.Impianto);
                            break;
                        case "matricola":
                            query = query.OrderBy(s => s.Matricola);
                            break;
                        case "stato":
                            query = query.OrderBy(s => s.Stato);
                            break;
                        case "modello":
                            query = query.OrderBy(s => s.Modello);
                            break;
                        case "scadenza taratura":
                            query = query.OrderBy(s => s.ScadenzaTaratura);
                            break;
                    }
                }
                else
                {
                    switch (sortOrder.ToLower().Trim())
                    {

                        case "area":
                            query = query.OrderByDescending(s => s.Area);
                            break;
                        case "reparto":
                            query = query.OrderByDescending(s => s.Reparto);
                            break;
                        case "impianto":
                            query = query.OrderByDescending(s => s.Impianto);
                            break;
                        case "matricola":
                            query = query.OrderByDescending(s => s.Matricola);
                            break;
                        case "stato":
                            query = query.OrderByDescending(s => s.Stato);
                            break;
                        case "modello":
                            query = query.OrderByDescending(s => s.Modello);
                            break;
                        case "scadenza taratura":
                            query = query.OrderByDescending(s => s.ScadenzaTaratura);
                            break;
                    }
                }
            }


            var valvole = await query.AsNoTracking().ToListAsync();

            if (!User.IsInRole("Admin") && !User.IsInRole("Manager"))
            {
                List<Valvola> valvolefiltrata = new List<Valvola>();
                foreach (Valvola valvola in valvole)
                {

                    if (ControlloUtente(valvola))
                    {
                        valvolefiltrata.Add(valvola);
                    }
                    else
                    {

                    }
                }
                valvole = valvolefiltrata;
            }

            int pageSize = 10;
            return View(PaginatedList<Valvola>.Create(valvole, pageNumber ?? 1, pageSize));
        }

        
        // GET: Valvole/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valvola = await _context.Valvole.Include(d => d.Files).Include(b => b.TecnicoUltimoAggiornamento).Include(p => p.PressioneMontata).ThenInclude(g => g.Pressione).FirstOrDefaultAsync(m => m.Id == id);

            if ((valvola == null) || !ControlloUtente(valvola))
            {
                return NotFound();
            }

            return View(valvola);
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

        // GET: Valvole/Edit/5
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> Edit(int? id)
        {
            Valvola valvola;
            if (id == null)
            {
                //return NotFound();
                valvola = new Valvola();
            } else
            {
                valvola = await _context.Valvole.Include(d => d.Files).Include(p=>p.PressioneMontata).ThenInclude(g => g.Pressione).FirstOrDefaultAsync(m => m.Id == id);
            }
            PopulatePressioniDropDownList();
            return View(valvola);
        }

        // POST: Valvole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,Manager,RW")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Modello,PressioneTaratura,VerificaTaratura,ScadenzaTaratura,Area,Reparto,Impianto,Matricola,Costruttore,Stato,Note")] Valvola valvola, [Bind("AggiungiPressione")] string AggiungiPressione, [Bind("operationType")] string operationType)
        {

            if (ModelState.IsValid && ControlloUtente(valvola))
            {
                if ((id == null) || (id < 1))
                {

                    Valvola nuovavalvola = new Valvola();

                    _context.Add(nuovavalvola);
                    await _context.SaveChangesAsync();
                    id = nuovavalvola.Id;


                }
                Valvola original = _context.Valvole.Include(d => d.Files).Include(p=>p.PressioneMontata).Single(x => x.Id == id);
                if (await this.TryUpdateModelAsync(original))
                {
                    try
                    {
                        if (original.DataCreazione.Year < 2019) original.DataCreazione = DateTime.Now;
                        original.DataAggiornamento = DateTime.Now;
                        if (original.Area != null) original.Area = original.Area.ToUpper().Trim();
                        if (original.Impianto != null) original.Impianto = original.Impianto.ToUpper().Trim();
                        if (original.Reparto != null) original.Reparto = original.Reparto.ToUpper().Trim();
                        if (original.Stato != null) original.Stato = original.Stato.ToUpper().Trim();


                        ApplicationUser user2 = await userManager.FindByNameAsync(User.Identity.Name);
                        original.TecnicoUltimoAggiornamento = user2;

                        if (!String.IsNullOrEmpty(AggiungiPressione))
                        {
                            bool presstrovata = false;
                            if (original.PressioneMontata != null)
                            {


                                foreach (var item in original.PressioneMontata)
                                {
                                    if (item.Pressione.Matricola == AggiungiPressione.Trim())
                                    {

                                        presstrovata = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                original.PressioneMontata = new List<PressioneValvola>();

                            }

                            if (!presstrovata)
                            {


                                Pressione pressione = await _context.Pressioni.FirstOrDefaultAsync(m => m.Matricola == AggiungiPressione.Trim());
                                if (pressione != null)
                                {
                                    PressioneValvola pressioneValvola = new PressioneValvola
                                    {
                                        PressioneID = pressione.Id,
                                        ValvolaID = original.Id
                                    };

                                    _context.Add(pressioneValvola);
                                    original.PressioneMontata.Add(pressioneValvola);
                                    pressione.TecnicoUltimoAggiornamento = user2;
                                    pressione.DataAggiornamento = DateTime.Now;

                                }

                            }

                        }

                        //_context.Update(valvola);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ValvolaExists(original.Id))
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
            return View(valvola);
        }
                [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> DissociaPressione(int id, int pressioneid)
        {

            Valvola original = _context.Valvole.Include(d => d.Files).Include(p => p.PressioneMontata).Single(x => x.Id == id);
            Pressione pressione = _context.Pressioni.Single(m => m.Id == pressioneid);
            if (original != null && pressione != null && ControlloUtente(pressione))
            {
                
                try
                {
                    ApplicationUser user2 = await userManager.FindByNameAsync(User.Identity.Name);
                    original.TecnicoUltimoAggiornamento = user2;
                    original.DataAggiornamento = DateTime.Now;
                    pressione.TecnicoUltimoAggiornamento = user2;
                    pressione.DataAggiornamento = DateTime.Now;
                    foreach (var item in original.PressioneMontata)
                    {
                        if (item.Pressione.Id == pressioneid)
                        {
                            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>pressione trovata");
                            original.PressioneMontata.Remove(item);
                            
                            await _context.SaveChangesAsync();
                            break;
                        }
                    }

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
        // GET: Valvole/Delete/5
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valvola = await _context.Valvole
                .FirstOrDefaultAsync(m => m.Id == id);
            if ((valvola == null) || !ControlloUtente(valvola))
            {
                return NotFound();
            }

            return View(valvola);
        }

        // POST: Valvole/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Manager,RW")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var valvola = await _context.Valvole.Include(d => d.Files).Include(p => p.PressioneMontata).FirstOrDefaultAsync(m => m.Id == id);
            if ((valvola == null) || !ControlloUtente(valvola))
            {
                return NotFound();
            }
            
            foreach (FileDescription file in valvola.Files) _context.FileDescriptions.Remove(file);
            valvola.PressioneMontata.Clear();
            _context.Valvole.Remove(valvola);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValvolaExists(int id)
        {
            var valvola = _context.Valvole.FirstOrDefault(m => m.Id == id);

            if ((valvola == null) || !ControlloUtente(valvola))
            {
                return false;
            }
            return true;

            //return _context.Valvole.Any(e => e.Id == id);
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
        [HttpPost]
        public bool ControlloUtente(Valvola valvola)
        {
            bool controllo = false;
            if (valvola != null)
            {
                if (!User.IsInRole("Admin") && !User.IsInRole("Manager"))
                {
                    if ((valvola.Area != null) &&
                           User.IsInRole(valvola.Area.ToUpper().Trim()) &&
                           (valvola.Impianto != null) &&
                           User.IsInRole(valvola.Impianto.ToUpper().Trim()) &&
                           (valvola.Reparto != null) &&
                           User.IsInRole(valvola.Reparto.ToUpper().Trim()))
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

        // GET: FileDescriptions/Create
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> Upload(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valvola = await _context.Valvole.Include(d => d.Files).FirstOrDefaultAsync(m => m.Id == id);
            if ((valvola == null) || !ControlloUtente(valvola))
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

                var valvola = await _context.Valvole.Include(d => d.Files).FirstOrDefaultAsync(m => m.Id == fileDescriptionShort.Id);
                if ((valvola == null) || !ControlloUtente(valvola))
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
                        file.Valvola = valvola;
                        ApplicationUser user2 = await userManager.FindByNameAsync(User.Identity.Name);
                        valvola.TecnicoUltimoAggiornamento = user2;
                        if (valvola.DataCreazione.Year < 2019) valvola.DataCreazione = DateTime.Now;
                        valvola.DataAggiornamento = DateTime.Now;

                        //_context.Add(file);
                        if (valvola.Files == null)
                        {
                            valvola.Files = new List<FileDescription>();
                        }

                        valvola.Files.Add(file);


                        
                        valvola.TecnicoUltimoAggiornamento = user2;
                        valvola.DataAggiornamento = DateTime.Now;
                        _context.Update(valvola);

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
                fileDescription.Valvola.TecnicoUltimoAggiornamento = user2;
                if (fileDescription.Valvola.DataCreazione.Year < 2019) fileDescription.Valvola.DataCreazione = DateTime.Now;
                fileDescription.Valvola.DataAggiornamento = DateTime.Now;
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
                return RedirectToAction(nameof(Edit), new { fileDescription.Valvola.Id });
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
            return RedirectToAction(nameof(Edit), new { fileDescription.Valvola.Id });
        }







    }
}
