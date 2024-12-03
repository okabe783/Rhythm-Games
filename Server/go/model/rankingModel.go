package model

import "gorm.io/gorm"

type Ranking struct {
	gorm.Model
	HighScore int `json:"highScore"`
	Combo     int `json:"combo"`
	ItemID    int `json:"itemID"`
}
