# e-commerce-simulation-v2

Live: https://www.techbygamers.co

Application starts to run with some products added in advance. Application has identity api for authentication/authorization and onion architecture used. During the order process, application checks stock of product and if product is in stock application sends request to bank api for payment confirmation. If payment api confirms the order, then application sends a confirmation email to user from application. User can check price of order and shipping informations from email. Admin’s can product/user/category crud process and assign role to user.

Tech Stack: .NET6, MSSQL, EntityFrameworkCore, Bootstrap5
