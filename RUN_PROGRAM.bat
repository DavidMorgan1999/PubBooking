@ECHO OFF
@ECHO Run Pub Booking Assessment
PAUSE
cd PubBooking
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
@ECHO Check Logs
PAUSE
docker-compose logs
PAUSE