# 1. ���� ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. ����������� ������ �������� � �������������� ������������
COPY ["API/API.csproj", "API/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "API/API.csproj"

# 3. ����������� ������ ���� � �������
RUN dotnet restore
COPY . .
RUN dotnet build "API/API.csproj" -c Release -o /app/build

# 4. ���� ����������
FROM build AS publish  
RUN dotnet publish "API/API.csproj" -c Release -o /app/publish

# 5. ��������� �����
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]