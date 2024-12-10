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

#[cfg(test)]
mod tests {
    use super::*;
    use actix_web::{test, web, App};

    #[test]
    async fn test_user_get_no_filter() {
        let app = test::init_service(
            App::new().service(user_get)
        ).await;

        let req = test::TestRequest::get()
            .uri("/")
            .to_request();
        let resp: Vec<User> = test::call_and_read_body_json(&app, req).await;

        assert_eq!(resp.len(), 3); // Since no filter is applied, all users should be returned
    }

    #[test]
    async fn test_user_get_filter_by_name() {
        let app = test::init_service(
            App::new().service(user_get)
        ).await;

        let req = test::TestRequest::get()
            .uri("/?name=John")
            .to_request();
        let resp: Vec<User> = test::call_and_read_body_json(&app, req).await;

        assert_eq!(resp.len(), 1);
        assert_eq!(resp[0].name, "John");
    }

    #[test]
    async fn test_user_get_filter_by_age() {
        let app = test::init_service(
            App::new().service(user_get)
        ).await;

        let req = test::TestRequest::get()
            .uri("/?age=23")
            .to_request();
        let resp: Vec<User> = test::call_and_read_body_json(&app, req).await;

        assert_eq!(resp.len(), 1);
        assert_eq!(resp[0].age, 23);
    }

    #[test]
    async fn test_user_get_filter_by_name_and_age() {
        let app = test::init_service(
            App::new().service(user_get)
        ).await;

        let req = test::TestRequest::get()
            .uri("/?name=Ron&age=23")
            .to_request();
        let resp: Vec<User> = test::call_and_read_body_json(&app, req).await;

        assert_eq!(resp.len(), 1);
        assert_eq!(resp[0].name, "Ron");
        assert_eq!(resp[0].age, 23);
    }

    #[test]
    async fn test_user_get_filter_no_matches() {
        let app = test::init_service(
            App::new().service(user_get)
        ).await;

        let req = test::TestRequest::get()
            .uri("/?name=Invalid&age=99")
            .to_request();
        let resp: Vec<User> = test::call_and_read_body_json(&app, req).await;

        assert_eq!(resp.len(), 0); // No matching users
    }
}
