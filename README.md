# Ecommerce API

Projekt Web API w technologii .NET 8, stworzony w ramach zadania rekrutacyjnego. Aplikacja to system e-commerce umożliwiający zarządzanie produktami oraz zamówieniami, z uwzględnieniem relacji wiele-do-wielu.

## Technologie
* .NET 8 (ASP.NET Core Web API)
* Entity Framework Core
* SQLite (baza danych)

## Automatyzacja (CI/CD)

Projekt korzysta z GitHub Actions do automatyzacji procesu budowania i weryfikacji kodu po każdym pushu na główną gałąź. Konfiguracja znajduje się w pliku: `.github/workflows/build.yml`.

### Jak działa workflow:
1. **Wyzwalacz:** Uruchamia się automatycznie przy zdarzeniach `push` oraz `pull_request` na gałąź `main`.
2. **Środowisko:** Proces działa na systemie Ubuntu (`ubuntu-latest`).
3. **Kroki:**
   * Pobranie aktualnego kodu z repozytorium.
   * Konfiguracja środowiska .NET 8 SDK.
   * Przywrócenie pakietów NuGet (`dotnet restore`).
   * Budowa aplikacji i weryfikacja pod kątem błędów (`dotnet build --no-restore`).

## Wdrożenie w Azure

Aplikacja jest publicznie dostępna – została wdrożona w chmurze Microsoft Azure przy użyciu usługi **Azure App Service** (plan oparty na systemie Linux). Proces wdrażania został zautomatyzowany dzięki integracji Azure z GitHubem.

### Jak przetestować API?

**Główny adres aplikacji:**
`https://krzysztof-ecommerce-api-dpcpa4dtgadvangu.polandcentral-01.azurewebsites.net`

**Uwaga dotycząca środowiska produkcyjnego:**
Ponieważ jest to czyste Web API, aplikacja nie ma strony głównej (HTML). Wejście pod główny adres URL celowo zwróci błąd 404 (Not Found). Ponadto, ze względów bezpieczeństwa, interfejs graficzny Swagger działa tylko lokalnie na etapie programowania i jest wyłączony na serwerze w chmurze.

Aby pobrać lub zmodyfikować dane, użyj pełnej ścieżki do wybranego endpointu w przeglądarce, Postmanie lub cURL.

**Przykładowe endpointy:**
* `GET /api/Products` - Zwraca listę wszystkich produktów (pełny link: https://krzysztof-ecommerce-api-dpcpa4dtgadvangu.polandcentral-01.azurewebsites.net/api/Products)
* `GET /api/Orders` - Zwraca listę zamówień wraz z przypisanymi produktami
* `POST /api/Products` - Dodaje nowy produkt (wymaga przesłania obiektu JSON)