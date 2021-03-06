## Statische Codeanalyse
- Erklärung
    - Ist ein statisches Software-Testverfahren zur Compile-Zeit! Es ist eine Form des Debugging, um Fehler bzw. Schwachstellen aufzuspüren. Der Quelltext wird hierbei einer Reihe formaler Prüfungen unterzogen noch bevor die entsprechende Software (z. B. im Modultest) ausgeführt wird.
- Vorteile Statischer Codeanalyse
    - Gezielte Codeanalyse ohne das die Software (die Komponenten unter Test) ausgeführt werden muss
    - Sicherstellung innerer Softwarequalität --> Aus allen Unterpunkten wird die Technische Schuld ermittelt!
        - Ermittlung von Kennzahlen zu Architektur und Design
        - Ermittlung von Duplikaten bzw. totem Code (Dead Code)
        - Einbeziehung der Code Coverage Ergebnisse
        - Ermittlung der Komplexität (zyklomatisch ...)
        - Ermittlung von potenziellen Bugs (Division durch 0 ...)
        - Einhaltung von Entwicklungsrichtlinien 
        - Einhaltung von Code-Dokumentationen (Kommentare ...)
    - Verbesserte Wissen über Qualitätsprobleme der Entwickler
    - Einheitliche Verständnis/Sichtweise auf bestimmte Qualitätsziele
    - Zyklisch prüfbar und auswertbar durch Metriken (Metrik = Methode zur Bewertung der Qualität eines Codes)
    - Qualität wird explizit eingefordert durch Richtlinien/Vorgaben (Codierrichtlinien=ReSharper oder Style­Cop, Architekturrichtlinie=NDepend)
- Abgrenzung zur dynamischer Codeanalyse
    - Bei dynamischer Codeanalysen sollen besonders Programmfehler erkannt werden, die in Abhängigkeit von dynamischen Laufzeitparametern auftreten, wie variierende Eingabeparameter, Laufzeitumgebung oder Nutzer-Interaktion
    - Grundprinzip der dynamischen Verfahren ist die kontrollierte Ausführung der zu testenden Software mit systematisch festgelegten Eingabedaten (Testfälle). Für jeden Testfall werden zu den Eingabedaten auch die erwarteten Ausgabedaten angegeben. Die vom Testlauf erzeugten Ausgabedaten werden mit den jeweils erwarteten Daten verglichen. Bei Abweichungen liegt ein Fehler vor.
    - Dynamischer Codeanalysen setzen ein laufendes Programm voraus
    - Beispiele Dynamischer Codeanalysen:
        - Unit-Tests
        - Profiling
- Motivation
    - Übersicht über den Gesundheitszustand des Systems zu erlangen
    - Gute änderbare Software entwickeln (Clean Code). D.h. z.B. lesbarer Code, gutes Klassendesign, gute Architektur (Schichten) etc.
    - Hohe Softwarequalität
        - Skalierbar
        - Wiederholbar
    - Vermeiden von nachträglichen, teuren Refaktorisierungen

## Code Metriken (Indikatoren, keine definitiven Aussagen)
- Klassifizierung: 
    - Code-Metriken: Applikation, Assemblies, Namespaces, Types, Methods, Fields
    - Technical Debt Metrik
- LOC
    -  Keine Ermittlung der Geschwindigkeit von Entwicklern!
    -  Auf Klassenebene in Verbund mit Typen, die die Klasse benutzen bzw. Typen die von der Klasse benutzt werden, lässt sich grob erkennen, ob das Prinzip der „Separation of Concerns“ beachtet wurde (Guter Stil dagegen zeichnet sich durch eher kleine Klassen und kleine Methoden aus)
- CC (Zyklomatische Komplexität oder McCabe-Metrik)
    - Misst ganz allgemein die Komplexität eines Softwaremoduls
    - Grundlage ist die Anzahl der Verzweigungen in einem Codeabschnitt
        - CC > 15 = Hard to understand and maintain
        - CC > 30 = Extremely complex and should be split into smaller methods
    - Obere Schranke für minimale Anzahl der Testfälle bei vollständiger Testabdeckung
- Coverage
    -  Nur weil ein System eine hohe Testüberdeckung hat, müssen die geschriebenen Tests nicht automatisch eine gute Qualität haben und die richtigen Testfälle testen
    -  Dem testenden Entwickler gibt die Metrik eine gute Aussage darüber, ob ein Testfall noch nicht berücksichtig und umgesetzt wurde.
    -  Maunelles Review erforderlich!

## NDepend (Parallel aufmachen)
- Entwickler: Patrick Smacchia
- Erscheinungsdatum: April 2004
- Beschreibung: 
    - Tool, das Qualität in .NET Softwareprodukten sicherstellen soll 
    - NDepend ist ein Tool, das Qualität in Softwareprodukten sicherstellen soll. Es erlaubt, bestimmte Regeln zu formulieren, welche die entwickelte Codebasis einhalten muss.
    - NDepend kümmert sich um die statische Codeanalyse von Quelltext und bietet verschiedene Metriken an, um den Zustand von Code zu prüfen, zum Beispiel im Sinne von Wartbarkeit.
    - Eine größere Neuerung ist das „Technical Debt Management“, das technische Schulden im Code aufspürt und die Kosten der Behebung (oder des Nicht-Behebens) berechnet.
    - Schlechte Resultate bei den Metriken sind Anzeichen dafür, dass der Code nicht so gut ist wie vielleicht vermutet (z.B. bisher nicht aufgefallene Fehler).
