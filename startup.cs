using Microsoft.EntityFrameworkCore;

public class appSettings
{
    public string Secret { get; set; }
}
public class startUp{
    public startUp(IConfiguration configuration){
        Configuration = configuration;
    }

    public IConfiguration Configuration {get;}
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddAutoMapper(typeof(startUp));
}


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env){
        if(env.IsDevelopment()){
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }


}