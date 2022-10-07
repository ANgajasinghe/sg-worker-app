Write-Host "Deployment started"
Write-Host "Prerequisite - azure cli , terraform  " -ForegroundColor Yellow
Write-Host "Deployment started" -ForegroundColor Yellow

az login
terraform init

terraform plan -out=./plans/pl-1.tfplan

terraform apply -auto-approve ./plans/pl-1.tfplan



Write-Host "Please get connection strings and function app publish profile" -ForegroundColor Yellow