use actix_web::{delete, get, post, web, HttpResponse, Responder};
use crate::db::{MyDbContext, accounts::Account, AccountTransfer};

// internal class AccountController : MyBaseController
pub struct AccountController {
    db_context: std::sync::Arc<MyDbContext>,
}

impl AccountController {
    pub fn new(db_context: std::sync::Arc<MyDbContext>) -> Self {
        AccountController { db_context }
    }

    #[get("/{id}")]
    pub async fn get(id: web::Path<u32>, db_context: web::Data<std::sync::Arc<MyDbContext>>) -> impl Responder {
        let accounts = &db_context.accounts;
        let account = accounts.iter().find(|a| a.id == *id);

        if account.is_none() {
            return HttpResponse::BadRequest().body(format!("No access to account with id {}", id));
        }

        HttpResponse::Ok().finish()
    }

    #[post("/create/")]
    pub async fn create(payload: web::Json<AccountTransfer>) -> impl Responder {
        HttpResponse::Ok().finish()
    }

    #[post("/update/")]
    pub async fn update(payload: web::Json<AccountTransfer>) -> impl Responder {
        HttpResponse::Ok().finish()
    }

    #[delete("/delete/{id}")]
    pub async fn delete(id: web::Path<i32>) -> impl Responder {
        HttpResponse::Ok().finish()
    }
}