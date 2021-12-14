#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["./**/JIS-BE.csproj", "JIS-BE/"]
RUN dotnet restore "JIS-BE/JIS-BE.csproj"
COPY . .
WORKDIR "/src/JIS-BE"
RUN dotnet build "JIS-BE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JIS-BE.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000 DOTNET_RUNNING_IN_CONTAINER=true 
ENTRYPOINT ["dotnet", "JIS-BE.dll"]