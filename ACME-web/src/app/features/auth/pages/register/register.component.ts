import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);

  loading = false;
  error: string | null = null;

  form = this.fb.group({
    firstName: ['', [Validators.required, Validators.minLength(2)]],
    lastName: ['', [Validators.required, Validators.minLength(2)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', [Validators.required]]
  }, {
    validators: this.passwordMatchValidator
  });

  passwordMatchValidator(form: any) {
    const password = form.get('password');
    const confirmPassword = form.get('confirmPassword');

    if (password && confirmPassword && password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    }

    return null;
  }

  submit() {
    if (this.form.invalid || this.loading) return;

    this.loading = true;
    this.error = null;

    const { confirmPassword, ...registerData } = this.form.value;

    this.authService.register(registerData as any)
      .subscribe({
        next: () => {
          this.router.navigate(['/tasks']);
        },
        error: (err) => {
          console.log('REGISTER ERROR:', err);
          this.loading = false;
          this.error = err?.error?.message || 'Registration failed. Please try again.';
          this.cdr.markForCheck();
        }
      });
  }

  goToLogin() {
    this.router.navigate(['/auth/login']);
  }
}