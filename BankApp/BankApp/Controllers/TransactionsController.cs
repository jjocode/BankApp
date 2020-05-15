using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankApp.Data;
using BankApp.Models;

namespace BankApp.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly BankAppContext _context;

        public TransactionsController(BankAppContext context)
        {
            _context = context;
        }

        #region GET
        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            AccountsDropDownList();
            return View();
        }

        #endregion GET

        #region POST
        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FromAccount,ToAccount,TransactionDate,TransactionAmount,FromBalance,ToBalance")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var fromAcct = GetAccount(transaction.FromAccount);                
                if (fromAcct.Balance < transaction.TransactionAmount)
                {
                    return RedirectToAction(nameof(Create));
                }

                var toAcct = GetAccount(transaction.ToAccount);

                transaction.TransactionDate = DateTime.Now;
                transaction.FromBalance = fromAcct.Balance;
                transaction.ToBalance = toAcct.Balance;

                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FromAccount,ToAccount,TransactionDate,TransactionAmount,FromBalance,ToBalance")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
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
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion POST

        #region Helper

        private void AccountsDropDownList(object selectedAccount = null)
        {
            var accountsQuery = from a in _context.Accounts
                                orderby a.Name
                                select a;
            ViewBag.FromAccount = new SelectList(accountsQuery.AsNoTracking(), "Id", "Name", selectedAccount);
            ViewBag.ToAccount = new SelectList(accountsQuery.AsNoTracking(), "Id", "Name", selectedAccount);
        }

        private Account GetAccount(int id)
        {
            var account = _context.Accounts.Find(id);
            return account;
        }

        //private void ToAccountDropDownList(object selectedAccount = null)
        //{
        //    var accountsQuery = from a in _context.Accounts
        //                        orderby a.Name
        //                        select a;
        //    ViewBag.ToAccount = new SelectList(accountsQuery.AsNoTracking(), "Name", "Name", selectedAccount);
        //}

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }

        #endregion Helper
    }
}
