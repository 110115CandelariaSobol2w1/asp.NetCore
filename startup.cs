public class startUp{
    public startUp(IConfiguration configuration){
        Configuration = configuration;
    }

    public IConfiguration Configuration {get;}

    public void ConfigureServices(IServiceCollection services){
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
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