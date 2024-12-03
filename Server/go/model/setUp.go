package model

import (
	"gorm.io/driver/mysql"
	"gorm.io/gorm"
	"log"
)

var DB *gorm.DB

func DBInit() {
	// MariaDBのランキングDBに接続
	dsn := "root:okaberoot@(db)/RANKING"
	// mySqlのドライバの初期化
	var err error
	DB, err = gorm.Open(mysql.Open(dsn), &gorm.Config{})
	if err != nil {
		log.Fatal("Failed to connect database: ", err)
	}

	err = DB.AutoMigrate(User{}, Ranking{})
	if err != nil {
		log.Fatal("Migrate Error: ", err)
	}
}
