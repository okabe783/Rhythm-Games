package controller

import (
	"Server/model"
	"github.com/gin-gonic/gin"
	"gorm.io/gorm"
	"log"
)

type UserController struct{}

func (u *UserController) Ping(c *gin.Context) {
	c.JSON(200, gin.H{
		"message": "接続",
	})
}

func (u *UserController) GetAllUser(c *gin.Context, db *gorm.DB) {
	var users []model.User
	db.Find(&users)
	c.JSON(200, users)
}

func (u *UserController) GetUser(c *gin.Context, db *gorm.DB) {
	id := c.Param("id")
	db.Select("name").Find(model.User{}, id)
	c.JSON(200, model.User{})
}

func (u *UserController) CreateUser(c *gin.Context, db *gorm.DB) {
	var user model.User
	err := c.BindJSON(&user)
	if err != nil {
		log.Fatal(err)
	}
	db.Create(&user)
	c.JSON(200, user)

}
