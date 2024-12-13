using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp9.Models;

public class BookController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Список всех книг с сортировкой по дате добавления
    public async Task<IActionResult> Index()
    {
        var books = await _context.Books
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
        return View(books);
    }

    // Форма создания книги
    public IActionResult Create()
    {
        return View();
    }

    // Метод создания книги с проверкой уникальности
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        // Проверка на существование книги с таким же названием и автором
        var existingBook = await _context.Books
       .FirstOrDefaultAsync(b =>
           b.Title.ToLower() == book.Title.ToLower() &&
           b.Author.ToLower() == book.Author.ToLower());

        if (existingBook != null)
        {
            // Используем TempData для передачи сообщения
            TempData["ErrorMessage"] = "Книга с таким названием и автором уже существует.";
            return View(book);
        }

        if (ModelState.IsValid)
        {
            book.CreatedAt = DateTime.UtcNow;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Добавляем сообщение об успешном создании
            TempData["SuccessMessage"] = "Книга успешно добавлена!";

            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    // Метод удаления книги
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // Метод для просмотра деталей книги
    public async Task<IActionResult> Details(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }
}