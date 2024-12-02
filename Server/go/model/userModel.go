package model

import "gorm.io/gorm"

type User struct {
	gorm.Model
	Name string `json:"Name"`
}

type Test struct {
	gorm.Model
	Name string `json:"Name"`
}
