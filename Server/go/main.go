package main

import (
	"github.com/gin-gonic/gin"
	"gorm.io/driver/mysql"
	"gorm.io/gorm"
)

type User struct {
	gorm.Model
	Name string `json:"Name"`
}

type Test struct {
	gorm.Model
	Name string `json:"Name"`
}

func main() {
	db := dbInit()

	initErr := db.AutoMigrate(&User{}, &Test{})
	if initErr != nil {
		panic(initErr)
	}

	// APIフレームワークの初期化
	r := gin.Default()

	// 接続に対してメッセージを送る
	r.GET("/ping", func(c *gin.Context) {
		c.JSON(200, gin.H{
			"message": "接続",
		})
	})

	// DBの全データをJSONで返す
	r.GET("/getAll", func(c *gin.Context) {
		var users []User
		db.Find(&users)
		c.JSON(200, users)
	})

	r.GET("/get/:id", func(c *gin.Context) {
		id := c.Param("id")
		var user User
		db.Select("name").Find(&user, id)
		c.JSON(200, user)
	})

	// DBへデータ追加
	r.POST("/post", func(c *gin.Context) {
		var user User
		err := c.BindJSON(&user)
		if err != nil {
			panic(err)
		}
		db.Create(&user)
		c.JSON(200, user)
	})

	// DBへデータ追加
	r.POST("/test", func(c *gin.Context) {
		var test Test
		err := c.BindJSON(&test)
		if err != nil {
			panic(err)
		}
		db.Create(&test)
		c.JSON(200, test)
	})

	err := r.Run(":8080")
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
		panic("failed to connect database")
	}

	return db
}
