package controller

import (
	"Server/model"
	"Server/utils"
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

func (a *AuthController) CurrentUser(c *gin.Context) {
	userId, err := utils.ExtractTokenId(c)

	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": err.Error()})
		return
	}

	var user model.User

	if err = model.DB.First(&user, userId).Error; err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": err.Error()})
		return
	} else {
		c.JSON(http.StatusOK, gin.H{
			"data": user.PrepareOutput(),
		})
	}
}
