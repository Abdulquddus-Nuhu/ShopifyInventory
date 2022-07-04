#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ShopifyInventory/ShopifyInventory.csproj", "ShopifyInventory/"]
RUN dotnet restore "ShopifyInventory/ShopifyInventory.csproj"
COPY . .
WORKDIR "/src/ShopifyInventory"
RUN dotnet build "ShopifyInventory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopifyInventory.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopifyInventory.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ShopifyInventory.dll