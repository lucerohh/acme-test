import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { NgIf, AsyncPipe, NgFor } from '@angular/common';
import { TaskCategoryService } from '../../../task-categories/services/task-category.service';
import { TaskCategory } from '../../../task-categories/models/task-category.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-create-task',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, AsyncPipe, NgFor],
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.scss']
})
export class CreateTaskComponent {

  private fb = inject(FormBuilder);
  private taskService = inject(TaskService);
  private categoryService = inject(TaskCategoryService);
  private router = inject(Router);

  loading = false;
  error: string | null = null;

  categories$: Observable<TaskCategory[]> = this.categoryService.getCategories();

  form = this.fb.group({
    title: ['', Validators.required],
    description: [''],
    dueDate: [''],
    taskCategoryId: [null]
  });

  submit() {
    if (this.form.invalid) return;

    this.loading = true;
    this.error = null;

    this.taskService.createTask(this.form.value as any).subscribe({
      next: () => {
        this.router.navigate(['/tasks']);
      },
      error: () => {
        this.error = 'Error creating task';
        this.loading = false;
      }
    });
  }
}