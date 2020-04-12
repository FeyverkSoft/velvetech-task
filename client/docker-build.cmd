DEL .\docker\build /s /f /q
xcopy build\*.* docker\build\ /E
docker build ./docker -t velvetech-client
docker tag velvetech-client maiznpetr/velvetech-client:latest
docker push maiznpetr/velvetech-client:latest
DEL .\docker\build /s /f /q