# SamMaPOS

## Install on Windows

Download **SamMaPOS-Setup.exe** from this repository's Releases page and run it on a supported 64-bit Windows PC with administrator access. Setup carries the .NET Framework 4.8 and SQL Server Express installation media and uses the local SQLEXPRESS instance. On a fresh installation, Setup creates PosDB with the sample products, customers and demo accounts. Choose **Open SamMaPOS** on the last page.

Demo login for a newly initialized database: **admin** / **Admin@123**. This build is for classroom demonstration and uses sample credentials. A populated PosDB keeps its existing records and accounts.

If Windows reports that a prerequisite requires a restart, restart and run the same setup file again. If an existing SQLEXPRESS instance denies access, a SQL administrator must give the Windows account access to PosDB.

## Source and installer build

Open `SamMaPOS.slnx` in Visual Studio with .NET desktop development and the .NET Framework 4.8 targeting pack. Build the solution in **Release**.

Put the .NET Framework 4.8 offline runtime at `Installer/prerequisites/dotnet48.exe` and the extracted SQL Server 2025 Express Core media at `Installer/prerequisites/SQLExpress/` (with `setup.exe` at its root). Open `Installer/SamMaPOS.iss` in Inno Setup and compile. Upload the resulting **SamMaPOS-Setup.exe** as a GitHub Release asset.
