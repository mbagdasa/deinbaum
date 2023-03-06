# deinBaumApp .Net 7 Maui App 
## Entwickelt durch: Luxson Kanagarajah, Micha Bagdasarianz-Meier

# Projekt Installationsanleitung
## 1 Docker
1. Docker Desktop installieren [Link zum Download](https://www.docker.com/products/docker-desktop/)
2. Docker starten
   Dabei muss man auf folgende Punkte beachten!
   1. Man soll mit einem gültigen Docker-Hub Account sich einloggen. (Die Registrierung ist kostenlos)
   2. Sicher stellen dass Docker Desktop im markierten Bereich grün leuchtet (Engine Running)
    ![Docker Desktop Img](https://github.zhaw.ch/storage/user/1165/files/953a964a-77af-4825-8cf8-3e5b00e1a519)

## 2 Visual Studio 2022
1. Visual Studio 2022 mit .Net 6 und .Net 7 inkl. Maui Komponenten installieren
2. deinBaum-Projekt (Master-Branch) im Github clonen und im Visual Studio 2022 öffnen (Vorsicht, man braucht hier eine Visual Studio Version mit .Net 7 MAUI Unterstützung).
   [Link um Git-Clone durchzuführen](https://github.zhaw.ch/kanaglux/deinBaumApp.git)
   ![GithubImage](https://github.zhaw.ch/storage/user/1165/files/406ce0ff-f759-4ccf-9673-04a323830c46)

## 3 Android Emulator
1. Android Emulator Installieren.
   Aufgrund [offenem Bug](https://github.com/dotnet/maui/issues/11275) in .Net 7 MAUI, wird empfohlen, eine SDK kleiner 33 zu verwenden. Ansonsten muss die berechtigung auf Dateien zuzugreifen manuell in den Einstellungen der installierten App gemacht werden.
   Bei der Entwicklung wurde folgender Emulator verwendet:
   ![UsedEmulator](https://github.zhaw.ch/storage/user/6211/files/d537ed08-1d2d-4742-96ff-adeabbc2afc1)
   
## 4 Docker-Compose (Web-API und Datenbank) starten
1. Projekt in Visual Studio 2022 öffnen
2. Startup Projekt 'docker-compose' wählen
3. Warten bis das Swagger Fenster geöffnet ist (Teilweise muss man ein Zertifikat genehmigen)
4. Mittels Swagger-Weboberfläche einen neuen User registrieren. (Falls "admin" im Username enthalten ist, wird ein Admin-Recht zugeteilt)
![Erfassung_AdminUser](https://github.zhaw.ch/storage/user/6211/files/44f014eb-a91c-4c8e-a3d0-33af6e9ec00b)

## 5 deinBaumApp starten (Voraussetzung Docker-Compose läuft)
1. Projekt ein zweites Mal in Visual Studio 2022 öffnen
*Einrichten mit Multiple Starterprojekt (deinbaumApp, docker-compose, run) möglich, jedoch kann bei der App kein Ziel mitgegeben werden (Windows oder Android Emulator).*
3. Startup Projekt definieren -> deinbaumApp
4. Starten mit Ziel Android Simulator
![Start_deinbaumApp](https://github.zhaw.ch/storage/user/6211/files/f4ea836b-6bc2-4560-b569-16c79e5236c2)
5. Warten bis die App offen ist.
6. Nun ist die Applikation betriebsbereit
7. Einloggen mit zuvor erstelltem User

## 6 Web API Unit Tests (Voraussetzung Docker-Compose läuft)
1. Projekt ein zweites Mal in Visual Studio 2022 öffnen
*Einrichten mit Multiple Starterprojekt (deinbaumApp, docker-compose, run) möglich, jedoch kann bei der App kein Ziel mitgegeben werden (Windows oder Android Emulator).*
3. Test Explorer öffen
4. Alle Testcases durchlaufen lassen
![Test Explorer](https://github.zhaw.ch/storage/user/1165/files/74aede68-5238-44f4-98d0-ca771bf55708)


# Projektbeschreibung

## 1 Projektidee
Der Verein deinbaum (https://www.deinbaum.ch/) verkauft Patenschaften für Bäume.
Beim Erwerb einer Patenschaft, verpflichtet sich der Baumeigentümer, diesen Baum zu schützen und eine grosse Biodiversität zu ermöglichen.
Die Bauminformationen werden einzeln mittels unterschieldichen Medien erfasst.
Ziel dieses Projektes war, eine App zu entwickeln, welche das erfassen potenzieller Bäume erleichtert.
Position und Fotodokumentation sind wesentliche Ansprüche, welche mithile einer Mobile-App gut abgedeckt werden können.

## 2 Projektaufbau
Unsere Solution haben wir folgendermassen aufgebaut:
### 2.1 Projekt deinBaum.DAL
Die Data Access Layer haben wir mit dem Code-First Ansatz realisiert und mithilfe von Entity Framework 6 im Docker migriert.
<img src="https://github.zhaw.ch/storage/user/1165/files/e41eb725-f541-40be-a3fb-d9c838e0b0ec" width="50%"/>

### 2.2 Projekt deinBaum.WebAPI
Die Web APi ist das "Kommunikationsglied" für die Postgres-Datenbank und für die App.
In diesem Projekt sind Controllers nach dem CRUD-Prinzipen erstellt:

Controller | CRUD | URLS 
--- | --- | --- 
**AuthController** | Create, Read, Delete | **Create:** <br> /api/Auth/register <br> /api/Auth/login <br><br>**Read:**<br>/api/Auth <br><br>**Delete:**<br> /api/Auth  
**BaumController** | Create, Read, Update, Delete | **Create:**<br>/api/Baum/ <br><br>**Read:**<br> /api/Baum <br> /api/Baum/{name} <br> /api/Baum/exists/{name}<br><br>**Update:**<br> /api/Baum<br><br>**Delete:**<br>/api/Baum  
**BaumArtController** | Create, Read, Update, Delete | **Create:**<br>/api/BaumArt/<br>/api/BaumArt/addBaumArt/{art} <br><br>**Read:**<br>/api/BaumArt<br>/api/BaumArt/id/{id}<br>/api/BaumArt/art/{art}<br><br>**Update:**<br>/api/BaumArt/updateBaumArt/artName/{alterArtName}/{neuerArtName} <br> /api/BaumArt/updateBaumArt/id/{id}/{neuerArtName} <br><br>**Delete:**<br>/api/BaumArt  
**BaumMerkmalController** | Create, Read, Update, Delete | **Create:**<br>/api/BaumMerkmal/<br>/api/BaumMerkmal/addMerkmal/{merkmal}<br><br>**Read:**<br>/api/BaumMerkmal<br>/api/BaumMerkmal/id/{id}<br>/api/BaumMerkmal/merkmal/{merkmal}<br><br>**Update:**<br>/api/BaumMerkmal/updateBaumMerkmal/merkmal/{alterMerkmal}/{neuerMerkmal} <br> /api/BaumMerkmal/updateBaumMerkmal/id/{id}/{neuerMerkmal}<br><br>**Delete:**<br>/api/BaumMerkmal
**BaumZustandController** | Create, Read, Update, Delete | **Create:**<br>/api/BaumZustand<br>/api/BaumZustand/addZustand/{zustand}<br><br>**Read:**<br>/api/BaumZustand<br>/api/BaumZustand/id/{id}<br>/api/BaumZustand/zustand/{zustand}<br><br>**Update:**<br>/api/BaumZustand/updateBaumZustand/zustand/{alterZustand}/{neuerZustand} <br> /api/BaumZustand/updateBaumZustand/id/{id}/{neuerZustand}<br><br>**Delete:**<br>/api/BaumZustand
**FotoController** | Create, Read, Delete | **Create:**<br>/api/Foto<br><br>**Read:**<br>/api/Foto<br>/api/Foto/id/{id}<br>/api/Foto/name/{name}<br><br>**Delete:**<br>/api/Foto
**WaldeigentuemerController** | Create, Read, Update, Delete | **Create:**<br>/api/Waldeigentuemer<br><br>**Read:**<br>/api/Waldeigentuemer<br>/api/Waldeigentuemer/id/{id}<br>/api/Waldeigentuemer/email/{email}<br>/api/Waldeigentuemer/existsWaldeigentuemer/{email}<br><br>**Update:**<br>/api/Waldeigentuemer/updateWaldeigentuemer/email/{email} <br> /api/Waldeigentuemer/updateWaldeigentuemer/id/{id}/<br><br>**Delete:**<br>/api/Waldeigentuemer
**FeldmitarbeiterController** | Create, Read, Update, Delete | **Create:**<br>/api/Feldmitarbeiter<br><br>**Read:**<br>/api/Feldmitarbeiter<br>/api/Feldmitarbeiter/id/{id}<br>/api/Feldmitarbeiter/login/{login}<br>/api/Feldmitarbeiter/existsLogin/{login}<br><br>**Update:**<br>/api/Feldmitarbeiter/updateFeldmitarbeiter/login/{login} <br> /api/Feldmitarbeiter/updateFeldmitarbeiter/id/{id}/<br><br>**Delete:**<br>/api/Feldmitarbeiter

### 2.3 Projekt deinBaumApp
Dieses Projekt ist die eigentliche .Net 7 MAUI Applikation.
Hier werden die Pages, die Navigation zwischen den Pages, das ganze MVVM Konstrukt und 
die restliche Logik für die Applikation gepflegt.

**MVVM Architektur:**
Wurde mitthilfe des MVVM Community Toolkit umgesetzt.

- **Views**: Sind die ContentPages, welche in XAML definiert sind. Das Viewmodel wird jeweils
         im CodeBehind des XAML File mittels Dependency Injection angebunden
- **ViewModel**: Logik, was geschieht, wenn ein Attribut im UI ändert, oder ein Control
             einen Befehl erhält (Beispiel Drücken eines Buttons)
             Es werden Requests an Service Klassen gemacht, welche wiederum das WebAPI aufrufen.
- **Model**: Im Projekt deinBaum.Lib definiert.

**Externe Libraries (openSource):**
- UraniumUI für die Controls
- MapSui für die Kartendarstellung
- MVVM Community Toolkit 

**Icons:**
Wurden mit unterschieldichen freiverfügbaren Icons (Quelle: https://www.iconfinder.com/) selber zusammengestellt.

**Login:**
Falls Login erfolreich gemacht werden konnte, wird ein Token im SecureStorage gespeichert. Somit muss sich der User nicht
immer erneut anmelden, wenn er die Applikation neu startet. Es wird jedesmal geprüft, ob das Token noch gültig ist. Falls das Token abgelaufen ist,
wird zur Login-Seite weitergeleitet.

**Flyoutitems (Menüs):**
Im oberen Bereich wird der aktuelle User mit seiner Rolle angezeigt.
Anschliessend sind für einen normalen User folgende Seiten verfügbar:
Baum, Bäume, Über

![FlyoutItems_normalUser](https://github.zhaw.ch/storage/user/6211/files/131d212a-7ae0-44ed-a268-1e29ddc34f8c)

Für einen Admin User werden folgende Seiten angezeigt:
Baum, Bäume, Feldmitarbeiter, Feldmitarbeiter erfassen, Waldeigentümer, Waldeigentümer erfassen, Über

![FlyoutItems_AdminUser](https://github.zhaw.ch/storage/user/6211/files/0475dbff-d1cb-43bc-8d29-33a433d17131)


**Baum:**
Auf dieser Seite kann ein Baum erfasst werden. Gewisse Felder sind zwingend auszufüllen, ansonsten kommt eine Meldung.
Bei der Auswahl eines Feldmitarbeiters oder eines Waldeigentümers, wird auf die Seite "Feldeigentümer"/"Waldeigentümer" weitergeleitet.
Es werden nur Feldmitarbeiter zur Auswahl angeboten, welche auch noch in der Firma arbeiten.

Foto und Position werden über die Sensoren des Gerätes ermittelt/erfasst. Auf der Windows App zurzeit nicht möglich.
Die Karte zeigt die erfasste Position des Baumes. Der Karten Hintergrund ist zurzeit Microsoft-BingMaps.
Die Werte für die Controls (Wertelisten wie Baumarten etc.) werden via WebApi geholt.

**Bäume:**
Auflistung aller erfassten Bäume.
Es kann ebenfalls nach einem bestimmten Baum gesucht werden.
Wenn auf einen bestimmten Baum geklickt wird, dann wird auf die Baum Seite mit dem ausgewählten Baum als Parameter weitergeleitet.
Dies erlaubt das Bearbeiten dieses Baumes.
Wenn bei einem Baum nach links geswipt wird, dann wird dieser Baum gelöscht.
![Baum_loeschen](https://github.zhaw.ch/storage/user/6211/files/c412adbd-2216-473d-b58c-01fb47917df6)

**Feldmitarbeiter:**
Auflistung aller erfassten Feldmitarbeiter.
Es kann ebenfalls nach einem bestimmeten Mitarbeiter gesucht werden.
Es werden alle Mitarbeiter angezeigt, auch solche, welche nicht mehr in der Firma arbeiten.
Wenn auf einen bestimmten Mitarbeiter geklickt wird, dann wird auf die "Feldmitarbeiter erfassen" Seite mit dem ausgewählten Mitarbeiter als Parameter weitergeleitet.
Dies erlaubt das Bearbeiten dieses Mitarbeiters.
Es kann kein Mitarbeiter gelöscht werden, da es evtl Bäume gibt, welche mit Fremdschlüssel auf den zu löschenden Feldmitarbeiter verweisen.
Deshalb nur via Checkbox "Arbeitet noch in Firma" steuerbar, dass keine weiteren Bäume mit diesem Mitarbeiter erfasst werden.

**Feldmitarbeiter erfassen:**
Seite um einen Feldmitarebieter zu erfassen.
Für jeden Mitarbeiter wird auch ein User-Login angelegt. 
Mit diesem Login kann man sich auch an der App anmelden.
Zurzeit wird die Rolle anhand des Loginnamen bestimmt. Admin-Rolle wird zugewiesen, sofern ein "admin" im Loginnamen vorhanden ist.

**Waldeigentümer:**
Dito Feldmitarbeiter

**Waldeigentümer erfassen:**
Dito Feldmitarbeiter erfassen

**Über:**
Informationen über die App.

### 2.4 Projekt deinBaum.Lib
Ursprünglich war die Idee: eine zentralisierte Ablage Ort für die Klassen damit die Projekte DAL, Web Api und die App daraufzugreifen können. Jedoch hat sich herausgestellt, dass Klasseb mit MVVM Toolkit und Entity Framework 6 nicht zusammen in der gleichen Klasse anwendbar ist. Deshalb wurde für DAL eine eigene DTO Model Klassen geschrieben und mittels Mapping in die Klassen serialisiert respk. deserialisiert.

### 2.5 Projekt deinBaum.WebAPI.Test
In diesem Projekt haben wir die Unittests für die API Calls geschrieben.  
Für die Projektabgabe haben wir 4 Unittests simuliert, welches folgende Calls ausführen und anschliessend die Daten wieder löschen:

Was wird getestet? | Erwartetes Resultat | Ergebnise
--- | --- | ---
**Registrierung Adminuser** | *User wird angelegt* | `Request Code: 200`
**Registrierung bereits existierender User** | *User wird nicht angelegt* | `Badrequest`
**Hinzufügen eines Feldmitarbeiter** | *User wird angelegt* | `Request Code: 200`
**Hinzufügen eines Baumes*** | *User wird angelegt* | `Request Code: 200`

* inklusive Registrierung Waldeigentümer, Registrierung Feldmitarbeiter, Registierung Foto, Auslesen von Baum- merkmalen, -zuständen und -arten.

### 2.6 Projekt docker-compose
Da wir für unsere Entwicklung keinen Server zur Verfügung hatten, haben wir uns für das Containerizing im Docker entschieden.
Durch diese Containerizing konnten wir ohne weiteren Konfigurationen oder Umstellungen einen lokalen Server (Postgres DB und den Rest Web API Schnittstelle) auf unserem lokalen Geräte simulieren.

Folgende Portkonfigruation wurden sichergestellt:
Für die PostgresDB: 5432
Für die REST WEB API Schnittstelle:
-  für HTTP: [http://localhost:7002/swagger/index.html](http://localhost:7002/swagger/index.html)
-  für HTTP: [https://localhost:7001/swagger/index.html](https://localhost:7001/swagger/index.html)

**Wichtig zum Wissen**
Wenn beim Starten vom docker-compose im Browser eine Sicherheitswarung erscheint, dann bitte die Portnummer im URL auf 7001 ändern!
<br>
<img src="https://github.zhaw.ch/storage/user/1165/files/723d6b69-2b8a-4180-bfc6-5e87211bed92" width="50%"/>


## 3 Highlights
Wir konnten unseren erste Android App über .Net MAUI entwickeln und das PoC mit den benötigten CRUD Operationen (Baum, Mitarbeiter,...) funktioniert auf unserem Laptops. Ebenso konnten wir im Bereich Kommunikation zwischen Datenbank, Server und Client gute Erfahrung mit Docker sammeln. Dadurch musste man nicht bei jeder Contributer die einzelnen Komponenten selber einrichten.
Zusätzlich konnten wir bei dieser Modularbeit schon zwei Nice-to-Have Feature einbauen (Login/Authentifikation und die Map Anbindung)

## 4 Lowlights
Da .Net MAUI relative neu auf dem Markt war, mussten wir viele Alternativen/Workarounds suchen:
- **Map Anbindung in MAUI**, Als wir mit dem Projekt begonnen haben, nutzen wir die Version .Net 6 MAUI. Da wurde die Map-Anbindung noch nicht angeboten. Glücklicherweise wurde die nächste Version .Net 7 gegen Ende November verfügbar, sodass leicht auf die neue Version umstellen konnten. Alternativ gab ein Framework für .Net6, jedoch konnte man nicht viel in der Dokumentation oder auch in Hilfe-Foren finden, was für die Entwicklung sehr viel Ressource gekostet hat
- **Bugs in MAUI**, ein weiteres Problem war, dass noch über 2000 Bugs, Issues auf dem .NET MAUI Github war. Deshalb musste auch dort Workarounds für die App gesucht werden.

## 5 Next Steps
Aktuell haben wir die App auf einem lokal Computer und mit einem lokalen Android Simulator entwickelt.
Die nächsten Schritte w$ren dementsprechend, die App auf einem Android Smartphone zu installieren und zum laufen bringen. 
Ebenfalls möchten wir gerne die App noch für die iOS Nutzern erweitern. Um diese nutzen zu können, wird eine Lizenz benötigt die jährlich 100 USD kostet.
Die Datenbank und die Web Api wird zurzeit ebenfalls lokal betrieben, für diese muss der Verein deinBaum noch einen produktiven Server zur Verfügung stellen.

In der App selber haben wir noch weitere Features geplant:
- Schnittselle zur kantonalen GIS für die Parzelleninfos etc.
- Import/Export Funktionalitäten
- Online/Offline Nutzung
- DB Migration mit den bereits existierenden Daten
- Navigation mit Anbindung Google Maps oder ähnliches

