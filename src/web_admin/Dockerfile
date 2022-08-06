FROM node:16-alpine

WORKDIR /usr/src/app/web_admin

COPY package*.json ./

RUN yarn dev

# production stage
FROM nginx:stable-alpine as production-stage

COPY ./nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]