using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Books")]
public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название книги")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Длина названия от 1 до 200 символов")]
    [Display(Name = "Название книги")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите автора")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Длина имени автора от 1 до 100 символов")]
    [Display(Name = "Автор")]
    public string Author { get; set; } = string.Empty;

    [Range(1800, 2025, ErrorMessage = "Некорректный год издания")]
    [Display(Name = "Год издания")]
    public int PublicationYear { get; set; }

    [Range(0, 100000, ErrorMessage = "Некорректная цена")]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Цена")]
    public decimal Price { get; set; }

    [Display(Name = "Дата добавления")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}