package utils

import (
	"fmt"
	"os"
	"strconv"
	"strings"
	"time"

	"github.com/dgrijalva/jwt-go"
	"github.com/gin-gonic/gin"
)

func GenerateToken(id uint) (string, error) {
	tokenLifespan, err := strconv.Atoi(os.Getenv("TOKEN_HOUR_LIFESPAN"))

	if err != nil {
		return "", err
	}

	claims := jwt.MapClaims{
		"authorized": true,
		"user_id":    id,
		"exp":        time.Now().Add(time.Hour * time.Duration(tokenLifespan)).Unix(),
	}
	token := jwt.NewWithClaims(jwt.SigningMethodHS256, claims)

	return token.SignedString([]byte(os.Getenv("API_SECRET")))
}

func extractTokenString(c *gin.Context) string {
	bearToken := c.Request.Header.Get("Authorization")
	strArr := strings.Split(bearToken, " ")
	if len(strArr) == 2 {
		return strArr[1]
	}

	return ""
}

func parseToken(tokenString string) (*jwt.Token, error) {
	token, err := jwt.Parse(tokenString, func(token *jwt.Token) (interface{}, error) {
		if _, ok := token.Method.(*jwt.SigningMethodHMAC); !ok {
			return nil, fmt.Errorf("there was an error")
		}
		return []byte(os.Getenv("API_SECRET")), nil
	})

	if err != nil {
		return nil, err
	}

	return token, nil
}

func ValidToken(c *gin.Context) error {
	tokenString := extractTokenString(c)

	_, err := parseToken(tokenString)

	if err != nil {
		return err
	}

	return nil
}

func ExtractTokenId(c *gin.Context) (uint, error) {
	tokenString := extractTokenString(c)

	token, err := parseToken(tokenString)

	if err != nil {
		return 0, err
	}

	claims, ok := token.Claims.(jwt.MapClaims)

	if ok && token.Valid {
		userId, ok := claims["user_id"].(float64)

		if !ok {
			return 0, nil
		}

		return uint(userId), nil
	}

	return 0, nil
}
