using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class UserExercisesController : Controller
{
    private readonly AppDbContext _context;

    public UserExercisesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userExercises = _context.UserExercises
            .Include(ue => ue.User)
            .Include(ue => ue.Exercise);
        return View(await userExercises.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userExercise = await _context.UserExercises
            .Include(ue => ue.User)
            .Include(ue => ue.Exercise)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (userExercise == null)
        {
            return NotFound();
        }

        return View(userExercise);
    }

    public IActionResult Create()
    {
        ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "Name");
        ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,UserID,ExerciseID,ExerciseDate,Duration")] UserExercise userExercise)
    {
        if (ModelState.IsValid)
        {
            _context.Add(userExercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "Name");
        ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
        return View(userExercise);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userExercise = await _context.UserExercises.FindAsync(id);
        if (userExercise == null)
        {
            return NotFound();
        }
        ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "Name", userExercise.ExerciseID);
        ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", userExercise.UserID);
        return View(userExercise);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,ExerciseID,ExerciseDate,Duration")] UserExercise userExercise)
    {
        if (id != userExercise.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(userExercise);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExerciseExists(userExercise.ID))
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
        ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "Name", userExercise.ExerciseID);
        ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", userExercise.UserID);
        return View(userExercise);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userExercise = await _context.UserExercises
           
            .FirstOrDefaultAsync(m => m.ID == id);
        if (userExercise == null)
        {
            return NotFound();
        }

        return View(userExercise);
    }


    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userExercise = await _context.UserExercises.FindAsync(id);
        _context.UserExercises.Remove(userExercise);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UserExerciseExists(int id)
    {
        return _context.UserExercises.Any(e => e.ID == id);
    }
}
