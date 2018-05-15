## Statische Codeanalyse
- Erklärung
- Vorteile Statischer Codeanalyse
    - Gezielte Codeanalyse ohne das die Software (die Komponenten unter Test) ausgeführt werden muss
    - Sicherstellung innerer Softwarequalität --> Aus allen Unterpunkten wird die Technische Schuld ermittelt!
        - Ermittlung von Kennzahlen zu Architektur und Design
        - Ermittlung von Duplikaten
        - Einbeziehung der Code Coverage Ergebisse
        - Ermittlung der Komplexität (zyklomatisch ...)
        - Ermittlung von potenziellen Bugs
        - Einhaltung von Entwicklungsrichtlinien 
        - Einhaltung von Code-Dokumentationen (Kommentare ...)
    - Verbesserte Wissen über Qualitätsprobleme der Entwickler
    - Einheitliche Verständnis/Sichtweise auf bestimmte Qualitätsziele
    - Zyklisch prüfbar und auswertbar durch Metriken (Metrik = Methode zur Bewertung der Qualität eines Codes)
    - Qualität wird explizit eingefordert durch Richtlinien/Vorgaben (Codierrichtlinien=ReSharper oder Style­Cop, Architekturrichtlinie=NDepend)
- Abgrenzung zur dynamischer Codeanalyse
    - Unit-Tests

## NDepend (Parallel aufmachen)
- Entwickler: Patrick Smacchia
- Erscheinungsdatum: Seit April 2004
- Versionen:
    - Standalone (Visual NDepend)
    - Visual Studio Integration
    - Integrationen für TeamCity, TFS und Co.
- Begriffe
    - Technische Schuld (Technical Debt)
        - Sie beschreibt, wie viel Zeit investiert werden muss, um die bestehenden Qualitätsmängel vollständig zu beheben
        - Hauptindikator für Softwarequalität
    - Annual Interest
        - Gibt die Manntage an, die es pro Jahr kostet, wenn die Korrektur ausbleibt
    - Code Smell
    - CC (Zyklomatische Komplexität oder McCabe-Metrik)
        - Misst ganz allgemein die Komplexität eines Softwaremoduls
        - Grundlage ist die Anzahl der Verzweigungen in einem Codeabschnitt
        - Obere Schranke für minimale Anzahl der Testfälle bei vollständiger Testabdeckung
    - Weitere Begrifflichkeiten (CC, LOC ...)
- Dashboard
    - Individuell erweiterbar
    - Drilldown-Funktionalität (bis zum Quellcode navigieren)
- Issues
    - Einstufung in Blocker/Critical/High/Medium/Low
- Rules
    - Code based rules (NDepend API)
- Quality Gates
    - Im Quality Gate werden Grenzwerte für beliebige Kennzahlen eingerichtet, welche die Mindestanforderungen an die Qualität bestimmen. Dabei kann zwischen Bedingungen unterschieden werden, die immer erfüllt werden müssen, und solchen, bei denen dies über einen gewissen Zeitraum der Fall sein muss. So kann man zum Beispiel festlegen, dass die Code-Abdeckung bei Unit-Tests jederzeit mindestens 80 Prozent betragen muss oder dass die technische Schuld im Monat um nicht mehr als zwei Stunden zunehmen darf.
- CQLinq
- API
- Funktionen:
    - Überwachen von Trends
    - Vergleich von Quellcode

## Code Metriken
-  LOC
-  CC
-  Coverage
-  Technical Debt



## Sonstiges
- Alternative: Code Coverage

## Todo
- Unterschiedliche Stände einchecken, so dass im Vortrag keine Probleme beim wechseln entstehen


# Vorgehensweise
- Motivation, ggf. aus dotnetnpro?
- Eigenes Beispiel in SSE-Infrastruktur, ggf. CI/CD-Integration?
- einheitliche Vorgaben (Quality Gates)
- Vorschlag zum Team-Review (z.B. 1x wöchentlich)
- Weiteres?