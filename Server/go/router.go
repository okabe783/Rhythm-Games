package main

import (
	"Server/controller"
	"github.com/gin-gonic/gin"
)

func Routes(e *gin.Engine) {
	auth := e.Group("/auth")
	{
		auth.GET("/register")
	}

	userSettings := e.Group("/user")
	{
		userController := controller.UserController{}
		// DBの全データをJSONで返す
		userSettings.GET("/getAll", userController.GetAllUser)
		userSettings.GET("/get/:id", userController.GetUser)
		userSettings.GET("/create", userController.CreateUser)
	}

	rankingSettings := e.Group("/ranking")
	{
		rankingController := controller.RankingController{}
		// DBへデータ追加
		rankingSettings.POST("/post", rankingController.PostRankingData)
	}
}
