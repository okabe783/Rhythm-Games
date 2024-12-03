package controller

import (
	"Server/model"
	"github.com/gin-gonic/gin"
	"log"
)

type UserController struct{}

func (u *UserController) Ping(c *gin.Context) {
	c.JSON(200, gin.H{
		"message": "接続",
	})
}

func (u *UserController) GetAllUser(c *gin.Context) {
	var users []model.User
	model.DB.Find(&users)
	c.JSON(200, users)
}

func (u *UserController) GetUser(c *gin.Context) {
	id := c.Param("id")
	model.DB.Select("name").Find(model.User{}, id)
	c.JSON(200, model.User{})
}

func (u *UserController) CreateUser(c *gin.Context) {
	var user model.User
	err := c.BindJSON(&user)
	if err != nil {
		log.Fatal(err)
	}
	model.DB.Create(&user)
	c.JSON(200, user)
}
