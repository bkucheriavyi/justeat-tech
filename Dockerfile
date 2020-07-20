FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY . .
RUN cd JE.Restaurant.Console && dotnet publish -c Release -r linux-x64 \
                    --self-contained false \
                    -p:PublishSingleFile=true \
                    -p:PublishReadyToRun=true \
                    -p:PublishTrimmed=false \
                    -o ../built

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/built .
RUN ls -la
ENTRYPOINT [ "/bin/bash", "-c"] 