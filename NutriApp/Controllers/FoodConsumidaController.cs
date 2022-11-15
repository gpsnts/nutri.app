using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NutriApp.Data;
using NutriApp.Models;

namespace NutriApp.Controllers
{
    [Authorize]
    public class FoodConsumidaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodConsumidaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FoodConsumida
        public async Task<IActionResult> Index()
        {
            return _context.foodConsumida != null ?
                        View(await _context.foodConsumida.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.foodConsumida'  is null.");
        }

        // GET: FoodConsumida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.foodConsumida == null)
            {
                return NotFound();
            }

            var foodConsumida = await _context.foodConsumida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodConsumida == null)
            {
                return NotFound();
            }

            return View(foodConsumida);
        }

        // GET: FoodConsumida/Create
        public IActionResult Create(int id)
        {
            FoodConsumida foodConsumida = new FoodConsumida();
            List<Food> food =  _context.foods.ToList();
            foodConsumida.ListaDeAlimentos = food;
            foodConsumida.idAlimentacao = id; 
            return View(foodConsumida);
        }

        // POST: FoodConsumida/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,idFood,idAlimentacao,quantidade")] FoodConsumida foodConsumida)
        {
            List<FoodConsumida> foodConsumidaId = _context.foodConsumida.ToList();
            if (foodConsumidaId != null)
            {   if (foodConsumida.Id > 1)
                {
                    foodConsumida.Id = foodConsumidaId[foodConsumidaId.Count - 1].Id;
                    foodConsumida.Id++;
                }
                else
                    foodConsumida.Id = 1;
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(foodConsumida);
                await _context.SaveChangesAsync();                
            }

            List<AlimentacaoDia> alimentacao = _context.alimentacaoDias.ToList();


          
            if (alimentacao != null)
            {
                for(int i = 0; i < alimentacao.Count; i++)
                {
                    if (alimentacao[i].Id == foodConsumida.idAlimentacao)
                    {
                        //calcular total de calorias com base na lista de alimentos vindo na query 
                        List<FoodConsumida> listaFoodConsumida = _context.foodConsumida.Where(x => x.idAlimentacao == foodConsumida.idAlimentacao).ToList();
                        List<Food> listaFood = _context.foods.ToList();

                        decimal? caloriasTotais = 0;
                        decimal? caloriaFood = 0;
                        foreach (var item in listaFoodConsumida)
                        {
                            foreach(var food in listaFood)
                            {
                                caloriaFood = food.gordurasTotais;
                                if(item.idFood == food.Id)
                                {
                                    caloriaFood = (decimal) food.valorEnergetico;
                                    listaFood.Remove(food);
                                    break;
                                }
                            }
                            caloriasTotais = (item.quantidade*caloriaFood) + caloriasTotais;
                        }
                        alimentacao[i].TotalCalorias = caloriasTotais.ToString()+"kcal";
                        _context.Update(alimentacao[i]);
                        _context.SaveChanges();
                        break;
                    }
                      
                }
              
            }
            return View("Index");
        }


        // POST: FoodConsumida/Create
        // GET: FoodConsumida/Create
        public IActionResult ResumoConsumo(int id)
        {
            FoodConsumida foodConsumida = new FoodConsumida();
              foodConsumida.idAlimentacao = id;
            List<FoodConsumida> listaFoodConsumida = _context.foodConsumida.Where(x => x.idAlimentacao == foodConsumida.idAlimentacao).ToList();
            List<Food> listaFood = _context.foods.ToList();
            List<Food> listaAlimentos = new List<Food>();


            foreach (var item in listaFoodConsumida)
            {
                foreach (var food in listaFood)
                {

                    if (item.idFood == food.Id)
                    {
                        for (int i = 0; i < item.quantidade; i++)
                        {
                            listaAlimentos.Add(food);
                        }
                        break;
                    }
                }

            }
            foodConsumida.ListaDeAlimentos = listaAlimentos;
            foodConsumida.idAlimentacao = id;

            return View("ResumoConsumo",foodConsumida);
        }

        // GET: FoodConsumida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.foodConsumida == null)
            {
                return NotFound();
            }

            var foodConsumida = await _context.foodConsumida.FindAsync(id);
            if (foodConsumida == null)
            {
                return NotFound();
            }
            return View(foodConsumida);
        }

        // POST: FoodConsumida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,idFood,idAlimentacao,quantidade")] FoodConsumida foodConsumida)
        {
            if (id != foodConsumida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodConsumida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodConsumidaExists(foodConsumida.Id))
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
            return View(foodConsumida);
        }

        // GET: FoodConsumida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.foodConsumida == null)
            {
                return NotFound();
            }

            var foodConsumida = await _context.foodConsumida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodConsumida == null)
            {
                return NotFound();
            }

            return View(foodConsumida);
        }

        // POST: FoodConsumida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.foodConsumida == null)
            {
                return Problem("Entity set 'ApplicationDbContext.foodConsumida'  is null.");
            }
            var foodConsumida = await _context.foodConsumida.FindAsync(id);
            if (foodConsumida != null)
            {
                _context.foodConsumida.Remove(foodConsumida);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodConsumidaExists(int id)
        {
            return (_context.foodConsumida?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
