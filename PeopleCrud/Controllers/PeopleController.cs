using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleCrud.Data;
using PeopleCrud.Model;
using Microsoft.AspNetCore.Authorization;

namespace PeopleCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires authentication for all actions in this controller
    public class PeopleController : ControllerBase
    {
        private readonly PeopleCrudContext _context;

        public PeopleController(PeopleCrudContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        [Authorize(Roles = "Admin,Contributor,Reader")] // Requires specific roles to access this action
        public async Task<ActionResult<IEnumerable<People>>> GetPeople()
        {
            if (_context.People == null)
            {
                return NotFound(); // Returns a 404 response if the "People" entity set is null
            }
            return await _context.People.ToListAsync(); // Retrieves and returns a list of people
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Contributor,Reader")] // Requires specific roles to access this action
        public async Task<ActionResult<People>> GetPeople(Guid id)
        {
            if (_context.People == null)
            {
                return NotFound(); // Returns a 404 response if the "People" entity set is null
            }
            var people = await _context.People.FindAsync(id); // Retrieves a specific person by their ID

            if (people == null)
            {
                return NotFound(); // Returns a 404 response if the person is not found
            }

            return people; // Returns the retrieved person
        }

        // PUT: api/People/5
        [Authorize(Roles = "Admin,Contributor")] // Requires specific roles to access this action
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeople(Guid id, People people)
        {
            if (id != people.Id)
            {
                return BadRequest(); // Returns a 400 response if the provided ID doesn't match the person's ID
            }

            _context.Entry(people).State = EntityState.Modified; // Marks the person as modified

            try
            {
                await _context.SaveChangesAsync(); // Saves the changes to the database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeopleExists(id))
                {
                    return NotFound(); // Returns a 404 response if the person doesn't exist
                }
                else
                {
                    throw; // Throws an exception if there's a concurrency issue
                }
            }

            return NoContent(); // Returns a 204 response indicating success without any content
        }

        // POST: api/People
        [HttpPost]
        [Authorize(Roles = "Admin,Contributor")] // Requires specific roles to access this action
        public async Task<ActionResult<People>> PostPeople(People people)
        {
            if (_context.People == null)
            {
                return Problem("Entity set 'PeopleCrudContext.People' is null."); // Returns an error response if the "People" entity set is null
            }
            _context.People.Add(people); // Adds a new person to the database
            await _context.SaveChangesAsync(); // Saves the changes to the database

            return CreatedAtAction("GetPeople", new { id = people.Id }, people); // Returns a 201 response with the created person
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Requires the "Admin" role to access this action
        public async Task<IActionResult> DeletePeople(Guid id)
        {
            if (_context.People == null)
            {
                return NotFound(); // Returns a 404 response if the "People" entity set is null
            }
            var people = await _context.People.FindAsync(id); // Retrieves a specific person by their ID
            if (people == null)
            {
                return NotFound(); // Returns a 404 response if the person is not found
            }

            _context.People.Remove(people); // Removes the person from the database
            await _context.SaveChangesAsync(); // Saves the changes to the database

            return NoContent(); // Returns a 204 response indicating success without any content
        }

        private bool PeopleExists(Guid id)
        {
            return (_context.People?.Any(e => e.Id == id)).GetValueOrDefault(); // Checks if a person with the given ID exists in the database
        }
    }
}
