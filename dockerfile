FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./", "Panda"]
RUN dotnet restore "Panda/Panda.Web/Panda.Web.csproj"
WORKDIR "/src/Panda"
RUN dotnet build "Panda.Web/Panda.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Panda.Web/Panda.Web.csproj" -c Release -o /app/publish

# # 构建后台

# FROM node:14.17.5 as admin
# WORKDIR "/src"
# COPY "./Admin/dist" "Admin"
# WORKDIR "/src/Admin"
# RUN npm install -g npm --registry=https://registry.npm.taobao.org
# RUN npm install -g yarn --registry=https://registry.npm.taobao.org
# RUN yarn global add @angular/cli
# RUN yarn install
# RUN yarn run build


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# COPY --from=admin /src/Admin ./wwwroot/
COPY ["./Admin/dist","/app/publish/wwwroot/admin"]
RUN echo "deb http://mirrors.aliyun.com/debian stretch main contrib non-free \
    deb-src http://mirrors.aliyun.com/debian stretch main contrib non-free \
    deb http://mirrors.aliyun.com/debian stretch-updates main contrib non-free \
    deb-src http://mirrors.aliyun.com/debian stretch-updates main contrib non-free \
    deb http://mirrors.aliyun.com/debian-security stretch/updates main contrib non-free \
    deb-src http://mirrors.aliyun.com/debian-security stretch/updates main contrib non-free \
    deb http://mirrors.aliyun.com/debian/ stretch-backports main non-free contrib \
    deb-src http://mirrors.aliyun.com/debian/ stretch-backports main non-free contrib" > /etc/apt/sources.list
RUN apt-get update && apt-get install libgdiplus -y && ln -s libgdiplus.so gdiplus.dll

ENV LANG C.UTF-8
ENV TZ=Asia/Shanghai
ENV BLOG_MYSQL_DB ""
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
RUN sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "Panda.Web.dll"]