#stage 1
FROM node:18.16.0 as node
WORKDIR /app
COPY . .
#COPY ./package.json ./
RUN npm install
RUN npm run build --prod
#stage 2
FROM nginx:alpine
COPY --from=node /app/dist/hahn.front-end /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 8080