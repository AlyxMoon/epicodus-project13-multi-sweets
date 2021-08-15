#!/bin/bash
cd "${0%/*}"

USE_A_PASSWORD=true

while getopts u:p:Pd: flag
do
    case "${flag}" in
        u) USER_NAME=${OPTARG};;
        p) USER_PASS=${OPTARG};;
        P) USE_A_PASSWORD=false;;
        d) DB_NAME=${OPTARG};;
    esac
done

USER_NAME="${USER_NAME:-root}"
USER_PASS="${USER_PASS:-epicodus}"
DB_NAME="${DB_NAME:-allister_kays}"

PASS_STRING="pwd=$USER_PASS";
if [ "$USE_A_PASSWORD" == false ]; then
  PASS_STRING="";
fi

echo "Creating appsettings with the following values:";
echo "username: $USER_NAME";
if [ "$USE_A_PASSWORD" == false ]; then
  echo "- no password";
else
  echo "password: $USER_PASS";
fi
echo "database: $DB_NAME";

cat <<-EOF > ../HairSalon/appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=$DB_NAME;uid=$USER_NAME;$PASS_STRING"
  }
}
EOF