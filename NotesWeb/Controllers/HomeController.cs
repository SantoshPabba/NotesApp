using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesWeb.Models;
using System.Diagnostics;

namespace NotesWeb.Controllers
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
                note.DateCreated = DateTime.UtcNow;
                await notesRepo.AddNoteAsync(note, User.Identity.Name);
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
                note.DateModified = DateTime.UtcNow;
                await notesRepo.UpdateNoteAsync(note, User.Identity.Name);
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
                await notesRepo.DeleteNoteByIDAsync(id, User.Identity.Name);
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