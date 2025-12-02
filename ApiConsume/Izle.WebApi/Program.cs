using Izle.BusinessLayer.Abstract;
using Izle.BusinessLayer.Concrete;
using Izle.BusinessLayer.ExternalApi;
using Izle.DataAccessLayer.Abstract;
using Izle.DataAccessLayer.Concrete;
using Izle.DataAccessLayer.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddDbContext<Context>();

builder.Services.AddScoped<IMovieDal, EfMovieDal>();
builder.Services.AddScoped<IMovieService, MovieManager>();

builder.Services.AddScoped<IAccountDal, EfAccountDal>();
builder.Services.AddScoped<IAccountService, AccountManager>();

builder.Services.AddScoped<IUserMovieDal, EfUserMovieDal>();
builder.Services.AddScoped<IUserMovieService, UserMovieManager>();

builder.Services.AddHttpClient<TmdbApiService>();


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("IzleProje", opts =>
    {
        opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

app.UseCors("IzleProje");
