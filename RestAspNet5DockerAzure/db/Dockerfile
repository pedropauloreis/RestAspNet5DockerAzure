FROM mysql:5.7.22
EXPOSE 3306
COPY ./RestAspNet5DockerAzure/db/migrations/ /home/database/
COPY ./RestAspNet5DockerAzure/db/dataset/ /home/database/
COPY ./RestAspNet5DockerAzure/ci/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh