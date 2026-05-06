import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../models/task.model';
import { CreateTaskRequest } from '../models/create-task-request.model';
import { UpdateTaskRequest } from '../models/update-task-request.model';

@Injectable({
  providedIn: 'root',
})
export class TaskService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7144/api/v1/task-items'; 

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiUrl);
  }

  createTask(data: CreateTaskRequest) {
    return this.http.post(
      this.apiUrl,
      data
    );
  }

  getTaskById(id: number) {
    return this.http.get<Task>(`${this.apiUrl}/${id}`);
  }

  updateTask(id: number, data: UpdateTaskRequest) {
    return this.http.put(
      `${this.apiUrl}/${id}`,
      data
    );
  }

  deleteTask(id: number) {
    return this.http.delete(
      `${this.apiUrl}/${id}`
    );
  }
}