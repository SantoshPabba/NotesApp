using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesWebApp.Models;
using System.Diagnostics;

namespace NotesWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotesRepository notesRepo;

        public HomeController(ILogger<HomeController> logger, INotesRepository notesRepo)
        {
            _logger = logger;
            this.notesRepo = notesRepo;
        }

        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Note> model = await notesRepo.GetAllNotesAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            try
            {
                note.DateCreated = DateTime.Now;
                await notesRepo.AddNoteAsync(note, "user");
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetNoteByID(int id)
        {
            try
            {
                return Ok(await notesRepo.GetNoteByIdAsync(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNote([FromBody] Note note)
        {
            try
            {
                note.DateModified = DateTime.Now;
                await notesRepo.UpdateNoteAsync(note, "user");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNote(int id)
        {
            try
            {
                await notesRepo.DeleteNoteByIDAsync(id, "user");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}