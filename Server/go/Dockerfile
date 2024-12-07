FROM golang:1.22
RUN apt update
RUN apt install tree

RUN mkdir server
WORKDIR /server
COPY . /server