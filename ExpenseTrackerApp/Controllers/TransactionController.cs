using ExpenseTrackerApp.Data;
using ExpenseTrackerApp.Models;
using ExpenseTrackerApp.Services.DropdownService;
using ExpenseTrackerApp.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTrackerApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IDropdownService _dropdownService;
        private readonly AppDbContext? _context;

        public TransactionController(ITransactionService transactionService, IDropdownService dropdownService, AppDbContext? context)
        {
            _transactionService = transactionService;
            _dropdownService = dropdownService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _transactionService.GetAllTransactionsAsync();
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            var categoryDropdown = await _dropdownService.GetCategoryDropdownValues();
            ViewBag.Categories = new SelectList(categoryDropdown.Categories, "Id", "IconAndName");

            var transaction = new Transaction();
            return View(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                var categoryDropdown = await _dropdownService.GetCategoryDropdownValues();
                ViewBag.Categories = new SelectList(categoryDropdown.Categories, "Id", "IconAndName");
                return View(nameof(Create), transaction);
            }
            await _transactionService.AddNewTransactionAsync(transaction);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Transaction transaction)
        {
            if (id != transaction.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(transaction);
            }

            await _transactionService.UpdateTransactionAsync(transaction);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _transactionService.DeleteTransactionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
