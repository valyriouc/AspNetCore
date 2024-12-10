mod user_endpoint;

use actix_web::{get, post, web, App, HttpResponse, HttpServer, Responder};

use serde::Deserialize;

#[derive(Deserialize)]
struct QueryParams {
    param1: String,
    param2: Option<String>, // Optional, if query parameter might not always be present
}

#[get("/query")]
async fn query_example(query: web::Query<QueryParams>) -> impl Responder {
    let param1 = &query.param1;
    let param2 = query.param2.as_deref().unwrap_or("default_value");
    
    HttpResponse::Ok().body(format!("param1: {}, param2: {}", param1, param2))
}

#[get("/")]
async fn hello() -> impl Responder {
    HttpResponse::Ok().body("Hello world!")
}

#[post("/echo")]
async fn echo(req_body: String) -> impl Responder {
    HttpResponse::Ok().body(req_body)
}

#[actix_web::main]
async fn main() -> std::io::Result<()> {
    HttpServer::new(|| {
        App::new()
            .service(
                web::scope("/users")
                    .service(user_endpoint::user_get)
                    .service(user_endpoint::user_post)
                    .service(user_endpoint::user_update)
                    .service(user_endpoint::user_delete))
            .service(query_example)
            .route("/", web::get().to(|| async { HttpResponse::Ok().body("Welcome to your web app!") }))
    })
        .bind("127.0.0.1:8080")?
        .run()
        .await
}
