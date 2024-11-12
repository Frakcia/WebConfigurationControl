# WebConfigurationControl

для запуска Dockerfile заходим в корневую папку проекта
и запускаем команды
docker build -t <image_name> -f WebConfigurationControl.Api/Dockerfile . - для создания образа
docker run -d -p 8080:8080 --name <container_name> <image_name> - для запуска
и можно переходить на http://localhost:8080/swagger
