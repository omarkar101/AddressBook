using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AddressBook.Data;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class PeopleController : Controller
    {
        private readonly AddressBookContext _context;

        public PeopleController(AddressBookContext context)
        {
            _context = context;
        }

        
        public IActionResult Create(int OrgId)
        {
            ViewData["OrgId"] = OrgId;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int OrgId, [Bind("Id,Name,PhoneNumber,Address,OrganizationId")] Person person)
        {
            person.OrganizationId = OrgId;
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Organizations", new { id = OrgId });
            }
            return View(person);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int OrgId, [Bind("Id,Name,PhoneNumber,Address")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }
            person.OrganizationId = OrgId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Organizations",  new{id = OrgId});
            }
            return View(person);
        }
        
        public async Task<IActionResult> Delete(int? id, int OrgId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            ViewData["OrgId"] = OrgId;
            return View(person);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int OrgId)
        {
            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Organizations", new { id = OrgId });
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
