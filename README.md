# Databázový Projekt
Program se spouští v aplikaci Visual Studio. Po spuštění Vás program vyzve na stisknutí klávesy ENTER, díky které se připojíte do databáze. Následně se Vám ukáže menu,
ve kterém si zvolíte volbu:
### 1.) Create
Dá uživateli na výběr, jestli chce vložit data z csv souboru (default insert), nebo jestli chce zadat vlastní data. Default insert lze použít pouze jednou v jednom
spuštění. Pokud si uživatel vybere, že chce zadávat vlastní data, zobrazí se mu nabídka, do jaké tabulky chce data vkládat a poté se provede insert.
### 2.) Read 
Vypíše data z tabulky.
### 3.) Update 
Zeptá se uživatele, v jaké tabulce chce provést update a následně ho provede. Provádí se pouze na prvky s id 1.
### 4.) Delete 
Vypíše data z tabulky a zeptá se uživatele, z které tabulky chce data smazat. Smaže data.
### 5.) Reset
Smaže tabulky v databázi a poté je hned zase vytvoří. Slouží pro lepší práci s id.
### 6.) Exit
Ukončí program
## Vhodné užití
Při vytváření/upravování je zapotřebí vkládat správná id a vytvářet objekty ve vhodném pořadí. K jejich případnému resetování slouží Reset. V opačném případě uživatel 
dostává feedback o chybách a příkaz se neprovede.
### Jiné informace
Součástí projektu je diagram propojení tabulek a i samotný .sql file
