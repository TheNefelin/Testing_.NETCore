var builder = DistributedApplication.CreateBuilder(args);

//var sql = builder.AddSqlServer("SqlServer");
//var cache = builder.AddRedis("cache");

builder.AddProject<Projects.WebApi_EntityFramework>("webapi");
    //.WithReference(sql)
    //.WithReference(cache);

builder.Build().Run();
