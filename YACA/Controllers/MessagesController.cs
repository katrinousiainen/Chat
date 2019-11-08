using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YACA.Models;

namespace YACA.Controllers
{
    public class MessagesController : Controller
    {
      
            AcademyChatContext ac = new AcademyChatContext();

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var academyChatContext = ac.Message.Include(m => m.FromPerson);
            return View(await academyChatContext.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await ac.Message
                .Include(m => m.FromPerson)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            ViewData["FromPersonId"] = new SelectList(ac.Person, "PersonId", "Description");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,Subject,MessageText,SendTime,FromPersonId,PrivateMessage,ToPersonId,ExpireAt,Category")] Message message)
        {
            if (ModelState.IsValid)
            {
                ac.Add(message);
                await ac.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromPersonId"] = new SelectList(ac.Person, "PersonId", "Description", message.FromPersonId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await ac.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["FromPersonId"] = new SelectList(ac.Person, "PersonId", "Description", message.FromPersonId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,Subject,MessageText,SendTime,FromPersonId,PrivateMessage,ToPersonId,ExpireAt,Category")] Message message)
        {
            if (id != message.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ac.Update(message);
                    await ac.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageId))
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
            ViewData["FromPersonId"] = new SelectList(ac.Person, "PersonId", "Description", message.FromPersonId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await ac.Message
                .Include(m => m.FromPerson)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await ac.Message.FindAsync(id);
            ac.Message.Remove(message);
            await ac.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return ac.Message.Any(e => e.MessageId == id);
        }
    }
}
