FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY output /app

ENV ASPNETCORE_URLS=http://*:5501
EXPOSE 5501
CMD dotnet RealityShiftLearning.dll