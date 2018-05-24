### Softwarequalität messen & verbessern mit <span style="color: #FE9D0E">NDepend</span>

- Entwickler: Patrick Smacchia
- Erscheinungsdatum: April 2004

![NDepend Logo](/images/full_logo.jpg)

Note:

Kurze NDepend Erklärung.

Ok, dann möchte ich vorab mal ein paar Fragen in die Runde stellen.

* Wer von euch hat nicht schon Code von einem Kollegen oder Vorgänger gesehen und darüber geflucht, da mann sich erstmal überhaupt nicht zurechtfindet?
* Wer von euch hat schonmal tagelang eine Software refaktorisieren musste?
* Wer von euch hat nicht  die Intention ein besserer Entwickler zu werden, am Besten Clean Code Paradigmen praktizieren?
* Wer es nicht schön, die in einem Projekt erlangte Software-Qualität mit in das nächste Projekt zu nehmen?

All das ist mit NDepend ohne Weiteres möglich!

Bevor ich weiter auf NDepend eingehe möchte ich noch ein paar Basics abklopfen.

---

### <span style="color: #FE9D0E">Statische</span> Codeanalyse

#### Erklärung
Statisches Software-Testverfahren zur Compile-Zeit. Dient dem Aufspüren von Fehlern bzw. Schwachstellen. Hierzu wird der Quelltext einer Reihe formaler Prüfungen unterzogen.

Note:
* Wer von euch hat denn schonmal eine statische Code-Analyse durchgeführt?

---

### <span style="color: #FE9D0E">Pros</span> statischer Codeanalyse

* Gezielte Analyse ohne Ausführung der Software
* Sicherstellung innerer Softwarequalität
  * Ermittlung von **Kennzahlen** zu Architektur, Design und Komplexität
  * Ermittlung von **Duplikaten**, **Dead Code**, potenziellen **Bugs**
  * Einbeziehung der **Code Coverage** Ergebnisse
  * Einhaltung von **Entwicklungsrichtlinien** sowie **Code-Dokumentationen**

---

### <span style="color: #FE9D0E">Pros</span> Fortsetzung

* Stärkt Wissen über **Qualitätsprobleme** der Entwickler
* **Einheitliches** Verständnis von Qualitätszielen
* Durch Metriken **zyklisch** prüfbar und auswertbar
* Qualität wird **explizit** eingefordert durch Richtlinien/Vorgaben
* ...

---

### Abgrenzung zur <span style="color: #FE9D0E">dynamischen</span> Codeanalyse

* Laufendes Programm wird benötigt
* Kontrollierte Ausführung von Testfällen
* Beispiel Unit-Tests pro Testfall:
  1. <span style="font-size:22px"><span style="color: #FE9D0E">Arrange</span>: Defintion der Eingabe- und zu erwarteten Ausgabedaten</span>
  2. <span style="font-size:22px"><span style="color: #FE9D0E">Act</span>: Ausführung der SUT Methode</span>
  3. <span style="font-size:22px"><span style="color: #FE9D0E">Assert</span>: Vergleich der erzeugten mit den erwarteten Daten (Fehler bei Abweichungen)</span>

---

### <span style="color: #FE9D0E">Motivation</span>

*	Übersicht **Gesundheitszustand** des Systems
* **Hohe** Softwarequalität <span style="font-size:22px">(Skalierbar, Wiederholbar, Änderbar)</span>
*	**Einheitliche** Vorgaben <span style="font-size:22px">(Rules, Quality Gates)</span>
*	CI/CD-Integration <span style="font-size:22px">(Quality Gates Fails)</span>
*	Vermeiden von **teuren** Refaktorisierungen

![Motivation](/images/Motivation.png)

---

### Code <span style="color: #FE9D0E">Metriken</span>

* Methode zur Bewertung der Qualität eines Codes
* <span style="color: #FE9D0E">Indikatoren</span>, keine definitiven Aussagen!
* Klassifizierung: 
  * <span style="font-size:22px">Applikation</span>
  * <span style="font-size:22px">Assemblies</span>
  * <span style="font-size:22px">Namespaces</span>
  * <span style="font-size:22px">Types</span>
  * <span style="font-size:22px">Methods</span>
  * <span style="font-size:22px">Fields</span>
 
---

### Code Metriken <span style="color: #FE9D0E">Beispiele</span>
 
* <span style="color: #FE9D0E">LOC</span>: Check auf SRP/Separation of Concerns
* <span style="color: #FE9D0E">CC (McCabe-Metrik)</span>: Misst ganz allgemein die Komplexität eines Softwaremoduls
 * Grundlage ist die Anzahl der Verzweigungen
   * <span style="font-size:22px">CC > 15 = Hard to understand and maintain</span>
   * <span style="font-size:22px">CC > 30 = Extremely complex and should be split into smaller methods</span>
 * Liefert minimale Anzahl der Testfälle
* <span style="color: #FE9D0E">Code Coverage</span>: Check der Testüberdeckung

---

