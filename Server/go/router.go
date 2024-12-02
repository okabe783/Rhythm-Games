package main

import (
	"Server/controller"
	"github.com/gin-gonic/gin"
)

func Routes(e *gin.Engine) {
	userController := controller.UserController{}
	rankingController := controller.RankingController{}

	// 接続に対してメッセージを送る
	e.GET("/ping", userController.Ping)

	userSettings := e.Group("/user")
	{
		// DBの全データをJSONで返す
		userSettings.GET("/getAll", userController.GetAllUser)
		userSettings.GET("/get/:id", userController.GetUser)
		userSettings.GET("/create", userController.CreateUser)
	}

	rankingSettings := e.Group("/ranking")
	{
		// DBへデータ追加
		rankingSettings.POST("/post", rankingController.PostRankingData)
	}
}
