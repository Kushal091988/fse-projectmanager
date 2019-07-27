// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  api: {
    url: 'http://localhost:63204/'
  },
  app: {
    name: 'Project Manager',
    description: 'DEV- Project Manager',
    year: ((new Date()).getFullYear())
  }
};

