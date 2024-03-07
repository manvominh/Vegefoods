FROM node:21-alpine

WORKDIR /vegefoods.reactui/

COPY public/ /vegefoods.reactui/public
COPY src/ /vegefoods.reactui/src
COPY package.json /vegefoods.reactui/

RUN npm install

RUN npm run build

CMD ["npm", "start"]