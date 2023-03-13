using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NotesWeb.Controllers
{
    [Authorize]
    public class AuditHistoryController : Controller
    {
        private readonly IAuditLogRepository auditRepo;

        public AuditHistoryController(IAuditLogRepository auditRepo)
        {
            this.auditRepo = auditRepo;
        }

        // GET: AuditHistoryController
        public async Task<IActionResult> Index()
        {
            var Audits = await auditRepo.GetAllAuditsAsync("note");

            return View(Audits);
        }
    }
}
