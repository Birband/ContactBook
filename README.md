# ContactBook 

### Opis aplikacji
ContactBook jest aplikacją webową, która umożliwia użytkownikom zarządzanie globalnymi kontaktami na wzór zarządzania kontami w organizacji. Aplikacja składa się z dwóch głównych części: front-end napisany w Angularze oraz back-end w ASP.NET Core.

### Funkcjonalności
- Wyświetlanie listy kontaktów z możliwością sortowania i paginacji.
- Dodawanie, edycja i usuwanie kontaktów.
- Logowanie i rejestracja użytkowników.
- Autoryzacja za pomocą tokena JWT.

## Struktura projektu

### Frontend (Angular)
- `src/app/components`: Komponenty Angularowe, takie jak kontakt, lista kontaktów, formularz dodawania kontaktów, itp.
- `src/app/services`: Usługi Angularowe, takie jak ApiService obsługujący komunikację z API, AuthService zarządzający logowaniem i rejestracją.

### Backend (ASP.NET Core)

Projekt został zaprojektowany zgodnie z zasadami Domain-Driven Design (DDD) oraz Clean Architecture. Składa się z warstw: API, Application, Domain i Infrastructure, które współpracują w celu zapewnienia modularności, czytelności i łatwości utrzymania kodu.

#### Warstwa API

- **Opis:** Obsługuje zewnętrzne żądania HTTP i prezentuje dane użytkownikowi końcowemu.
- **Elementy:** Kontrolery, middleware.

#### Warstwa Application

- **Opis:** Zawiera logikę aplikacyjną, operacje na danych i reguły biznesowe.
- **Elementy:** Usługi aplikacyjne, przypadki użycia, fasady dla warstwy domeny.

#### Warstwa Domain

- **Opis:** Rdzeń aplikacji zawierający modele domenowe i reguły biznesowe.
- **Elementy:** Opis i implementacja encji.

#### Warstwa Infrastructure

- **Opis:** Odpowiada za techniczne szczegóły aplikacji.
- **Elementy:** Implementacje repozytoriów, zarządzanie persystencją danych, aspekty wspólne.

## Kompilacja aplikacji

1. Koniguracja `appsettings.json` i wartości **connection string** to połączenia się z bazą danych (w tym projekcie **mssql**) oraz wartości dla generowania tokenu **JWT**:
    ```json
    "JwtConfig": {
        "Secret" : "przynajmniej 32 znakowy string",
        "ExpiryInMinutes" : 0,
        "Issuer" : "",
        "Audience" : ""
    },
    "ConnectionStrings": {
        "DefaultConnection": "connection string dla używanej bazy danych"
    } 
    ```

2. Użycie narzędzi `dotnet-ef` w celu utworzenia bazy danych:

    1. Przejście do głównego folderu (tym z .sln)
    2. Utworzenie migracji:
        ```powershell
        dotnet ef migrations add nazwa_migracji --project .\ContactBook.Infrastructure\ --startup-project .\ContactBook.Api\
        ```
    3. Aktualizacja bazy danych:
        ```powershell
        dotnet ef database update --project .\ContactBook.Infrastructure\ --startup-project .\ContactBook.Api\
        ```
3. Uruchomienie głównej aplikacji backendowej, czyli **ContactBook.Api**:
    ```powershell
    dotnet run --project .\ContactBook.Api\ --launch-profile https
    ```

4. Urchomienie aplikacji frontendowej, czyli **ContactBook.Client**:

    _Potrzebna może być instalacja node.js orac angular/cli_
    ```powershell
    ng serve
    ```

## Opis poszczególnych klas i metod

#### JwtTokenGenerator
`JwtTokenGenerator` odpowiada za generowanie tokenów JWT używanych do autoryzacji użytkowników.
 - `GenerateToken`:
 Metoda GenerateToken generuje token JWT dla użytkownika na podstawie przekazanych parametrów: userId (identyfikator użytkownika) i email (adres email). Tworzony token zawiera odpowiednie roszczenia (claims), takie jak identyfikator subiekta, email użytkownika oraz unikalny identyfikator JWT. Token jest podpisany przy użyciu klucza symetrycznego zdefiniowanego w konfiguracji JWT i posiada określone wartości dla nadawcy, odbiorcy, czasu ważności oraz algorytmu szyfrowania.


