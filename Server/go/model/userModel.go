package model

import "gorm.io/gorm"

type User struct {
	gorm.Model
	Name string `json:"name"`
	Pass string `json:"pass"`
}

type Test struct {
	gorm.Model
	Name string `json:"Name"`
}

func (u *User) Save() (User, error) {
	err := DB.Create(u).Error
	if err != nil {
		return User{}, err
	}
	return *u, nil
}
