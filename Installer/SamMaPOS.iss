#ifndef ReleaseDir
#define ReleaseDir "..\EnterprisePOS.WinForms\bin\Release"
#endif
#ifndef PrerequisitesDir
#define PrerequisitesDir "prerequisites"
#endif
#define MyAppName "SamMaPOS"
#define MyAppVersion "1.0.0"
#define MyAppExeName "EnterprisePOS.WinForms.exe"

[Setup]
AppId={{e51a90cb-b3fa-446f-a944-2d1d0939424b}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
DefaultDirName={autopf}\{#MyAppName}
UninstallDisplayIcon={app}\{#MyAppExeName}
DisableProgramGroupPage=yes
PrivilegesRequired=admin
ArchitecturesAllowed=x64os
ArchitecturesInstallIn64BitMode=x64os
OutputDir=Output
OutputBaseFilename=SamMaPOS-Setup
SolidCompression=no
SetupLogging=yes
WizardStyle=modern dynamic

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#ReleaseDir}\*.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#ReleaseDir}\*.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#ReleaseDir}\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "PosDB.sql"; Flags: dontcopy
Source: "Initialize-Database.ps1"; Flags: dontcopy
Source: "{#PrerequisitesDir}\dotnet48.exe"; Flags: dontcopy noencryption
Source: "{#PrerequisitesDir}\SQLExpress\*"; DestDir: "{app}\SQLExpress"; Flags: dontcopy recursesubdirs noencryption

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; WorkingDir: "{app}"; Description: "Open SamMaPOS"; Flags: nowait postinstall runasoriginaluser skipifsilent

[Code]
function PrepareToInstall(var NeedsRestart: Boolean): String;
var
  FrameworkRelease: Cardinal;
  ExitCode: Integer;
  SqlMediaDir: String;
  ErrorDetails: AnsiString;
begin
  Result := '';

  if not RegQueryDWordValue(
    HKLM64, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full',
    'Release', FrameworkRelease) then
    FrameworkRelease := 0;

  if FrameworkRelease < 528040 then
  begin
    ExtractTemporaryFile('dotnet48.exe');
    if not Exec(ExpandConstant('{tmp}\dotnet48.exe'), '/q /norestart',
      '', SW_HIDE, ewWaitUntilTerminated, ExitCode) then
    begin
      Result := 'Could not start the .NET Framework installer.';
      Exit;
    end;

    if (ExitCode = 3010) or (ExitCode = 1641) then
    begin
      NeedsRestart := True;
      Result := 'Restart Windows, then run this installer again.';
      Exit;
    end;

    if ExitCode <> 0 then
    begin
      Result := '.NET Framework installation failed. Exit code: ' + IntToStr(ExitCode);
      Exit;
    end;
  end;

  if not RegKeyExists(
    HKLM64, 'SYSTEM\CurrentControlSet\Services\MSSQL$SQLEXPRESS') then
  begin
    ExtractTemporaryFiles('{app}\SQLExpress\*');
    SqlMediaDir := ExpandConstant('{tmp}') + '\{app}\SQLExpress';

    if not Exec(SqlMediaDir + '\setup.exe',
      '/Q /ACTION=Install /FEATURES=SQLEngine /INSTANCENAME=SQLEXPRESS ' +
      '/ADDCURRENTUSERASSQLADMIN=True /SQLSVCSTARTUPTYPE=Automatic ' +
      '/TCPENABLED=0 /NPENABLED=0 /UpdateEnabled=False /IACCEPTSQLSERVERLICENSETERMS',
      SqlMediaDir, SW_HIDE, ewWaitUntilTerminated, ExitCode) then
    begin
      Result := 'Could not start SQL Server Express setup.';
      Exit;
    end;

    if (ExitCode = 3010) or (ExitCode = 1641) then
    begin
      NeedsRestart := True;
      Result := 'Restart Windows, then run this installer again.';
      Exit;
    end;

    if ExitCode <> 0 then
    begin
      Result := 'SQL Server Express installation failed. Exit code: ' + IntToStr(ExitCode);
      Exit;
    end;
  end;

  ExtractTemporaryFile('PosDB.sql');
  ExtractTemporaryFile('Initialize-Database.ps1');

  if not Exec(ExpandConstant('{sys}\WindowsPowerShell\v1.0\powershell.exe'),
    '-NoProfile -NonInteractive -ExecutionPolicy Bypass -File "' +
    ExpandConstant('{tmp}\Initialize-Database.ps1') + '"',
    ExpandConstant('{tmp}'), SW_HIDE, ewWaitUntilTerminated, ExitCode) then
  begin
    Result := 'Could not start database setup.';
    Exit;
  end;

  if ExitCode <> 0 then
  begin
    Result := 'Database setup failed. Exit code: ' + IntToStr(ExitCode);
    if LoadStringFromFile(ExpandConstant('{tmp}\database-error.txt'), ErrorDetails) then
      Result := Result + #13#10 + String(ErrorDetails);
  end;
end;