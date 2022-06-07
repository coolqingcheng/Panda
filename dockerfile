FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5005

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./", "Panda"]
RUN dotnet restore "Panda/Panda.Site/Panda.Site.csproj"


FROM build AS publish
RUN dotnet publish "Panda/Panda.Site/Panda.Site.csproj" -c Release -o /publish

# # 构建后台

FROM node:14.17.5 as admin
WORKDIR "/Panda/Admin"
COPY "./Admin" "."
WORKDIR "/Panda/Admin"
RUN rm ./yarn.lock
RUN rm -rf ./node_modules
RUN npm install -g npm --registry=https://registry.npm.taobao.org
RUN npm install
RUN npm run build


FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
COPY --from=admin /Panda/Panda.Site/wwwroot/admin ./wwwroot/admin


ENV LANG C.UTF-8
ENV TZ=Asia/Shanghai
ENV BLOG_MYSQL_DB ""
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
RUN sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "Panda.Site.dll"]