#### InfrastructureExtension
`InfrastructureExtension` to klasa zawierająca metody rozszerzeń do konfiguracji i dodawania usług do kontenera Dependency Injection w aplikacji ASP.NET Core. Oferuje funkcje do konfiguracji autentykacji JWT, dostępu do bazy danych oraz AutoMapper. Metody te są używane w metodzie ConfigureServices klasy Startup, co pozwala na łatwe zarządzanie zależnościami i konfiguracją aplikacji.

#### ContactBookContext
Klasa `ContactBookDbContext` reprezentuje kontekst bazy danych dla książki kontaktowej. Dziedziczy po klasie DbContext z Entity Framework Core i zawiera właściwości DbSet dla encji użytkowników, kontaktów, podkategorii oraz kategorii.

#### *Controller
Kontrolery (*Controller) obsługują żądania HTTP i zarządzają logiką biznesową aplikacji, przetwarzając dane i zwracając odpowiedzi.

- `CategoryController`:
    Kontroler CategoryController zarządza kategoriami w systemie.

    - **GetCategories**: Pobiera wszystkie kategorie.
    - **AddCategory**: Dodaje nową kategorię (dostępne tylko w środowisku deweloperskim).
    - **UpdateCategory**: Aktualizuje istniejącą kategorię (dostępne tylko w środowisku deweloperskim).
    - **DeleteCategory**: Usuwa kategorię o podanym identyfikatorze (dostępne tylko w środowisku deweloperskim).
- `ContactController`:
    Kontroler ContactController zarządza kontaktami w systemie.

    - **GetContacts**: Pobiera wszystkie kontakty z bazy danych.
    - **GetContactByEmail**: Pobiera kontakt po adresie email.
    - **AddContact**: Dodaje nowy kontakt.
    - **UpdateContact**: Aktualizuje istniejący kontakt.
    - **DeleteContact**: Usuwa kontakt o podanym adresie email.
- `UserController`:
    Kontroler UserController zarządza użytkownikami w systemie.

    - **Login**: Loguje użytkownika na podstawie danych logowania.
    - **Register**: Rejestruje nowego użytkownika.

#### *Service
Serwisy (*Service) implementują logikę biznesową aplikacji, manipulują dane i wykonują operacje zgodnie z regułami biznesowymi.
- `CategoryService`:
Serwis CategoryService obsługuje operacje na kategoriach.

    - **GetCategoriesAsync**: Pobiera wszystkie kategorie z repozytorium.
    - **AddCategoryAsync**: Dodaje nową kategorię do repozytorium.
    - **DeleteCategoryAsync**: Usuwa kategorię na podstawie podanego identyfikatora.
    - **UpdateCategoryAsync**: Aktualizuje istniejącą kategorię w repozytorium.
- `ContactService`:
Serwis ContactService zarządza operacjami na kontaktach.

    - **GetContactsAsync**: Pobiera wszystkie kontakty z repozytorium.
    - **GetContactByEmailAsync**: Pobiera kontakt na podstawie adresu e-mail.
    - **AddContactAsync**: Dodaje nowy kontakt do repozytorium po walidacji i hashowaniu hasła.
    - **UpdateContactAsync**: Aktualizuje istniejący kontakt po walidacji i hashowaniu hasła.
    - **DeleteContactAsync**: Usuwa kontakt na podstawie adresu e-mail.
- `SubcategoryService`:
Serwis SubcategoryService zarządza operacjami na podkategoriach.

    - **GetSubcategoriesAsync**: Pobiera wszystkie podkategorie z repozytorium.
    - **AddSubcategoryAsync**: Dodaje nową podkategorię do repozytorium.
    - **DeleteSubcategoryAsync**: Usuwa podkategorię na podstawie podanego identyfikatora.
    - **UpdateSubcategoryAsync**: Aktualizuje istniejącą podkategorię w repozytorium.
- `UserService`:
Serwis UserService obsługuje operacje dotyczące użytkowników.

    - **LoginAsync**: Loguje użytkownika po walidacji i weryfikacji hasła, generując token JWT.
    - **RegisterAsync**: Rejestruje nowego użytkownika po walidacji e-maila i hasła, hashowaniu hasła oraz tworzeniu tokenu JWT.

