DEL -r -f ./docker/build
copy build/* ./docker/build
docker build ./docker -t velvetech-client
docker tag velvetech-client maiznpetr/velvetech-client:latest
docker push maiznpetr/velvetech-client:latest