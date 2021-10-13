FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Panda.Web/Panda.Web.csproj", "Panda.Web/"]
RUN dotnet restore "Panda.Web/Panda.Web.csproj"
COPY . .
WORKDIR "/src/Panda.Web"
RUN dotnet build "Panda.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Panda.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Panda.Web.dll"]
