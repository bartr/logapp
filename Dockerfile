### build the app
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# Copy the source
COPY src /src

### Build the release app
WORKDIR /src
RUN dotnet publish -c Release -o /app

    
###########################################################


### build the runtime container
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime

### create a user
### dotnet needs a home directory
RUN addgroup -S logapp && \
    adduser -S logapp -G logapp && \
    mkdir -p /home/logapp && \
    chown -R logapp:logapp /home/logapp

WORKDIR /app
COPY --from=build /app .

RUN chown -R logapp:logapp /app

# run as the logapp user
USER logapp

ENTRYPOINT [ "dotnet",  "logapp.dll" ]