### <span style="color: #FE9D0E">NDepend</span>
* Statische Codeanalyse von **.NET-Quelltext**
* Prüfung von Code mit Hilfe von **Metriken**
* **„Technical Debt Management“**
* Versionen:
  - <span style="font-size:22px">Standalone (Visual NDepend)</span>
  - <span style="font-size:22px">Visual Studio Integration</span>
  - <span style="font-size:22px">Integrationen für TeamCity, TFS und Co.</span>

![NDepend Logo](/images/full_logo_width200.jpg)

Note:
* Erkläre Versionen
* Zeige Erst Visual NDepend
* Zeige Version "demo_V1" der WPF-Taschenrechner-Beispielanwendung 

---

### <span style="color: #FE9D0E">Issues, Rules ...</span>

* Rules: 
  * Es existieren **vorgefertigte** Regelsätze 
  * **Eigene** Regelsätze können definiert werden
  * Code based rules (NDepend API)
* Issues: <span style="font-size:22px">Einstufung in Blocker/Critical/High/Medium/Low</span>
* Code Smells: <span style="font-size:22px">Qualitative Schwachstellen (z.B. Software-Klone)</span>
* Quality Gates, Technical Debt, Annual Interest ...

---

### <span style="color: #FE9D0E">Quality Gates</span>

#### Erklärung
Im QG werden **Grenzwerte** für beliebige Kennzahlen eingerichtet, welche die **Mindestanforderungen** an die Qualität bestimmen. Dabei kann zwischen Bedingungen unterschieden werden, die **immer** erfüllt werden müssen, und solchen, bei denen dies über einen **gewissen Zeitraum** der Fall sein muss (z.B. Code-Abdeckung > 80% für neuen Code).

---

### <span style="color: #FE9D0E">Technical Debt</span>

#### Erklärung
Sie beschreibt, wie viel Zeit (Manntage) **investiert** werden muss, um die bestehenden Qualitätsmängel **vollständig** zu beheben. Wir schulden dem System als X Tage um es in Ordnung zu bringen. Die technische Schuld ist der Hauptindikator für Softwarequalität.

https://martinfowler.com/bliki/TechnicalDebt.html

---

### <span style="color: #FE9D0E">Annual Interest</span>

#### Erklärung
Gibt die Manntage an, die es **pro Jahr** kostet, wenn die Korrektur ausbleibt

![Kosten](/images/AV-Kosten-300x168.jpg)

Note:
* Zeige Version "demo_V5" der WPF-Taschenrechner-Beispielanwendung 

---

### <span style="color: #FE9D0E">NDepend Funktionen</span>

* Anzeige/Vergleich von Metriken, Quality Gates ...
* Überwachen von Trends
* Visualisierung von Abhängigkeiten, Tree-Maps ...
* Erstellen von Berichten
* Suchen & Filtern
* Dashboard individuell erweitern ...

---

### <span style="color: #FE9D0E">NDepend Diagramme</span>

* <span style="color: #FE9D0E">„Zone of Pain“</span>: <span style="font-size:22px">Pakete, die sich auf den äußeren Zonen befinden, sollten in jedem Fall umstrukturiert werden</span>
* <span style="color: #FE9D0E">Abhängigkeits-Graph</span>: <span style="font-size:22px">Zeigt Abhängigkeiten visuell an</span>
* <span style="color: #FE9D0E">Abhängigkeits-Matrix</span>: <span style="font-size:22px">Zeigt Abhängigkeiten in einer Matrix an</span>
* <span style="color: #FE9D0E">Tree-Maps</span>: <span style="font-size:22px">Helfen Muster zu erkennen, welche auf andere Art und Weise nicht erkannt werden können. Extrem nützlich um z.B. die Code Coverage zu visualisieren</span>

---

### <span style="color: #FE9D0E">NDepend ...</span>

* <span style="color: #FE9D0E">NDepend API</span>: <span style="font-size:22px">Eigene statische Analyse-Tools, Codebase Queries ...</span>
* <span style="color: #FE9D0E">CQLinq</span> (Code Query through Linq): <span style="font-size:22px">Auf LINQ basierenden Abfragesprache zur Definition von NDepend Regeln etc.</span>
* Vergleich zu <span style="color: #FE9D0E">SonarQube</span>
* Navigation zwischen Solutions

---

### <span style="color: #FE9D0E">Empfehlungen</span>

* Klare Empfehlung für **Produktentwicklung**!
* Evtl. Einbinden in den **CI/CD-Prozess**
* **Regelmäßiges** (Wöchentliches?) Team-Review der NDepend-Ergebnisse/Analyse
* **Kritikalitäten** auf Komponenten-Ebene bestimmen
* Alternativ mindest. **Code-Coverage** durchführen

Note:
Kritikalitätswerte zwischen eins und fünf bestimmen den Grad an zu leistender Code-Coverage 
* Kritikalität 1: 20 Prozent
* Kritikalität 2: 40 Prozent
* ...
* Kritikalität 4: 80 Prozent

---

### <span style="color: #FE9D0E">The End</span>

![Danke](/images/ThankYou.jpg)

Vortrag und Beispielprojekt auf:
https://github.com/htochenhagen/static-code-analysis-sample
