dotnet restore
dotnet build -c release /nowarn:CS1591
dotnet publish -c release -o ./docker/build
rmdir ./docker/build /S /Q
docker build ./docker -t velvetech-task
docker tag velvetech-task maiznpetr/velvetech-task:latest
docker push maiznpetr/velvetech-task:latest