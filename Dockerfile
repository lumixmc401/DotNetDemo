FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Directory.Build.props", "."]
COPY ["Directory.Packages.props", "."]
COPY ["src/DotNetDemo.Api/DotNetDemo.Api.csproj", "src/DotNetDemo.Api/"]
COPY ["src/DotNetDemo.Application/DotNetDemo.Application.csproj", "src/DotNetDemo.Application/"]
COPY ["src/DotNetDemo.Domain/DotNetDemo.Domain.csproj", "src/DotNetDemo.Domain/"]
COPY ["src/DotNetDemo.Infrastructure/DotNetDemo.Infrastructure.csproj", "src/DotNetDemo.Infrastructure/"]

RUN dotnet restore "src/DotNetDemo.Api/DotNetDemo.Api.csproj"

COPY . .
RUN dotnet build "src/DotNetDemo.Api/DotNetDemo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/DotNetDemo.Api/DotNetDemo.Api.csproj" -c Release -r linux-x64 --self-contained false -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80 443
ENTRYPOINT ["dotnet", "DotNetDemo.Api.dll"]
