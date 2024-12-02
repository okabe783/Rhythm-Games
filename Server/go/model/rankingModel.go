package model

import "gorm.io/gorm"

type Ranking struct {
	gorm.Model
	HighScore int `json:"HighScore"`
	Combo     int `json:"Combo"`
	ItemID    int `json:"ItemID"`
}
