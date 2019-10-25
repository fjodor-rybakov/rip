@echo on
set DB_CONNECTOR_STRING=Host=localhost;Port=5003;Database=rip_db;Username=rip_user;Password=dsfb32fuuis
set AUTH_KEY=@e38hr@!r%$^%@^$7324f2&%$%$&&*@sd
set AUTH_AUDIENCE=http://localhost:5001
set AUTH_ISSUER=MyAuthServer
set AUTH_LIFETIME=60
set FRONTEND_ADDRESS=http://localhost:4200
set PATH_IMAGES_NEWS=../static-server/static/photo-news/
set PATH_IMAGES_AVATARS=../static-server/static/user-avatars/
dotnet ef migrations add InitialCreate