package controller

import (
	"Server/model"
	"github.com/gin-gonic/gin"
	"net/http"
)

type AuthController struct{}

type RegisterInput struct {
	Name string `json:"name" binding:"required"`
	Pass string `json:"pass" binding:"required"`
}

func (a *AuthController) Register(c *gin.Context) {
	var input RegisterInput

	if err := c.ShouldBindJSON(&input); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{
			"error": err.Error(),
		})
		return
	}

	user := model.User{
		Name: input.Name,
		Pass: input.Pass,
	}

	if user, err := user.Save(); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{
			"error": err.Error(),
		})
	} else {
		c.JSON(http.StatusOK, gin.H{
			"data": user.PrepareOutput(),
		})
	}
}

type LoginInput struct {
	Name string `json:"name" binding:"required"`
	Pass string `json:"pass" binding:"required"`
}

func (a *AuthController) Login(c *gin.Context) {
	var input LoginInput

	if err := c.ShouldBindJSON(&input); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{
			"error": err.Error(),
		})
		return
	}

	if token, err := model.GenerateToken(input.Name, input.Pass); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{
			"error": err.Error(),
		})
	} else {
		c.JSON(http.StatusOK, gin.H{
			"token": token,
		})
	}
}
