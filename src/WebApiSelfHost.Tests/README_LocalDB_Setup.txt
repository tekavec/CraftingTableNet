1. The connectionString in app.config is set to work with LocalDB for SQL Server 2014 Express.
2. Check with "sqllocaldb info" if it displays the automatic instance, namely "MSSQLLocalD". If not, check the "SQL Server 2014 Express Installation" section.
3. If "sqllocaldb" doesn't show the automatic instance "MSSQLLocalD", create own instane with "sqllocaldb create "[instance_name]" v12.0 -s". "v12.0" stands for SQL Server version 2014, "-s" switch starts the instance immediately.
4. Use the "Server=(localdb)\MSSQLLocalDB" setting in the Tests project "app.config" for testing purposes

## SQL Server 2014 Express Installation
- Open web site: https://www.microsoft.com/en-US/download/details.aspx?id=42299
- Download SQLEXPRADV_x64_ENU.exe or SQLEXPRADV_x86_ENU.exe file, according to Windows version.
- Install package.
- If "sqllocaldb" cannot be run in CMD, check folder "C:\Program Files\Microsoft SQL Server\120" if "sqllocaldb.exe" exists there.
- If the file is not there, it wasn't installed via installer. In this case, use the search in "C:\Program Files\Microsoft SQL Server\120" if "sqllocaldb.msi" is there and install the package.
- Open CMD and type "sqllocaldb info" to check again everything works as expected.