FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs
WORKDIR /src
COPY ["Project1/Project1.csproj", "Project1/"]
COPY ["Project1.DAL/Project1.DAL.csproj", "Project1.DAL/"]
COPY ["Project1.Services/Project1.Services.csproj", "Project1.Services/"]
RUN dotnet restore "Project1/Project1.csproj"
COPY . .
WORKDIR "/src/Project1"
RUN dotnet build "Project1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project1.dll"]