FROM node:22.14.0-alpine AS build
WORKDIR /app
COPY package*.json /app/
RUN npm install
COPY . /app
ARG configuration=production
RUN npm run build -- --configuration=production

FROM nginx:1.17.1-alpine
COPY woozle-web-nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/woozle/browser /var/www/woozle-game.com/
