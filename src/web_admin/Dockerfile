# develop stage
FROM node:16-alpine3.15 as builder-stage

WORKDIR /app/web_admin

COPY package*.json ./

RUN yarn

COPY . .

RUN yarn build --mode staging

# production stage
FROM nginx:1.15.7-alpine as production-stage

COPY ./nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=builder-stage /app/web_admin/dist /var/www/html

EXPOSE 3000

CMD ["nginx", "-g", "daemon off;"]