package controller

import (
	"Server/model"
	"github.com/gin-gonic/gin"
	"gorm.io/gorm"
	"log"
)

type RankingController struct{}

func (r *RankingController) PostRankingData(c *gin.Context, db *gorm.DB) {
	var user model.User
	err := c.BindJSON(user)
	if err != nil {
		log.Fatal(err)
	}
	db.Create(&user)
	c.JSON(200, user)
}
