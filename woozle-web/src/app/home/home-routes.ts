import { Routes } from "@angular/router";


export const homeRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('./components/welcome/welcome.component')
      .then(x => x.WelcomeComponent),
  },
]