using Microsoft.AspNetCore.Mvc;
using Bibliotheque.MVC.Models;
using Bibliotheque.MVC.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bibliotheque.MVC.Controllers
{
    public class EmpruntsController : Controller
    {
        private readonly IEmpruntsService _empruntsService;
        private readonly ILivresService _livresService;
        private readonly IUsagersService _usagersService;

        public EmpruntsController(IEmpruntsService empruntsService,
            ILivresService livresService, IUsagersService usagersService)
        {
            _empruntsService = empruntsService;
            _livresService = livresService;
            _usagersService = usagersService;
        }

        // GET: Emprunts
        public async Task<ActionResult> Index(bool filtre)
        {
            var emprunts = await _empruntsService.ObtenirTout();
            ViewData["filtre"] = filtre;

            // Afficher les emprunts en cours
            if (filtre)
            {
                emprunts = emprunts.Where(e => e.DateRetour == null).ToList();
            }

            return View(emprunts);
        }

        // GET: Emprunts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var emprunt = await _empruntsService.ObtenirSelonId(id);

                if (emprunt == null)
                {
                    return NotFound();
                }
                return View(emprunt);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: Emprunts/Create
        public async Task<ActionResult> Create()
        {
            await ChargerSelectList();
            return View();
        }

        // POST: Emprunts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequeteEmprunt requeteEmprunt)
        {
            await ChargerSelectList();
            try
            {
                if (ModelState.IsValid)
                {
                    await _empruntsService.Ajouter(requeteEmprunt);
                    return RedirectToAction(nameof(Index));
                }
                
                return View(requeteEmprunt);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(requeteEmprunt);
            }
        }

        // GET: Emprunts/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Emprunt emprunt = await _empruntsService.ObtenirSelonId(id);

                if (emprunt == null) 
                { 
                    return NotFound(); 
                }

                return View(emprunt);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: Emprunts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Emprunt emprunt)
        {
            if (ModelState.IsValid)
            {
                await _empruntsService.Modifier(emprunt);
                return RedirectToAction(nameof(Index));
            }

            return View(emprunt);
        }

        // GET: Emprunts/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Emprunt emprunt = await _empruntsService.ObtenirSelonId(id);

                if (emprunt == null)
                {
                    return NotFound();
                }

                return View(emprunt);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: Emprunts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Emprunt emprunt)
        {
            Emprunt empruntASupprimer = await _empruntsService.ObtenirSelonId(id);

            try
            {
                await _empruntsService.Supprimer(empruntASupprimer);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(empruntASupprimer);
            }
        }

        // Charger les dropdown lists
        private async Task ChargerSelectList()
        {
            ViewData["LivreID"] = new SelectList(await _livresService.ObtenirTout(), "ID", "Titre");
            ViewData["UsagerID"] = new SelectList(await _usagersService.ObtenirTout(), "ID", "Nom");
        }
    }
}
