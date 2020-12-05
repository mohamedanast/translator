
## Create React App

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

## Available Scripts

In the project directory, you can run:

`npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

`npm test`

Launches the test runner in the interactive watch mode.\
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

`npm run build`

Builds the app for production to the `build` folder.\

## Deployment

Fork the repository
Create a static web app from Azure portal, sign-in to github to choose the repository. Choose the repository and use build details as below
Build Presets: React, App location: /translator-api, Api location: (empty), App artifact: build
Create and wait for for the CI pipeline to complete the deployment.
Add the URL to CORS in the function app.