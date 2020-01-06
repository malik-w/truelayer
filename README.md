# Software Engineer - Payments - Challenge

This repository defines a .NET Core & SQL Server web application that is used to authorize with TrueLayer, read transactions from the data API and calculate some statistics for transaction categories.

### Instructions:

> Prerequisites: Docker and cURL will be used to run and interact with this project

#### 1. Download the source files from this repository

#### 2. Sign up to the TrueLayer Data API.

#### 3. Credentials

Edit the file secrets.json, filling in the credentials provided to you by TrueLayer.

```json
{
  "client_id": "[CLIENT_ID]",
  "client_secret": "[CLIENT_SECRET]",
  "redirect_uri": "http://localhost:5000/callback",
  "auth_link": "[AUTH_LINK]"
}
```

Important: The redirect uri must be registered to `http://localhost:5000/callback` in the TrueLayer settings.

#### 4. Edit db config settings

Create a password for the database user. This must be at least 8 characters and contain a mixture of uppercase letters, lowercase letters, and digits.

Replace the token `[DB_PASSWORD]` with this password into the following files: `appsettings.json`, `Dockerfile-DB`, and `setup-db.sh`.

#### 5. Run application

Open a command window in the `Interview.Wajid.Malik` folder and run the following command:

```
docker-compose up
```

This builds and starts two connected services, one for the app and one for the db. This will take a few seconds to spin up.

#### 6. Authorise

Navigate to `http://localhost:5000` in a browser. This will redirect to the Truelayer authorisation page. Log in with the details: 

```
Bank: Mock Bank
Username: john
Password: doe
```

This will log in and redirect to the callback url. The message `"You have successfully authenticated."` will appear in the browser.

#### 7. Explore API

The following endpoints can now be contacted.

  ###### a. An endpoint that given a user id returns all the transactions grouped by account
  
  ```
  curl http://localhost:5000/users/{userID}/transactions
  ```
  
  (Any userID can be used to test e.g. "testuser"). These transactions are also saved in the db.
  
  ###### b. An endpoint that given a user returns min, max and average amount for each stored transaction's category across accounts
  
  ```
  curl http://localhost:5000/users/{userID}/transactions/categories/stats
  ```
  
  This retrieves the above transactions from the db (must be called after (a)) and calculates the statistics.

### Running unit tests

> Prerequisites: dotnet

To run the unit tests, open a command window in the `Interview.Wajid.Malik.Tests` folder and run the following command:

```
dotnet test
```

### Tearing down application

To shut down the application and remove the containers and images, run the following command from the `Interview.Wajid.Malik` folder:

```
docker-compose down --rmi all
```

To also remove the images
