#FROM golang:1.22-alpine
FROM golang:1.22
RUN apt update
RUN apt install tree

RUN mkdir server
WORKDIR /server
COPY go/ /server

#COPY ./main.go ./server
#COPY ./go.mod ./server
#COPY ./go.sum ./server

#RUN go get -u github.com/gin-gonic/gin
#RUN go get -u gorm.io/driver/mysql
#RUN go get -u gorm.io/gorm