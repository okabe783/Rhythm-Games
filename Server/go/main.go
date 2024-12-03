package main

import (
	"Server/model"
	"github.com/gin-gonic/gin"
)

func main() {
	model.DBInit()

	e := *gin.Default()
	Routes(&e)

	err := e.Run(":8080")
	if err != nil {
		panic(err)
	}
}
