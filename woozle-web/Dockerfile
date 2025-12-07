FROM node:24.11.1-alpine AS build
WORKDIR /app
COPY package*.json /app/
RUN npm install
COPY . /app
ARG configuration=production
RUN npm run build -- --configuration=production

FROM nginx:1.29.3-alpine
COPY woozle-web-nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/woozle/browser /var/www/woozle-game.com/
