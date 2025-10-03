# GroceryApp sprint4 Studentversie  
## Single Responsibility Principle (SRP)

Dit project past het **Single Responsibility Principle (SRP)** toe, één van de SOLID-principes.  
SRP stelt dat elke klasse of module slechts **één reden tot verandering** mag hebben.  
Door slimme abstrahering en een duidelijke scheiding van verantwoordelijkheden blijft de codebase overzichtelijk, uitbreidbaar en testbaar.

### Toepassing in dit project
- **ViewModels (in `Grocery.App/ViewModels`)**  
  Elke ViewModel is verantwoordelijk voor de logica van één specifiek scherm.  
  → UI-logica blijft gescheiden van businesslogica en datalaag.  

- **Models (in `Grocery.Core/Models`)**  
  Definiëren de kernobjecten van de applicatie, zoals producten of boodschappenlijsten.  
  → Ze bevatten geen logica voor data-opslag of UI-weergave.  

- **Services (in `Grocery.Core/Services`)**  
  Bevatten de business rules en operaties die losstaan van infrastructuur.  
  → Maken gebruik van interfaces om afhankelijkheden te minimaliseren.  

- **Repositories (in `Grocery.Core.Data/Repositories`)**  
  Verantwoordelijk voor data-opslag en communicatie met externe bronnen.  
  → Implementeren interfaces gedefinieerd in `Core`, zodat business rules onafhankelijk blijven.  

### Documentatie van keuzes
- **Scheiding van lagen**  
  Door de opdeling in *App*, *Core* en *Core.Data* is er een duidelijke afbakening van verantwoordelijkheden.  
  UI kent alleen de ViewModels, die afhankelijk zijn van services en interfaces uit de Core.  

- **Slimme abstrahering via interfaces**  
  Interfaces in `Core/Interfaces` zorgen ervoor dat de business logica niet afhankelijk is van specifieke data-opslag.  
  Hierdoor kunnen repositories gemakkelijk vervangen of uitgebreid worden.  

- **Testbaarheid**  
  Dankzij SRP en abstrahering kunnen onderdelen geïsoleerd getest worden (`TestCore`).  
  Bijvoorbeeld: services kunnen getest worden met mock-repositories.  

### Voordelen
- Duidelijkheid in verantwoordelijkheden.  
- Minder kans op onverwachte neveneffecten bij wijzigingen.  
- Betere testbaarheid en onderhoudbaarheid.  

## Clean Architecture

In dit project is **Clean Architecture** toegepast om de codebase overzichtelijk, testbaar en uitbreidbaar te houden. Clean Architecture draait om een duidelijke scheiding van verantwoordelijkheden, waarbij afhankelijkheden altijd van buiten naar binnen wijzen (UI → Domain → Data).  

De lagen in dit project zijn als volgt ingericht:

- **Grocery.App (Presentation Layer)**  
  Bevat de UI en ViewModels (MAUI). Deze laag is verantwoordelijk voor de interactie met de gebruiker en het tonen van data.  

- **Grocery.Core (Domain Layer)**  
  Dit is de kern van de applicatie. Hier bevinden zich de **Models**, **Interfaces** en **Services** die de business rules bevatten. Deze laag is onafhankelijk van externe frameworks en infrastructuur.  

- **Grocery.Core.Data (Data Layer)**  
  Hier zitten de **Repositories** en implementaties voor data-opslag en communicatie met externe bronnen. Deze laag implementeert de interfaces die gedefinieerd zijn in `Grocery.Core`.  

### Voordelen van Clean Architecture
- **Testbaarheid** → Business logica kan eenvoudig getest worden zonder afhankelijkheden van UI of database.  
- **Onderhoudbaarheid** → Wijzigingen in de UI of data-opslag hebben geen directe impact op de kern van de applicatie.  
- **Schaalbaarheid** → Nieuwe features of databronnen kunnen worden toegevoegd door simpelweg nieuwe implementaties toe te voegen.  



## UC10 Productaantal in boodschappenlijst
Aanpassingen zijn compleet.

## UC11 Meest verkochte producten
Vereist aanvulling:  
- Werk in GroceryListItemsService de methode GetBestSellingProducts uit.  
- In BestSellingProductsView de kop van de tabel aanvullen met de gewenste kopregel boven de tabel. Daarnaast de inhoud van de tabel uitwerken.

## UC13 Klanten tonen per product  
Deze UC toont de klanten die een bepaald product hebben gekocht:  
- Maak enum Role met als waarden None en Admin.  
- Geef de Client class een property Role metb als type de enum Role. De default waarde is None.  
- In Client Repo koppel je de rol Role.Admin aan user3 (= admin).
- In BoughtProductsService werk je de Get(productid) functie uit zodat alle Clients die product met productid hebben gekocht met client, boodschappenlijst en product in de lijst staan die wordt geretourneerd.  
- In BoughtProductsView moet de naam van de Client ewn de naam van de Boodschappenlijst worden getoond in de CollectionView.  
- In BoughtProductsViewModel de OnSelectedProductChanged uitwerken zodat bij een ander product de lijst correct wordt gevuld.  
- In GroceryListViewModel maak je de methode ShowBoughtProducts(). Als de Client de rol admin heeft dan navigeer je naar BoughtProductsView. Anders doe je niets.  
- In GroceryListView voeg je een ToolbarItem toe met als binding Client.Name en als Command ShowBoughtProducts.  


  
