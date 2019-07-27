
# Longrich-FTP

Install node.js
Install npm

Run `npm uninstall -g @angular/cli`
Run `npm npm cache clean`
Run `npm install -g @angular/cli@latest`

Run `npm install`

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 7.1.3.

# Rules
1. Always `clone()` the return object on `xxxService.get()` & `xxxxService.tryInstantGet()` before return
2. Do not modify any `getFullList()` `return data` (`clone()` if modification of data is needed)

## Debugger for Chrome
https://github.com/Microsoft/vscode-recipes/tree/master/Angular-CLI
Visual Studio Code -> TERMINAL TAB
1. In (1:node) run [npm start]
2. Go [Debug] / (Ctrl+Shift+D)
3. Run {ng serve}

## Build errors
Node Sass could not find a binding for your current environment: Windows 64-bit with Node.js 10.x
 - https://stackoverflow.
com/questions/37986800/node-sass-could-not-find-a-binding-for-your-current-environment

## Deploy an Angular App From Visual Studio Code to Azure
https://dzone.com/articles/deploy-an-angular-app-from-visual-studio-code-to-a-1
1. On Azure App settings, set: WEBSITE_NODE_DEFAULT_VERSION = 10.6.0

## Deploy to IIS
1. Install url-rewrite. https://www.iis.net/downloads/microsoft/url-rewrite

## Build
https://stackoverflow.com/questions/51258031/angular-cli-how-to-use-ng-build-prod-command-for-with-environment-staging-t

## Staging
 ng build --prod --configuration=staging
[/base/] ng build --base-href /longrich-uat/ --prod --configuration=staging

## UX upgrade
1. https://wrapbootstrap.com/theme/angle-bootstrap-admin-template-WB04HF123
2. ng new my-sassy-app --style=scss
3. npm install @progress/kendo-angular-buttons @progress/kendo-angular-dateinputs @progress/kendo-angular-dialog @progress/kendo-angular-dropdowns @progress/kendo-angular-excel-export @progress/kendo-angular-grid @progress/kendo-angular-inputs @progress/kendo-angular-intl @progress/kendo-angular-l10n @progress/kendo-angular-label @progress/kendo-angular-tooltip @progress/kendo-angular-upload @progress/kendo-data-query @progress/kendo-drawing @progress/kendo-theme-default angular-2-local-storage bootstrap country-code-lookup country-state-city primeicons primeng --save

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Reading materials
1. ngModel in Form - https://stackoverflow.com/questions/39126638/ngmodel-cannot-be-used-to-register-form-controls-with-a-parent-formgroup-directi

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).
