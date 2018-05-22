## Softwarequalität messen & verbessern mit <span style="color: #FE9D0E">NDepend</span>

- Entwickler: Patrick Smacchia
- Erscheinungsdatum: April 2004

![NDepend Logo](/images/full_logo.jpg)

---

### <span style="color: #FE9D0E">Statische</span> Codeanalyse

#### Erklärung
Statisches Software-Testverfahren zur Compile-Zeit. Dient dem Aufspüren von Fehlern bzw. Schwachstellen. Hierzu wird der Quelltext einer Reihe formaler Prüfungen unterzogen.

---

### <span style="color: #FE9D0E">Pros</span> statischer Codeanalyse

* Gezielte Analyse ohne Ausführung der Software
* Sicherstellung innerer Softwarequalität
  * Ermittlung von Kennzahlen zu Architektur, Design und Komplexität
  * Ermittlung von Duplikaten, Dead Code, potenziellen Bugs
  * Einbeziehung der Code Coverage Ergebnisse
  * Einhaltung von Entwicklungsrichtlinien sowie Code-Dokumentationen

---

### <span style="color: #FE9D0E">Pros</span> statischer Codeanalyse

* Stärkt Wissen über Qualitätsprobleme der Entwickler
* Einheitliches Verständnis von Qualitätszielen
* Durch Metriken zyklisch prüfbar und auswertbar
* Qualität wird explizit eingefordert durch Richtlinien/Vorgaben
* ...

---

### Abgrenzung zur <span style="color: #FE9D0E">dynamischen</span> Codeanalyse

* Laufendes Programm wird benötigt
* Kontrollierte Ausführung von Testfällen
* Beispiel Unit-Tests pro Testfall:
 1. Arrange: Defintion der Eingabe- und zu erwarteten Ausgabedaten
 2. Act: Ausführung des SUT Methode
 3. Assert: Vergleich der erzeugten mit den erwarteten Daten (Fehler bei Abweichungen)

---

### <span style="color: #FE9D0E">Motivation</span>

*	Übersicht Gesundheitszustand des Systems
* Hohe Softwarequalität (Skalierbar, Wiederholbar, Änderbar)
*	Einheitliche Vorgaben (Rules, Quality Gates)
*	CI/CD-Integration (Quality Gates Fails)
*	Vermeiden von teuren Refaktorisierungen

![Motivation](/images/Motivation.png)

---

### Code <span style="color: #FE9D0E">Metriken</span>

* Definition: Methode zur Bewertung der Qualität eines Codes
* Klassifizierung: 
 * Code-Metriken: Applikation, Assemblies, Namespaces, Types, Methods, Fields
 * Technical Debt Metrik
 
---

### Code Metriken <span style="color: #FE9D0E">Beispiele</span>
 
* LOC: Check auf SRP oder Separation of Concerns
* CC (McCabe-Metrik): Misst ganz allgemein die Komplexität eines Softwaremoduls
 * Grundlage ist die Anzahl der Verzweigungen in einem Codeabschnitt
  * CC > 15 = Hard to understand and maintain
  * CC > 30 = Extremely complex and should be split into smaller methods
  * Obere Schranke für minimale Anzahl der Testfälle bei vollständiger Testabdeckung
