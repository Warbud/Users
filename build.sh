#!/bin/sh
warbudHome="C:/WEBSITES/Warbud"
folderPath="Builds/Users"

rm -r $warbudHome/$folderPath
cd Warbud.Users.Api
dotnet publish -c Release -o $warbudHome/$folderPath