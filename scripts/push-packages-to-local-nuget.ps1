Remove-Item .\packages\*.nupkg
dotnet pack -c release -o ./packages
cd packages
dotnet nuget push "*.nupkg" -s LocalSource
cd ..