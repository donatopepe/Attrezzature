using AttrOleo.Data;
using AttrOleo.Models;
using AttrOleo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AttrOleo.Controllers
{
    public class PressioniController : Controller
    {

        private readonly IFileRepository _fileRepository;
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public PressioniController(IFileRepository fileRepository, ApplicationDbContext context, RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg)
        {
            _fileRepository = fileRepository;
            _context = context;
            userManager = userMrg;
            roleManager = roleMgr;
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
        // GET: Pressiones
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


            var query = from m in _context.Pressioni
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
                    case "ubicazione":
                        query = query.Where(s => s.Ubicazione.Contains(searchString.ToUpper().Trim()));
                        break;
                    case "numero seriale":
                        query = query.Where(s => s.SerialNoFabbrica.Contains(searchString.ToUpper().Trim()));
                        break;
                    case "matricola":
                        query = query.Where(s => s.Matricola.Contains(searchString.ToUpper().Trim()));
                        break;
                    case "stato":
                        query = query.Where(s => s.Stato.Contains(searchString.ToUpper().Trim()));
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
                        case "ubicazione":
                            query = query.OrderBy(s => s.Ubicazione);
                            break;
                        case "numero seriale":
                            query = query.OrderBy(s => s.SerialNoFabbrica);
                            break;
                        case "matricola":
                            query = query.OrderBy(s => s.Matricola);
                            break;
                        case "stato":
                            query = query.OrderBy(s => s.Stato);
                            break;
                        case "scadenza funzionamento":
                            query = query.OrderBy(s => s.ScadenzaFunzionalita);
                            break;
                        case "scadenza integrità":
                            query = query.OrderBy(s => s.ScadenzaIntegrita);
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
                        case "ubicazione":
                            query = query.OrderByDescending(s => s.Ubicazione);
                            break;
                        case "numero seriale":
                            query = query.OrderByDescending(s => s.SerialNoFabbrica);
                            break;
                        case "matricola":
                            query = query.OrderByDescending(s => s.Matricola);
                            break;
                        case "stato":
                            query = query.OrderByDescending(s => s.Stato);
                            break;
                        case "scadenza funzionamento":
                            query = query.OrderByDescending(s => s.ScadenzaFunzionalita);
                            break;
                        case "scadenza integrità":
                            query = query.OrderByDescending(s => s.ScadenzaIntegrita);
                            break;
                    }
                }
            }

            //if (!User.IsInRole("Admin") && !User.IsInRole("Manager"))
            //{
            //    var user = await userManager.FindByNameAsync(User.Identity.Name);
            //    var ruoli = await userManager.GetRolesAsync(user);
            //    foreach (var ruolo in ruoli)
            //    {
            //        Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>" + ruolo);
            //        query = query.Where(s => s.Area.Equals(ruolo.ToUpper().Trim()) ||
            //        s.Impianto.Equals(ruolo.ToUpper().Trim()) ||
            //        s.Reparto.Equals(ruolo.ToUpper().Trim()));
            //    }
            //}

            var pressioni = await query.AsNoTracking().ToListAsync();

            if (!User.IsInRole("Admin") && !User.IsInRole("Manager"))
            {
                List<Pressione> pressionefiltrata = new List<Pressione>();
                foreach (Pressione pressione in pressioni)
                {

                    if (ControlloUtente(pressione))
                    {
                        pressionefiltrata.Add(pressione);
                    }
                    else
                    {

                    }
                }
                pressioni = pressionefiltrata;
            }

            int pageSize = 10;
            return View(PaginatedList<Pressione>.Create(pressioni, pageNumber ?? 1, pageSize));
        }

        // GET: Pressiones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pressione = await _context.Pressioni.Include(d => d.Files).Include(a => a.TecnicoReferente).Include(b => b.TecnicoUltimoAggiornamento).Include(d => d.DiscoMontato).Include(v => v.ValvolaMontata).ThenInclude(g => g.Valvola).FirstOrDefaultAsync(m => m.Id == id);
            if ((pressione == null) || !ControlloUtente(pressione))
            {
                return NotFound();
            }

            //pressione.Files = await _fileRepository.GetAllFilesPress(id);
            return View(pressione);
        }

        private void PopulateUserDropDownList(string selectedUser)
        {
            var Query = userManager.Users;
            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>" + selectedUser);
            ViewBag.UserName = new SelectList(Query.AsNoTracking(), "Name", "Name", selectedUser);
        }
        private void PopulateDischiDropDownList(string discomontato)
        {
            var Query = _context.Dischi.AsNoTracking();
            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>" + discomontato+"<<<<<<<<<<<<<<<<<<<<<<<<");
            var lista = new List<SelectListItem>();
            foreach (var item in Query) {
                lista.Add(new SelectListItem(item.LottoNo, item.LottoNo,(discomontato == item.LottoNo)));
            }
            //SelectList items = new SelectList(Query, "LottoNo", "LottoNo", discomontato);

            ViewBag.ListaDischi = lista;// items;
        }

        private void PopulateValvoleDropDownList()
        {
            var Query = _context.Valvole;
            var lista = new List<SelectListItem>();
            foreach (var item in Query)
            {
                if (ControlloUtente(item)) lista.Add(new SelectListItem(item.Matricola, item.Matricola));
            }

            ViewBag.Valvole = lista;// new SelectList(Query.AsNoTracking(), "Matricola", "Matricola");
        }

         // GET: Pressiones/Edit/5
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> Edit(int? id)
        {
            Pressione pressione;
            if (id == null)
            {
                // return NotFound();
                pressione = new Pressione();
            }
            else
            {

                pressione = await _context.Pressioni.AsNoTracking().Include(d => d.Files).Include(a => a.TecnicoReferente).Include(d => d.DiscoMontato).Include(v => v.ValvolaMontata).ThenInclude(g => g.Valvola).FirstOrDefaultAsync(m => m.Id == id);
            }

            if (pressione.TecnicoReferente != null)
            {
                PopulateUserDropDownList(pressione.TecnicoReferente.Name);
            }
            else
            {
                PopulateUserDropDownList("");
            }
            if (pressione.DiscoMontato != null)
            {
                PopulateDischiDropDownList(pressione.DiscoMontato.LottoNo);
            }
            else
            {
                PopulateDischiDropDownList("");
            }

            PopulateValvoleDropDownList();
            return View(pressione);
        }


        // POST: Pressiones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,Manager,RW")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("SerialNoFabbrica,Volume,Fluido,Descrizione,Ubicazione,Norma,VerificaPrimoImpianto,PrimaVerificaFunzionalita,VerificaFunzionalita,VerificaIntegrita,ScadenzaFunzionalita,ScadenzaIntegrita,Area,Reparto,Impianto,Matricola,Costruttore,Stato,MassimaPressione,Note")] Pressione pressione, [Bind("TecnicoReferente")] string TecnicoReferente, [Bind("LottoNo")] string LottoNo, [Bind("AggiungiValvola")] string AggiungiValvola, [Bind("operationType")] string operationType)
        {


            if (ModelState.IsValid && ControlloUtente(pressione))
            {

                if ((id == null) || (id < 1))
                {

                    Pressione nuovapressione = new Pressione();
                    
                    _context.Add(nuovapressione);
                    await _context.SaveChangesAsync();
                    id = nuovapressione.Id;


                }

                Pressione original = _context.Pressioni.Include(d => d.Files).Include(a => a.TecnicoReferente).Include(d => d.DiscoMontato).Include(v => v.ValvolaMontata).ThenInclude(g => g.Valvola).Single(x => x.Id == id);


                if (await this.TryUpdateModelAsync(original))
                {

                    try
                    {
                        ApplicationUser user2 = await userManager.FindByNameAsync(User.Identity.Name);
                        original.TecnicoUltimoAggiornamento = user2;

                        if (original.DataCreazione.Year < 2019) original.DataCreazione = DateTime.Now;
                        original.DataAggiornamento = DateTime.Now;
                        if (original.Area != null) original.Area = original.Area.ToUpper().Trim();
                        if (original.Impianto != null) original.Impianto = original.Impianto.ToUpper().Trim();
                        if (original.Reparto != null) original.Reparto = original.Reparto.ToUpper().Trim();
                        if (original.Ubicazione != null) original.Ubicazione = original.Ubicazione.ToUpper().Trim();
                        if (original.Stato != null) original.Stato = original.Stato.ToUpper().Trim();
                        if (original.PrimaVerificaFunzionalita == null)
                        {
                            if (original.VerificaFunzionalita != null)
                            {

                                original.PrimaVerificaFunzionalita = original.VerificaFunzionalita;
                            }
                        }

                        Disco disco = null;
                        if (!String.IsNullOrEmpty(LottoNo))
                        {
                            disco = await _context.Dischi.FirstOrDefaultAsync(m => m.LottoNo == LottoNo.Trim());

                        }
                        original.DiscoMontato = disco;

                        ApplicationUser user = null;
                        if (!String.IsNullOrEmpty(TecnicoReferente))
                        {
                            user = await userManager.FindByNameAsync(TecnicoReferente);
                        }
                        original.TecnicoReferente = user;


                        
                        
                        
                        if (!String.IsNullOrEmpty(AggiungiValvola))
                        {
                            bool valvtrovata = false;
                            
                            if (original.ValvolaMontata != null)
                            {


                                foreach (var item in original.ValvolaMontata)
                                {
                                    if (item.Valvola.Matricola == AggiungiValvola.Trim())
                                    {

                                        valvtrovata = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                original.ValvolaMontata = new List<PressioneValvola>();
                                
                            }

                            if (!valvtrovata)
                            {


                                Valvola valvola = await _context.Valvole.FirstOrDefaultAsync(m => m.Matricola == AggiungiValvola.Trim());
                                if (valvola!=null)
                                {
                                    PressioneValvola pressioneValvola = new PressioneValvola
                                    {
                                        PressioneID = original.Id,
                                        ValvolaID = valvola.Id
                                    };

                                    //valvola.TecnicoUltimoAggiornamento = user2;
                                    //valvola.DataAggiornamento = DateTime.Now;
                                    _context.Add(pressioneValvola);
                                    original.ValvolaMontata.Add(pressioneValvola);
                                }
                                
                            }
                        }



                        //_context.Update(pressione);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PressioneExists(original.Id))
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
                    } else
                    if (operationType != "AddValvola")
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }

            return RedirectToAction(nameof(Edit), id);
            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>refer:"+ Request.Headers["Referer"].ToString());
            //return Redirect(Request.Headers["Referer"].ToString());
        }





        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> DissociaValvola(int id, int valvolaid)
        {

            Pressione original = _context.Pressioni.Include(d => d.Files).Include(a => a.TecnicoReferente).Include(d => d.DiscoMontato).Include(v => v.ValvolaMontata).ThenInclude(g => g.Valvola).Single(x => x.Id == id);
            Valvola valvola = _context.Valvole.Single(m => m.Id == valvolaid);
            if (original != null && valvola != null && ControlloUtente(original) && ControlloUtente(valvola))
            {
                
                try
                {
                    ApplicationUser user2 = await userManager.FindByNameAsync(User.Identity.Name);
                    original.TecnicoUltimoAggiornamento = user2;
                    original.DataAggiornamento = DateTime.Now;
                    //valvola.TecnicoUltimoAggiornamento = user2;
                    //valvola.DataAggiornamento = DateTime.Now;
                    foreach (var item in original.ValvolaMontata)
                    {
                        if (item.Valvola.Id == valvolaid)
                        {
                           // Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>valvola trovata");
                            original.ValvolaMontata.Remove(item);
                            await _context.SaveChangesAsync();
                            break;
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PressioneExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }

            //return RedirectToAction(nameof(Edit), new { id });
            return Redirect(Request.Headers["Referer"].ToString());
        }






        // GET: Pressiones/Delete/5
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pressione = await _context.Pressioni
                .FirstOrDefaultAsync(m => m.Id == id);
            if ((pressione == null) || !ControlloUtente(pressione))
            {
                return NotFound();
            }

            return View(pressione);
        }

        // POST: Pressiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Manager,RW")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var pressione = await _context.Pressioni.FindAsync(id);
            var pressione = await _context.Pressioni.Include(d => d.Files).Include(v => v.ValvolaMontata).FirstOrDefaultAsync(m => m.Id == id);
            if ((pressione == null) || !ControlloUtente(pressione))
            {
                return NotFound();
            }

            foreach (FileDescription file in pressione.Files) _context.FileDescriptions.Remove(file);
            pressione.ValvolaMontata.Clear();

            _context.Pressioni.Remove(pressione);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PressioneExists(int id)
        {
            var pressione = _context.Pressioni.FirstOrDefault(m => m.Id == id);

            if ((pressione == null) || !ControlloUtente(pressione))
            {
                return false;
            }
            return true;

            //return _context.Pressioni.Any(e => e.Id == id);
        }


        // GET: FileDescriptions/Create
        [Authorize(Roles = "Admin,Manager,RW")]
        public async Task<IActionResult> Upload(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pressione = await _context.Pressioni.Include(d => d.Files).FirstOrDefaultAsync(m => m.Id == id);
            if ((pressione == null) || !ControlloUtente(pressione))
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

                var pressione = await _context.Pressioni.Include(d => d.Files).FirstOrDefaultAsync(m => m.Id == fileDescriptionShort.Id);
                if ((pressione == null) || !ControlloUtente(pressione))
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
                        file.Pressione = pressione;
                        ApplicationUser user2 = await userManager.FindByNameAsync(User.Identity.Name);
                        pressione.TecnicoUltimoAggiornamento = user2;
                        if (pressione.DataCreazione.Year < 2019) pressione.DataCreazione = DateTime.Now;
                        pressione.DataAggiornamento = DateTime.Now;

                        //_context.Add(file);
                        if (pressione.Files == null)
                        {
                            pressione.Files = new List<FileDescription>();
                        }

                        pressione.Files.Add(file);


                        
                        pressione.TecnicoUltimoAggiornamento = user2;
                        pressione.DataAggiornamento = DateTime.Now;
                        _context.Update(pressione);

                        await _context.SaveChangesAsync();

                        Console.WriteLine("Dimensione del file OK");

                        return RedirectToAction(nameof(Edit), new { fileDescriptionShort.Id } );

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
            ApplicationUser user2 = await userManager.FindByNameAsync(User.Identity.Name);
            fileDescription.Pressione.TecnicoUltimoAggiornamento = user2;
            if (fileDescription.Pressione.DataCreazione.Year < 2019) fileDescription.Pressione.DataCreazione = DateTime.Now;
            fileDescription.Pressione.DataAggiornamento = DateTime.Now;

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
               // var fileDescription = await _fileRepository.Include(d => d.Pressioni).FirstOrDefaultAsync(m => m.Id == fileDescriptionNoFile.Id);

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

                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Edit), new { fileDescription.Pressione.Id });
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
            return RedirectToAction(nameof(Edit), new { fileDescription.Pressione.Id });
        }

    }
}
