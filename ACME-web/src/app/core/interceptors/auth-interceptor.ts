import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../../features/auth/services/auth.service';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const authService = inject(AuthService);

  // Evita interceptar login y registro
  if (req.url.endsWith('/auth/signin') || req.url.endsWith('/auth/signup')) {
  return next(req);
}
  const token = authService.getToken();

  let authReq = req;

  // agregar token si existe
  if (token) {
    authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  // 🚀 IMPORTANTE: manejar errores correctamente
  return next(authReq).pipe(
    catchError((error) => {
      // opcional: aquí podrías loggear o centralizar errores

      return throwError(() => error); // 🔥 deja que el componente lo maneje
    })
  );

  // // Si no hay token → deja pasar la request
  // if (!token) {
  //   return next(req);
  // }

  // // Clonar request y agregar header
  // const authReq = req.clone({
  //   setHeaders: {
  //     Authorization: `Bearer ${token}`
  //   }
  // });

  // return next(authReq);
};