#### *Repository
Repozytoria (*Repository) zapewniają dostęp do danych i realizują operacje CRUD, komunikując się z bazą danych lub innym źródłem danych.

- `CategoryRepository`: Zarządza operacjami związanymi z kategoriami w bazie danych.

    - **AddCategoryAsync**: Dodaje nową kategorię.
    - **DeleteCategoryAsync**: Usuwa kategorię na podstawie ID.
    - **GetCategoriesAsync**: Pobiera wszystkie kategorie.
    - **UpdateCategoryAsync**: Aktualizuje istniejącą kategorię.

- `ContactRepository`: Zarządza operacjami związanymi z kontaktami w bazie danych.

    - **AddContactAsync**: Dodaje nowy kontakt.
    - **DeleteContactAsync**: Usuwa kontakt na podstawie e-maila.
    - **GetContactByEmailAsync**: Pobiera kontakt na podstawie e-maila.
    - **GetContactsAsync**: Pobiera wszystkie kontakty.
    - **UpdateContactAsync**: Aktualizuje istniejący kontakt.

- `SubcategoryRepository`: Zarządza operacjami związanymi z podkategoriami w bazie danych.

    - **AddSubcategoryAsync**: Dodaje nową podkategorię.
    - **DeleteSubcategoryAsync**: Usuwa podkategorię na podstawie ID.
    - **GetSubcategoriesAsync**: Pobiera wszystkie podkategorie.
    - **UpdateSubcategoryAsync**: Aktualizuje istniejącą podkategorię.

- `UserRepository`: Zarządza operacjami związanymi z użytkownikami w bazie danych.

    - **GetUserByEmailAsync**: Pobiera użytkownika na podstawie e-maila.
    - **AddUserAsync**: Dodaje nowego użytkownika.

#### Validators
Walidatory sprawdzają poprawność danych wejściowych przed ich przetworzeniem lub zapisaniem w systemie.

- `EmailValidator`:
Klasa EmailValidator zawiera metodę `ValidateEmail`, która sprawdza poprawność formatu adresu email. Jeśli adres jest pusty lub nie spełnia określonych warunków, dodawane są odpowiednie komunikaty o błędach.

- `PasswordValidator`:
Klasa PasswordValidator udostępnia metodę `ValidatePassword`, która sprawdza złożoność hasła. Hasło musi mieć co najmniej 8 znaków, zawierać przynajmniej jedną cyfrę, jedną wielką literę i jedną małą literę. Jeśli hasło nie spełnia tych warunków, generowane są odpowiednie komunikaty o błędach.

- `PhoneValidator`:
Klasa PhoneValidator zawiera metodę `ValidatePhone`, która waliduje format numeru telefonu. Numer musi składać się z dokładnie dziewięciu cyfr. Jeśli numer telefonu jest pusty lub nie spełnia tego warunku, dodawany jest komunikat o błędzie.

#### PasswordHash
`PasswordHash` zajmuje się bezpiecznym haszowaniem i weryfikacją haseł użytkowników w systemie.
- `HashPassword`:
    Metoda HashPassword generuje unikalny losowo wygenerowany sól, a następnie hashowanie hasła z wykorzystaniem tej soli. Wynikowy hash hasła wraz z solą jest zapisywany w formacie tekstowym i zwracany jako string gotowy do przechowywania w bazie danych.

- `VerifyPassword`:
    Metoda VerifyPassword służy do weryfikacji podanego hasła względem zapisanego w formie zhaszowanej wraz z solą. Metoda pobiera solę z zapisanego hasza, używa jej do ponownego zhashowania podanego hasła i porównuje wynik z zapisanym zhashowanym hasłem.

#### ErrorHandler
Klasa `ErrorHandler` jest middleware'em dla obsługi wyjątków w aplikacji ASP.NET Core, przetwarzając normalne wyjątki oraz ValidationException.

- `Invoke` przechwytuje wyjątki podczas przetwarzania żądań HTTP i przekierowuje je do obsługi w HandleExceptionAsync.