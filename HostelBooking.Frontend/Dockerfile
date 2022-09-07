FROM node:14.15.0-alpine3.11 as build
WORKDIR /app

RUN npm install -g @angular/cli

COPY ./package.json .
RUN npm install
COPY . .
RUN ng build --prod

CMD ["ng", "serve", "--host=0.0.0.0"]
