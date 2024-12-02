package main

import (
	"Server/controller"
	"github.com/gin-gonic/gin"
)

var MasterController = controller.MasterController{}

func main() {
	MasterController.DBInit()

	e := *gin.Default()
	Routes(&e)

	err := e.Run(":8080")
	if err != nil {
		panic(err)
	}
}
