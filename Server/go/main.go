package main

import (
	"Server/model"
	"github.com/gin-gonic/gin"
	"gorm.io/driver/mysql"
	"gorm.io/gorm"
	"log"
)

var User = model.User{}
var Ranking = model.Ranking{}
var DB = dbInit()

func main() {
	initErr := DB.AutoMigrate(&User, &Ranking)
	if initErr != nil {
		log.Fatal(initErr)
	}

	e := *gin.Default()
	Routes(&e, DB)

	err := e.Run(":8080")
	if err != nil {
		panic(err)
	}
}

func dbInit() *gorm.DB {
	// MariaDBのランキングDBに接続
	dsn := "root:okaberoot@(db)/RANKING"
	// mySqlのドライバの初期化
	db, err := gorm.Open(mysql.Open(dsn), &gorm.Config{})
	if err != nil {
		log.Fatal("failed to connect database")
	}

	return db
}
