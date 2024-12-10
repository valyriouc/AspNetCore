use actix_web::{post, get, web, HttpResponse};
use serde::{Deserialize, Serialize};

#[derive(Deserialize)]
struct UserFilter {
    name: Option<String>,
    age: Option<u8>
}

#[derive(Deserialize, Serialize)]
struct User {
    name: String,
    age: u8,
    email: String,
    password: String,
}

#[get("/")]
async fn user_get(query: web::Query<UserFilter>) -> impl actix_web::Responder {
    let name = &query.name;
    let age = &query.age;

    // Filtering logic
    let users = vec![
        User {
            name: String::from("John"),
            age: 20,
            email: String::from("john@gmail.com"),
            password: String::from("password123"),
        },
        User {
            name: String::from("Ron"),
            age: 23,
            email: String::from("ron@gmail.com"),
            password: String::from("password123"),
        },
        User {
            name: String::from("Valcor"),
            age: 54,
            email: String::from("valcor@gmail.com"),
            password: String::from("password123"),
        }
    ];

    let filtered: Vec<User> = users.into_iter().filter(|user| {
        match (name, age) {
            (None, None) => true,
            (Some(name), None) => user.name.contains(name),
            (None, Some(age)) => user.age == *age,
            (Some(name), Some(age)) => user.name.contains(name) && user.age == *age,
        }
    }).collect();

    HttpResponse::Ok().json(filtered)
}

#[post("/create")]
async fn user_post(body: web::Json<User>) -> impl actix_web::Responder {
    HttpResponse::Ok().body("User created successfully")
}

#[post("/update")]
async fn user_update(body: web::Json<User>) -> impl actix_web::Responder {
    // Logic to update user details in the database or data storage
    HttpResponse::Ok().body("User was updated")
}

#[post("/delete")]
async fn user_delete(body: web::Json<UserFilter>) -> impl actix_web::Responder {
    // Logic to delete a user based on the applied filter
    HttpResponse::Ok().body("User was deleted")
}