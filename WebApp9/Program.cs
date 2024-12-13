using Microsoft.EntityFrameworkCore;
//using WebApp9.Data;

var builder = WebApplication.CreateBuilder(args);

// ���������� �����
builder.Services.AddControllersWithViews();

// ��������� ����������� � �� � �������������� �������������
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
});
var app = builder.Build();

// ��������� ��������� ��������� HTTP-��������
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();