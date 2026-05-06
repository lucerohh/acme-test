import { Component, inject} from '@angular/core';
import { NgFor, NgIf, AsyncPipe, NgClass} from '@angular/common';
import { TaskService } from '../../services/task.service';
import { Task } from '../../models/task.model';
import { Observable, startWith, Subject, switchMap, combineLatest, map, BehaviorSubject } from 'rxjs';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, CheckCircle, Clock, Loader, Folder } from 'lucide-angular';
import { TaskCategoryService } from '../../../task-categories/services/task-category.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.service';


@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [NgFor, NgIf, AsyncPipe, RouterLink, NgClass, LucideAngularModule],
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss'],
})
export class TasksComponent {

  private taskService = inject(TaskService);
  private categoryService = inject(TaskCategoryService);
  private router = inject(Router);
  private authService = inject(AuthService);
  folderIcon = Folder;
  private refresh$ = new Subject<void>();

  private statusFilter$ = new BehaviorSubject<string>('All');
  private categoryFilter$ = new BehaviorSubject<number | 'All'>('All');

  categories$ = this.categoryService.getCategories();

  tasks$: Observable<Task[]> = this.refresh$.pipe(
    startWith(void 0), 
    switchMap(() => this.taskService.getTasks())
  );

  filteredTasks$ = combineLatest([
    this.tasks$,
    this.statusFilter$,
    this.categoryFilter$
  ]).pipe(
    map(([tasks, status, category]) =>
      tasks.filter(t =>
        (status === 'All' || t.status === status) &&
        (category === 'All' || t.taskCategoryId === category)
      )
    )
  );

  setStatusFilter(value: string) {
    this.statusFilter$.next(value);
  }

  // setCategoryFilter(value: number | 'All') {
  //   this.categoryFilter$.next(value);
  // }
  setCategoryFilter(value: string) {
    this.categoryFilter$.next(
      value === 'All' ? 'All' : Number(value)
    );
  }

  deleteTask(id: number) {
    const confirmDelete = confirm('Are you sure you want to delete this task?');

    if (!confirmDelete) return;

    this.taskService.deleteTask(id).subscribe({
      next: () => {
        this.refresh$.next();
      },
      error: () => {
        alert('Error deleting task');
      }
    });
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Pending':
        return 'status pending';
      case 'InProgress':
        return 'status in-progress';
      case 'Done':
        return 'status done';
      default:
        return 'status';
    }
  }

  getStatusIcon(status: string) {
    switch (status) {
      case 'Pending':
        return Clock;
      case 'InProgress':
        return Loader;
      case 'Done':
        return CheckCircle;
      default:
   return Clock;
    }     
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/auth/login']);
  }  

}