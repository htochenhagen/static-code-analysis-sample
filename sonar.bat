@echo off
setlocal

set msbuild_args=%*
if "%msbuild_args%"=="" set msbuild_args=WpfCalculatorApp\WpfCalculatorApp.csproj /v:m /t:Coverage
echo Analyzing msbuild: %msbuild_args%

set project_key=de.schoenhofer.WpfCalculatorApp
set project_name=Frequency Manager
for /F "tokens=* USEBACKQ" %%a in (`gitversion /showvariable MajorMinorPatch`) do set git_version=%%a

set settings_file=%~dp0%SonarQube.Analysis.xml

rem NDepend stuff
set ndepend_path=c:\Users\marce\apps\ndepend\Integration\SonarQube\NDepend.SonarQube.RuleRunner.exe
if not exist %ndepend_path% goto analyze
set sonar_opts=%sonar_opts% /d:sonar.cs.ndepend.ruleRunnerPath=%ndepend_path%
set project_path=%~dp0%WpfCalculatorApp.ndproj
set sonar_opts=%sonar_opts% /d:sonar.cs.ndepend.projectPath=%project_path%

:analyze
call SonarQube.Scanner.MSBuild.exe begin /k:"%project_key%" /n:"%project_name%" /v:"%git_version%" /s:%settings_file% %sonar_opts%
rmdir /s /q testresults
call build.bat %msbuild_args%
call SonarQube.Scanner.MSBuild.exe end