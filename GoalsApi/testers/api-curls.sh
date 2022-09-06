#! /usr/bin/env bash

BASE_URL="http://localhost:5000/api"

method=$1
short_url=$2
token=$3
url_param=$4

if [ ! $method ]; then
    echo "Missing the request method"
    exit 1
fi

if [ ! $short_url ]; then
    echo "Missing the short url parameter"
    exit 1
fi

# Use t as 3rd param if you don't want to inform a specif token and use default
if [ $token ] && [ $token == "t" ]; then
    echo "Using the test token as default. Update test token if not working"
    if [ -e $file ]; then
        # Read file into variable
        token=$(<./test-token.txt)
    fi
fi

case "$short_url" in

    # Sign Up User
    "users")
        if [ $method = "POST" ]; then
            curl "$BASE_URL/users" \
                --header 'Content-Type: application/json' \
                --data \
                '{
                     "name":"Jane Doe",
                     "email":"jane@doe.com",
                     "password":"1234",
                     "phone":"123-456-7890"
                }'
            exit 1
        fi
        ;;

    # Sign In User
    "users/signin")
        if [ $method = "POST" ]; then
            curl "$BASE_URL/users/signin" \
                --header 'Content-Type: application/json' \
                --data \
                '{
                    "email": "john@doe.com",
                    "password": "1234"
                }'
            exit 1
        fi
        ;;

    # Users verify token
    "users/verify")
        if [ $method = "GET" ]; then
            curl "$BASE_URL/users/verify" \
                --header 'Content-Type: application/json' \
                --header "Authorization: Bearer $token"
            exit 1
        fi
        ;;

    # Add a goal
    "goals")
        if [ $method = "GET" ]; then
            curl "$BASE_URL/goals" \
                --header 'Content-Type: application/json' \
                --header "Authorization: Bearer $token"
            exit 1
        fi
        if [ $method = "POST" ]; then
            curl "$BASE_URL/goals" \
                --header 'Content-Type: application/json' \
                --header "Authorization: Bearer $token" \
                --data '{ "text": "My First Goal" }'
            exit 1
        fi
        ;;

    *) 
        echo "Default: no valid curl found for the parameters passed"
        echo "Tip: {method} {short_url} {token or t} {param}"
        exit 1
        ;;
esac

