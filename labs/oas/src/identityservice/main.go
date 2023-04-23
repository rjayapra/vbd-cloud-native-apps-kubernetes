package main

import (
	"encoding/json"
	"fmt"
	"log"
	"net/http"
)

type User struct {
	UserId   string
	UserName string
}

func homeLink(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "Welcome home!")
}

func signIn(w http.ResponseWriter, r *http.Request) {
	user := User{}

	query := r.URL.Query()
	if query.Get("user") == "testuser@oas.com" && query.Get("pwd") == "password" {

		user.UserId = "testuser@oas.com"
		user.UserName = "Test user"

		userJson, err := json.Marshal(user)
		if err != nil {
			panic(err)
		}
		w.Header().Set("Content-Type", "application/json; charset=UTF-8")
		w.WriteHeader(http.StatusOK)
		w.Write(userJson)

	}
}

func main() {
	http.HandleFunc("/api/identity/healthcheck", homeLink)
	http.HandleFunc("/api/identity/signin", signIn)
	log.Fatal(http.ListenAndServe(":5032", nil))
}
