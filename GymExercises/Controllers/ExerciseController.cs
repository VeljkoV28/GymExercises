using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class ExercisesController : Controller
{
    private readonly AppDbContext _context;

    public ExercisesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Exercises.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(m => m.ID == id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Name,Description,MuscleGroup,Equipment,DifficultyLevel")] Exercise exercise)
    {
        if (ModelState.IsValid)
        {
            _context.Add(exercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(exercise);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var exercise = await _context.Exercises.FindAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }
        return View(exercise);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,MuscleGroup,Equipment,DifficultyLevel")] Exercise exercise)
    {
        if (id != exercise.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(exercise);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(exercise.ID))
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
        return View(exercise);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(m => m.ID == id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var exercise = await _context.Exercises.FindAsync(id);
        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ExerciseExists(int id)
    {
        return _context.Exercises.Any(e => e.ID == id);
    }
}
