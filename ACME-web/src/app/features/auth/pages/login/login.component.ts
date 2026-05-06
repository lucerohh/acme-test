// import { Component, inject } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
// import { Router } from '@angular/router';
// import { AuthService } from '../../services/auth.service';
// import { finalize } from 'rxjs';

// @Component({
//   selector: 'app-login',
//   standalone: true,
//   imports: [CommonModule, ReactiveFormsModule],
//   templateUrl: './login.component.html',
//   styleUrl: './login.component.scss',
// })
// export class LoginComponent {

//   private fb = inject(FormBuilder);
//   private authService = inject(AuthService);
//   private router = inject(Router);

//   loading = false;
//   error: string | null = null;

//   form = this.fb.group({
//     email: ['', [Validators.required, Validators.email]],
//     password: ['', [Validators.required]]
//   });

//   // submit() {
//   //   if (this.form.invalid) return;

//   //   this.loading = true;
//   //   this.error = null;

//   //   this.authService.login(this.form.value as any)
//   //   .pipe(
//   //     finalize(() => this.loading = false)
//   //   )
//   //   .subscribe({
//   //     next: () => this.router.navigate(['/tasks']),
//   //     error: (err) => {
//   //       console.log('LOGIN ERROR:', err);
//   //       this.error = err.error?.message || 'Invalid Credentials';
//   //       // this.loading = false;
//   //     }
//   //   });
//   // }

//   submit() {
//     if (this.form.invalid) return;

//     this.loading = true;
//     this.error = null;

//     this.authService.login(this.form.value as any)
//       .subscribe({
//         next: () => {
//           // this.loading = false;
//           this.router.navigate(['/tasks']);
//         },
//         error: (err) => {
//           console.log('LOGIN ERROR:', err);

//           this.loading = false; 
//           this.error = err?.error?.message || 'Invalid Credentials';
//         }
//       });
//   }
// }








import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);

  loading = false;
  error: string | null = null;

  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]]
  });

  submit() {
    if (this.form.invalid || this.loading) return;

    this.loading = true;
    this.error = null;

    this.authService.login(this.form.value as any)
      .subscribe({
        next: () => {
          this.router.navigate(['/tasks']);
        },
        error: (err) => {
          console.log('LOGIN ERROR:', err);
          this.loading = false;
          this.error = err?.error?.message || 'Invalid Credentials';
          this.cdr.markForCheck();
        }
      });
  }

  goToRegister() {
    this.router.navigate(['/auth/register']);
  }
}