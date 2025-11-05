# --- Этап сборки ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем всё
COPY . .

# Покажем структуру папок
RUN echo "=== Полная структура папок ==="
RUN find . -type f -name "*.csproj" | head -20

RUN echo "=== Содержимое корневой папки ==="
RUN ls -la

RUN echo "=== Содержимое FinancialAssetsApp ==="
RUN ls -la FinancialAssetsApp/

RUN echo "=== Ищем все csproj файлы ==="
RUN find . -name "*.csproj" -type f

# Остановим сборку чтобы увидеть логи
RUN exit 1