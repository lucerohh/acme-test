import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';

export const routes: Routes = [
   {
    path: '',
    redirectTo: 'auth/login',
    pathMatch: 'full'
  },
  {
    path: 'auth/login',
    loadComponent: () =>
      import('./features/auth/pages/login/login.component')
        .then(m => m.LoginComponent)
  },
  {
    path: 'auth/register',
    loadComponent: () =>
      import('./features/auth/pages/register/register.component')
        .then(m => m.RegisterComponent)
  },
  {
    path: 'tasks',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./features/task-items/pages/tasks/tasks.component')
        .then(m => m.TasksComponent)
  },
  {
  path: 'tasks/create',
  canActivate: [authGuard],
  loadComponent: () =>
    import('./features/task-items/pages/create-task/create-task.component')
      .then(m => m.CreateTaskComponent)
  },
  {
  path: 'tasks/:id/edit',
  canActivate: [authGuard],
  loadComponent: () =>
    import('./features/task-items/pages/edit-task/edit-task.component')
      .then(m => m.EditTaskComponent)
  }
];