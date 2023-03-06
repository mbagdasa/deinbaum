#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["deinBaum.WebAPI/deinBaum.WebAPI.csproj", "deinBaum.WebAPI/"]
COPY ["deinBaum.DAL/deinBaum.DAL.csproj", "deinBaum.DAL/"]
COPY ["deinBaum.Lib/deinBaum.Lib.csproj", "deinBaum.Lib/"]
RUN dotnet restore "deinBaum.WebAPI/deinBaum.WebAPI.csproj"
COPY . .
WORKDIR "/src/deinBaum.WebAPI"
RUN dotnet build "deinBaum.WebAPI.csproj" -c Release -o /app/build

#RUN dotnet ef migrations remove
RUN dotnet ef migrations add "CreateDatabase" 
RUN dotnet ef database update

FROM build AS publish
RUN dotnet publish "deinBaum.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "deinBaum.WebAPI.dll"]