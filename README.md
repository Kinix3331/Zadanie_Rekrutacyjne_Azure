# Ecommerce API

Projekt Web API w technologii .NET 8 stworzony w ramach zadania rekrutacyjnego. Aplikacja umożliwia zarządzanie produktami oraz zamówieniami (z uwzględnieniem relacji wiele-do-wielu) w systemie e-commerce.

## Technologie
* .NET 8 (ASP.NET Core Web API)
* Entity Framework Core
* SQLite (baza danych)

## Proces CI/CD (GitHub Actions)

Projekt wykorzystuje GitHub Actions do automatyzacji procesu budowania i weryfikacji kodu (Continuous Integration).
Plik konfiguracyjny znajduje się w ścieżce: `.github/workflows/build.yml`.

### Działanie Workflow:
1. **Wyzwalacz (Trigger):** Pipeline jest uruchamiany automatycznie przy każdym zdarzeniu `push` oraz otwarciu `pull_request` skierowanym na gałąź `main`.
2. **Środowisko robocze:** Proces jest wykonywany na systemie operacyjnym Ubuntu (`ubuntu-latest`).
3. **Etapy pipeline'u (Jobs):**
   * **Checkout:** Pobranie aktualnego kodu z repozytorium.
   * **Setup .NET:** Przygotowanie środowiska z .NET 8 SDK.
   * **Restore:** Pobranie wszystkich niezbędnych pakietów NuGet (`dotnet restore`).
   * **Build:** Skompilowanie aplikacji w celu wykrycia ewentualnych błędów w kodzie (`dotnet build --no-restore`).

## Wdrożenie w Azure (Część 3)
*Tutaj wkrótce pojawią się informacje o wdrożeniu do chmury.*