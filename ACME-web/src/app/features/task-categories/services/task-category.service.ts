import { inject, Injectable } from '@angular/core';
import { TaskCategory } from '../models/task-category.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class TaskCategoryService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7144/api/v1/task-categories';

  getCategories(): Observable<TaskCategory[]> {
    return this.http.get<TaskCategory[]>(this.apiUrl);
  }
}