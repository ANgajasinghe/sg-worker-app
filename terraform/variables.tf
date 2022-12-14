variable "location" {
  type = string
  description = "Azure region to deploy module to"
  default = "West US 2"
}

variable "project" {
  type = string
  description = "Project name"
  default = "sgcustomer"
}

variable "environment" {
  type = string
  default = "stage"
  description = "Environment (dev / stage / prod)"
}

variable "mysql-admin-login" {
  type = string
  default = "ag"
  description = "Login to authenticate to MySQL Server"
}
variable "mysql-admin-password" {
  type = string
  default = "#Compaq12345"
  description = "Password to authenticate to MySQL Server"
}

variable "mysql-version" {
  type = string
  description = "MySQL Server version to deploy"
  default = "5.7"
}
variable "mysql-sku-name" {
  type = string
  description = "MySQL SKU Name"
  default = "B_Gen5_2"
}
variable "mysql-storage" {
  type = string
  description = "MySQL Storage in MB"
  default = "5120"
}