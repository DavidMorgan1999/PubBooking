@ECHO OFF
@ECHO Remove Pub Booking Assessment Container images and volumes
PAUSE
docker rmi webapplication1
docker rmi pubbooking_bookingmysql
docker volume rm pubbooking_mysql-volume
@ECHO Pub Booking Assessment Container images and volumes removed.
PAUSE