import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { TaskService } from '../../services/task.service';
import { Task } from '../../models/task.model';
import { Observable, switchMap, tap } from 'rxjs';
import { TaskCategoryService } from '../../../task-categories/services/task-category.service';
import { TaskCategory } from '../../../task-categories/models/task-category.model';
import { UpdateTaskRequest } from '../../models/update-task-request.model';

@Component({
  selector: 'app-edit-task',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, AsyncPipe, NgFor],
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.scss']
})
export class EditTaskComponent {

  private fb = inject(FormBuilder);
  private taskService = inject(TaskService);
  private categoryService = inject(TaskCategoryService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  saving = false;
  error: string | null = null;

  categories$: Observable<TaskCategory[]> = this.categoryService.getCategories();

  form = this.fb.group({
    title: ['', Validators.required],
    description: [''],
    dueDate: [''],
    status: ['Pending'],
    taskCategoryId: [null as number | null]
  });

  task$: Observable<Task> = this.route.paramMap.pipe(
    switchMap(params => {
      const id = Number(params.get('id'));
      return this.taskService.getTaskById(id);
    }),
    tap(task => {
      this.form.patchValue({
        title: task.title,
        description: task.description,
        dueDate: task.dueDate?.substring(0, 10),
        status: task.status,
        taskCategoryId: task.taskCategoryId ?? null
      });
    })
  );

  submit() {
  if (this.form.invalid) return;

  const id = Number(this.route.snapshot.paramMap.get('id'));

  const formValue = this.form.value;

  const request: UpdateTaskRequest = {
    title: formValue.title ?? '',
    description: formValue.description ?? undefined,
    dueDate: formValue.dueDate ?? undefined,
    status: formValue.status ?? 'Pending',
    taskCategoryId: formValue.taskCategoryId ?? null
  };

  this.saving = true;

  this.taskService.updateTask(id, request).subscribe({
    next: () => this.router.navigate(['/tasks']),
    error: () => {
      this.error = 'Error updating task';
      this.saving = false;
    }
  });
}
}