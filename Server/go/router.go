package main

import (
	"Server/controller"
	"Server/middlewares"
	"github.com/gin-gonic/gin"
)

func Routes(e *gin.Engine) {
	auth := e.Group("/auth")
	{
		authController := controller.AuthController{}
		auth.POST("/register", authController.Register)
		auth.POST("/login", authController.Login)

		protected := e.Group("/admin")
		{
			protected.Use(middlewares.JwtAuthMiddleware())
			protected.GET("/user", authController.CurrentUser)
		}
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
