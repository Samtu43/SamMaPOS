# SamMaPOS

A point-of-sale and inventory system for Windows.

## Install

1. Go to **Releases** and download `SamMaPOS-Setup.exe`.
2. Run the installer as administrator on 64-bit Windows.
3. Click **Open SamMaPOS** when setup finishes.

On a fresh install, sign in with **admin** / **Admin@123**. The installer sets up SQL Server Express and loads sample products and customers into `PosDB`.

If Windows asks you to restart, restart and run the same installer again.

## Source code and database

Download `SamMaPOS-Source.zip` from **Releases**, extract it, and open `SamMaPOS.slnx` in Visual Studio. The project uses .NET Framework 4.8.

The release also includes `SamMaPOS-PosDB.sql`. To review the database in SSMS, create an empty database named `PosDB` and run the script. Setup handles this step automatically on a fresh install.

## Build the installer

1. Build the solution in **Release** in Visual Studio.
2. Put the .NET Framework 4.8 offline installer at `Installer/prerequisites/dotnet48.exe`.
3. Extract the SQL Server 2025 Express Core files into `Installer/prerequisites/SQLExpress/`. Put `setup.exe` directly inside that folder.
4. Open `Installer/SamMaPOS.iss` in Inno Setup and click **Build → Compile**.