- Versionen:
    - Standalone (Visual NDepend)
    - Visual Studio Integration
    - Integrationen für TeamCity, TFS und Co.
- Begriffe
    - Technical Debt (Technische Schuld): https://martinfowler.com/bliki/TechnicalDebt.html
        - Sie beschreibt, wie viel Zeit investiert werden muss, um die bestehenden Qualitätsmängel vollständig zu beheben. Wir schulden dem System als X Tage um es in Ordnung zu bringen.
        - Hauptindikator für Softwarequalität 
        - "Technical Debt is a wonderful metaphor developed by Ward Cunningham to help us think about this problem. In this metaphor, doing things the quick and dirty way sets us up with a technical debt, which is similar to a financial debt. Like a financial debt, the technical debt incurs interest payments, which come in the form of the extra effort that we have to do in future development because of the quick and dirty design choice. We can choose to continue paying the interest, or we can pay down the principal by refactoring the quick and dirty design into the better design. Although it costs to pay down the principal, we gain by reduced interest payments in the future."
    - Annual Interest
        - Gibt die Manntage an, die es pro Jahr kostet, wenn die Korrektur ausbleibt
    - Code Smell
        - Qualitative Schwachstellen
        - Unter "Übelriechender Code" versteht man in der Programmierung ein Konstrukt, das eine Überarbeitung des Programm-Quelltextes nahelegt.
        - Beispiel: Software-Klone
- Issues
    - Einstufung in Blocker/Critical/High/Medium/Low
- Rules
    - Es existieren vorgefertigte Regelsätze 
    - Eigene Regelsätze können definiert werden
    - Code based rules (NDepend API) können via Quellcode definiert werden
- Quality Gates
    - Im Quality Gate werden Grenzwerte für beliebige Kennzahlen eingerichtet, welche die Mindestanforderungen an die Qualität bestimmen. Dabei kann zwischen Bedingungen unterschieden werden, die immer erfüllt werden müssen, und solchen, bei denen dies über einen gewissen Zeitraum der Fall sein muss. So kann man zum Beispiel festlegen, dass die Code-Abdeckung bei Unit-Tests jederzeit mindestens 80 Prozent betragen muss oder dass die technische Schuld im Monat um nicht mehr als zwei Stunden zunehmen darf.
- CQLinq
    - Auf LINQ basierenden Abfragesprache zur Definition von NDepend Regeln etc.
- API
- Funktionen:
    - Anzeige/Vergleich von Code-Metriken, Quality Gates, Code Smells, Coverage, Issues, Rules ...
    - Überwachen von Trends
    - Vergleich von Quellcode
    - Anzeige von Abhängigkeiten, Tree-Maps, „Zone of Pain“ ...
    - Drilldown-Funktionalität (bis zum Quellcode navigieren)
    - Erstellen von Berichten
    - Suchen & Filtern
    - Das Dashboard Individuell erweiterbar
- Diagramme:
    - „Zone of Pain“
        - Pakete, die sich auf den beiden äußeren Zonen befinden, sollten in jedem Fall umstrukturiert und auf geeignete Weise abstrahiert werden.
    - Abhängigkeits-graph/matrix
        - Zeigt auch Kreisabhängigkeiten (rote Pfeile), die auf jeden Fall zu untersuchen sind
        - Zusätzlich lässt sich bereits ablesen, ob Schichten in der Applikation zu erkennen sind und ob diese Schichten konsequent getrennt wurden.
    - Tree-Maps
        - Helfen Muster zu erkennen, welche auf andere Art und Weise nicht erkannt werden können

## Empfehlungen
- Für Produktentwicklung klare Empfehlung meinseits! Aber auch aus Projekten enstandene Softwareprodukte sollten regelmäßig untersucht werden.
- Wöchentliches oder alle 2 Wochen Team-Review der NDepend-Ergebnisse/Analysen/Auswertungen.
- Evtl. Einbinden in den CI-Prozess. Verweigern des Builds, wenn bestimmte oder selbst definierte Quality Gates nicht eingehalten werden können.
- Kritikalitäten auf Komponenten-Ebene
    - Kritikalitätswerte zwischen eins und fünf bestimmen den Grad an zu leistender Code-Coverage (Kritikalität 1: 20 Prozent, Kritikalität 2: 40 Prozent, ..., Kritikalität 4: 80 Prozent ...)
- Wem das zu alles viel ist kann bzw. sollte zumindest die Alternative "Code Coverage" anwenden

## Sonstiges
- Pros&Cons gegenüber SonarQube
- SOLID, YAGNI, KISS, DRY etc.
- Anstatt von Anfang an eine saubere Architektur mit mehreren Schichten zugrunde zu legen, wird oft ein großer Monolith entwickelt. Die anfangs schnelle Entwicklungsgeschwindigkeit sinkt zunehmend, die Zahl der Fehler steigt und ein neues Merkmal wird immer teurer.
- Vorgaben
	- Eine Klasse darf nicht mehr als 220 Zeilen Code enthalten
	- Nie mehr als 10 Methoden pro Klasse!
	- Eine Methode sollte nicht über mehr als 20 Zeilen hinausgehen

## Todo
- Unterschiedliche Stände einchecken, so dass im Vortrag keine Probleme beim wechseln entstehen


https://www.ndepend.com/docs/quality-gates

A Quality Gate outputs a status (Pass, Warn, Fail).
Typically a Quality Gate must be validated before releasing to production.
A Rule outputs issues.
An issue is a code smell that should be fixed to make the code cleaner and avoid potential problems.
Typically the team can release to production even if some issues are still reported.