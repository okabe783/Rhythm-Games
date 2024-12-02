package controller

import (
	"Server/model"
	"github.com/gin-gonic/gin"
	"log"
)

type RankingController struct{}

func (r *RankingController) PostRankingData(c *gin.Context) {
	var user model.User
	err := c.BindJSON(user)
	if err != nil {
		log.Fatal(err)
	}
	DB.Create(&user)
	c.JSON(200, user)
}
