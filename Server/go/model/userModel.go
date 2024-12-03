package model

import (
	"Server/utils"
	"golang.org/x/crypto/bcrypt"
	"gorm.io/gorm"
	"strings"
)

type User struct {
	gorm.Model
	Name string `gorm:"size:255;not null;unique" json:"name"`
	Pass string `gorm:"size:255;not null;" json:"pass"`
}

func (u *User) Save() (User, error) {
	err := DB.Create(u).Error
	if err != nil {
		return User{}, err
	}
	return *u, nil
}

func (u *User) BeforeSave() error {
	hashedPassword, err := bcrypt.GenerateFromPassword([]byte(u.Pass), bcrypt.DefaultCost)
	if err != nil {
		return err
	}

	u.Pass = string(hashedPassword)

	u.Name = strings.ToLower(u.Name)

	return nil
}

func (u *User) PrepareOutput() User {
	u.Pass = ""
	return *u
}

func GenerateToken(name string, pass string) (string, error) {
	var user User

	if err := DB.Where("name = ?", name).First(&user).Error; err != nil {
		return "", err
	}

	if err := bcrypt.CompareHashAndPassword([]byte(user.Pass), []byte(pass)); err != nil {
		return "", err
	}

	t, err := utils.GenerateToken(user.ID)

	return t, err
}
