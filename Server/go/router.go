package main

import (
	"Server/controller"
	"github.com/gin-gonic/gin"
	"gorm.io/gorm"
)

func Routes(e *gin.Engine, db *gorm.DB) {
	userController := controller.UserController{}

	// 接続に対してメッセージを送る
	e.GET("/ping", userController.Ping)

	// DBの全データをJSONで返す
	e.GET("/getAll")

	e.GET("/get/:id")

	// DBへデータ追加
	e.POST("/post")
}
