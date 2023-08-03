@echo off
iisreset /stop
rmdir /s /q out
cd..
dotnet publish In-Anh.sln -o out --no-build
iisreset /noforce