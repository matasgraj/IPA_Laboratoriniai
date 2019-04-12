# IPA 3-4

Integruotu programavimo aplinku 3-4 laboratorinis darbas.

## Versijavimas

Kiekviena versija turi savo saka (angl. branch) ir versijos relyza github aplinkoje.

# v0.1

Atlikta:
* Studento informacijos nuskaitymas;
* Pasirinkimas automatiniam rezultatu generavimui;
* Vidurkio arba medianos skaiciavimas;
* Informacijos isvedimas i konsoles langa;
* Papildomas .cs failas, demonstruojantis List<> pakeitima Array tipu rezultatu surinkimui;

# v0.2

Atlikta:
* Duomenu nuskaitymas is failo;
* Logikos blokai perkelti i nuosavus metodus;
* Perdarytas konsoles isvedimas;
* Isvedami duomenys isrikiuojami pagal varda ASC tvarka;

# v0.3

Atlikta:
* Iskelta Student klase i nauja faila;
* Iskelti meniu pasirinkimai i atskirus metodus;
* Pritaikytas isimciu valdymas;
* Klaidos taisymas;

# v0.4

Atlikta:
* Nauju studentu failu generavimas
* Studentu grupavimas i dvi grupes
* Performance testing

## Performance testing results:
Testuota programos greitaveika su 10, 100, 1000, 10000, 100000 irasu.

Rezultatai:
10 - ~0.2 sec  
100 - ~0.5 sec  
1000 - ~0.7 sec  
10000 - 1 sec  
100000 - 1.2 sec  

# v0.5

Atlikta:
* Perdaryta logika, priimanti List, LinkedList ir Queue tipo konteinerius
* Performace testing rusiavimui su skirtingo tipo konteineriais
* Padarytas relus performance rezultatu isvedimas

### Rezultatai:
|LIST rusiavimas                             |Laikas sekundemis|
|--------------------------------------------|-----------------|
|...10students_generated.txt | 0.008s|
|...100students_generated.txt | 0.001s|
|...1000students_generated.txt | 0.004s|
|...10000students_generated.txt | 0.031s|
|...100000students_generated.txt | 0.347s|

|QUEUE rusiavimas                            |Laikas sekundemis|
|--------------------------------------------|-----------------|
|...10students_generated.txt | 0.001s|
|...100students_generated.txt | 0.001s|
|...1000students_generated.txt | 0.003s|
|...10000students_generated.txt | 0.033s|
|...100000students_generated.txt | 0.398s|

|LINKEDLIST rusiavimas                       |Laikas sekundemis|
|--------------------------------------------|-----------------|
|...10students_generated.txt | 0.001s|
|...100students_generated.txt | 0.001s|
|...1000students_generated.txt | 0.003s|
|...10000students_generated.txt | 0.034s|
|...100000students_generated.txt | 0.387s|

# v1.0

Ismatuoti 5 skirtingu failu rusiavimas pagal 2 strategijas, skaiciuojant laika ir uzimamos atminties dydi.

### 1 Strategija

|LIST rusiavimas                             |Laikas sekundemis| Panaudito baitai|
|--------------------------------------------|-----------------|-----------------|
|...10students_generated.txt | 0.008s| 10 653 696
|...100students_generated.txt | 0.001s| 10 850 304
|...1000students_generated.txt | 0.004s| 12 357 632
|...10000students_generated.txt | 0.031s| 20 127 744
|...100000students_generated.txt | 0.347s| 55 693 312

|QUEUE rusiavimas                            |Laikas sekundemis| Panaudito baitai|
|--------------------------------------------|-----------------|-----------------|
|...10students_generated.txt | 0.001s| 55 697 408
|...100students_generated.txt | 0.001s| 55 697 408
|...1000students_generated.txt | 0.003s| 55 697 408
|...10000students_generated.txt | 0.033s| 59 973 632
|...100000students_generated.txt | 0.398s| 58 343 424

|LINKEDLIST rusiavimas                       |Laikas sekundemis| Panaudito baitai|
|--------------------------------------------|-----------------|-----------------|
|...10students_generated.txt | 0.001s| 58 343 424
|...100students_generated.txt | 0.001s| 58 343 424
|...1000students_generated.txt | 0.003s| 58 343 424
|...10000students_generated.txt | 0.034s| 59 973 632
|...100000students_generated.txt | 0.387s| 61 222 912

Maziausiai atminties uzima ``LIST`` tipo konteineris, taciau jis leciau veikia su nedideliu duomenu kiekiu.  
Optimaliausias variantas naudti ``LINKEDLIST`` tipo konteineri, laiko ir uzimamos vietos santykiu.















