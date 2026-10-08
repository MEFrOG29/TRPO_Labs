using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab1.Models;

[Route("api/[controller]")]
[ApiController]
public class LectureMaterialsController : ControllerBase
{
    private readonly AppDbContext _context;
    public LectureMaterialsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/LectureMaterial
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LectureMaterial>>> GetLectureMaterial()
    {
        return await _context.LectureMaterials.ToListAsync();
    }

    // GET: api/LectureMaterial/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LectureMaterial>> GetLectureMaterial(int id)
    {
        var lecturematerial = await _context.LectureMaterials.FindAsync(id);

        if (lecturematerial == null)
        {
            return NotFound();
        }

        return lecturematerial;
    }

    // PUT: api/LectureMaterial/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLectureMaterial(int? id, LectureMaterial lecturematerial)
    {
        if (id != lecturematerial.Id)
        {
            return BadRequest();
        }

        _context.Entry(lecturematerial).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LectureMaterialExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/LectureMaterial
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<LectureMaterial>> PostLectureMaterial(LectureMaterial lecturematerial)
    {
        _context.LectureMaterials.Add(lecturematerial);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLectureMaterial", new { id = lecturematerial.Id }, lecturematerial);
    }

    // DELETE: api/LectureMaterial/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLectureMaterial(int? id)
    {
        var lecturematerial = await _context.LectureMaterials.FindAsync(id);
        if (lecturematerial == null)
        {
            return NotFound();
        }

        _context.LectureMaterials.Remove(lecturematerial);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LectureMaterialExists(int? id)
    {
        return _context.LectureMaterials.Any(e => e.Id == id);
    }
}
