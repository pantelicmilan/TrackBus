#!/bin/bash
set -e

# Primena migracija
echo "Applying database migrations..."
dotnet ef database update --project ../Infrastructure --startup-project . --context ApplicationDbContext

# Pokretanje aplikacije
echo "Starting application..."
exec dotnet WebAPI.dll