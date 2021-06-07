using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dota.Data;
using Entites;
using AutoMapper;
using Dota.Models.Heroes;

namespace Dota.Controllers
{
    public class HeroesController : Controller
    {
        #region Data and Const
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HeroesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion


        #region Actions
        public async Task<IActionResult> Index()
        {
            var heroes = await _context
                                       .Heroes
                                       .Include(heroes => heroes.Items)
                                       .ToListAsync();

            var herosVM = _mapper.Map<List<Hero>, List<HeroesVM>>(heroes);

            return View(herosVM);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hero = await _context.Heroes.FirstOrDefaultAsync(m => m.Id == id);

            if (hero == null)
            {
                return NotFound();
            }

            var heroVM = _mapper.Map<Hero, HeroesVM>(hero);
            

            return View(heroVM);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HeroesVM heroesVM)
        {
            if (ModelState.IsValid)
            {
                var hero = _mapper.Map<HeroesVM, Hero>(heroesVM);
                _context.Add(hero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(heroesVM);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hero = await _context.Heroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound();
            }
            var heroesVM = _mapper.Map<Hero, HeroesVM>(hero);

            return View(heroesVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,HeroesVM heroesVM)
        {
            if (id != heroesVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var hero = _mapper.Map<HeroesVM, Hero>(heroesVM);
                    _context.Update(hero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeroExists(heroesVM.Id))
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
            return View(heroesVM);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hero = await _context.Heroes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hero == null)
            {
                return NotFound();
            }

            return View(hero);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            _context.Heroes.Remove(hero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Private Actions
        private bool HeroExists(int id)
        {
            return _context.Heroes.Any(e => e.Id == id);
        } 
        #endregion
    }
}
